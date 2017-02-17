using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BusinessObjects.Common;
using System.Text.RegularExpressions;

public partial class c : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 365 * 24 * 60 * 60;
        string src = Request.QueryString["src"], type = Request.QueryString["type"];

        if (string.IsNullOrEmpty(src)) return;
        if (string.IsNullOrEmpty(type)) type = "js";

        Response.ContentType = type == "js" ? "text/javascript" : "text/css";
        src = GetContent(type, src, Server);
        Response.Write(src);
    }
    public static string GetContent(string type, string src, HttpServerUtility server)
    {
        string result = "";
        if (src.Contains("..")) return "nada";
        char[] sep = { ',' };

        foreach (string file in src.Split(sep, StringSplitOptions.RemoveEmptyEntries))
        {
            src = server.MapPath(string.Concat(@"~\", type, @"\",  file));

            if (!File.Exists(src)) continue;
            src = File.ReadAllText(src);
            result += string.Concat("/*", file, "*/", Environment.NewLine, src, Environment.NewLine);
        }
        return result;
    }

}