using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using BusinessObjects;
using BusinessObjects.Common;
using Utility;

public partial class OrderMainEdit : BasePage
{
    public int OrderMainID { get { return base.QInt("OrderMainID"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles(OrderMainID > 0 ? "Edit" : "Add", "OrderMain");
        if (!this.IsPostBack)
            this.InitData();
    }

    private void InitData()
    {
        if (OrderMainID > 0)
        {
            OrderMain oOrderMain = OrderMainManager.GetOrderMainByID(OrderMainID);
            if (oOrderMain != null)
            {

                this.txtOrderCode.Text = oOrderMain.OrderCode;
                this.ddlMemberID.Select(oOrderMain.MemberID);
                this.ddlCustomerID.Select(oOrderMain.CustomerID);
                this.ddlContactID.Select(oOrderMain.ContactID);
                this.ddlAddressID.Select(oOrderMain.AddressID);
                this.txtReferenceNo.Text = oOrderMain.ReferenceNo;
                this.ddlOrderTypeID.Select(oOrderMain.OrderTypeID);
                this.txtOrderContent.Text = oOrderMain.OrderContent;
                this.txtCargoName.Text = oOrderMain.CargoName;
                this.txtBuyerOrderNo.Text = oOrderMain.BuyerOrderNo;
                this.txtDescription.Text = oOrderMain.Description;
                this.txtTotalPrice.ValueDecimal = oOrderMain.TotalPrice;
                this.txtTaxAmount.ValueDecimal = oOrderMain.TaxAmount;
                this.txtDiscountAmount.ValueDecimal = oOrderMain.DiscountAmount;
                this.txtFinalPrice.ValueDecimal = oOrderMain.FinalPrice;
                this.ddlOrderStatusID.Select(oOrderMain.OrderStatusID);
                this.txtFileName.Text = oOrderMain.FileName;
                this.dtOrderDate.SelectedDate = oOrderMain.OrderDate;
                this.dtUpdateDate.SelectedDate = oOrderMain.UpdateDate;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
		if (!Page.IsValid) return;
        bool Updating = false;
        OrderMain oOrderMain = null;

        if (OrderMainID > 0)
        {
            oOrderMain = OrderMainManager.GetOrderMainByID(OrderMainID);
            Updating = oOrderMain != null;
        }
        if (!Updating)
        {
            oOrderMain = new OrderMain();
oOrderMain.CreateDate = DateTime.Now;
        }

        oOrderMain.OrderCode = this.txtOrderCode.Text;
        oOrderMain.MemberID = this.ddlMemberID.SelectedValue.ToInt();
        oOrderMain.CustomerID = this.ddlCustomerID.SelectedValue.ToInt();
        oOrderMain.ContactID = this.ddlContactID.SelectedValue.ToInt();
        oOrderMain.AddressID = this.ddlAddressID.SelectedValue.ToInt();
        oOrderMain.ReferenceNo = this.txtReferenceNo.Text;
        oOrderMain.OrderTypeID = this.ddlOrderTypeID.SelectedValue.ToByte();
        oOrderMain.OrderContent = this.txtOrderContent.Text;
        oOrderMain.CargoName = this.txtCargoName.Text;
        oOrderMain.BuyerOrderNo = this.txtBuyerOrderNo.Text;
        oOrderMain.Description = this.txtDescription.Text;
        oOrderMain.TotalPrice = this.txtTotalPrice.ValueDecimal;
        oOrderMain.TaxAmount = this.txtTaxAmount.ValueDecimal;
        oOrderMain.DiscountAmount = this.txtDiscountAmount.ValueDecimal;
        oOrderMain.FinalPrice = this.txtFinalPrice.ValueDecimal;
        oOrderMain.OrderStatusID = this.ddlOrderStatusID.SelectedValue.ToInt();
        oOrderMain.FileName = this.txtFileName.Text;
        oOrderMain.OrderDate = this.dtOrderDate.SelectedDate;
        oOrderMain.UpdateDate = this.dtUpdateDate.SelectedDate;
        bool bSuccess = Updating ? OrderMainManager.UpdateOrderMain(oOrderMain) : OrderMainManager.InsertOrderMain(oOrderMain);

        if (bSuccess)
            Redirect("/ordermain-list?s=1");
        else
            base.Warn("error.save");
    }
}