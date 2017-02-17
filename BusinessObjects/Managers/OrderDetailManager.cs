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
    public sealed class OrderDetailManager
    {
        public static bool InsertOrderDetail(OrderDetail oOrderDetail)
        {
            return EntityManager.InsertObject<OrderDetail>("InsertOrderDetail", oOrderDetail);
        }
        public static bool UpdateOrderDetail(OrderDetail oOrderDetail)
        {
            return EntityManager.UpdateObject<OrderDetail>("UpdateOrderDetail", oOrderDetail);
        }
        public static OrderDetail GetOrderDetailByID(int OrderDetailID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@OrderDetailID", OrderDetailID) };
            return EntityManager.GetSingleObjectByCriteria<OrderDetail>("GetOrderDetailByID", Parameters);
        }
        public static List<OrderDetail> GetAllOrderDetails()
        {
            return EntityManager.GetObjectsByCriteria<OrderDetail>("GetAllOrderDetails");
        }
        public static List<OrderDetail> GetOrderDetailsByFilter(Dictionary<string, object> Filters, string SortExpression = null)
        {
            return EntityManager.GetStuffByPage<OrderDetail>("admGetOrderDetailsByPage", Filters, SortExpression = null);
        }        
        public static bool DeleteOrderDetailByID(int OrderDetailID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@OrderDetailID", OrderDetailID) };
            return EntityManager.ExecuteQuery("DeleteOrderDetailByID", Parameters);
        }
        public static DataTable GetOrderDetailByPage(Dictionary<string, object> Filters, string SortExpression, int PageIndex, int PageSize, string Columns, out int TotalRows)
        {
            return Common.CustomQueries.GetStuffByPage("admGetOrderDetailsByPage", Filters, SortExpression, PageIndex, PageSize, Columns, out TotalRows);
        }
    }
}
