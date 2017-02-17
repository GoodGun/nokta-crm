using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using BusinessObjects;
using BusinessObjects.Common;
using Utility;

public partial class ContactEdit : BasePage
{
    public int ContactID { get { return base.QInt("ContactID"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles(ContactID > 0 ? "Edit" : "Add", "Contact");
        if (!this.IsPostBack)
            this.InitData();
    }

    private void InitData()
    {
        if (ContactID > 0)
        {
            Contact oContact = ContactManager.GetContactByID(ContactID);
            if (oContact != null)
            {

                this.txtContactCode.Text = oContact.ContactCode;
                this.ddlMemberID.Select(oContact.MemberID);
                this.txtName.Text = oContact.Name;
                this.txtSurname.Text = oContact.Surname;
                this.txtTitle.Text = oContact.Title;
                this.txtPhone.Text = oContact.Phone;
                this.txtGSM.Text = oContact.GSM;
                this.ddlCustomerID.Select(oContact.CustomerID);
                this.ddlAddressID.Select(oContact.AddressID);
                this.txtGender.ValueInt = oContact.Gender;
                this.txtEmail.Text = oContact.Email;
                this.txtDescription.Text = oContact.Description;
                this.dtUpdateDate.SelectedDate = oContact.UpdateDate;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
		if (!Page.IsValid) return;
        bool Updating = false;
        Contact oContact = null;

        if (ContactID > 0)
        {
            oContact = ContactManager.GetContactByID(ContactID);
            Updating = oContact != null;
        }
        if (!Updating)
        {
            oContact = new Contact();
oContact.CreateDate = DateTime.Now;
        }

        oContact.ContactCode = this.txtContactCode.Text;
        oContact.MemberID = this.ddlMemberID.SelectedValue.ToInt();
        oContact.Name = this.txtName.Text;
        oContact.Surname = this.txtSurname.Text;
        oContact.Title = this.txtTitle.Text;
        oContact.Phone = this.txtPhone.Text;
        oContact.GSM = this.txtGSM.Text;
        oContact.CustomerID = this.ddlCustomerID.SelectedValue.ToInt();
        oContact.AddressID = this.ddlAddressID.SelectedValue.ToInt();
        oContact.Gender = this.txtGender.ValueByte;
        oContact.Email = this.txtEmail.Text;
        oContact.Description = this.txtDescription.Text;
        oContact.UpdateDate = this.dtUpdateDate.SelectedDate;
        bool bSuccess = Updating ? ContactManager.UpdateContact(oContact) : ContactManager.InsertContact(oContact);

        if (bSuccess)
            Redirect("/contact-list?s=1");
        else
            base.Warn("error.save");
    }
}