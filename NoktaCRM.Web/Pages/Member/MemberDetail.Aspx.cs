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

public partial class MemberDetail : BasePage
{
    public int MemberID { get { return base.QInt("MemberID"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles("Detail", "Member");
        if (!this.IsPostBack)
            this.InitData();
    }
    
    private void InitData()
    {
        bool visible = false;
        if (MemberID > 0)
        {
            Member oMember = MemberManager.GetMemberByID(MemberID);
            if (oMember != null)
            {
                visible = true;
				this.ltrMemberID.Text = oMember.MemberID.ToSureString();
					this.ltrName.Text = oMember.Name;
					this.ltrSurname.Text = oMember.Surname;
					this.ltrUsername.Text = oMember.Username;
					this.ltrEmail.Text = oMember.Email;
					this.ltrPasswordHashed.Text = oMember.PasswordHashed;
					this.ltrMemberTypeID.Text = oMember.MemberTypeID.ToSureString();
					this.ltrStatus.Text = base.ShowBool(oMember.Status);
					this.ltrCreateDate.Text = base.ShowDate(oMember.CreateDate);
				this.ltrLastUpdate.Text = base.ShowDate(oMember.LastUpdate);

            }
        }
        tblData.Visible = visible;
        if (!visible)
            Warn(ResourceManager.GetResource("form.nodata"));
    }
}