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

public partial class ParamsDetail : BasePage
{
    public int ParamsID { get { return base.QInt("ParamsID"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles("Detail", "Params");
        if (!this.IsPostBack)
            this.InitData();
    }
    
    private void InitData()
    {
        bool visible = false;
        if (ParamsID > 0)
        {
            Params oParams = ParamsManager.GetParamsByID(ParamsID);
            if (oParams != null)
            {
                visible = true;
				this.ltrTypeID.Text = oParams.TypeID.ToSureString();
					this.ltrTypeCode.Text = oParams.TypeCode;
					this.ltrTypeName.Text = oParams.TypeName;
					this.ltrDescription.Text = oParams.Description;
					this.ltrObjectValue.Text = oParams.ObjectValue;
	
            }
        }
        tblData.Visible = visible;
        if (!visible)
            Warn(ResourceManager.GetResource("form.nodata"));
    }
}