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
    public sealed class ContentManager
    {
        public static bool InsertContent(Content oContent)
        {
            return EntityManager.InsertObject<Content>("InsertContent", oContent);
        }
        public static bool UpdateContent(Content oContent)
        {
            return EntityManager.UpdateObject<Content>("UpdateContent", oContent);
        }
        public static Content GetContentByID(int ContentID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@ContentID", ContentID) };
            return EntityManager.GetSingleObjectByCriteria<Content>("GetContentByID", Parameters);
        }
        public static List<Content> GetAllContents()
        {
            return EntityManager.GetObjectsByCriteria<Content>("GetAllContents");
        }
        public static List<Content> GetContentsByFilter(Dictionary<string, object> Filters, string SortExpression = null)
        {
            return EntityManager.GetStuffByPage<Content>("admGetContentsByPage", Filters, SortExpression = null);
        }        
        public static bool DeleteContentByID(int ContentID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@ContentID", ContentID) };
            return EntityManager.ExecuteQuery("DeleteContentByID", Parameters);
        }
        public static DataTable GetContentByPage(Dictionary<string, object> Filters, string SortExpression, int PageIndex, int PageSize, string Columns, out int TotalRows)
        {
            return Common.CustomQueries.GetStuffByPage("admGetContentsByPage", Filters, SortExpression, PageIndex, PageSize, Columns, out TotalRows);
        }
        public static Content GetContentByTitle(string Title, string lang)
        {
            SqlParameter[] Parameters = { new SqlParameter("@Title", Title), new SqlParameter("@Lang", lang) };
            return EntityManager.GetSingleObjectByCriteria<Content>("GetContentByTitle", Parameters);
        }
    }
}
