using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using BusinessObjects;
using BusinessObjects.Common;
using Utility;

public partial class CustomerEdit : BasePage
{
    public int CustomerID { get { return base.QInt("CustomerID"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles(CustomerID > 0 ? "Edit" : "Add", "Customer");
        ltrTitle.Text = CustomerID > 0 ? "Müşteri Düzenle" : "Yeni Müşteri";
        if (!this.IsPostBack)
            this.InitData();
    }

    private void InitData()
    {
        if (CustomerID > 0)
        {
            Customer oCustomer = CustomerManager.GetCustomerByID(CustomerID);
            if (oCustomer != null)
            {

                this.txtCustomerCode.Text = oCustomer.CustomerCode;
                this.txtReferenceNo.Text = oCustomer.ReferenceNo;
                this.ddlMemberID.Select(oCustomer.MemberID);
                this.ddlSellerMemberID.Select(oCustomer.SellerMemberID);
                this.txtName.Text = oCustomer.Name;
                this.ddlCustomerTypeID.Select(oCustomer.CustomerTypeID);
                this.ddlSectorTypeID.Select(oCustomer.SectorTypeID);
                this.txtMainReseller.Text = oCustomer.MainReseller;
                this.txtReseller.Text = oCustomer.Reseller;
                this.txtDescription.Text = oCustomer.Description;
                this.txtCode1.Text = oCustomer.Code1;
                this.txtCode2.Text = oCustomer.Code2;
                this.txtCode3.Text = oCustomer.Code3;
                this.ddlBillingAddressID.Select(oCustomer.BillingAddressID);
                this.ddlDeliveryAddressID.Select(oCustomer.DeliveryAddressID);
                this.dtUpdateDate.SelectedDate = oCustomer.UpdateDate;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
		if (!Page.IsValid) return;
        bool Updating = false;
        Customer oCustomer = null;

        if (CustomerID > 0)
        {
            oCustomer = CustomerManager.GetCustomerByID(CustomerID);
            Updating = oCustomer != null;
        }
        if (!Updating)
        {
            oCustomer = new Customer();
oCustomer.CreateDate = DateTime.Now;
        }

        oCustomer.CustomerCode = this.txtCustomerCode.Text;
        oCustomer.ReferenceNo = this.txtReferenceNo.Text;
        oCustomer.MemberID = this.ddlMemberID.SelectedValue.ToInt();
        oCustomer.SellerMemberID = this.ddlSellerMemberID.SelectedValue.ToInt();
        oCustomer.Name = this.txtName.Text;
        oCustomer.CustomerTypeID = this.ddlCustomerTypeID.SelectedValue.ToByte();
        oCustomer.SectorTypeID = this.ddlSectorTypeID.SelectedValue.ToByte();
        oCustomer.MainReseller = this.txtMainReseller.Text;
        oCustomer.Reseller = this.txtReseller.Text;
        oCustomer.Description = this.txtDescription.Text;
        oCustomer.Code1 = this.txtCode1.Text;
        oCustomer.Code2 = this.txtCode2.Text;
        oCustomer.Code3 = this.txtCode3.Text;
        oCustomer.BillingAddressID = this.ddlBillingAddressID.SelectedValue.ToInt();
        oCustomer.DeliveryAddressID = this.ddlDeliveryAddressID.SelectedValue.ToInt();
        oCustomer.UpdateDate = this.dtUpdateDate.SelectedDate;
        bool bSuccess = Updating ? CustomerManager.UpdateCustomer(oCustomer) : CustomerManager.InsertCustomer(oCustomer);

        if (bSuccess)
            Redirect("/customer-list?s=1");
        else
            base.Warn("error.save");
    }
}