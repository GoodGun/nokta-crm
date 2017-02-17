using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Utility;

namespace BusinessObjects.Common
{
    /// <summary>
    /// Provides general Data Access Layer functions.
    /// </summary>
    public class EntityManager
    {
        #region Get Functions
        /// <summary>
        /// Returns an instance of class, whose type is given, populated by the stored procedure whose name is provided.
        /// </summary>
        /// <typeparam name="T">Type of the class to return.</typeparam>
        /// <param name="ProcedureName">Name of the stored procedure to execute.</param>
        /// <returns>Instance of class, populated by the stored procedure.</returns>
        public static T GetSingleObjectByCriteria<T>(string ProcedureName) where T : class
        {
            return GetSingleObjectByCriteria<T>(ProcedureName, null);
        }

        /// <summary>
        /// Returns an instance of class, whose type is given, populated by the stored procedure whose name is provided.
        /// </summary>
        /// <typeparam name="T">Type of the class to return.</typeparam>
        /// <param name="ProcedureName">Name of the stored procedure to execute.</param>
        /// <param name="Parameters">SqlParameter array which contains parameters to be passed to stored procedure.</param>
        /// <returns>Instance of class, populated by the stored procedure.</returns>
        public static T GetSingleObjectByCriteria<T>(string ProcedureName, SqlParameter[] Parameters) where T : class
        {
            try
            {
                return GetObjectsByCriteria<T>(ProcedureName, Parameters)[0];
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Returns an List of class, whose type is given, whose items are populated by the stored procedure whose name is provided.
        /// </summary>
        /// <typeparam name="T">Type of the class to return.</typeparam>
        /// <param name="ProcedureName">Name of the stored procedure to execute.</param>
        /// <returns>List of class, whose items are populated by the stored procedure.</returns>
        public static List<T> GetObjectsByCriteria<T>(string ProcedureName) where T : class
        {
            return GetObjectsByCriteria<T>(ProcedureName, (SqlParameter[])null);
        }

        /// <summary>
        /// Returns an List of class, whose type is given, whose items are populated by the stored procedure whose name is provided.
        /// </summary>
        /// <typeparam name="T">Type of the class to return.</typeparam>
        /// <param name="ProcedureName">Name of the stored procedure to execute.</param>
        /// <param name="Parameters">Dictionary which contains parameters to be passed to stored procedure in a name value pattern.</param>
        /// <returns>List of class, whose items are populated by the stored procedure.</returns>
        public static List<T> GetObjectsByCriteria<T>(string ProcedureName, Dictionary<string, object> Parameters) where T : class
        {
            return GetObjectsByCriteria<T>(ProcedureName, DictionaryToParameterArray(Parameters));
        }

        /// <summary>
        /// Returns an List of class, whose type is given, whose items are populated by the stored procedure whose name is provided.
        /// </summary>
        /// <typeparam name="T">Type of the class to return.</typeparam>
        /// <param name="ProcedureName">Name of the stored procedure to execute.</param>
        /// <param name="Parameters">SqlParameter array which contains parameters to be passed to stored procedure.</param>
        /// <returns>List of class, whose items are populated by the stored procedure.</returns>
        public static List<T> GetObjectsByCriteria<T>(string ProcedureName, SqlParameter[] Parameters) where T : class
        {
            int tmp = -1;
            return GetObjectsByCriteria<T>(ProcedureName, Parameters, out tmp);
        }

        public static List<T> GetObjectsByCriteria<T>(string ProcedureName, SqlParameter[] Parameters, out int TotalRows) where T : class
        {
            List<T> arrResults = new List<T>();
            TotalRows = 0;
            bool tried = false;

            try
            {
                using (SqlDataReader oReader = SqlHelper.ExecuteReader(ConfigManager.Current.ConnectionString, CommandType.StoredProcedure, ProcedureName, Parameters))
                {
                    while (oReader.Read())
                    {
                        arrResults.Add(GetItemFromReader<T>(oReader));
                        try
                        {
                            if (!tried)
                            {
                                tried = true;
                                if (oReader.ContainsColumn("TotalRows"))
                                    TotalRows = oReader["TotalRows"].ToInt();
                            }
                        }
                        catch (Exception ex) 
                        { 
                            ExceptionManager.Publish(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return arrResults;
        }

        public static List<T> GetStuffByPage<T>(string ProcedureName, Dictionary<string, object> Filters, string SortExpression) where T : class
        {
            return GetStuffByPage<T>(ProcedureName, FilterToString(Filters), SortExpression);
        }
        public static List<T> GetStuffByPage<T>(string ProcedureName, string Filter, string SortExpression) where T : class
        {
            List<T> arrResults = new List<T>();

            int PageIndex = 0, PageSize = 100000;

            SqlParameter[] Parameters = { new SqlParameter("@PageIndex", PageIndex), 
                new SqlParameter("@PageSize", PageSize) ,
                new SqlParameter("@SortExpression", SortExpression) ,
                new SqlParameter("@Filter", Filter) };

            try
            {
                using (SqlDataReader oReader = SqlHelper.ExecuteReader(ConfigManager.Current.ConnectionString, CommandType.StoredProcedure, ProcedureName, Parameters))
                {
                    while (oReader.Read())
                    {
                        arrResults.Add(GetItemFromReader<T>(oReader));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return arrResults;
        }
        public static string FilterToString(Dictionary<string, object> Filters)
        {
            if (Filters == null || Filters.Count == 0) return null;
            string Filter = "";
            
            foreach (string key in Filters.Keys)
            {
                object o = Filters[key];
                if (o != null && o is int && ((int)o == Param.Leave))
                    Filter += string.Concat(" ", key, " ");
                else if (key.Contains("IN"))
                    Filter += string.Format("{0} ({1}) AND ", key, Filters[key]);
                else
                    Filter += string.Format("{0} = '{1}' AND ", key, Filters[key]);
            }
            Filter = string.Concat(Filter, "1=1").Replace(" AND 1=1", "");

            return string.IsNullOrEmpty(Filter) ? null : Filter;
        }
        #endregion

        #region Utility Functions
        public static T GetItemFromReader<T>(IDataReader oReader) where T : class
        {
            Type oCustomType = typeof(T);
            T objResult = Activator.CreateInstance<T>();
            DBTableInfo oTableInfo = SchemaManager.DBSchema[oCustomType.Name] as DBTableInfo;
            PropertyInfo[] arrProperties = oCustomType.GetProperties();

            foreach (PropertyInfo oProperty in arrProperties)
            {
                DBColumnInfo oColumnInfo = oTableInfo.Columns[oProperty.Name] as DBColumnInfo;
                
                if (oColumnInfo != null && !Convert.IsDBNull(oReader[oColumnInfo.Name]))
                {
                    if (oProperty.PropertyType == oReader[oColumnInfo.Name].GetType())
                        oProperty.SetValue(objResult, oReader[oColumnInfo.Name], null);
                    else
                        oProperty.SetValue(objResult, Convert.ChangeType(oReader[oColumnInfo.Name], oProperty.PropertyType), null);
                }
            }
            
            return objResult;
        }

        private static bool SaveObject<T>(string ProcedureName, T EntityInstance, bool IsInserting) where T : class
        {
            bool bSuccess = false;

            try
            {
                Type oCustomType = typeof(T);

                DBTableInfo oDBInfo = SchemaManager.GetTable(oCustomType.Name);
                
                object objPropertyValue = null;
                PropertyInfo[] arrProperties = oCustomType.GetProperties();

                ArrayList arrParams = new ArrayList();

                foreach (PropertyInfo oInfo in arrProperties)
                {
                    DBColumnInfo oColumnInfo = oDBInfo.Columns[oInfo.Name] as DBColumnInfo;
                    if (oColumnInfo != null && !(IsInserting && oColumnInfo.IsPrimaryKey))
                    {
                        objPropertyValue = oInfo.GetValue(EntityInstance, null);

                        if (objPropertyValue == null || DateTime.MinValue.Equals(objPropertyValue))
                            arrParams.Add(new SqlParameter("@" + oColumnInfo.Name, DBNull.Value));
                        else
                            arrParams.Add(new SqlParameter("@" + oColumnInfo.Name, objPropertyValue));
                    }
                }
                SqlParameter [] Parameters = new SqlParameter[arrParams.Count];
                arrParams.CopyTo(Parameters, 0);
                object Result = SqlHelper.ExecuteScalar(ConfigManager.Current.ConnectionString, CommandType.StoredProcedure, ProcedureName, Parameters);
                bSuccess = true;

				if (IsInserting && Result != null)
                {
                    try
                    {
                        foreach (string Key in oDBInfo.Columns.Keys)
                        {
                            DBColumnInfo oColInfo = oDBInfo.Columns[Key] as DBColumnInfo;

                            if (oColInfo.IsPrimaryKey)
                            {
                                PropertyInfo oInfo = oCustomType.GetProperty(oColInfo.Name);
                                oInfo.SetValue(EntityInstance, Convert.ChangeType(Result, oInfo.PropertyType), null);

                                return bSuccess;
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        private static SqlParameter[] DictionaryToParameterArray(Dictionary<string, object> Parameters)
        {
            if (Parameters == null || Parameters.Count == 0)
                return new SqlParameter[] { };

            SqlParameter[] arrParameters = new SqlParameter[Parameters.Count];
            int i = 0;

            foreach (string Key in Parameters.Keys)
                arrParameters[i++] = new SqlParameter(Key.StartsWith("@") ? Key : "@" + Key, Parameters[Key]);

            return arrParameters;
        }
        #endregion

        #region Insert, Update and Execute
        /// <summary>
        /// Generic Insert gateway which uses the related info in DBSchema.map 
        /// and ProcedureName to generically insert the class instance.
        /// 
        /// If the Class has a PrimaryKey, like Member.MemberID, this field is populated with the inserted item's property (@@IDENTITY in SQL Server).
        /// </summary>
        /// <typeparam name="T">Class to be inserted. The class's definition should exist in DBSchema.map file.</typeparam>
        /// <param name="ProcedureName">The name of the stored procedure to use while inserting class instance.</param>
        /// <param name="EntityInstance">The class instance to be inserted.</param>
        /// <returns>Flag regarding the operation's success.</returns>
        public static bool InsertObject<T>(string ProcedureName, T EntityInstance) where T : class
        {
            return SaveObject<T>(ProcedureName, EntityInstance, true);
        }

        /// <summary>
        /// Generic Update gateway which uses the related info in DBSchema.map 
        /// and ProcedureName to generically update the class instance.
        /// </summary>
        /// <typeparam name="T">Class to be updated. The class's definition should exist in DBSchema.map file.</typeparam>
        /// <param name="ProcedureName">The name of the stored procedure to use while updating class instance.</param>
        /// <param name="EntityInstance">The class instance to be updated.</param>
        /// <returns>Flag regarding the operation's success.</returns>
        public static bool UpdateObject<T>(string ProcedureName, T EntityInstance) where T : class
        {
            return SaveObject<T>(ProcedureName, EntityInstance, false);
        }

        /// <summary>
        /// Executes the stored procedure and returns a flag regarding any errors encountered.
        /// </summary>
        /// <param name="ProcedureName">Name of the stored procedure to execute.</param>
        /// <returns>Flag regarding the operation's success.</returns>
        public static bool ExecuteQuery(string ProcedureName)
        {
            return ExecuteQuery(ProcedureName, (SqlParameter[])null);
        }
        /// <summary>
        /// Executes the stored procedure and returns a flag regarding any errors encountered.
        /// <example>
        /// Dictionary<string, object> Parameters = new Dictionary<string, object>();
        /// Parameters.Add("@Username", txtUsername.Text);
        /// bool Success = EntityManager.ExecuteQuery("UpdateMemberLastLogin", Parameters);
        /// </example>
        /// </summary>
        /// <param name="ProcedureName">Name of the stored procedure to execute.</param>
        /// <param name="Parameters">Dictionary which contains parameters to be passed to stored procedure in a name value pattern.</param>
        /// <returns>Flag regarding the operation's success.</returns>
        public static bool ExecuteQuery(string ProcedureName, Dictionary<string, object> Parameters)
        {
            return ExecuteQuery(ProcedureName, DictionaryToParameterArray(Parameters));
        }

        /// <summary>
        /// Executes the stored procedure and returns a flag regarding any errors encountered.
        /// <example>
        /// SqlParameter[] Parameters = { new SqlParameter("@Username", txtUsername.Text) };
        /// bool Success = EntityManager.ExecuteQuery("UpdateMemberLastLogin", Parameters);
        /// </example>
        /// </summary>
        /// <param name="ProcedureName">Name of the stored procedure to execute.</param>
        /// <param name="Parameters">SqlParameter array which contains parameters to be passed to stored procedure.</param>
        /// <returns>Flag regarding the operation's success.</returns>
        public static bool ExecuteQuery(string ProcedureName, SqlParameter[] Parameters)
        {
            bool bSuccess = false;
            
            try
            {
                SqlHelper.ExecuteNonQuery(ConfigManager.Current.ConnectionString, CommandType.StoredProcedure, ProcedureName, Parameters);
				bSuccess = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return bSuccess;
        }
        /// <summary>
        /// Executes the stored procedure and returns a flag regarding any errors encountered.
        /// </summary>
        /// <param name="ProcedureName">Name of the stored procedure to execute.</param>
        public static object ExecuteQueryWithResult(string ProcedureName)
        {
            return ExecuteQueryWithResult(ProcedureName, (SqlParameter[])null);
        }
        /// <summary>
        /// Executes the stored procedure and returns a flag regarding any errors encountered.
        /// <example>
        /// Dictionary<string, object> Parameters = new Dictionary<string, object>();
        /// Parameters.Add("@Username", txtUsername.Text);
        /// bool Success = EntityManager.ExecuteQueryWithResult("UpdateMemberLastLogin", Parameters);
        /// </example>
        /// </summary>
        /// <param name="ProcedureName">Name of the stored procedure to execute.</param>
        /// <param name="Parameters">Dictionary which contains parameters to be passed to stored procedure in a name value pattern.</param>
        /// <returns>Flag regarding the operation's success.</returns>
        public static object ExecuteQueryWithResult(string ProcedureName, Dictionary<string, object> Parameters)
        {
            return ExecuteQueryWithResult(ProcedureName, DictionaryToParameterArray(Parameters));
        }

        /// <summary>
        /// Executes the stored procedure and returns a flag regarding any errors encountered.
        /// <example>
        /// SqlParameter[] Parameters = { new SqlParameter("@Username", txtUsername.Text) };
        /// bool Success = EntityManager.ExecuteQueryWithResult("UpdateMemberLastLogin", Parameters);
        /// </example>
        /// </summary>
        /// <param name="ProcedureName">Name of the stored procedure to execute.</param>
        /// <param name="Parameters">SqlParameter array which contains parameters to be passed to stored procedure.</param>
        /// <returns>Flag regarding the operation's success.</returns>
        public static object ExecuteQueryWithResult(string ProcedureName, SqlParameter[] Parameters)
        {
            object objResult = null;

            try
            {
                objResult = SqlHelper.ExecuteScalar(ConfigManager.Current.ConnectionString, CommandType.StoredProcedure, ProcedureName, Parameters);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return objResult;
        }
        #endregion
    }
}
