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
    public sealed class ContactManager
    {
        public static bool InsertContact(Contact oContact)
        {
            return EntityManager.InsertObject<Contact>("InsertContact", oContact);
        }
        public static bool UpdateContact(Contact oContact)
        {
            return EntityManager.UpdateObject<Contact>("UpdateContact", oContact);
        }
        public static Contact GetContactByID(int ContactID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@ContactID", ContactID) };
            return EntityManager.GetSingleObjectByCriteria<Contact>("GetContactByID", Parameters);
        }
        public static List<Contact> GetAllContacts()
        {
            return EntityManager.GetObjectsByCriteria<Contact>("GetAllContacts");
        }
        public static List<Contact> GetContactsByFilter(Dictionary<string, object> Filters, string SortExpression = null)
        {
            return EntityManager.GetStuffByPage<Contact>("admGetContactsByPage", Filters, SortExpression = null);
        }        
        public static bool DeleteContactByID(int ContactID)
        {
            SqlParameter[] Parameters = { new SqlParameter("@ContactID", ContactID) };
            return EntityManager.ExecuteQuery("DeleteContactByID", Parameters);
        }
        public static DataTable GetContactByPage(Dictionary<string, object> Filters, string SortExpression, int PageIndex, int PageSize, string Columns, out int TotalRows)
        {
            return Common.CustomQueries.GetStuffByPage("admGetContactsByPage", Filters, SortExpression, PageIndex, PageSize, Columns, out TotalRows);
        }
    }
}
