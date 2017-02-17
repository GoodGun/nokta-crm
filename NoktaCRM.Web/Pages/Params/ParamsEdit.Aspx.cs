using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using BusinessObjects;
using BusinessObjects.Common;
using Utility;

public partial class ParamsEdit : BasePage
{
    public int ParamsID { get { return base.QInt("ParamsID"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles(ParamsID > 0 ? "Edit" : "Add", "Params");
        if (!this.IsPostBack)
            this.InitData();
    }

    private void InitData()
    {
        if (ParamsID > 0)
        {
            Params oParams = ParamsManager.GetParamsByID(ParamsID);
            if (oParams != null)
            {

                this.txtTypeCode.Text = oParams.TypeCode;
                this.txtTypeName.Text = oParams.TypeName;
                this.txtDescription.Text = oParams.Description;
                this.txtObjectValue.Text = oParams.ObjectValue;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
		if (!Page.IsValid) return;
        bool Updating = false;
        Params oParams = null;

        if (ParamsID > 0)
        {
            oParams = ParamsManager.GetParamsByID(ParamsID);
            Updating = oParams != null;
        }
        if (!Updating)
        {
            oParams = new Params();

        }

        oParams.TypeCode = this.txtTypeCode.Text;
        oParams.TypeName = this.txtTypeName.Text;
        oParams.Description = this.txtDescription.Text;
        oParams.ObjectValue = this.txtObjectValue.Text;
        bool bSuccess = Updating ? ParamsManager.UpdateParams(oParams) : ParamsManager.InsertParams(oParams);

        if (bSuccess)
            Redirect("/params-list?s=1");
        else
            base.Warn("error.save");
    }
}