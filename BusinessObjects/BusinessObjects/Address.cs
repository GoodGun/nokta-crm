using System;
using System.Data;

/// <summary>
/// Summary description for Address
/// </summary>
namespace BusinessObjects
{
    public class Address
    {
        #region Constructors
        public Address()
        {
        }
        #endregion

        #region Variables
        private int _AddressID;
        private int _CustomerID = 0;
        private string _AddressName;
        private bool _IsBillingAddress = false;
        private bool _IsUsed = false;
        private string _Name;
        private string _AddressLine ;
        private int _CityID = 0;
        private int _DistrictID = 0;
        private int _AreaID = 0;
        private string _TaxOffice ;
        private string _TaxNo ;
        private string _ZipCode ;
        private string _Phone ;
        private DateTime _CreateDate = System.DateTime.Now;

        #endregion

        #region Properties
        public int AddressID { get { return _AddressID; } set { _AddressID = value; } }
        public int CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
        public string AddressName { get { return _AddressName; } set { _AddressName = value; } }
        public bool IsBillingAddress { get { return _IsBillingAddress; } set { _IsBillingAddress = value; } }
        public bool IsUsed { get { return _IsUsed; } set { _IsUsed = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string AddressLine { get { return _AddressLine; } set { _AddressLine = value; } }
        public int CityID { get { return _CityID; } set { _CityID = value; } }
        public int DistrictID { get { return _DistrictID; } set { _DistrictID = value; } }
        public int AreaID { get { return _AreaID; } set { _AreaID = value; } }
        public string TaxOffice { get { return _TaxOffice; } set { _TaxOffice = value; } }
        public string TaxNo { get { return _TaxNo; } set { _TaxNo = value; } }
        public string ZipCode { get { return _ZipCode; } set { _ZipCode = value; } }
        public string Phone { get { return _Phone; } set { _Phone = value; } }
        public DateTime CreateDate { get { return _CreateDate; } set { _CreateDate = value; } }

        #endregion
    }
}