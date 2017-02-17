using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using BusinessObjects;
using BusinessObjects.Common;
using Utility;

public partial class MemberEdit : BasePage
{
    public int MemberID { get { return base.QInt("MemberID"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles(MemberID > 0 ? "Edit" : "Add", "Member");
        ltrTitle.Text = MemberID > 0 ? "Üye Düzenle" : "Yeni Üye";
        if (!this.IsPostBack)
            this.InitData();
    }

    private void InitData()
    {
        var Filters = Populator.GetFilter();
        Filters.Add("TypeCode", "MemberTypeID");
        var arrType = ParamsManager.GetParamssByFilter(Filters);
        ddlMemberTypeID.BindData(arrType, "TypeName", "ObjectValue");

        if (MemberID > 0)
        {
            Member oMember = MemberManager.GetMemberByID(MemberID);
            if (oMember != null)
            {
                this.txtName.Text = oMember.Name;
                this.txtSurname.Text = oMember.Surname;
                this.txtEmail.Text = oMember.Email;
                this.txtPasswordHashed.Text = oMember.PasswordHashed.Decrypt();
                this.ddlMemberTypeID.Select(oMember.MemberTypeID);
                this.chkStatus.Checked = oMember.Status;
                //this.dtLastUpdate.SelectedDate = oMember.LastUpdate;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
		if (!Page.IsValid) return;
        bool Updating = false;
        Member oMember = null;

        if (MemberID > 0)
        {
            oMember = MemberManager.GetMemberByID(MemberID);
            Updating = oMember != null;
        }
        if (!Updating)
        {
            oMember = new Member();
            oMember.CreateDate = DateTime.Now;
        }

        oMember.Name = this.txtName.Text;
        oMember.Surname = this.txtSurname.Text;
        oMember.Username = oMember.Email = this.txtEmail.Text;
        oMember.PasswordHashed = this.txtPasswordHashed.Text.Encrypt();
        oMember.MemberTypeID = this.ddlMemberTypeID.SelectedValue.ToByte();
        oMember.Status = this.chkStatus.Checked;
        //oMember.LastUpdate = this.dtLastUpdate.SelectedDate;
        bool bSuccess = Updating ? MemberManager.UpdateMember(oMember) : MemberManager.InsertMember(oMember);

        if (bSuccess)
            Redirect("/member-list?s=1");
        else
            base.Warn("error.save");
    }
}