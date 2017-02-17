using System;
using System.Web.UI;
using Utility;

public class BaseUserControl : System.Web.UI.UserControl
{
    public BasePage ThisPage { get { return (BasePage)this.Page; } }

    
    public BaseUserControl()
	{
        
	}
    protected override void Render(HtmlTextWriter writer)
    {
        writer.Write(string.Format("<!--begin {0}-->", this.ClientID));
        base.Render(writer);
        writer.Write(Environment.NewLine);
        writer.Write(string.Format("<!--end {0}-->", this.ClientID));
    }
    public string PageTitle { get { return ThisPage.PageTitle; } }
    public string CurrentURL { get { return ThisPage.CurrentURL; } }
    public string CurrentURLEncoded { get { return ThisPage.CurrentURLEncoded; } }
    public string FriendlyURL { get { return ThisPage.FriendlyURL; } }
    public string FriendlyURLEncoded { get { return ThisPage.FriendlyURLEncoded; } }
    public bool IsSecure { get { return ThisPage.IsSecure; } }
    public void Redirect(string Path) { ThisPage.Redirect(Path); }
    public string img(object BasePath, string relDir = "", bool generateTag = false) { return ThisPage.img(BasePath, relDir, generateTag); }
    public string js(object BasePath) { return ThisPage.js(BasePath); }
    public string css(object BasePath) { return ThisPage.css(BasePath); }
    public string QString(string Key) { return ThisPage.QString(Key); }
    public string res(string key) { return ResourceManager.GetResource(key); }
    public string href(string key, string path) { return ThisPage.href(key, path); }
    public string QString(string Key, string DefaultValue) { return ThisPage.QString(Key, DefaultValue); }
    public int QInt(string Key) { return ThisPage.QInt(Key); }
    public int QInt(string Key, int DefaultValue) { return ThisPage.QInt(Key, DefaultValue); }
    public long QLong(string Key) { return ThisPage.QLong(Key); }
    public long QLong(string Key, long DefaultValue) { return ThisPage.QLong(Key, DefaultValue); }

    public int CurrentPageIndex { get { return ThisPage.CurrentPageIndex; } }
    public string VString(string Key, string DefaultValue) { return ThisPage.VString(Key, DefaultValue); }
    public int VInt(string Key) { return ThisPage.VInt(Key); }
    public int VInt(string Key, int DefaultValue) { return ThisPage.VInt(Key, DefaultValue); }

    public void Warn(string message, int hideAfter = 0, BasePage.MessageType mType = BasePage.MessageType.error, bool fromResource = true)
    {
        ThisPage.Warn(message, hideAfter, mType, fromResource);
    }
}
