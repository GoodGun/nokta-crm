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

public partial class OrderDetailDetail : BasePage
{
    public int OrderDetailID { get { return base.QInt("OrderDetailID"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles("Detail", "OrderDetail");
        if (!this.IsPostBack)
            this.InitData();
    }
    
    private void InitData()
    {
        bool visible = false;
        if (OrderDetailID > 0)
        {
            OrderDetail oOrderDetail = OrderDetailManager.GetOrderDetailByID(OrderDetailID);
            if (oOrderDetail != null)
            {
                visible = true;
				this.ltrOrderDetailID.Text = oOrderDetail.OrderDetailID.ToSureString();
					this.ltrOrderID.Text = oOrderDetail.OrderID.ToSureString();
					this.ltrProductID.Text = oOrderDetail.ProductID.ToSureString();
					this.ltrQuantity.Text = oOrderDetail.Quantity.ToSureString();
					this.ltrTaxRate.Text = oOrderDetail.TaxRate.ToSureString();
					this.ltrUnitPrice.Text = oOrderDetail.UnitPrice.ToSureString();
					this.ltrDiscountAmount.Text = oOrderDetail.DiscountAmount.ToSureString();
					this.ltrTotalPrice.Text = oOrderDetail.TotalPrice.ToSureString();
					this.ltrFinalPrice.Text = oOrderDetail.FinalPrice.ToSureString();
					this.ltrCurrencyID.Text = oOrderDetail.CurrencyID.ToSureString();
	
            }
        }
        tblData.Visible = visible;
        if (!visible)
            Warn(ResourceManager.GetResource("form.nodata"));
    }
}