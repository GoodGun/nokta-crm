using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Web.UI;
using System.Data;

namespace Utility
{
    public static class UsercontrolsExtension
    {
        private static readonly CultureInfo TurkishCulture = new CultureInfo("tr-TR");

        public static List<int> GetSelectedValues(this ListControl ddl)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < ddl.Items.Count; i++)
                if (ddl.Items[i].Selected)
                    result.Add(ddl.Items[i].Value.ToInt());
            return result;
        }
        public static void Chosen(this ListControl control, string chooseKey = "seciniz", int maxSelected = 0)
        {
            control.Attributes["data-placeholder"] = ResourceManager.GetResource(chooseKey);
            control.Attributes["data-no_results_text"] = ResourceManager.GetResource("filter.no.data");
            if (maxSelected > 0)
                control.Attributes["data-max_selection"] = maxSelected.ToString();
            control.CssClass = control.CssClass.Replace(" chosen", "").Add(" chosen");
        }
        /// <summary>
        /// Sets the Button you want to submit when the Enter key is pressed within a TextBox.
        /// </summary>
        /// <param name="thisPage">The container page of the TextBox and Button</param>
        /// <param name="textControl">The TextBox to monitor for the Enter key</param>
        /// <param name="defaultButton">The Button you want to click when the Enter key is pressed</param>
        public static void SetDefaultButton(this TextBox textControl, WebControl defaultButton)
        {
            textControl.Attributes.Add("onkeydown", "defaultButton('" + defaultButton.ClientID + "',event)");
        }
        public static void GlobalizeDataTable(this GridView grdData, ref DataTable dt, string idColumn = null)
        {
            string key = null;

            for (int i = dt.Columns.Count - 1; i >= 0; i--)
            {
                bool colFound = false;
                string dbColName = dt.Columns[i].ColumnName;
                if (!string.IsNullOrEmpty(idColumn) && dbColName == idColumn)
                    dbColName = "ID";
                for (int j = 0; j < grdData.Columns.Count; j++)
                {
                    DataControlField col = grdData.Columns[j];
                    if (!string.IsNullOrEmpty(col.HeaderText) && col.Visible)
                    {
                        key = ResourceManager.GetResource(string.Concat("form.", dbColName));
                        if (key == col.HeaderText)
                        {
                            dt.Columns[i].ColumnName = key;
                            colFound = true;
                            break;
                        }
                    }
                }
                if (!colFound)
                {
                    dt.Columns.RemoveAt(i);
                    dt.AcceptChanges();
                }
            }
        }
        public static long GetAND(this CheckBoxList cbl)
        {
            long optionSum = 0;
            foreach (ListItem li in cbl.Items)
            {
                if (li.Selected)
                    optionSum += li.Value.ToLong();
            }
            return optionSum;
        }
        public static void SelectALL(this CheckBoxList cbl)
        {
            cbl.SelectAND(Convert.ToInt64(Math.Pow(2, cbl.Items.Count) - 1));
        }
        public static void SelectAND(this CheckBoxList cbl, long valueSum)
        {
            cbl.ClearSelection();
            foreach (ListItem li in cbl.Items)
            {
                long val = li.Value.ToLong();
                if ((val & valueSum) == val)
                    li.Selected = true;
            }
        }
        public static void CheckColumnFilter(this GridView grdData, string vis)
        {
            if (string.IsNullOrEmpty(vis)) return;
            //if (vis.Length != grdData.Columns.Count) return;

            for (int i = 0; i < vis.Length - 1; i++)
                grdData.Columns[i].Visible = vis.Substring(i, 1) == "1";
        }

        public static void ColumnFilter(this GridView grdData, CheckBoxList cbl)
        {
            for (int i = 0; i < grdData.Columns.Count; i++)
            {
                DataControlField col = grdData.Columns[i];

                if (string.IsNullOrEmpty(col.HeaderText))
                {
                    ListItem li = new ListItem("--", "--");
                    
                    li.Enabled = false;
                    li.Selected = col.Visible;
                    cbl.Items.Add(li);
                }
                else
                {
                    ListItem li = new ListItem(col.HeaderText, col.HeaderText);
                    li.Selected = col.Visible;
                    cbl.Items.Add(li);
                }
            }
        }
        public static void Wizard(this Wizard wiz)
        {
            wiz.DisplayCancelButton = false;
            wiz.DisplaySideBar = true;
            
            wiz.CssClass = "wizard";
            wiz.FinishCompleteButtonStyle.CssClass = "btn btn-warning";
            wiz.FinishPreviousButtonText = ResourceManager.GetResource("nav.previous");
            wiz.FinishCompleteButtonText = ResourceManager.GetResource("nav.save");
            wiz.HeaderStyle.CssClass = "wizardHeader";
            
            wiz.NavigationButtonStyle.CssClass = "btn btn-primary";
            wiz.NavigationStyle.CssClass = "wizNavigation";
            wiz.SideBarButtonStyle.CssClass = "btn btn-small btn-info";
            wiz.SideBarStyle.CssClass = "wizSidebar";
            wiz.StartNextButtonText = ResourceManager.GetResource("nav.next");
            wiz.StepNextButtonText = ResourceManager.GetResource("nav.next");
            wiz.StepPreviousButtonText = ResourceManager.GetResource("nav.previous");
            wiz.StepStyle.CssClass = "wizStep";
            
            for (int i = 0; i < wiz.WizardSteps.Count; i++)
            {
                wiz.WizardSteps[i].Title = ResourceManager.GetResource(wiz.WizardSteps[i].Title);
            }
        }
        public static void FirstItemToAll(this ListControl ddl, string chooseKey = "all", bool fromResource = true)
        {
            if (ddl.Items.Count < 1 || !string.IsNullOrEmpty(ddl.Items[0].Value)) return;
            ddl.Items[0].Text = fromResource ? ResourceManager.GetResource(chooseKey) : chooseKey;
        }
        public static void InsertChooseItem(this ListControl ddl, bool fromResource = true, string textKey = "seciniz", string val = "")
        {
            if (fromResource) textKey = ResourceManager.GetResource(textKey);

            ddl.Items.Insert(0, new ListItem(textKey, val));
        }
        public static void Select(this ListControl ddl, object val)
        {
            ddl.ClearSelection();
            
            if (val == null) return;
            try
            {
                List<string> values = new List<string>();
                char [] sep = {','};
                
                if (val is string)
                    values = new List<string>(val.ToString().Split(sep, StringSplitOptions.RemoveEmptyEntries));

                if (val.IsNumeric())
                    values.Add(val.ToString());
                else
                    values.Add(val.ToString());

                Select(ddl, values);
            }
            catch {}
        }

        private static void Select(this ListControl ddl, List<string> values)
        {
            ddl.ClearSelection();
            if (values == null || values.Count == 0) return;
            try
            {
                foreach (string val in values)
                {
                    ListItem li = ddl.Items.FindByValue(val);
                    if (li != null) li.Selected = true;
                }
            }
            catch { }
        }
        public static bool IsNumeric(this object value)
        {
            if (value == null)
                return false;

            double number;
            return Double.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), NumberStyles.Any, NumberFormatInfo.InvariantInfo, out number);
        }
        public static void Disable(this WebControl control, bool disabled = true)
        {
            if (disabled)
                control.Attributes.Add("disabled", "disabled");
            else
                control.Attributes.Remove("disabled");
        }
        public static ListControl BindData(this ListControl control, object dataSource, string textField = null, string valueField = null)
        {
            if (!string.IsNullOrEmpty(textField)) control.DataTextField = textField;
            if (!string.IsNullOrEmpty(valueField)) control.DataValueField = valueField;

            control.DataSource = dataSource;
            control.DataBind();
            return control;
        }

        public static DateTime SelectedDate(this TextBox txt)
        {
            DateTime dt = DateTime.MinValue;
            DateTime.TryParse(txt.Text, TurkishCulture, DateTimeStyles.None, out dt);
            return dt;
        }
        public static void SetSelectedDate(this TextBox txt, DateTime dt)
        {
            txt.Text = string.Format("{0}.{1}.{2}", dt.Day.ToString().PadLeft(2, '0'),
                dt.Month.ToString().PadLeft(2, '0'),
                dt.Year);
        }
    }
}
