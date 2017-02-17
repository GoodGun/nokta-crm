using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using BusinessObjects;
using BusinessObjects.Common;
using Utility;

public partial class ActivityEdit : BasePage
{
    public int ActivityID { get { return base.QInt("ActivityID"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles(ActivityID > 0 ? "Edit" : "Add", "Activity");
        if (!this.IsPostBack)
            this.InitData();
    }

    private void InitData()
    {
        if (ActivityID > 0)
        {
            Activity oActivity = ActivityManager.GetActivityByID(ActivityID);
            if (oActivity != null)
            {

                this.ddlOrderID.Select(oActivity.OrderID);
                this.txtActivityCode.Text = oActivity.ActivityCode;
                this.txtName.Text = oActivity.Name;
                this.txtDescription.Text = oActivity.Description;
                this.ddlMemberID.Select(oActivity.MemberID);
                this.ddlCustomerID.Select(oActivity.CustomerID);
                this.ddlContactID.Select(oActivity.ContactID);
                this.ddlActivityTypeID.Select(oActivity.ActivityTypeID);
                this.ddlActivityStatusID.Select(oActivity.ActivityStatusID);
                this.txtCreatedBy.ValueInt = oActivity.CreatedBy;
                this.dtActivityDate.SelectedDate = oActivity.ActivityDate;
                this.dtUpdateDate.SelectedDate = oActivity.UpdateDate;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
		if (!Page.IsValid) return;
        bool Updating = false;
        Activity oActivity = null;

        if (ActivityID > 0)
        {
            oActivity = ActivityManager.GetActivityByID(ActivityID);
            Updating = oActivity != null;
        }
        if (!Updating)
        {
            oActivity = new Activity();
oActivity.CreateDate = DateTime.Now;
        }

        oActivity.OrderID = this.ddlOrderID.SelectedValue.ToInt();
        oActivity.ActivityCode = this.txtActivityCode.Text;
        oActivity.Name = this.txtName.Text;
        oActivity.Description = this.txtDescription.Text;
        oActivity.MemberID = this.ddlMemberID.SelectedValue.ToInt();
        oActivity.CustomerID = this.ddlCustomerID.SelectedValue.ToInt();
        oActivity.ContactID = this.ddlContactID.SelectedValue.ToInt();
        oActivity.ActivityTypeID = this.ddlActivityTypeID.SelectedValue.ToInt();
        oActivity.ActivityStatusID = this.ddlActivityStatusID.SelectedValue.ToInt();
        oActivity.CreatedBy = this.txtCreatedBy.ValueInt;
        oActivity.ActivityDate = this.dtActivityDate.SelectedDate;
        oActivity.UpdateDate = this.dtUpdateDate.SelectedDate;
        bool bSuccess = Updating ? ActivityManager.UpdateActivity(oActivity) : ActivityManager.InsertActivity(oActivity);

        if (bSuccess)
            Redirect("/activity-list?s=1");
        else
            base.Warn("error.save");
    }
}