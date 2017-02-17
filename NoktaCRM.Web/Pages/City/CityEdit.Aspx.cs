using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using BusinessObjects;
using BusinessObjects.Common;
using Utility;

public partial class CityEdit : BasePage
{
    public int CityID { get { return base.QInt("CityID"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles(CityID > 0 ? "Edit" : "Add", "City");
        if (!this.IsPostBack)
            this.InitData();
    }

    private void InitData()
    {
        if (CityID > 0)
        {
            City oCity = CityManager.GetCityByID(CityID);
            if (oCity != null)
            {

                this.ddlCountryID.Select(oCity.CountryID);
                this.txtCityName.Text = oCity.CityName;
                this.chkStatus.Checked = oCity.Status;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
		if (!Page.IsValid) return;
        bool Updating = false;
        City oCity = null;

        if (CityID > 0)
        {
            oCity = CityManager.GetCityByID(CityID);
            Updating = oCity != null;
        }
        if (!Updating)
        {
            oCity = new City();

        }

        oCity.CountryID = this.ddlCountryID.SelectedValue.ToInt();
        oCity.CityName = this.txtCityName.Text;
        oCity.Status = this.chkStatus.Checked;
        bool bSuccess = Updating ? CityManager.UpdateCity(oCity) : CityManager.InsertCity(oCity);

        if (bSuccess)
            Redirect("/city-list?s=1");
        else
            base.Warn("error.save");
    }
}