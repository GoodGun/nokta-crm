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

public partial class ActivityDetail : BasePage
{
    public int ActivityID { get { return base.QInt("ActivityID"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles("Detail", "Activity");
        if (!this.IsPostBack)
            this.InitData();
    }
    
    private void InitData()
    {
        bool visible = false;
        if (ActivityID > 0)
        {
            Activity oActivity = ActivityManager.GetActivityByID(ActivityID);
            if (oActivity != null)
            {
                visible = true;
				this.ltrActivityID.Text = oActivity.ActivityID.ToSureString();
					this.ltrOrderID.Text = oActivity.OrderID.ToSureString();
					this.ltrActivityCode.Text = oActivity.ActivityCode;
					this.ltrName.Text = oActivity.Name;
					this.ltrDescription.Text = oActivity.Description;
					this.ltrMemberID.Text = oActivity.MemberID.ToSureString();
					this.ltrCustomerID.Text = oActivity.CustomerID.ToSureString();
					this.ltrContactID.Text = oActivity.ContactID.ToSureString();
					this.ltrActivityTypeID.Text = oActivity.ActivityTypeID.ToSureString();
					this.ltrActivityStatusID.Text = oActivity.ActivityStatusID.ToSureString();
					this.ltrCreatedBy.Text = oActivity.CreatedBy.ToSureString();
					this.ltrActivityDate.Text = base.ShowDate(oActivity.ActivityDate);
				this.ltrCreateDate.Text = base.ShowDate(oActivity.CreateDate);
				this.ltrUpdateDate.Text = base.ShowDate(oActivity.UpdateDate);

            }
        }
        tblData.Visible = visible;
        if (!visible)
            Warn(ResourceManager.GetResource("form.nodata"));
    }
}