using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using Utility;
using System.Text;

public partial class UserControls_SidebarMenu: BaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Visible = QInt("Popup") == 0;
        bool loggedIn = Member.LoggedIn;

        //var filter = Populator.GetFilter();
        //filter.Add("ParentPageID", 0);
        //filter.Add("Status", 1);
        //var ParentPages = MemberManager.GetMembersByFilter(filter);

        //StringBuilder strMenu = new StringBuilder();
        //foreach(AdminPage oParent in ParentPages.OrderBy(t => t.PageOrder))
        //{
        //    filter = Populator.GetFilter();
        //    filter.Add("ParentPageID", oParent.AdminPageID);
        //    filter.Add("Status", 1);
        //    var subPages = AdminPageManager.GetAdminPagesByFilter(filter);

        //    StringBuilder strSubMenu = new StringBuilder();
        //    foreach(AdminPage oSubPage in subPages.OrderBy(t => t.PageOrder))
        //        strSubMenu.AppendFormat("<li><a href=\"{0}\"><i class=\"icon-folder\"></i>{1}</a></li>", oSubPage.PageLink, oSubPage.PageName);

        //    strMenu.AppendFormat("<li><a href=\"{0}\"><i class=\"icon-docs\"></i><span class=\"title\">{1}</span><span class=\"arrow\"></span</a><ul class=\"sub-menu\">{2}</ul></li>", oParent.PageLink, oParent.PageName, strSubMenu.ToString());
        //}

        //ltrMenu.Text = strMenu.ToString();
    }
}