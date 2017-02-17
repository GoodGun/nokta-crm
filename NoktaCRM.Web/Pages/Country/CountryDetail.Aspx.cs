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

public partial class CountryDetail : BasePage
{
    public int CountryID { get { return base.QInt("CountryID"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles("Detail", "Country");
        if (!this.IsPostBack)
            this.InitData();
    }
    
    private void InitData()
    {
        bool visible = false;
        if (CountryID > 0)
        {
            Country oCountry = CountryManager.GetCountryByID(CountryID);
            if (oCountry != null)
            {
                visible = true;
				this.ltrCountryID.Text = oCountry.CountryID.ToSureString();
					this.ltrCountryName.Text = oCountry.CountryName;
					this.ltrStatus.Text = base.ShowBool(oCountry.Status);
	
            }
        }
        tblData.Visible = visible;
        if (!visible)
            Warn(ResourceManager.GetResource("form.nodata"));
    }
}