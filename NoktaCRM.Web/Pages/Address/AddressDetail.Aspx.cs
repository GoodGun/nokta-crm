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

public partial class AddressDetail : BasePage
{
    public int AddressID { get { return base.QInt("AddressID"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles("Detail", "Address");
        if (!this.IsPostBack)
            this.InitData();
    }
    
    private void InitData()
    {
        bool visible = false;
        if (AddressID > 0)
        {
            Address oAddress = AddressManager.GetAddressByID(AddressID);
            if (oAddress != null)
            {
                visible = true;
				this.ltrAddressID.Text = oAddress.AddressID.ToSureString();
					this.ltrCustomerID.Text = oAddress.CustomerID.ToSureString();
					this.ltrAddressName.Text = oAddress.AddressName;
					this.ltrIsBillingAddress.Text = base.ShowBool(oAddress.IsBillingAddress);
					this.ltrIsUsed.Text = base.ShowBool(oAddress.IsUsed);
					this.ltrName.Text = oAddress.Name;
					this.ltrAddressLine.Text = oAddress.AddressLine;
					this.ltrCityID.Text = oAddress.CityID.ToSureString();
					this.ltrDistrictID.Text = oAddress.DistrictID.ToSureString();
					this.ltrAreaID.Text = oAddress.AreaID.ToSureString();
					this.ltrTaxOffice.Text = oAddress.TaxOffice;
					this.ltrTaxNo.Text = oAddress.TaxNo;
					this.ltrZipCode.Text = oAddress.ZipCode;
					this.ltrPhone.Text = oAddress.Phone;
					this.ltrCreateDate.Text = base.ShowDate(oAddress.CreateDate);

            }
        }
        tblData.Visible = visible;
        if (!visible)
            Warn(ResourceManager.GetResource("form.nodata"));
    }
}