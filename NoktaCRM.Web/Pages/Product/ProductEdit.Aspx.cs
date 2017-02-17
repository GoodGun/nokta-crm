using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using BusinessObjects;
using BusinessObjects.Common;
using Utility;

public partial class ProductEdit : BasePage
{
    public int ProductID { get { return base.QInt("ProductID"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles(ProductID > 0 ? "Edit" : "Add", "Product");
        if (!this.IsPostBack)
            this.InitData();
    }

    private void InitData()
    {
        if (ProductID > 0)
        {
            Product oProduct = ProductManager.GetProductByID(ProductID);
            if (oProduct != null)
            {

                this.txtProductCode.Text = oProduct.ProductCode;
                this.txtReferenceNo.Text = oProduct.ReferenceNo;
                this.ddlMemberID.Select(oProduct.MemberID);
                this.txtProductName.Text = oProduct.ProductName;
                this.txtProductGroup.Text = oProduct.ProductGroup;
                this.txtProductType.Text = oProduct.ProductType;
                this.txtUnit.Text = oProduct.Unit;
                this.txtTax.ValueInt = oProduct.Tax;
                this.txtProducer.Text = oProduct.Producer;
                this.txtPrice1.ValueDecimal = oProduct.Price1;
                this.txtPrice2.ValueDecimal = oProduct.Price2;
                this.txtCurrencyPrice.ValueDecimal = oProduct.CurrencyPrice;
                this.txtDescription.Text = oProduct.Description;
                this.chkStatus.Checked = oProduct.Status;
                this.dtUpdateDate.SelectedDate = oProduct.UpdateDate;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
		if (!Page.IsValid) return;
        bool Updating = false;
        Product oProduct = null;

        if (ProductID > 0)
        {
            oProduct = ProductManager.GetProductByID(ProductID);
            Updating = oProduct != null;
        }
        if (!Updating)
        {
            oProduct = new Product();
oProduct.CreateDate = DateTime.Now;
        }

        oProduct.ProductCode = this.txtProductCode.Text;
        oProduct.ReferenceNo = this.txtReferenceNo.Text;
        oProduct.MemberID = this.ddlMemberID.SelectedValue.ToInt();
        oProduct.ProductName = this.txtProductName.Text;
        oProduct.ProductGroup = this.txtProductGroup.Text;
        oProduct.ProductType = this.txtProductType.Text;
        oProduct.Unit = this.txtUnit.Text;
        oProduct.Tax = this.txtTax.ValueInt;
        oProduct.Producer = this.txtProducer.Text;
        oProduct.Price1 = this.txtPrice1.ValueDecimal;
        oProduct.Price2 = this.txtPrice2.ValueDecimal;
        oProduct.CurrencyPrice = this.txtCurrencyPrice.ValueDecimal;
        oProduct.Description = this.txtDescription.Text;
        oProduct.Status = this.chkStatus.Checked;
        oProduct.UpdateDate = this.dtUpdateDate.SelectedDate;
        bool bSuccess = Updating ? ProductManager.UpdateProduct(oProduct) : ProductManager.InsertProduct(oProduct);

        if (bSuccess)
            Redirect("/product-list?s=1");
        else
            base.Warn("error.save");
    }
}