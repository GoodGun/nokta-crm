using System;
using System.Web.Caching;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace Utility
{
    public sealed class SchemaManager
    {
        private static object _locker = 1;
        private static Hashtable _DBSchema;
        public static Hashtable DBSchema
        {
            get
            {
                if (CacheUtil.GlobalCache["DBSchema"] != null)
                    return _DBSchema;
                else
                {
                    lock (_locker)
                    {
                        if (CacheUtil.GlobalCache["DBSchema"] == null)
                        {
                            _DBSchema = GetDBSchema();
                            CacheUtil.GlobalCache.Add("DBSchema", "exists", 
                                new System.Web.Caching.CacheDependency(ConfigManager.Current.pathDBSchema), 
                                DateTime.Now.AddYears(1), 
                                Cache.NoSlidingExpiration, 
                                CacheItemPriority.NotRemovable, 
                                null);
                        }
                        return _DBSchema;
                    }
                }
            }
        }
        public static DBTableInfo GetTable(string table)
        {
            return DBSchema[table] as Utility.DBTableInfo;
        }
        private static Hashtable GetDBSchema()
        {
            Hashtable tables = new Hashtable();
            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigManager.Current.pathDBSchema);

            DBTableInfo oTable = null;

            foreach (XmlNode tnode in doc.SelectNodes("DBTables/DBTable"))
            {
                oTable = getTable(tnode);
                if (!tables.ContainsKey(oTable.Name))
                    tables.Add(oTable.Name, oTable);
            }

            return tables;
        }
        private static Utility.DBTableInfo getTable(XmlNode tnode)
        {
            DBTableInfo t = new DBTableInfo();
            t.Name = tnode.Attributes["Name"].Value;

            XmlAttribute act = tnode.Attributes["AccessTypes"];
            XmlAttribute menugroup = tnode.Attributes["MenuGroup"];
            XmlAttribute sortorder = tnode.Attributes["SortOrder"];
            if (act != null)
            {
                char [] sep = {','};
                t.AccessTypes = new List<string>(act.Value.Split(sep, StringSplitOptions.RemoveEmptyEntries));
            }
            if (menugroup != null) t.MenuGroup = menugroup.Value;
            if (sortorder != null) t.SortOrder = sortorder.Value.ToInt();

            DBColumnInfo c = null;

            foreach (XmlNode cnode in tnode.SelectNodes("DBColumns/DBColumn"))
            {
                c = new DBColumnInfo();
                c.Name = cnode.Attributes["NetName"].Value;
                c.IsPrimaryKey = cnode.Attributes["IsPrimaryKey"].Value.ToLower() != "false";
                
                t.Columns.Add(c.Name, c);
            }

            return t;
        }
    }
}
