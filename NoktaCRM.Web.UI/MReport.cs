using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Obout.Grid;
using Utility;
using System.Web.UI.WebControls;

namespace NoktaCRM.Web.UI
{
    public class MReport : Grid
    {
        public MReport()
        {
            this.AllowAddingRecords = false;
            this.AllowColumnReordering = true;
            this.AllowKeyNavigation = true;
            this.AllowFiltering = false;
            this.AllowMultiRecordSelection = false;
            this.AllowManualPaging = false;
            this.AllowPageSizeSelection = false;
            this.AllowRecordSelection = false;
            this.AllowGrouping = true;

            this.AutoGenerateColumns = false;
            this.CallbackMode = false;
            this.EnableRecordHover = true;
            this.FolderLocalization = "styles";
            this.FolderStyle = "styles/grand_gray";
            this.HideColumnsWhenGrouping = false;
            this.Language = Util.CurrentUserLang == "TR" ? "tr" : "en";
            this.NumberOfPagesShownInFooter = 5;
            this.PageSize = 20;
            this.PageSizeOptions = "20,25,50,100,250,500";
            this.Serialize = false;
            this.ShowGroupFooter = true;
            this.ShowTotalNumberOfPages = true;
        }
        protected override void OnInit(EventArgs e)
        {
            try
            {
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    DataControlField col = this.Columns[i];
                    if (!string.IsNullOrEmpty(col.HeaderText))
                    {
                        string key = string.Concat("form.", col.HeaderText);
                        string res = ResourceManager.GetResource(key);
                        if (res != key)
                            col.HeaderText = res;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            base.OnInit(e);
        }
    }
}
