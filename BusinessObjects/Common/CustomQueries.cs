using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace BusinessObjects.Common
{
    public class CustomQueries
    {
        public static string GetSeperatedNamesByID(string ObjectID, string SourceTable, string SourceColumn, 
            string ForeignTable, string ForeignColumn, string ForeignName)
        {
            SqlParameter [] Parameters = { new SqlParameter("@ObjectID", ObjectID), new SqlParameter("@SourceTable", SourceTable), new SqlParameter("@SourceColumn", SourceColumn), new SqlParameter("@ForeignTable", ForeignTable), new SqlParameter("@ForeignColumn", ForeignColumn), new SqlParameter("@ForeignName", ForeignName)};
            object result = EntityManager.ExecuteQueryWithResult("GetSeperatedNamesByID", Parameters);
            return result != null ? result.ToString() : "--";
        }

        public static int GetTableCountByFilter(string TableName, Dictionary<string, object> Filters = null)
        {
            try
            {
                string Filter = EntityManager.FilterToString(Filters);

                Filter = string.IsNullOrEmpty(Filter) ? "" : string.Concat(" WHERE ", Filter);
                string query = string.Format("SELECT COUNT(1) FROM {0}{1}", TableName, Filter);
                return (int)SqlHelper.ExecuteScalar(ConfigManager.Current.ConnectionString, CommandType.Text, query);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
        public static int GetDistinctTableCountByFilter(string TableName, string ColumnName, Dictionary<string, object> Filters = null)
        {
            try
            {
                string Filter = EntityManager.FilterToString(Filters);

                Filter = string.IsNullOrEmpty(Filter) ? "" : string.Concat(" WHERE ", Filter);
                string query = string.Format("SELECT COUNT(DISTINCT {1}) ADET FROM {0}{2}", TableName, ColumnName, Filter);
                return (int)SqlHelper.ExecuteScalar(ConfigManager.Current.ConnectionString, CommandType.Text, query);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }

        public static DataTable GetStuffByPage(string ProcedureName, SqlParameter[] Parameters)
        {
            int TotalRows = 0;

            DataTable dt = ExecuteDataTable(ProcedureName, Parameters);
            try
            {
                TotalRows = Convert.ToInt32(dt.Rows[0]["TotalRows"]);
                dt.Columns.Remove("TotalRows");
            }
            catch (Exception)
            {
            }
            return dt;
        }

        public static DataTable GetStuffByPage(string ProcedureName, Dictionary<string, object> Filters)
        {
            int TotalRows = 0;

            DataTable dt = ExecuteDataTable(ProcedureName, Filters);
            try
            {
                TotalRows = Convert.ToInt32(dt.Rows[0]["TotalRows"]);
                dt.Columns.Remove("TotalRows");
            }
            catch (Exception)
            {
            }
            return dt;
        }

        public static DataTable GetStuffByPage(string ProcedureName, Dictionary<string, object> Filters, string SortExpression, int PageIndex, int PageSize, string Columns, out int TotalRows)
        {
            return GetStuffByPage(ProcedureName, EntityManager.FilterToString(Filters),
                SortExpression, PageIndex, PageSize, Columns, out TotalRows);
        }
        public static DataTable GetStuffByPage(string ProcedureName, string Filter, string SortExpression, int PageIndex, int PageSize, string Columns, out int TotalRows)
        {
            TotalRows = 0;
            if (Filter.NullOrEmpty()) Filter = null;

            SqlParameter[] Parameters = { new SqlParameter("@PageIndex", PageIndex), 
                new SqlParameter("@PageSize", PageSize) ,
                new SqlParameter("@SortExpression", SortExpression) ,
                new SqlParameter("@Filter", Filter),
                new SqlParameter("@Columns", Columns)};

            DataTable dt = ExecuteDataTable(ProcedureName, Parameters);
            try
            {
                TotalRows = Convert.ToInt32(dt.Rows[0]["TotalRows"]);
                dt.Columns.Remove("TotalRows");
            }
            catch (Exception)
            {
            }
            return dt;
        }

        public static SqlParameter[] FilterToSQLParams(Dictionary<string, object> Filter)
        {
            List<SqlParameter> arr = new List<SqlParameter>();

            foreach (string key in Filter.Keys)
            {
                if (key.NullOrEmpty()) continue;
                arr.Add(new SqlParameter(string.Concat("@", key), Filter[key]));
            }
            return arr.ToArray();
        }
        public static DataTable GetDataTableFromQuery(string Query)
        {
            DataSet ds = SqlHelper.ExecuteDataset(ConfigManager.Current.ConnectionString, CommandType.Text, Query);

            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }
        public static DataTable ExecuteDataTable(string ProcedureName, Dictionary<string, object> Filter)
        {
            return ExecuteDataTable(ProcedureName, FilterToSQLParams(Filter));
        }
        public static DataTable ExecuteDataTable(string ProcedureName, SqlParameter[] Parameters)
        {
            DataSet ds = SqlHelper.ExecuteDataset(ConfigManager.Current.ConnectionString, CommandType.StoredProcedure, ProcedureName, Parameters);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

        public static DataTable GetSerialsByFilter(Dictionary<string, object> Filter, string SortExpression, int PageIndex, int PageSize, bool FlagProduct)
        {
            int TotalRows = 0;
            DataTable dt = new DataTable();

            if (!FlagProduct)
                dt = CustomQueries.GetStuffByPage("admGetSerialsByPage", Filter, SortExpression, PageIndex + 1, PageSize, "*", out TotalRows);
            else
                dt = CustomQueries.GetStuffByPage("admGetProductsByPage", Filter, SortExpression, PageIndex + 1, PageSize, "*", out TotalRows);

            return dt;
        }

        public static DataTable GetVariantsForBuy()
        {
            return ExecuteDataTable("FillSearchSellItems", (SqlParameter[])null);
        }
        public static DataTable GetVariantsForSell()
        {
            return ExecuteDataTable("FillSearchItems", (SqlParameter[])null);
        }
    }
}
