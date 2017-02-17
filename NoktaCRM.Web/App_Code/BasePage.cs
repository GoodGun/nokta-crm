using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using BusinessObjects;
using BusinessObjects.Common;
using System.Data;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Web;
using System.IO.Compression;
using System.Linq;

public class BasePage : Page
{
    public void Authorize()
    {
        int adminID = Member.CurrentMemberID;
        if (adminID < 1 || Member.Current == null) Redirect("/login", true);

        //bool hasAuth = Member.Current.RoleID == (byte)UserRole.SuperAdmin || (CurrentAdminPage != null && CurrentAdminPage.Status == (byte)Status.Active);

        //if (!hasAuth)
        //    Response.Redirect(string.Format("{0}/Authorization.aspx?code={1}", ConfigManager.Current.AdminVirtualPath, "noauth"));
    }
    //public AdminPage CurrentAdminPage
    //{
    //    get
    //    {
    //        return Admin.CurrentAdminPageRights != null ? Admin.CurrentAdminPageRights.Where(t => CurrentPageName.Contains(StringExtensions.ReplaceTurkishChars(t.PageLink.ToLower()).Split('/').Last())).FirstOrDefault() : null;
    //    }
    //}
    public string CurrentPageName
    {
        get
        {
            string[] folder = Request.Url.PathAndQuery.Split('/');
            return StringExtensions.ReplaceTurkishChars(folder[folder.Length - 1].ToLower());
        }
    }
    public string GotoURL { get { return QString("goto", null); } }
    protected override void OnLoad(EventArgs e)
    {
        if (!this.IsPostBack && !string.IsNullOrEmpty(GotoURL))
            Warn("err.login.first", 0, MessageType.success);
        base.OnLoad(e);
    }
    public bool IsSuccess { get { return QInt("s") == 1; } }
    public int CurrentPageIndex { get { return QInt("Page", 0); } }
    public string ShowDate(object val, bool addTime = false)
    {
        if (val == null || val == DBNull.Value) return "--";
        DateTime dt = DateTime.MinValue;
        if (val.GetType() != typeof(byte))
        {
            dt = Convert.ToDateTime(val);
        }        
        return dt == DateTime.MinValue ? "--" : (addTime ? dt.ToString() : dt.ToShortDateString());
    }
    public string ShowBool(object val, bool important = false) { return ShowBool(val, "", "", true, important); }
    public string ShowBool(object val, string posText, string negText, bool isImg = false, bool important = false)
    {
        bool x = val.ToBool();
        if (isImg)
            return string.Format("<i class='fa {0}{1}'></i>", x ? "fa-check-circle-o" : "fa-minus-circle", !x && important ? " important" : "");
        else
            return x ? posText : negText;
    }
    public string ShowNum(object val, int decimals = 2, string suffix = null, string zeroValue="--")
    {
        decimal res = val.ToDecimal();

        if (res == 0) return zeroValue;
        string result = res.ToString(string.Concat("N", decimals));
        return suffix.NullOrEmpty() ? result : string.Concat(result, suffix);
    }
    public string Embed(string folder, string path)
    {
        if (!path.Contains(","))
            return string.Format("{0}/{1}/{2}", ConfigManager.Current.ImagesPath, folder, path);

        return string.Format("{0}/merge-{1}?{2}", ConfigManager.Current.ImagesPath, folder, path);
    }
    public string RefererURL { get { return Request.ServerVariables["HTTP_REFERER"]; } }
    public string CurrentURL
    {
        get
        {
            string url = string.Concat(Request.Url.Scheme, "://", Request.ServerVariables["HTTP_HOST"], Request.ServerVariables["PATH_INFO"]);
            string query = Request.ServerVariables["QUERY_STRING"];
            if (!string.IsNullOrEmpty(query))
                url += string.Concat("?", query);

            return url;
        }
    }
    public string CurrentURLEncoded { get { return CurrentURL.EncodeURL(); } }
    public string FriendlyURL { get { return string.Concat(Request.Url.Scheme, "://", Request.ServerVariables["HTTP_HOST"], Request.RawUrl); } }
    public string FriendlyURLEncoded { get { return FriendlyURL.EncodeURL(); } }
    public string res(string key) { return ResourceManager.GetResource(key); }
    public string res(bool flag) { return ResourceManager.GetResource(flag ? "yes" :"no"); }
    public string href(string key, string path) { return string.Format("<a href='{0}'>{1}</a>", path, ResourceManager.GetResource(key)); }
    public string ipath { get { return !IsSecure ? ConfigManager.Current.ImagesPath : ConfigManager.Current.ImagesPathS; } }
    public bool IsSecure { get { return Request.Url.Scheme == "https" || QString("SECURE") == "1"; } }
    public string PageTitle = ConfigManager.Current.DefaultPageTitle;
    public string ShortText(object val, int MaxLen)
    {
        if (val == null) return "";
        string res = val.ToString();
        return res.Length > MaxLen ? res.Left(MaxLen) + "..." : res;
    }

