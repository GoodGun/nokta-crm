using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

namespace Utility
{
    public class DBTableInfo
    {
        public DBTableInfo() { }
        public DBTableInfo(string name) { this.Name = name; }
        
        private string _Name;
        private string _MenuGroup;
        private int _SortOrder = 0;
        private Hashtable _Columns = new Hashtable();
        private List<string> _AccessTypes = new List<string>();

        public string Name { get { return _Name; } set { _Name = value; } }
        public string MenuGroup { get { return _MenuGroup; } set { _MenuGroup = value; } }
        public int SortOrder { get { return _SortOrder; } set { _SortOrder = value; } }
        public Hashtable Columns { get { return _Columns; } set { _Columns = value; } }
        public List<string> AccessTypes { get { return _AccessTypes; } set { _AccessTypes = value; } }
    }
}
