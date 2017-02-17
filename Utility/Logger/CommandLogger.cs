using System;
using System.Data;
using System.Data.SqlClient;

namespace Utility
{
    public class CommandLogger
    {
        public static void LogCommand(SqlCommand Command)
        {
            LogCommand(Command, true);
        }

        public static void LogCommand(string commandText, SqlParameter[] commandParameters)
        {
            SqlCommand oCommand = new SqlCommand(commandText);
            foreach (SqlParameter oParameter in commandParameters)
                oCommand.Parameters.Add(new SqlParameter(oParameter.ParameterName, oParameter.Value));
            LogCommand(oCommand);
            oCommand.Dispose();
        }

        public static void LogCommand(SqlCommand Command, bool Success)
        {
            if (Command == null || !IsLoggable(Command.CommandText))
                return;
            try
            {
                CommandDetail oDetail = new CommandDetail();

                oDetail.CommandText = Command.CommandText;
                oDetail.ParameterDetails = Util.GetSqlCommandDetails(Command, false, true);
                oDetail.Success = Success;
                System.Web.HttpContext oContext = System.Web.HttpContext.Current;

                if (oContext != null)
                {
                    if (oContext.Session["LoggableEditorID"] != null)
                        oDetail.EditorID = Convert.ToInt32(oContext.Session["LoggableEditorID"]);
                    
                    if (oContext.Session["LoggableUserID"] != null)
                        oDetail.UserID = Convert.ToInt32(oContext.Session["LoggableUserID"]);

                    if (oContext.Session["LoggableUserType"] != null)
                        oDetail.UserType = 0;
                }
                LogCommand(oDetail);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(new Exception("LogCommand loglayamadý: ", ex));
            }
        }

        private static bool IsLoggable(string CommandText)
        {
            if (!ConfigManager.Current.LogCommands || CommandText == "LogCommandDetails") return false;

            string[] arrFilters = Util.Split(ConfigManager.Current.CommandLoggerFilter);
            
            foreach (string filter in arrFilters)
            {
                if (filter.EndsWith("*"))
                {
                    if (CommandText.ToLower().StartsWith(filter.ToLower().Replace("*", "")))
                        return true;
                }
                else
                    if (CommandText.ToLower() == filter.ToLower())
                        return true;
            }
            return false;
        }

        private static void LogCommand(CommandDetail oDetail)
        {
            try
            {
                SqlParameter oUserID = new SqlParameter("@UserID", SqlDbType.Int);
                SqlParameter oEditorID = new SqlParameter("@EditorID", SqlDbType.Int);
                SqlParameter oUserType = new SqlParameter("@UserType", SqlDbType.Int);
                SqlParameter oCommandText = new SqlParameter("@CommandText", SqlDbType.VarChar);
                SqlParameter oParameterDetails = new SqlParameter("@ParameterDetails", SqlDbType.VarChar);
                SqlParameter oSuccess = new SqlParameter("@Success", SqlDbType.Bit);

                oUserID.Value = oDetail.UserID;
                oEditorID.Value = oDetail.EditorID;
                oUserType.Value = Convert.ToInt32(oDetail.UserType);
                oCommandText.Value = oDetail.CommandText;
                oParameterDetails.Value = oDetail.ParameterDetails;
                oSuccess.Value = oDetail.Success ? 1 : 0;

                SqlParameter[] arrParameters = { oUserID, oEditorID, oUserType, oCommandText, oParameterDetails, oSuccess };

                SqlHelper.ExecuteNonQuery(ConfigManager.Current.ConnectionString, CommandType.StoredProcedure, "LogCommandDetails", arrParameters);
                if (oDetail.Success == false)
                {
                    string Detail = string.Format(@"CommandLogger-Success=0<br>
UserID={0}<br>
UserType={1}<br>
EditorID={2}<br>
CommandText={3}<br>
Parameters={4}<br>", oDetail.UserID, oDetail.UserType, oDetail.EditorID, oDetail.CommandText, oDetail.ParameterDetails);

                    ExceptionManager.Publish(Detail);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }

    public class CommandDetail
    {
        #region Variables
        private int _DetailID;
        private int _UserID;
        private int _EditorID;
        private byte _UserType = 0;
        private string _CommandText;
        private string _ParameterDetails;
        private bool _Success;
        private DateTime _CreateDate = DateTime.Now;
        #endregion

        #region Properties
        public int DetailID
        {
            get { return _DetailID; }
            set { _DetailID = value; }
        }
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public int EditorID
        {
            get { return _EditorID; }
            set { _EditorID = value; }
        }
        public byte UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }
        public string CommandText
        {
            get { return _CommandText; }
            set { _CommandText = value; }
        }
        public string ParameterDetails
        {
            get { return _ParameterDetails; }
            set { _ParameterDetails = value; }
        }
        public bool Success
        {
            get { return _Success; }
            set { _Success = value; }
        }
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
        #endregion
    }
}
