using System;
using System.Data;

/// <summary>
/// Summary description for City
/// </summary>
namespace BusinessObjects
{
    public class City
    {
        #region Constructors
        public City()
        {
        }
        #endregion

        #region Variables
        private int _CityID;
        private int _CountryID = 0;
        private string _CityName ;
        private bool _Status = false;

        #endregion

        #region Properties
        public int CityID { get { return _CityID; } set { _CityID = value; } }
        public int CountryID { get { return _CountryID; } set { _CountryID = value; } }
        public string CityName { get { return _CityName; } set { _CityName = value; } }
        public bool Status { get { return _Status; } set { _Status = value; } }

        #endregion
    }
}