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

public partial class ContactDetail : BasePage
{
    public int ContactID { get { return base.QInt("ContactID"); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles("Detail", "Contact");
        if (!this.IsPostBack)
            this.InitData();
    }
    
    private void InitData()
    {
        bool visible = false;
        if (ContactID > 0)
        {
            Contact oContact = ContactManager.GetContactByID(ContactID);
            if (oContact != null)
            {
                visible = true;
				this.ltrContactID.Text = oContact.ContactID.ToSureString();
					this.ltrContactCode.Text = oContact.ContactCode;
					this.ltrMemberID.Text = oContact.MemberID.ToSureString();
					this.ltrName.Text = oContact.Name;
					this.ltrSurname.Text = oContact.Surname;
					this.ltrTitle.Text = oContact.Title;
					this.ltrPhone.Text = oContact.Phone;
					this.ltrGSM.Text = oContact.GSM;
					this.ltrCustomerID.Text = oContact.CustomerID.ToSureString();
					this.ltrAddressID.Text = oContact.AddressID.ToSureString();
					this.ltrGender.Text = oContact.Gender.ToSureString();
					this.ltrEmail.Text = oContact.Email;
					this.ltrDescription.Text = oContact.Description;
					this.ltrCreateDate.Text = base.ShowDate(oContact.CreateDate);
				this.ltrUpdateDate.Text = base.ShowDate(oContact.UpdateDate);

            }
        }
        tblData.Visible = visible;
        if (!visible)
            Warn(ResourceManager.GetResource("form.nodata"));
    }
}