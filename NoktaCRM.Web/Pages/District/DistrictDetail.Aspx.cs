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

public partial class DistrictDetail : BasePage
{
    public int DistrictID { get { return base.QInt("DistrictID"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles("Detail", "District");
        if (!this.IsPostBack)
            this.InitData();
    }
    
    private void InitData()
    {
        bool visible = false;
        if (DistrictID > 0)
        {
            District oDistrict = DistrictManager.GetDistrictByID(DistrictID);
            if (oDistrict != null)
            {
                visible = true;
				this.ltrDistrictID.Text = oDistrict.DistrictID.ToSureString();
					this.ltrCityID.Text = oDistrict.CityID.ToSureString();
					this.ltrDistrictName.Text = oDistrict.DistrictName;
	
            }
        }
        tblData.Visible = visible;
        if (!visible)
            Warn(ResourceManager.GetResource("form.nodata"));
    }
}