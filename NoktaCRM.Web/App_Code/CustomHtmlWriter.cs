using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public class CustomHtmlWriter : HtmlTextWriter
{
    private string ActionURL { get; set; }
    private const string newLine = "\n", tab="\t";
    private bool doneCheck = false;

    public CustomHtmlWriter(TextWriter writer) : base(writer, newLine) { }
    public CustomHtmlWriter(ref HtmlTextWriter writer, string tabstring, string RewrittenAction)
        : base(writer, newLine)
    {
        this.ActionURL = RewrittenAction;
    }
    public override void WriteBeginTag(string tagName)
    {
        base.Write(newLine);
        base.WriteBeginTag(tagName);
    }
    //public override void WriteLine()
    //{
    //    return;
    //}
    
    public override void RenderBeginTag(HtmlTextWriterTag tagKey)
    {
        base.Write(newLine);
        base.RenderBeginTag(tagKey);
    }
    protected override string RenderAfterTag()
    {
        return "";
    }
    protected override string RenderBeforeContent()
    {
        return "";
    }
    protected override string RenderAfterContent()
    {
        return "";
    }
    
    public override void WriteAttribute (string name, string value, bool fEncode)
    {
        if (!doneCheck && string.Compare(name, "action", true) == 0) { value = ActionURL; doneCheck = true; base.WriteAttribute("dataaction", value, false); }
        base.WriteAttribute(name, value, false);
    }
}