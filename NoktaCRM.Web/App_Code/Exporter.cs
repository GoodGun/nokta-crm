using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessObjects;
using BusinessObjects.Common;
using Utility;
using System.Data;
using NoktaCRM.Web.UI;
using System.IO;

public class Exporter
{
    public enum FileType { PDF, Excel }
    public static string Export(DataTable dt, FileType fileType = FileType.Excel, string prefix = "export.key", bool formatPrefix = true)
    {
        if (prefix != "export.key" && formatPrefix) prefix = string.Concat("export.", prefix);

        if (formatPrefix) prefix = ResourceManager.GetResource(prefix);
        DateTime now = DateTime.Now;
        string unique = string.Format("{0}{1}{2}.{3}{4}{5}_({6})", 
            now.Day.ToString().PadLeft(2, '0'), now.Month.ToString().PadLeft(2, '0'), now.Year,
            now.Hour.ToString().PadLeft(2, '0'), now.Minute.ToString().PadLeft(2, '0'), now.Second.ToString().PadLeft(2, '0'),
            Guid.NewGuid().ToString().ToSecureFileName().Left(5));
        

        string extension = fileType == FileType.Excel ? "xlsx" : "pdf";

        string filePath = string.Format(@"{0}\{1} ({2}).{3}",
            ConfigManager.Current.pathReport, prefix, unique, extension);

        switch (fileType)
        {
            case FileType.Excel:
                dt.ExportToExcel(filePath);
                break;
            case FileType.PDF:
                dt.ExportToPDF(filePath);
                break;
        }

        filePath = string.Concat(prefix, " (", unique, ").", extension);
        filePath = string.Format("<iframe width=1 height=1 frameborder=1 src='/Stuff/Download.aspx?File={0}'></iframe>", filePath.EncodeURL());

        return filePath;
    }
}