using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Utility;
using BusinessObjects;

public partial class Stuff_Download : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string filename = QString("File");
        if (string.IsNullOrEmpty(filename)) return;
        filename = filename.Replace("..", "").Replace("/", "").Replace(@"\", "");

        string filePath = string.Concat(ConfigManager.Current.pathReport, @"\", filename);
        if (!File.Exists(filePath)) { Response.Write("404: " + filename); return; }

        bool isPdf = filename.EndsWith(".pdf");

        Response.Clear();
        Response.ContentType = isPdf ? "application/pdf" : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename.Replace(" ", "_").ToSecureFileName());
        Response.TransmitFile(filePath);
        Response.End(); 
    }
}