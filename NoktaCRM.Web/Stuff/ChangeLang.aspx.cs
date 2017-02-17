using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using BusinessObjects;
using System.Configuration;

public partial class Stuff_ChangeLang : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string lang = QString("Lang", "TR").ToUpper();
        string langs = ConfigurationManager.AppSettings["AvailableLanguages"];
        char[] Seperators = { ',' };
        List<string> avail = new List<string>(langs.Split(Seperators));
        if (!avail.Contains(lang))
            lang = avail[0];

        Util.CurrentUserLang = lang;
        Response.Redirect(RefererURL ?? "/Default.aspx");
    }
}