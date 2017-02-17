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

public partial class ContentDetail : BasePage
{
    public int ContentID { get { return base.QInt("ContentID"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles("Detail", "Content");
        if (!this.IsPostBack)
            this.InitData();
    }
    
    private void InitData()
    {
        bool visible = false;
        if (ContentID > 0)
        {
            BusinessObjects.Content oContent = ContentManager.GetContentByID(ContentID);
            if (oContent != null)
            {
                visible = true;
				this.ltrContentID.Text = oContent.ContentID.ToSureString();
					this.ltrTitle.Text = oContent.Title;
					this.ltrBody.Text = oContent.Body;
					this.ltrMemberID.Text = oContent.MemberID.ToSureString();
					this.ltrCreateDate.Text = base.ShowDate(oContent.CreateDate);

            }
        }
        tblData.Visible = visible;
        if (!visible)
            Warn(ResourceManager.GetResource("form.nodata"));
    }
}