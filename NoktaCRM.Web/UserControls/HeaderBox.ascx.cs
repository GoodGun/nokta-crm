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

public partial class UserControls_HeaderBox : BaseUserControl
{
    protected string AdminName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Visible = QInt("Popup") == 0;
        bool loggedIn = Member.LoggedIn;
        AdminName = Member.Current.FullName;
    }
}