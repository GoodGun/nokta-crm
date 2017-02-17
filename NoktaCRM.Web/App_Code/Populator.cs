using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using BusinessObjects;
using System.Data;
using System.Data.SqlClient;
using Utility;
using BusinessObjects.Common;

public static class Populator
{
    public static Dictionary<string, object> GetFilter()
    {
        return new Dictionary<string, object>();
    }
    public static List<string> GetDBColumns(this string tableName)
    {
        List<string> cols = new List<string>();

        DBTableInfo ti = SchemaManager.GetTable(tableName);
        
        if (ti != null)
        {
            foreach (string colName in ti.Columns.Keys)
                cols.Add(colName);
        }

        return cols;
    }
    
    public static string GetName(DBObject tableInfo, int ID, string defValue = "--", bool addLink = true, bool addIDPrefix = false)
    {
        if (ID < 1) return defValue;
        string query = string.Format("SELECT {0} FROM {1} (NOLOCK) WHERE {2}={3}", 
            tableInfo.NameColumn, tableInfo.TableName, tableInfo.IDColumn, ID);
        DataTable dt = CustomQueries.GetDataTableFromQuery(query);
        if (dt.Rows.Count == 0) return defValue;
        defValue = dt.Rows[0][tableInfo.NameColumn].ToString();
        if (tableInfo.FromResource) defValue = ResourceManager.GetResource(defValue);

        defValue = addIDPrefix ? string.Format("#{0} - {1}", ID, defValue) : defValue;

        if (addLink) defValue = string.Format("<a href='/{0}-detail/{1}?Popup=1' class='popup'>{2}</a>", tableInfo.TableName.ToLower(), ID, defValue);
        return defValue;
    }
    public static ListControl DBPopulate(this ListControl ddl, DBObject tableInfo, 
        bool cacheDatasource = true,
        bool addSelect = true, string chooseKey = "seciniz", bool searchable = false, int maxSelection = 0,
        string filter = null, OrderBy sortOrder = OrderBy.Default, bool addIDPrefix = false)
    {
        DataTable dt = null;
        if (!cacheDatasource) dt = GetLookupData(tableInfo, filter, sortOrder, addIDPrefix);
        else
        {
            string cacheKey = string.Join(":", tableInfo.TableName, filter);
            dt = CacheManager.Get<DataTable>("dbpopulate", cacheKey, typeof(Populator), "GetLookupData", tableInfo, filter, sortOrder, addIDPrefix);
        }
        ddl.BindDatatable(dt, addSelect, addIDPrefix, chooseKey, tableInfo.NameColumn, tableInfo.IDColumn);
        if (searchable) ddl.Chosen(chooseKey, maxSelection);
        return ddl;
    }
    public static DataTable GetLookupData(DBObject tableInfo, string filter = null, OrderBy sortOrder = OrderBy.Name, bool addIDPrefix = false)
    {
        string query = string.Format("SELECT {0} AS {3}, {1} AS {4} FROM {2} (NOLOCK)", 
            tableInfo.IDColumn, tableInfo.NameColumn, tableInfo.TableName,
            addIDPrefix ? "ID" : tableInfo.IDColumn,
            addIDPrefix ? "Name" : tableInfo.NameColumn);

        if (!string.IsNullOrEmpty(filter))
            query = string.Concat(query, " WHERE ", filter);
        
        if (sortOrder != OrderBy.Default)
            query = string.Concat(query, " ORDER BY ", sortOrder == OrderBy.Name ? tableInfo.NameColumn : string.Concat(tableInfo.IDColumn, " DESC"));

        string key = string.Concat("lookup", query.GetHashCode().ToString("X"));

        DataTable dt = CustomQueries.GetDataTableFromQuery(query);
        if (tableInfo.FromResource && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
                dr[1] = ResourceManager.GetResource(dr[1].ToSureString());
        }
        return dt;
    }
    public static void BindDatatable(this ListControl ddl, DataTable dt, bool addSelect = true, bool addIDPrefix = true, string chooseKey = "seciniz", string textField = "Name", string valueField = "ID")
    {
        if (addIDPrefix)
            foreach (DataRow dr in dt.Rows)
                dr[textField] = string.Format("#{0} - {1}", dr[valueField], dr[textField]);
        ddl.Items.Clear();
        ddl.DataTextField = textField;
        ddl.DataValueField = valueField;
        ddl.DataSource = dt;
        ddl.DataBind();
        ddl.Attributes.Add("data-no_results_text", ResourceManager.GetResource("filter.no.data"));
        if (addSelect) ddl.InsertChooseItem(true, chooseKey);
    }
}