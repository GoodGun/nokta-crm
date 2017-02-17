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

public partial class OrderMainDetail : BasePage
{
    public int OrderMainID { get { return base.QInt("OrderMainID"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles("Detail", "OrderMain");
        if (!this.IsPostBack)
            this.InitData();
    }
    
    private void InitData()
    {
        bool visible = false;
        if (OrderMainID > 0)
        {
            OrderMain oOrderMain = OrderMainManager.GetOrderMainByID(OrderMainID);
            if (oOrderMain != null)
            {
                visible = true;
				this.ltrOrderID.Text = oOrderMain.OrderID.ToSureString();
					this.ltrOrderCode.Text = oOrderMain.OrderCode;
					this.ltrMemberID.Text = oOrderMain.MemberID.ToSureString();
					this.ltrCustomerID.Text = oOrderMain.CustomerID.ToSureString();
					this.ltrContactID.Text = oOrderMain.ContactID.ToSureString();
					this.ltrAddressID.Text = oOrderMain.AddressID.ToSureString();
					this.ltrReferenceNo.Text = oOrderMain.ReferenceNo;
					this.ltrOrderTypeID.Text = oOrderMain.OrderTypeID.ToSureString();
					this.ltrOrderContent.Text = oOrderMain.OrderContent;
					this.ltrCargoName.Text = oOrderMain.CargoName;
					this.ltrBuyerOrderNo.Text = oOrderMain.BuyerOrderNo;
					this.ltrDescription.Text = oOrderMain.Description;
					this.ltrTotalPrice.Text = oOrderMain.TotalPrice.ToSureString();
					this.ltrTaxAmount.Text = oOrderMain.TaxAmount.ToSureString();
					this.ltrDiscountAmount.Text = oOrderMain.DiscountAmount.ToSureString();
					this.ltrFinalPrice.Text = oOrderMain.FinalPrice.ToSureString();
					this.ltrOrderStatusID.Text = oOrderMain.OrderStatusID.ToSureString();
					this.ltrFileName.Text = oOrderMain.FileName;
					this.ltrOrderDate.Text = base.ShowDate(oOrderMain.OrderDate);
				this.ltrCreateDate.Text = base.ShowDate(oOrderMain.CreateDate);
				this.ltrUpdateDate.Text = base.ShowDate(oOrderMain.UpdateDate);

            }
        }
        tblData.Visible = visible;
        if (!visible)
            Warn(ResourceManager.GetResource("form.nodata"));
    }
}