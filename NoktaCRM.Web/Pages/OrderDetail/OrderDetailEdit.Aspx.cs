using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using BusinessObjects;
using BusinessObjects.Common;
using Utility;

public partial class OrderDetailEdit : BasePage
{
    public int OrderDetailID { get { return base.QInt("OrderDetailID"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles(OrderDetailID > 0 ? "Edit" : "Add", "OrderDetail");
        if (!this.IsPostBack)
            this.InitData();
    }

    private void InitData()
    {
        if (OrderDetailID > 0)
        {
            OrderDetail oOrderDetail = OrderDetailManager.GetOrderDetailByID(OrderDetailID);
            if (oOrderDetail != null)
            {

                this.ddlOrderID.Select(oOrderDetail.OrderID);
                this.ddlProductID.Select(oOrderDetail.ProductID);
                this.txtQuantity.ValueInt = oOrderDetail.Quantity;
                this.txtTaxRate.ValueInt = oOrderDetail.TaxRate;
                this.txtUnitPrice.ValueDecimal = oOrderDetail.UnitPrice;
                this.txtDiscountAmount.ValueDecimal = oOrderDetail.DiscountAmount;
                this.txtTotalPrice.ValueDecimal = oOrderDetail.TotalPrice;
                this.txtFinalPrice.ValueDecimal = oOrderDetail.FinalPrice;
                this.ddlCurrencyID.Select(oOrderDetail.CurrencyID);
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
		if (!Page.IsValid) return;
        bool Updating = false;
        OrderDetail oOrderDetail = null;

        if (OrderDetailID > 0)
        {
            oOrderDetail = OrderDetailManager.GetOrderDetailByID(OrderDetailID);
            Updating = oOrderDetail != null;
        }
        if (!Updating)
        {
            oOrderDetail = new OrderDetail();

        }

        oOrderDetail.OrderID = this.ddlOrderID.SelectedValue.ToInt();
        oOrderDetail.ProductID = this.ddlProductID.SelectedValue.ToInt();
        oOrderDetail.Quantity = this.txtQuantity.ValueInt;
        oOrderDetail.TaxRate = this.txtTaxRate.ValueByte;
        oOrderDetail.UnitPrice = this.txtUnitPrice.ValueDecimal;
        oOrderDetail.DiscountAmount = this.txtDiscountAmount.ValueDecimal;
        oOrderDetail.TotalPrice = this.txtTotalPrice.ValueDecimal;
        oOrderDetail.FinalPrice = this.txtFinalPrice.ValueDecimal;
        oOrderDetail.CurrencyID = this.ddlCurrencyID.SelectedValue.ToInt();
        bool bSuccess = Updating ? OrderDetailManager.UpdateOrderDetail(oOrderDetail) : OrderDetailManager.InsertOrderDetail(oOrderDetail);

        if (bSuccess)
            Redirect("/orderdetail-list?s=1");
        else
            base.Warn("error.save");
    }
}