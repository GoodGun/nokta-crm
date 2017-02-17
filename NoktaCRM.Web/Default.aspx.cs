using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Utility;
using BusinessObjects;

public partial class _Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (QInt("Logout") == 1)
        {
            Member.SetMember(0, null);
            Redirect("/login");
        }
        else
            Authorize();
    }
}