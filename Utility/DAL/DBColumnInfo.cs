using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

namespace Utility
{
    public class DBColumnInfo
    {
        #region Constructors
        public DBColumnInfo() { }
        #endregion

        #region Variables
        private string _Name;
        private bool _IsPrimaryKey;
        #endregion

        #region Properties
        public string Name { get { return _Name; } set { _Name = value; } }
        public bool IsPrimaryKey { get { return _IsPrimaryKey; } set { _IsPrimaryKey = value; } }
        #endregion
    }
}