    public string img(object BasePath, string relDir = "", bool generateTag = false)
    {
        var src = string.Format("{0}/img/{1}{2}", ipath, relDir, BasePath);
        if (!generateTag) return src;

        return string.Format("<img src='{0}' />", src);
    }
    public string js(object BasePath)
    {
        return string.Format("{0}/js/{1}", ipath, BasePath);
    }
    public string css(object BasePath)
    {
        return string.Format("{0}/style/{1}", ipath, BasePath);
    }

    #region General Page Utilities
    /// <summary>
    /// Finds a control recursively, searching from Masterpage to Page.
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Root"></param>
    /// <returns></returns>
    public Control GetControlR(string Id, Control Root = null)
    {
        if (Root == null)
            if (this.Master != null) Root = this.Master;
            else Root = this.Page;
        if (Root.ID == Id) return Root;

        foreach (Control Ctl in Root.Controls)
        {
            Control FoundCtl = GetControlR(Id, Ctl);
            if (FoundCtl != null)
                return FoundCtl;
        }
        return null;
    }
    protected override void Render(HtmlTextWriter writer)
    {
        writer = new CustomHtmlWriter(ref writer, "   ", Request.RawUrl);
        base.Render(writer);
    }
    public void ActivateViewState(bool Enabled) { this.Master.EnableViewState = Enabled; }
    public void Redirect(string path = null, bool addReturnURL = false)
    {
        path = (path ?? "/login");
        if (addReturnURL) path += string.Concat("?goto=", FriendlyURLEncoded);

        Response.Redirect(path, true);
    }

    public void AddHeaderContent(IncludeType CType, string Content)
    {
        AddHeaderContent(CType, Content, !(!string.IsNullOrEmpty(Content) && Content.StartsWith("http")));
    }
    public void AddHeaderContent(IncludeType CType, string Content, bool IsRelative)
    {
        if (string.IsNullOrEmpty(Content)) return;
        Literal ltrHeader = new Literal();
        ltrHeader.ID = string.Format("ltr{0}", Content.GetHashCode().ToString("X"));
        string ltrText = null;

        switch (CType)
        {
            case IncludeType.Js:
                ltrText = string.Format("<script type='text/javascript' src='{0}'></script>", GetContentPath(Content, IsRelative));
                break;
            case IncludeType.Css:
                ltrText = string.Format("<link rel='stylesheet' href='{0}' type='text/css' />", GetContentPath(Content, IsRelative));
                break;
            case IncludeType.RawContent:
                ltrText = Content;
                break;
        }
        ltrHeader.Text = string.Concat(ltrText, Environment.NewLine);
        this.Header.Controls.Add(ltrHeader);
    }

    public string GetContentPath(string Content, bool IsRelative)
    {
        return IsRelative ? string.Format("/{0}", Content) : Content;
    }
    public void SetTitle(string metaTitle, bool setInnerTitle = true, bool fromResource = true)
    {
        PageTitle = fromResource ? ResourceManager.GetResource(metaTitle) : metaTitle;
    }
    
    #endregion

    #region Querystring and Paging section

