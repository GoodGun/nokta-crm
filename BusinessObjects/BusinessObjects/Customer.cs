using System;
using System.Data;

/// <summary>
/// Summary description for Customer
/// </summary>
namespace BusinessObjects
{
    public class Customer
    {
        #region Constructors
        public Customer()
        {
        }
        #endregion

        #region Variables
        private int _CustomerID;
        private string _CustomerCode ;
        private string _ReferenceNo ;
        private int _MemberID = 0;
        private int _SellerMemberID = 0;
        private string _Name ;
        private byte _CustomerTypeID = 0;
        private byte _SectorTypeID = 0;
        private string _MainReseller ;
        private string _Reseller ;
        private string _Description ;
        private string _Code1 ;
        private string _Code2 ;
        private string _Code3 ;
        private int _BillingAddressID = 0;
        private int _DeliveryAddressID = 0;
        private DateTime _CreateDate = System.DateTime.Now;
        private DateTime _UpdateDate ;

        #endregion

        #region Properties
        public int CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
        public string CustomerCode { get { return _CustomerCode; } set { _CustomerCode = value; } }
        public string ReferenceNo { get { return _ReferenceNo; } set { _ReferenceNo = value; } }
        public int MemberID { get { return _MemberID; } set { _MemberID = value; } }
        public int SellerMemberID { get { return _SellerMemberID; } set { _SellerMemberID = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public byte CustomerTypeID { get { return _CustomerTypeID; } set { _CustomerTypeID = value; } }
        public byte SectorTypeID { get { return _SectorTypeID; } set { _SectorTypeID = value; } }
        public string MainReseller { get { return _MainReseller; } set { _MainReseller = value; } }
        public string Reseller { get { return _Reseller; } set { _Reseller = value; } }
        public string Description { get { return _Description; } set { _Description = value; } }
        public string Code1 { get { return _Code1; } set { _Code1 = value; } }
        public string Code2 { get { return _Code2; } set { _Code2 = value; } }
        public string Code3 { get { return _Code3; } set { _Code3 = value; } }
        public int BillingAddressID { get { return _BillingAddressID; } set { _BillingAddressID = value; } }
        public int DeliveryAddressID { get { return _DeliveryAddressID; } set { _DeliveryAddressID = value; } }
        public DateTime CreateDate { get { return _CreateDate; } set { _CreateDate = value; } }
        public DateTime UpdateDate { get { return _UpdateDate; } set { _UpdateDate = value; } }

        #endregion
    }
}