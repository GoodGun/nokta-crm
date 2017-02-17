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
    public sealed class AddressManager
    {
        public static bool InsertAddress(Address oAddress)
        {
            return EntityManager.InsertObject<Address>("InsertAddress", oAddress);
        }
        public static bool UpdateAddress(Address oAddress)
        {
            return EntityManager.UpdateObject<Address>("UpdateAddress", oAddress);
        }
        public static Address GetAddressByID(int AddressID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@AddressID", AddressID) };
            return EntityManager.GetSingleObjectByCriteria<Address>("GetAddressByID", Parameters);
        }
        public static List<Address> GetAllAddresss()
        {
            return EntityManager.GetObjectsByCriteria<Address>("GetAllAddresss");
        }
        public static List<Address> GetAddresssByFilter(Dictionary<string, object> Filters, string SortExpression = null)
        {
            return EntityManager.GetStuffByPage<Address>("admGetAddresssByPage", Filters, SortExpression = null);
        }        
        public static bool DeleteAddressByID(int AddressID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@AddressID", AddressID) };
            return EntityManager.ExecuteQuery("DeleteAddressByID", Parameters);
        }
        public static DataTable GetAddressByPage(Dictionary<string, object> Filters, string SortExpression, int PageIndex, int PageSize, string Columns, out int TotalRows)
        {
            return Common.CustomQueries.GetStuffByPage("admGetAddresssByPage", Filters, SortExpression, PageIndex, PageSize, Columns, out TotalRows);
        }
    }
}
