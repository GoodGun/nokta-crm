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

public partial class CityDetail : BasePage
{
    public int CityID { get { return base.QInt("CityID"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles("Detail", "City");
        if (!this.IsPostBack)
            this.InitData();
    }
    
    private void InitData()
    {
        bool visible = false;
        if (CityID > 0)
        {
            City oCity = CityManager.GetCityByID(CityID);
            if (oCity != null)
            {
                visible = true;
				this.ltrCityID.Text = oCity.CityID.ToSureString();
					this.ltrCountryID.Text = oCity.CountryID.ToSureString();
					this.ltrCityName.Text = oCity.CityName;
					this.ltrStatus.Text = base.ShowBool(oCity.Status);
	
            }
        }
        tblData.Visible = visible;
        if (!visible)
            Warn(ResourceManager.GetResource("form.nodata"));
    }
}