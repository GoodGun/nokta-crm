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
    public sealed class ParamsManager
    {
        public static bool InsertParams(Params oParams)
        {
            return EntityManager.InsertObject<Params>("InsertParams", oParams);
        }
        public static bool UpdateParams(Params oParams)
        {
            return EntityManager.UpdateObject<Params>("UpdateParams", oParams);
        }
        public static Params GetParamsByID(int ParamsID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@TypeID", ParamsID) };
            return EntityManager.GetSingleObjectByCriteria<Params>("GetParamsByID", Parameters);
        }
        public static List<Params> GetAllParamss()
        {
            return EntityManager.GetObjectsByCriteria<Params>("GetAllParamss");
        }
        public static List<Params> GetParamssByFilter(Dictionary<string, object> Filters, string SortExpression = null)
        {
            return EntityManager.GetStuffByPage<Params>("admGetParamssByPage", Filters, SortExpression = null);
        }        
        public static bool DeleteParamsByID(int ParamsID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@TypeID", ParamsID) };
            return EntityManager.ExecuteQuery("DeleteParamsByID", Parameters);
        }
        public static DataTable GetParamsByPage(Dictionary<string, object> Filters, string SortExpression, int PageIndex, int PageSize, string Columns, out int TotalRows)
        {
            return Common.CustomQueries.GetStuffByPage("admGetParamssByPage", Filters, SortExpression, PageIndex, PageSize, Columns, out TotalRows);
        }
    }
}
