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
    public sealed class OrderMainManager
    {
        public static bool InsertOrderMain(OrderMain oOrderMain)
        {
            return EntityManager.InsertObject<OrderMain>("InsertOrderMain", oOrderMain);
        }
        public static bool UpdateOrderMain(OrderMain oOrderMain)
        {
            return EntityManager.UpdateObject<OrderMain>("UpdateOrderMain", oOrderMain);
        }
        public static OrderMain GetOrderMainByID(int OrderMainID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@OrderID", OrderMainID) };
            return EntityManager.GetSingleObjectByCriteria<OrderMain>("GetOrderMainByID", Parameters);
        }
        public static List<OrderMain> GetAllOrderMains()
        {
            return EntityManager.GetObjectsByCriteria<OrderMain>("GetAllOrderMains");
        }
        public static List<OrderMain> GetOrderMainsByFilter(Dictionary<string, object> Filters, string SortExpression = null)
        {
            return EntityManager.GetStuffByPage<OrderMain>("admGetOrderMainsByPage", Filters, SortExpression = null);
        }        
        public static bool DeleteOrderMainByID(int OrderMainID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@OrderID", OrderMainID) };
            return EntityManager.ExecuteQuery("DeleteOrderMainByID", Parameters);
        }
        public static DataTable GetOrderMainByPage(Dictionary<string, object> Filters, string SortExpression, int PageIndex, int PageSize, string Columns, out int TotalRows)
        {
            return Common.CustomQueries.GetStuffByPage("admGetOrderMainsByPage", Filters, SortExpression, PageIndex, PageSize, Columns, out TotalRows);
        }
    }
}
