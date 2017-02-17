using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using BusinessObjects.Common;
using System.Text;
using System.Text.RegularExpressions;
using Utility;

namespace BusinessObjects
{
    public sealed class ActivityManager
    {
        public static bool InsertActivity(Activity oActivity)
        {
            return EntityManager.InsertObject<Activity>("InsertActivity", oActivity);
        }
        public static bool UpdateActivity(Activity oActivity)
        {
            return EntityManager.UpdateObject<Activity>("UpdateActivity", oActivity);
        }
        public static Activity GetActivityByID(int ActivityID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@ActivityID", ActivityID) };
            return EntityManager.GetSingleObjectByCriteria<Activity>("GetActivityByID", Parameters);
        }
        public static List<Activity> GetAllActivitys()
        {
            return EntityManager.GetObjectsByCriteria<Activity>("GetAllActivitys");
        }
        public static List<Activity> GetActivitysByFilter(Dictionary<string, object> Filters, string SortExpression = null)
        {
            return EntityManager.GetStuffByPage<Activity>("admGetActivitysByPage", Filters, SortExpression = null);
        }        
        public static bool DeleteActivityByID(int ActivityID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@ActivityID", ActivityID) };
            return EntityManager.ExecuteQuery("DeleteActivityByID", Parameters);
        }
        public static DataTable GetActivityByPage(Dictionary<string, object> Filters, string SortExpression, int PageIndex, int PageSize, string Columns, out int TotalRows)
        {
            return Common.CustomQueries.GetStuffByPage("admGetActivitysByPage", Filters, SortExpression, PageIndex, PageSize, Columns, out TotalRows);
        }
    }
}
