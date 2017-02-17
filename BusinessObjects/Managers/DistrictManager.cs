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
    public sealed class DistrictManager
    {
        public static bool InsertDistrict(District oDistrict)
        {
            return EntityManager.InsertObject<District>("InsertDistrict", oDistrict);
        }
        public static bool UpdateDistrict(District oDistrict)
        {
            return EntityManager.UpdateObject<District>("UpdateDistrict", oDistrict);
        }
        public static District GetDistrictByID(int DistrictID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@", DistrictID) };
            return EntityManager.GetSingleObjectByCriteria<District>("GetDistrictByID", Parameters);
        }
        public static List<District> GetAllDistricts()
        {
            return EntityManager.GetObjectsByCriteria<District>("GetAllDistricts");
        }
        public static List<District> GetDistrictsByFilter(Dictionary<string, object> Filters, string SortExpression = null)
        {
            return EntityManager.GetStuffByPage<District>("admGetDistrictsByPage", Filters, SortExpression = null);
        }        
        public static bool DeleteDistrictByID(int DistrictID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@", DistrictID) };
            return EntityManager.ExecuteQuery("DeleteDistrictByID", Parameters);
        }
        public static DataTable GetDistrictByPage(Dictionary<string, object> Filters, string SortExpression, int PageIndex, int PageSize, string Columns, out int TotalRows)
        {
            return Common.CustomQueries.GetStuffByPage("admGetDistrictsByPage", Filters, SortExpression, PageIndex, PageSize, Columns, out TotalRows);
        }
    }
}
