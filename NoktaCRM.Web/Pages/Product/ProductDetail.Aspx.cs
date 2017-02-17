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

public partial class ProductDetail : BasePage
{
    public int ProductID { get { return base.QInt("ProductID"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles("Detail", "Product");
        if (!this.IsPostBack)
            this.InitData();
    }
    
    private void InitData()
    {
        bool visible = false;
        if (ProductID > 0)
        {
            Product oProduct = ProductManager.GetProductByID(ProductID);
            if (oProduct != null)
            {
                visible = true;
				this.ltrProductID.Text = oProduct.ProductID.ToSureString();
					this.ltrProductCode.Text = oProduct.ProductCode;
					this.ltrReferenceNo.Text = oProduct.ReferenceNo;
					this.ltrMemberID.Text = oProduct.MemberID.ToSureString();
					this.ltrProductName.Text = oProduct.ProductName;
					this.ltrProductGroup.Text = oProduct.ProductGroup;
					this.ltrProductType.Text = oProduct.ProductType;
					this.ltrUnit.Text = oProduct.Unit;
					this.ltrTax.Text = oProduct.Tax.ToSureString();
					this.ltrProducer.Text = oProduct.Producer;
					this.ltrPrice1.Text = oProduct.Price1.ToSureString();
					this.ltrPrice2.Text = oProduct.Price2.ToSureString();
					this.ltrCurrencyPrice.Text = oProduct.CurrencyPrice.ToSureString();
					this.ltrDescription.Text = oProduct.Description;
					this.ltrStatus.Text = base.ShowBool(oProduct.Status);
					this.ltrCreateDate.Text = base.ShowDate(oProduct.CreateDate);
				this.ltrUpdateDate.Text = base.ShowDate(oProduct.UpdateDate);

            }
        }
        tblData.Visible = visible;
        if (!visible)
            Warn(ResourceManager.GetResource("form.nodata"));
    }
}