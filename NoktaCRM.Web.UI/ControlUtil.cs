using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Xml;
using Utility;
using System.Data;
using ClosedXML.Excel;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Drawing;
using System.Collections.Generic;
using System.IO;

namespace NoktaCRM.Web.UI
{
    public static class ControlUtil
    {
        private static Type[] arrNumeric = { typeof(int), typeof(long), typeof(short), typeof(byte), typeof(decimal), typeof(double) };
        private static Type[] arrDecimal = { typeof(decimal), typeof(double) };
        private static List<Type> numericTypes = new List<Type>(arrNumeric);
        private static List<Type> decimalTypes = new List<Type>(arrDecimal);

        public static void AddTooltip(this WebControl control, string keyTooltip, bool fromResource = true)
        {
            if (string.IsNullOrEmpty(keyTooltip))
                return;

            string tooltip = fromResource ? ResourceManager.GetResource(keyTooltip) : keyTooltip;
            if (fromResource)
                if (keyTooltip == tooltip || string.IsNullOrEmpty(tooltip)) return;

            control.Attributes.Add("data-trigger", "hover");
            control.Attributes.Add("data-original-title", tooltip);
            control.CssClass += " tooltips ";
        }
        public static void AddPlaceholder(this WebControl control, string keyPlaceholder)
        {
            if (string.IsNullOrEmpty(keyPlaceholder))
                return;

            try { if (control.Page.Request.Browser.Browser == "IE") return; }
            catch { }
            control.Attributes.Add("placeholder", ResourceManager.GetResource(keyPlaceholder));
        }

        private static List<float> getDataTableWidth(DataTable dt)
        {
            List<float> result = new List<float>();

            float wColnameCoeff = 7, wNumeric = 80, wString = 400, wDatetime = 140, wBool = 50, wDefault = 5;

            
            foreach (DataColumn dc in dt.Columns)
            {
                Type colType = dc.DataType;
                float typeWidth = 0;
                string colName = dc.ColumnName;

                if (numericTypes.Contains(colType))
                    typeWidth = wNumeric;
                else
                {
                    if (colType == typeof(string))
                        typeWidth = wString;
                    else
                    {
                        if (colType == typeof(DateTime))
                            typeWidth = wDatetime;
                        else
                        {
                            if (colType == typeof(bool))
                                typeWidth = wBool;
                            else
                                typeWidth = wDefault;
                        }
                    }
                }
                if (colName.Length * wColnameCoeff > typeWidth)
                    typeWidth = (float)wColnameCoeff * colName.Length;
                result.Add(typeWidth);
            }
            return result;
        }
        private static string TurkceKarakter(string text)
        {
            text = text.Replace("ı", "\u0131");
            text = text.Replace("İ", "\u0130");
            text = text.Replace("ş", "\u015f");
            text = text.Replace("Ş", "\u015e");
            text = text.Replace("ğ", "\u011f");
            text = text.Replace("Ğ", "\u011e");
            text = text.Replace("ö", "\u00f6");
            text = text.Replace("Ö", "\u00d6");
            text = text.Replace("ç", "\u00e7");
            text = text.Replace("Ç", "\u00c7");
            text = text.Replace("ü", "\u00fc");
            text = text.Replace("Ü", "\u00dc");
            return text;

        }
        private static string FormatObject(object o, Type t)
        {
            if (o == null) return "--";
            if (o is DateTime && (DateTime)o == DateTime.MinValue) return "--";
            if (decimalTypes.Contains(t)) return ((decimal)o).ToString("N2");
            if (numericTypes.Contains(t)) return Convert.ToDecimal(o).ToString("N0");
            if (o is bool) return ResourceManager.GetResource((bool)o ? "yes" : "no");
            return TurkceKarakter(o.ToString());
        }
        public static bool ExportToPDF(this DataTable dt, string fileName)
        {
            try
            {
                float colWidth = 0;
                List<float> dtWidths = getDataTableWidth(dt);
                foreach (float w in dtWidths)
                    colWidth += w;

                Document pdfDoc = new Document();
                pdfDoc.SetMargins(10, 10, 10, 10);
                //iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7);
                BaseFont arial = BaseFont.CreateFont(@"C:\windows\fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(arial, 9, iTextSharp.text.Font.NORMAL);

                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(fileName, FileMode.OpenOrCreate));
                pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(colWidth, colWidth));
                pdfDoc.Open();

                PdfPTable table = new PdfPTable(dt.Columns.Count);
                table.HorizontalAlignment = 0;
                table.SetTotalWidth(dtWidths.ToArray());
                table.SpacingBefore = 0;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PdfPCell cell = new PdfPCell();

                    cell.AddElement(new Phrase(FormatObject(dt.Columns[i].ColumnName, typeof(string)), font));
                    cell.NoWrap = true;
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);
                }

                BaseColor odd = new BaseColor(249, 249, 249), even = new BaseColor(255, 255, 255);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        object val = dt.Rows[i][j];
                        PdfPCell cell = new PdfPCell(new Phrase(FormatObject(val, dt.Columns[j].DataType), font));
                        cell.BackgroundColor = i % 2 == 1 ? odd : even;
                        table.AddCell(cell);
                    }
                }

                pdfDoc.Add(table);
                pdfDoc.Close();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }
        public static bool ExportToExcel(this DataTable dt, string fileName)
        {
            try
            {
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add(dt);

                wb.SaveAs(fileName);
                wb.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }
    }
}
