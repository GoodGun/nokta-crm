using System;
using System.Data;

/// <summary>
/// Summary description for OrderMain
/// </summary>
namespace BusinessObjects
{
    public class OrderMain
    {
        #region Constructors
        public OrderMain()
        {
        }
        #endregion

        #region Variables
        private int _OrderID;
        private string _OrderCode ;
        private int _MemberID = 0;
        private int _CustomerID = 0;
        private int _ContactID = 0;
        private int _AddressID = 0;
        private string _ReferenceNo ;
        private byte _OrderTypeID = 0;
        private string _OrderContent ;
        private string _CargoName ;
        private string _BuyerOrderNo ;
        private string _Description ;
        private decimal _TotalPrice = 0;
        private decimal _TaxAmount = 0;
        private decimal _DiscountAmount = 0;
        private decimal _FinalPrice = 0;
        private int _OrderStatusID = 0;
        private string _FileName ;
        private DateTime _OrderDate = System.DateTime.Now;
        private DateTime _CreateDate = System.DateTime.Now;
        private DateTime _UpdateDate ;

        #endregion

        #region Properties
        public int OrderID { get { return _OrderID; } set { _OrderID = value; } }
        public string OrderCode { get { return _OrderCode; } set { _OrderCode = value; } }
        public int MemberID { get { return _MemberID; } set { _MemberID = value; } }
        public int CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
        public int ContactID { get { return _ContactID; } set { _ContactID = value; } }
        public int AddressID { get { return _AddressID; } set { _AddressID = value; } }
        public string ReferenceNo { get { return _ReferenceNo; } set { _ReferenceNo = value; } }
        public byte OrderTypeID { get { return _OrderTypeID; } set { _OrderTypeID = value; } }
        public string OrderContent { get { return _OrderContent; } set { _OrderContent = value; } }
        public string CargoName { get { return _CargoName; } set { _CargoName = value; } }
        public string BuyerOrderNo { get { return _BuyerOrderNo; } set { _BuyerOrderNo = value; } }
        public string Description { get { return _Description; } set { _Description = value; } }
        public decimal TotalPrice { get { return _TotalPrice; } set { _TotalPrice = value; } }
        public decimal TaxAmount { get { return _TaxAmount; } set { _TaxAmount = value; } }
        public decimal DiscountAmount { get { return _DiscountAmount; } set { _DiscountAmount = value; } }
        public decimal FinalPrice { get { return _FinalPrice; } set { _FinalPrice = value; } }
        public int OrderStatusID { get { return _OrderStatusID; } set { _OrderStatusID = value; } }
        public string FileName { get { return _FileName; } set { _FileName = value; } }
        public DateTime OrderDate { get { return _OrderDate; } set { _OrderDate = value; } }
        public DateTime CreateDate { get { return _CreateDate; } set { _CreateDate = value; } }
        public DateTime UpdateDate { get { return _UpdateDate; } set { _UpdateDate = value; } }

        #endregion
    }
}