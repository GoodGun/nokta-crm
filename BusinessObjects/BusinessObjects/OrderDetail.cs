using System;
using System.Data;

/// <summary>
/// Summary description for OrderDetail
/// </summary>
namespace BusinessObjects
{
    public class OrderDetail
    {
        #region Constructors
        public OrderDetail()
        {
        }
        #endregion

        #region Variables
        private int _OrderDetailID;
        private int _OrderID = 0;
        private int _ProductID = 0;
        private int _Quantity = 0;
        private byte _TaxRate = 0;
        private decimal _UnitPrice = 0;
        private decimal _DiscountAmount = 0;
        private decimal _TotalPrice = 0;
        private decimal _FinalPrice = 0;
        private int _CurrencyID = 0;

        #endregion

        #region Properties
        public int OrderDetailID { get { return _OrderDetailID; } set { _OrderDetailID = value; } }
        public int OrderID { get { return _OrderID; } set { _OrderID = value; } }
        public int ProductID { get { return _ProductID; } set { _ProductID = value; } }
        public int Quantity { get { return _Quantity; } set { _Quantity = value; } }
        public byte TaxRate { get { return _TaxRate; } set { _TaxRate = value; } }
        public decimal UnitPrice { get { return _UnitPrice; } set { _UnitPrice = value; } }
        public decimal DiscountAmount { get { return _DiscountAmount; } set { _DiscountAmount = value; } }
        public decimal TotalPrice { get { return _TotalPrice; } set { _TotalPrice = value; } }
        public decimal FinalPrice { get { return _FinalPrice; } set { _FinalPrice = value; } }
        public int CurrencyID { get { return _CurrencyID; } set { _CurrencyID = value; } }

        #endregion
    }
}