    /// <summary>
    /// Querystring'den değer döndürür
    /// </summary>
    /// <param name="Key">Key</param>
    /// <returns>Değer bulunduysa değer, bulunamadıysa boş string döndürür</returns>
    public string QString(string Key)
    {
        return QString(Key, "");
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
        if (string.IsNullOrEmpty(Value)) return DefaultValue;
        
        char [] err = {'\'', '"'};

        return Value.IndexOfAny(err) < 0 ? Value : Value.Substring(0, Value.IndexOfAny(err));
    }
    public int QInt(string Key)
    {
        return QInt(Key, 0);
    }
    public int QInt(string Key, int DefaultValue)
    {
        int x = QString(Key, DefaultValue.ToString()).ToInt();
        return x != 0 ? x : DefaultValue;
    }
    public long QLong(string Key)
    {
        return QLong(Key, 0);
    }
    public long QLong(string Key, long DefaultValue)
    {
        long x = QString(Key, DefaultValue.ToString()).ToLong();
        return x != 0 ? x : DefaultValue;
    }
    /// <summary>
    /// ViewState'den değer döndürür
    /// </summary>
    /// <param name="Key">Key</param>
    /// <returns>Değer bulunduysa değer, bulunamadıysa boş string döndürür</returns>
    public string VString(string Key)
    {
        return VString(Key, "");
    }

    /// <summary>
    /// ViewState'den değer döndürür.
    /// </summary>
    /// <param name="Key">Key</param>
    /// <param name="DefaultValue">Key yoksa ne dönsün?</param>
    /// <returns>Key varsa key değeri, yoksa default değer döner...</returns>
    public string VString(string Key, string DefaultValue)
    {
        string Value = ViewState[Key] as string;
        if (string.IsNullOrEmpty(Value)) return DefaultValue;

        char[] err = { '\'', '"' };

        return Value.IndexOfAny(err) < 0 ? Value : Value.Substring(0, Value.IndexOfAny(err));
    }
    public int VInt(string Key)
    {
        return VInt(Key, 0);
    }
    public int VInt(string Key, int DefaultValue)
    {
        int x = VString(Key, DefaultValue.ToString()).ToInt();
        return x != 0 ? x : DefaultValue;
    }
    #endregion

    protected override void OnError(EventArgs e)
    {
        return;
        string redir = "";
        Exception ex = Server.GetLastError();
        
        try
        {
            ExceptionManager.Publish(ex);
            if (ConfigManager.Current.IsTestEnvironment) Application["last.ex"] = ex;
            redir = "~/error";
            Server.ClearError();
        }
        catch
        {
        }
        if (redir != "")
            Response.Redirect(redir);
        base.OnError(e);
    }
    public bool IsModal { get { return QInt("Modal") == 1; } }
    public void CloseModal()
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "refresher", "<script>closePage();</script>", false);
    }
    protected override void OnPreInit(EventArgs e)
    {
        if (QInt("Modal") == 1)
            this.MasterPageFile = "~/MasterPages/PopupMaster.master";
        base.OnPreInit(e);
    }
    protected override void OnPreLoad(EventArgs e)
    {
        if (QString("cc", "") == "2")
            CacheManager.RestartCache();
        base.OnPreLoad(e);
    }
    protected override void OnLoadComplete(EventArgs e)
    {
        try { this.Title = string.Concat(PageTitle, " - ", ConfigManager.Current.DefaultPageTitle); }
        catch { }
        base.OnLoadComplete(e); 
    }
    
    #region Title, Warning, Attention Utilities
    public enum MessageType
    {
        success = 1,
        info = 2,
        error = 3
    }
    public void Warn(string message, int hideAfter = 10, MessageType mType = MessageType.error, bool fromResource = true)
    {
        if (fromResource) message = ResourceManager.GetResource(message);

        var scr = string.Format("warn('{0}','{1}',{2});", message, mType, hideAfter);
        ScriptManager.RegisterStartupScript(Page, typeof(BasePage), message.GetHashCode().ToString("X"), scr, true);
    }
    public void SetMetaTag(string tagName, string tagValue = null)
    {
        if (string.IsNullOrEmpty(tagValue)) return;
        string key = tagName.StartsWith("og:") || tagName.StartsWith("fb:") ? "property" : "name";

        this.AddHeaderContent(IncludeType.RawContent, string.Format(@"<meta {0}=""{1}"" content=""{2}"" />", key, tagName, Util.FormatMultiline(tagValue).Replace("<br />", "")));
    }
    #endregion
}
public enum IncludeType
{
    Js = 1,
    Css = 2,
    RawContent = 3
}
public struct OpenGraph
{
    public const string image = "og:image";
    public const string description = "og:description";
    public const string title = "og:title";
    public const string type = "og:type";
    public const string url = "og:url";
    public const string siteName = "og:site_name";
    public const string appID = "fb:app_id";
    public const string adminIDs = "fb:admins";
    public const string pageID = "fb:page_id";
    public const string keywords = "";
}