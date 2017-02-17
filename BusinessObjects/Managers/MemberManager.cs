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
    public sealed class MemberManager
    {
        public static bool InsertMember(Member oMember)
        {
            return EntityManager.InsertObject<Member>("InsertMember", oMember);
        }
        public static bool UpdateMember(Member oMember)
        {
            return EntityManager.UpdateObject<Member>("UpdateMember", oMember);
        }
        public static Member GetMemberByID(int MemberID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@MemberID", MemberID) };
            return EntityManager.GetSingleObjectByCriteria<Member>("GetMemberByID", Parameters);
        }
        public static List<Member> GetAllMembers()
        {
            return EntityManager.GetObjectsByCriteria<Member>("GetAllMembers");
        }
        public static List<Member> GetMembersByFilter(Dictionary<string, object> Filters, string SortExpression = null)
        {
            return EntityManager.GetStuffByPage<Member>("admGetMembersByPage", Filters, SortExpression = null);
        }        
        public static bool DeleteMemberByID(int MemberID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@MemberID", MemberID) };
            return EntityManager.ExecuteQuery("DeleteMemberByID", Parameters);
        }
        public static DataTable GetMemberByPage(Dictionary<string, object> Filters, string SortExpression, int PageIndex, int PageSize, string Columns, out int TotalRows)
        {
            return Common.CustomQueries.GetStuffByPage("admGetMembersByPage", Filters, SortExpression, PageIndex, PageSize, Columns, out TotalRows);
        }
    }
}
