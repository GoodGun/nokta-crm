using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using Utility;

public partial class Stuff_Error : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.SetTitles("error.page.title", "", true);
        string key = QString("key", "general");
        switch (key)
        {
            case "auth-id":
            case "auth-type":
            case "general":
            case "404":
                break;
            default:
                key = "general";
                break;
        }
        key = string.Concat("error.", key);
        ltrMessage.Text = ResourceManager.GetResource(key);
        Exception ex = Application["last.ex"] as Exception;
        if (ex == null) return;
        ltrMessage.Text = Util.GetExceptionMessageRecursive(ex);
        Application.Remove("last.ex");
        //ltrMessage.Text = ResourceManager.GetResource("error.permission.1");
    }
}