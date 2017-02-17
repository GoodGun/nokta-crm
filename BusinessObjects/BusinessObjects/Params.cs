using System;
using System.Data;

/// <summary>
/// Summary description for Params
/// </summary>
namespace BusinessObjects
{
    public class Params
    {
        #region Constructors
        public Params()
        {
        }
        #endregion

        #region Variables
        private int _TypeID;
        private string _TypeCode ;
        private string _TypeName ;
        private string _Description ;
        private string _ObjectValue ;

        #endregion

        #region Properties
        public int TypeID { get { return _TypeID; } set { _TypeID = value; } }
        public string TypeCode { get { return _TypeCode; } set { _TypeCode = value; } }
        public string TypeName { get { return _TypeName; } set { _TypeName = value; } }
        public string Description { get { return _Description; } set { _Description = value; } }
        public string ObjectValue { get { return _ObjectValue; } set { _ObjectValue = value; } }

        #endregion
    }
}