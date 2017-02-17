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
    public sealed class CityManager
    {
        public static bool InsertCity(City oCity)
        {
            return EntityManager.InsertObject<City>("InsertCity", oCity);
        }
        public static bool UpdateCity(City oCity)
        {
            return EntityManager.UpdateObject<City>("UpdateCity", oCity);
        }
        public static City GetCityByID(int CityID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@CityID", CityID) };
            return EntityManager.GetSingleObjectByCriteria<City>("GetCityByID", Parameters);
        }
        public static List<City> GetAllCitys()
        {
            return EntityManager.GetObjectsByCriteria<City>("GetAllCitys");
        }
        public static List<City> GetCitysByFilter(Dictionary<string, object> Filters, string SortExpression = null)
        {
            return EntityManager.GetStuffByPage<City>("admGetCitysByPage", Filters, SortExpression = null);
        }        
        public static bool DeleteCityByID(int CityID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@CityID", CityID) };
            return EntityManager.ExecuteQuery("DeleteCityByID", Parameters);
        }
        public static DataTable GetCityByPage(Dictionary<string, object> Filters, string SortExpression, int PageIndex, int PageSize, string Columns, out int TotalRows)
        {
            return Common.CustomQueries.GetStuffByPage("admGetCitysByPage", Filters, SortExpression, PageIndex, PageSize, Columns, out TotalRows);
        }
    }
}
