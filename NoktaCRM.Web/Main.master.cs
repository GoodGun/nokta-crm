using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using Utility;
using System.Reflection;
using System.Text;

public partial class MainMaster : BaseMaster
{
    public string Embed(string folder, string path)
    {
        if (!path.Contains(","))
            return string.Format("/{0}/{1}", folder, path);

        return string.Format("/merge-{0}?{1}", folder, path);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Header.DataBind();
    }
    public void SetBreadcrumb(params string[] data)
    {
        int count = data.Length / 2;
        bool hasData = data.Length > 0;
        //dvBread.Visible = hasData && thisPage.QInt("Popup") == 0;

        string result = string.Format("<li><a href='/'>{0}</a> <span class='divider'>»</span> </li>",
            ResourceManager.GetResource("home"));

        if (data.Length == 1)
            result += string.Format("<li class='active'>{0}</li>", ResourceManager.GetResource(data[0]));
        else
            for (int i = 0; i <= data.Length / 2; i++)
            {
                if (i < data.Length / 2)
                    result += string.Format("<li><a href='{0}'>{1}</a> <span class='divider'>»</span> </li>",
                        data[2 * i], ResourceManager.GetResource(data[2 * i + 1]));
                else
                    result += string.Format("<li class='active'>{0}</li>", ResourceManager.GetResource(data[2 * i]));
            }
        //ltrBreadcrumb.Text = result;
    }
    private char[] sep = { '.' };
    public BasePage thisPage { get { return this.Page as BasePage; } }
    public void btnRefresh_Click(object sender, EventArgs e)
    {
        MethodInfo minfo = this.Page.GetType().GetMethod("FillForm");
        if (minfo == null) minfo = this.Page.GetType().GetMethod("Search");
        if (minfo == null) Response.Redirect(Request.RawUrl);
        else minfo.Invoke(this.Page, null);
    }
    public void SetTitles(string titleType, string pageTitle = null, bool fromResource = true)
    {
        bool generalTitle = true;
        string res = "";

        switch (titleType)
        {
            case "List":
                break;
            case "Edit":
                break;
            case "Add":
            case "New":
                break;
            case "Detail":
                break;
            default:
                generalTitle = false;
                break;
        }
        if (generalTitle)
        {
            res = ResourceManager.GetResource(string.Concat("format.", titleType));
            res = string.Format(res, ResourceManager.GetResource(string.Concat("dbtable.", pageTitle)));
        }
        else
            res = fromResource ? ResourceManager.GetResource(titleType) : titleType;
        
        SetBreadcrumb(res);
        

        if (this.Page is BasePage)
            ((BasePage)this.Page).SetTitle(res);
    }
}
