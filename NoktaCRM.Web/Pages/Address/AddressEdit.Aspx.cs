using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using BusinessObjects;
using BusinessObjects.Common;
using Utility;

public partial class AddressEdit : BasePage
{
    public int AddressID { get { return base.QInt("AddressID"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles(AddressID > 0 ? "Edit" : "Add", "Address");
        if (!this.IsPostBack)
            this.InitData();
    }

    private void InitData()
    {
        if (AddressID > 0)
        {
            Address oAddress = AddressManager.GetAddressByID(AddressID);
            if (oAddress != null)
            {

                this.ddlCustomerID.Select(oAddress.CustomerID);
                this.txtAddressName.Text = oAddress.AddressName;
                this.chkIsBillingAddress.Checked = oAddress.IsBillingAddress;
                this.chkIsUsed.Checked = oAddress.IsUsed;
                this.txtName.Text = oAddress.Name;
                this.txtAddressLine.Text = oAddress.AddressLine;
                this.ddlCityID.Select(oAddress.CityID);
                this.ddlDistrictID.Select(oAddress.DistrictID);
                this.ddlAreaID.Select(oAddress.AreaID);
                this.txtTaxOffice.Text = oAddress.TaxOffice;
                this.txtTaxNo.Text = oAddress.TaxNo;
                this.txtZipCode.Text = oAddress.ZipCode;
                this.txtPhone.Text = oAddress.Phone;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
		if (!Page.IsValid) return;
        bool Updating = false;
        Address oAddress = null;

        if (AddressID > 0)
        {
            oAddress = AddressManager.GetAddressByID(AddressID);
            Updating = oAddress != null;
        }
        if (!Updating)
        {
            oAddress = new Address();
oAddress.CreateDate = DateTime.Now;
        }

        oAddress.CustomerID = this.ddlCustomerID.SelectedValue.ToInt();
        oAddress.AddressName = this.txtAddressName.Text;
        oAddress.IsBillingAddress = this.chkIsBillingAddress.Checked;
        oAddress.IsUsed = this.chkIsUsed.Checked;
        oAddress.Name = this.txtName.Text;
        oAddress.AddressLine = this.txtAddressLine.Text;
        oAddress.CityID = this.ddlCityID.SelectedValue.ToInt();
        oAddress.DistrictID = this.ddlDistrictID.SelectedValue.ToInt();
        oAddress.AreaID = this.ddlAreaID.SelectedValue.ToInt();
        oAddress.TaxOffice = this.txtTaxOffice.Text;
        oAddress.TaxNo = this.txtTaxNo.Text;
        oAddress.ZipCode = this.txtZipCode.Text;
        oAddress.Phone = this.txtPhone.Text;
        bool bSuccess = Updating ? AddressManager.UpdateAddress(oAddress) : AddressManager.InsertAddress(oAddress);

        if (bSuccess)
            Redirect("/address-list?s=1");
        else
            base.Warn("error.save");
    }
}