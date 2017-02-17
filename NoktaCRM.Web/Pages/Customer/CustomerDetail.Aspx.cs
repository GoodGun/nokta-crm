using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BusinessObjects;
using Utility;

public partial class CustomerDetail : BasePage
{
    public int CustomerID { get { return base.QInt("CustomerID"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles("Detail", "Customer");
        if (!this.IsPostBack)
            this.InitData();
    }
    
    private void InitData()
    {
        bool visible = false;
        if (CustomerID > 0)
        {
            Customer oCustomer = CustomerManager.GetCustomerByID(CustomerID);
            if (oCustomer != null)
            {
                visible = true;
				this.ltrCustomerID.Text = oCustomer.CustomerID.ToSureString();
					this.ltrCustomerCode.Text = oCustomer.CustomerCode;
					this.ltrReferenceNo.Text = oCustomer.ReferenceNo;
					this.ltrMemberID.Text = oCustomer.MemberID.ToSureString();
					this.ltrSellerMemberID.Text = oCustomer.SellerMemberID.ToSureString();
					this.ltrName.Text = oCustomer.Name;
					this.ltrCustomerTypeID.Text = oCustomer.CustomerTypeID.ToSureString();
					this.ltrSectorTypeID.Text = oCustomer.SectorTypeID.ToSureString();
					this.ltrMainReseller.Text = oCustomer.MainReseller;
					this.ltrReseller.Text = oCustomer.Reseller;
					this.ltrDescription.Text = oCustomer.Description;
					this.ltrCode1.Text = oCustomer.Code1;
					this.ltrCode2.Text = oCustomer.Code2;
					this.ltrCode3.Text = oCustomer.Code3;
					this.ltrBillingAddressID.Text = oCustomer.BillingAddressID.ToSureString();
					this.ltrDeliveryAddressID.Text = oCustomer.DeliveryAddressID.ToSureString();
					this.ltrCreateDate.Text = base.ShowDate(oCustomer.CreateDate);
				this.ltrUpdateDate.Text = base.ShowDate(oCustomer.UpdateDate);

            }
        }
        tblData.Visible = visible;
        if (!visible)
            Warn(ResourceManager.GetResource("form.nodata"));
    }
}