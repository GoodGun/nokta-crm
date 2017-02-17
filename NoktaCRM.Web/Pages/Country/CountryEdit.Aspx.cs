using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using BusinessObjects;
using BusinessObjects.Common;
using Utility;

public partial class CountryEdit : BasePage
{
    public int CountryID { get { return base.QInt("CountryID"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles(CountryID > 0 ? "Edit" : "Add", "Country");
        if (!this.IsPostBack)
            this.InitData();
    }

    private void InitData()
    {
        if (CountryID > 0)
        {
            Country oCountry = CountryManager.GetCountryByID(CountryID);
            if (oCountry != null)
            {

                this.txtCountryName.Text = oCountry.CountryName;
                this.chkStatus.Checked = oCountry.Status;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
		if (!Page.IsValid) return;
        bool Updating = false;
        Country oCountry = null;

        if (CountryID > 0)
        {
            oCountry = CountryManager.GetCountryByID(CountryID);
            Updating = oCountry != null;
        }
        if (!Updating)
        {
            oCountry = new Country();

        }

        oCountry.CountryName = this.txtCountryName.Text;
        oCountry.Status = this.chkStatus.Checked;
        bool bSuccess = Updating ? CountryManager.UpdateCountry(oCountry) : CountryManager.InsertCountry(oCountry);

        if (bSuccess)
            Redirect("/country-list?s=1");
        else
            base.Warn("error.save");
    }
}