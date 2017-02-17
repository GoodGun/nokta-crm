using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using BusinessObjects.Common;
using Utility;

public partial class Stuff_Login : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string aa = StringExtensions.Encrypt("123451");
        this.SetTitle("title.login");

        if (!this.IsPostBack)
        {
            if (Member.Current != null && Member.LoggedIn) { Redirect(GotoURL ?? "/"); return; }
            txtEmail.Focus();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;
        var filter = new Dictionary<string, object>();
        filter.Add("Email", txtEmail.Text);
        filter.Add("PasswordHashed", txtPassword.Text.Encrypt());
        var members = MemberManager.GetMembersByFilter(filter);
        if (members.Count == 0)
        {
            Warn("err.bad.login", 10);
            return;
        }
        Member m = members[0];
        if (m.Status == false)
        {
            Warn("err.admin.disabled", 10);
            return;
        }
        Member.SetMember(m.MemberID, m.Name);
        //m.LastIP = Util.CurrentUserIP;
        //m.LastLogin = DateTime.Now;
        //AdminManager.UpdateAdmin(m);

        Redirect(GotoURL ?? "/");
    }
}