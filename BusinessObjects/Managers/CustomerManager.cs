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
    public sealed class CustomerManager
    {
        public static bool InsertCustomer(Customer oCustomer)
        {
            return EntityManager.InsertObject<Customer>("InsertCustomer", oCustomer);
        }
        public static bool UpdateCustomer(Customer oCustomer)
        {
            return EntityManager.UpdateObject<Customer>("UpdateCustomer", oCustomer);
        }
        public static Customer GetCustomerByID(int CustomerID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@CustomerID", CustomerID) };
            return EntityManager.GetSingleObjectByCriteria<Customer>("GetCustomerByID", Parameters);
        }
        public static List<Customer> GetAllCustomers()
        {
            return EntityManager.GetObjectsByCriteria<Customer>("GetAllCustomers");
        }
        public static List<Customer> GetCustomersByFilter(Dictionary<string, object> Filters, string SortExpression = null)
        {
            return EntityManager.GetStuffByPage<Customer>("admGetCustomersByPage", Filters, SortExpression = null);
        }        
        public static bool DeleteCustomerByID(int CustomerID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@CustomerID", CustomerID) };
            return EntityManager.ExecuteQuery("DeleteCustomerByID", Parameters);
        }
        public static DataTable GetCustomerByPage(Dictionary<string, object> Filters, string SortExpression, int PageIndex, int PageSize, string Columns, out int TotalRows)
        {
            return Common.CustomQueries.GetStuffByPage("admGetCustomersByPage", Filters, SortExpression, PageIndex, PageSize, Columns, out TotalRows);
        }
    }
}
