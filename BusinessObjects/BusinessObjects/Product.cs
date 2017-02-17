using System;
using System.Data;

/// <summary>
/// Summary description for Product
/// </summary>
namespace BusinessObjects
{
    public class Product
    {
        #region Constructors
        public Product()
        {
        }
        #endregion

        #region Variables
        private int _ProductID;
        private string _ProductCode ;
        private string _ReferenceNo ;
        private int _MemberID = 0;
        private string _ProductName ;
        private string _ProductGroup ;
        private string _ProductType ;
        private string _Unit ;
        private int _Tax = 0;
        private string _Producer ;
        private decimal _Price1 = 0;
        private decimal _Price2 = 0;
        private decimal _CurrencyPrice = 0;
        private string _Description ;
        private bool _Status = true;
        private DateTime _CreateDate = System.DateTime.Now;
        private DateTime _UpdateDate ;

        #endregion

        #region Properties
        public int ProductID { get { return _ProductID; } set { _ProductID = value; } }
        public string ProductCode { get { return _ProductCode; } set { _ProductCode = value; } }
        public string ReferenceNo { get { return _ReferenceNo; } set { _ReferenceNo = value; } }
        public int MemberID { get { return _MemberID; } set { _MemberID = value; } }
        public string ProductName { get { return _ProductName; } set { _ProductName = value; } }
        public string ProductGroup { get { return _ProductGroup; } set { _ProductGroup = value; } }
        public string ProductType { get { return _ProductType; } set { _ProductType = value; } }
        public string Unit { get { return _Unit; } set { _Unit = value; } }
        public int Tax { get { return _Tax; } set { _Tax = value; } }
        public string Producer { get { return _Producer; } set { _Producer = value; } }
        public decimal Price1 { get { return _Price1; } set { _Price1 = value; } }
        public decimal Price2 { get { return _Price2; } set { _Price2 = value; } }
        public decimal CurrencyPrice { get { return _CurrencyPrice; } set { _CurrencyPrice = value; } }
        public string Description { get { return _Description; } set { _Description = value; } }
        public bool Status { get { return _Status; } set { _Status = value; } }
        public DateTime CreateDate { get { return _CreateDate; } set { _CreateDate = value; } }
        public DateTime UpdateDate { get { return _UpdateDate; } set { _UpdateDate = value; } }

        #endregion
    }
}