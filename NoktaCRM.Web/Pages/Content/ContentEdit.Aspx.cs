using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using BusinessObjects;
using BusinessObjects.Common;
using Utility;

public partial class ContentEdit : BasePage
{
    public int ContentID { get { return base.QInt("ContentID"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles(ContentID > 0 ? "Edit" : "Add", "Content");
        if (!this.IsPostBack)
            this.InitData();
    }

    private void InitData()
    {
        if (ContentID > 0)
        {
            Content oContent = ContentManager.GetContentByID(ContentID);
            if (oContent != null)
            {

                this.txtTitle.Text = oContent.Title;
                this.txtBody.Text = oContent.Body;
                this.ddlMemberID.Select(oContent.MemberID);
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
		if (!Page.IsValid) return;
        bool Updating = false;
        Content oContent = null;

        if (ContentID > 0)
        {
            oContent = ContentManager.GetContentByID(ContentID);
            Updating = oContent != null;
        }
        if (!Updating)
        {
            oContent = new Content();
oContent.CreateDate = DateTime.Now;
        }

        oContent.Title = this.txtTitle.Text;
        oContent.Body = this.txtBody.Text;
        oContent.MemberID = this.ddlMemberID.SelectedValue.ToInt();
        bool bSuccess = Updating ? ContentManager.UpdateContent(oContent) : ContentManager.InsertContent(oContent);

        if (bSuccess)
            Redirect("/content-list?s=1");
        else
            base.Warn("error.save");
    }
}