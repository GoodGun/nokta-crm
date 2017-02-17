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
    public sealed class ProductManager
    {
        public static bool InsertProduct(Product oProduct)
        {
            return EntityManager.InsertObject<Product>("InsertProduct", oProduct);
        }
        public static bool UpdateProduct(Product oProduct)
        {
            return EntityManager.UpdateObject<Product>("UpdateProduct", oProduct);
        }
        public static Product GetProductByID(int ProductID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@ProductID", ProductID) };
            return EntityManager.GetSingleObjectByCriteria<Product>("GetProductByID", Parameters);
        }
        public static List<Product> GetAllProducts()
        {
            return EntityManager.GetObjectsByCriteria<Product>("GetAllProducts");
        }
        public static List<Product> GetProductsByFilter(Dictionary<string, object> Filters, string SortExpression = null)
        {
            return EntityManager.GetStuffByPage<Product>("admGetProductsByPage", Filters, SortExpression = null);
        }        
        public static bool DeleteProductByID(int ProductID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@ProductID", ProductID) };
            return EntityManager.ExecuteQuery("DeleteProductByID", Parameters);
        }
        public static DataTable GetProductByPage(Dictionary<string, object> Filters, string SortExpression, int PageIndex, int PageSize, string Columns, out int TotalRows)
        {
            return Common.CustomQueries.GetStuffByPage("admGetProductsByPage", Filters, SortExpression, PageIndex, PageSize, Columns, out TotalRows);
        }
    }
}
