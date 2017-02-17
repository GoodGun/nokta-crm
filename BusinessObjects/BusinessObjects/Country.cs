using System;
using System.Data;

/// <summary>
/// Summary description for Country
/// </summary>
namespace BusinessObjects
{
    public class Country
    {
        #region Constructors
        public Country()
        {
        }
        #endregion

        #region Variables
        private int _CountryID;
        private string _CountryName ;
        private bool _Status = false;

        #endregion

        #region Properties
        public int CountryID { get { return _CountryID; } set { _CountryID = value; } }
        public string CountryName { get { return _CountryName; } set { _CountryName = value; } }
        public bool Status { get { return _Status; } set { _Status = value; } }

        #endregion
    }
}