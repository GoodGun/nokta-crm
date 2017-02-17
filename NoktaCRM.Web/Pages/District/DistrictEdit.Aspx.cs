using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using BusinessObjects;
using BusinessObjects.Common;
using Utility;

public partial class DistrictEdit : BasePage
{
    public int DistrictID { get { return base.QInt("DistrictID"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles(DistrictID > 0 ? "Edit" : "Add", "District");
        if (!this.IsPostBack)
            this.InitData();
    }

    private void InitData()
    {
        if (DistrictID > 0)
        {
            District oDistrict = DistrictManager.GetDistrictByID(DistrictID);
            if (oDistrict != null)
            {

                this.ddlCityID.Select(oDistrict.CityID);
                this.txtDistrictName.Text = oDistrict.DistrictName;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
		if (!Page.IsValid) return;
        bool Updating = false;
        District oDistrict = null;

        if (DistrictID > 0)
        {
            oDistrict = DistrictManager.GetDistrictByID(DistrictID);
            Updating = oDistrict != null;
        }
        if (!Updating)
        {
            oDistrict = new District();

        }

        oDistrict.CityID = this.ddlCityID.SelectedValue.ToInt();
        oDistrict.DistrictName = this.txtDistrictName.Text;
        bool bSuccess = Updating ? DistrictManager.UpdateDistrict(oDistrict) : DistrictManager.InsertDistrict(oDistrict);

        if (bSuccess)
            Redirect("/district-list?s=1");
        else
            base.Warn("error.save");
    }
}