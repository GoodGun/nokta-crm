using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Linq;
using Utility;
using BusinessObjects;

public class BaseMaster : System.Web.UI.MasterPage
{
    public bool IsSecure { get { return Request.Url.Scheme == "http"; } }
    public string vpath { get { return !IsSecure ? ConfigManager.Current.AdminVirtualPath : ConfigManager.Current.AdminVirtualPathS; } }
    public string ipath { get { return !IsSecure ? ConfigManager.Current.ImagesPath : ConfigManager.Current.ImagesPathS; } }

    public string CurrentPageName 
    { 
        get 
        {
            string[] folder = Request.Url.PathAndQuery.Split('/');
            return  StringExtensions.ReplaceTurkishChars(folder[folder.Length - 1].ToLower());
        } 
    }

    //public AdminPage CurrentAdminPage
    //{
    //    get
    //    {
    //        return Admin.CurrentAdminPageRights != null ? Admin.CurrentAdminPageRights.Where(t => CurrentPageName.Contains(StringExtensions.ReplaceTurkishChars(t.PageLink.ToLower()).Split('/').Last())).FirstOrDefault() : null;
    //    }
    //}

    public string ConvertToMoney(object Value)
    {
        return Value.ToInt().ToString("N0");
    }

    public string GetMenuIcon(int AdminPageID)
    {
        string cname = "";

        switch (AdminPageID)
        {
            case 1:
                cname = "ion-ios7-home";
                break;
            case 4:
                cname = "ion-ios7-film";
                break;
            case 5:
                cname = "ion-grid";
                break;
            case 6:
                cname = "ion-stats-bars";
                break;
            case 7:
                cname = "ion-ios7-paper";
                break;
            case 8:
                cname = "ion-ios7-people";
                break;
            case 9:
                cname = "ion-podium";
                break;
            case 10:
                cname = "ion-ios7-copy";
                break;
            case 11:
                cname = "ion-ios7-gear";
                break;
            case 26:
                cname = "ion-beer";
                break;
            case 45:
                cname = "ion-qr-scanner";
                break;
            case 69:
                cname = "ion-drag";
                break;
            case 72:
                cname = "ion-log-in";
                break;
            case 76:
                cname = "ion-outlet";
                break;
            default:
                break;
        }

        return cname;
    }
    
    /// <summary>
    /// Querystring'den değer döndürür.
    /// </summary>
    /// <param name="Key">Key</param>
    /// <param name="DefaultValue">Key yoksa ne dönsün?</param>
    /// <returns>Key varsa key değeri, yoksa default değer döner...</returns>
    public string QString(string Key, string DefaultValue)
    {
        string Value = Request.QueryString[Key];
        return string.IsNullOrEmpty(Value) ? DefaultValue : Value;
    }
    public int QInt(string Key)
    {
        return QInt(Key, 0);
    }
    public int QInt(string Key, int DefaultValue)
    {
        string Value = QString(Key, DefaultValue.ToString());
        int intValue = 0;
        try
        {
            intValue = !string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[Key]) ? Convert.ToInt32(HttpContext.Current.Request.QueryString[Key]) : DefaultValue;
        }
        catch
        {
            intValue = DefaultValue;
        }

        return intValue;
    }

    public void Authorize()
    {
        var hasAuth = false;

        if (Member.Current != null && Member.LoggedIn)
        {
            Member adm = Member.Current;
            if (adm == null)
                hasAuth = false;
            else
            {
                //hasAuth = adm.RoleID == (byte)UserRole.SuperAdmin || (CurrentAdminPage != null && CurrentAdminPage.Status == (byte)Status.Active);

                if (!hasAuth)
                    Response.Redirect(string.Format("{0}/Authorization.aspx?code={1}", vpath, "noauth"));
            }
        }
        else
        {
            var returnPage = Request.RawUrl;
            if (returnPage.IndexOf("Login.aspx") > -1)
                returnPage = "/Default.aspx";
            Response.Redirect(string.Format("{0}/Stuff/Login.aspx?ReturnURL={1}", vpath, Server.HtmlEncode(returnPage)));
        }
    }

    public BasePage CurrentPage { get { return (BasePage)Page; } }

    public BaseMaster()
    {
       
    }
}
