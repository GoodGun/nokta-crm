using System;
using System.Data;

/// <summary>
/// Summary description for District
/// </summary>
namespace BusinessObjects
{
    public class District
    {
        #region Constructors
        public District()
        {
        }
        #endregion

        #region Variables
        private int _DistrictID;
        private int _CityID = 0;
        private string _DistrictName;

        #endregion

        #region Properties
        public int DistrictID { get { return _DistrictID; } set { _DistrictID = value; } }
        public int CityID { get { return _CityID; } set { _CityID = value; } }
        public string DistrictName { get { return _DistrictName; } set { _DistrictName = value; } }

        #endregion
    }
}