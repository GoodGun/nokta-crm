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
    public sealed class CountryManager
    {
        public static bool InsertCountry(Country oCountry)
        {
            return EntityManager.InsertObject<Country>("InsertCountry", oCountry);
        }
        public static bool UpdateCountry(Country oCountry)
        {
            return EntityManager.UpdateObject<Country>("UpdateCountry", oCountry);
        }
        public static Country GetCountryByID(int CountryID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@CountryID", CountryID) };
            return EntityManager.GetSingleObjectByCriteria<Country>("GetCountryByID", Parameters);
        }
        public static List<Country> GetAllCountrys()
        {
            return EntityManager.GetObjectsByCriteria<Country>("GetAllCountrys");
        }
        public static List<Country> GetCountrysByFilter(Dictionary<string, object> Filters, string SortExpression = null)
        {
            return EntityManager.GetStuffByPage<Country>("admGetCountrysByPage", Filters, SortExpression = null);
        }        
        public static bool DeleteCountryByID(int CountryID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@CountryID", CountryID) };
            return EntityManager.ExecuteQuery("DeleteCountryByID", Parameters);
        }
        public static DataTable GetCountryByPage(Dictionary<string, object> Filters, string SortExpression, int PageIndex, int PageSize, string Columns, out int TotalRows)
        {
            return Common.CustomQueries.GetStuffByPage("admGetCountrysByPage", Filters, SortExpression, PageIndex, PageSize, Columns, out TotalRows);
        }
    }
}
