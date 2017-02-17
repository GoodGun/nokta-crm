using System;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI;
using Utility;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace NoktaCRM.Web.UI
{
    public class MGridView : GridView
    {
        #region Constructor and Stuff
        public MGridView()
            : base()
        {
            this.AllowPaging = true;
            this.AllowSorting = true;
            this.PagerSettings.Mode = PagerButtons.NumericFirstLast;
            this.PagerSettings.Position = PagerPosition.Bottom;
            this.CellPadding = 2;
            this.CellSpacing = 0;
            this.HeaderStyle.CssClass = "GridTitleRow";
            this.PagerStyle.CssClass = "GridPager";
        }
        [Description("Enable the custom pager when paging is allowed.")]
        [Category("behavior")]
        public bool EnableCustomPaging
        {
            get
            {
                object temp = ViewState["EnableCustomPaging"];
                return temp == null ? false : (bool)temp;
            }
            set { ViewState["EnableCustomPaging"] = value; }
        }

        [Browsable(true), Category("NewDynamic")]
        [Description("Set the virtual item count for this grid")]
        public int VirtualItemCount
        {
            get
            {
                if (ViewState["pgv_vitemcount"] == null)
                    ViewState["pgv_vitemcount"] = -1;
                return Convert.ToInt32(ViewState["pgv_vitemcount"]);
            }
            set
            {
                ViewState["pgv_vitemcount"] = value;
            }
        }

        [Browsable(true), Category("NewDynamic")]
        public string OrderBy
        {
            get
            {
                if (ViewState["pgv_orderby"] == null)
                    ViewState["pgv_orderby"] = string.Empty;
                return ViewState["pgv_orderby"].ToString();
            }
            set
            {
                ViewState["pgv_orderby"] = value;
            }
        }
        public int CurrentPageIndex
        {
            get
            {
                if (ViewState["pgv_pageindex"] == null)
                    ViewState["pgv_pageindex"] = 0;
                return Convert.ToInt32(ViewState["pgv_pageindex"]);
            }
            set
            {
                ViewState["pgv_pageindex"] = value;
            }
        }
        private bool CustomPaging
        {
            get
            {
                return (VirtualItemCount != -1);
            }
        }
        #endregion

        #region Overriding the parent methods

        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = value;
                CurrentPageIndex = PageIndex;
                CheckSorting();
            }
        }
        public void ShowData(object dataSource, int totalRows, int pageIndex, int pageSize, string sortExpression)
        {
            this.OrderBy = sortExpression;
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.VirtualItemCount = totalRows;
            this.DataSource = dataSource;
            this.DataBind();
            this.Visible = true;
        }
        protected override void OnSorting(GridViewSortEventArgs e)
        {
            this.PageIndex = CurrentPageIndex;
            OrderBy = e.SortExpression;
            base.OnSorting(e);
        }

        protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            if (CustomPaging)
            {
                pagedDataSource.AllowCustomPaging = true;
                pagedDataSource.VirtualCount = VirtualItemCount;
                pagedDataSource.CurrentPageIndex = CurrentPageIndex;
            }
            base.InitializePager(row, columnSpan, pagedDataSource);
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
        private void CheckSorting()
        {
            try
            {
                if (!this.AllowSorting || string.IsNullOrEmpty(this.OrderBy)) return;

                char[] Chars = { ' ' };
                List<string> grdOrder = new List<string>(this.OrderBy.Trim(Chars).Split(Chars));

                for (int i = 0; i < this.Columns.Count; i++)
                {
                    DataControlField col = this.Columns[i];

                    if (string.IsNullOrEmpty(col.SortExpression)) continue;
                    List<string> colOrder = new List<string>(col.SortExpression.Trim(Chars).Split(Chars));

                    if (colOrder[0] != grdOrder[0])
                        col.HeaderStyle.CssClass = "sortable";
                    else
                    {
                        string sort1 = grdOrder.Count > 1 ? grdOrder[1] : "null";
                        string sort2 = colOrder.Count > 1 ? colOrder[1] : "null";

                        if (sort1 == sort2)
                        {
                            if (sort1 == "DESC")
                            {
                                col.SortExpression = colOrder[0];
                                col.HeaderStyle.CssClass = "sortdown";
                            }
                            else
                            {
                                col.SortExpression = string.Concat(colOrder[0], " DESC");
                                col.HeaderStyle.CssClass = "sortup";
                            }
                        }
                        else
                            col.HeaderStyle.CssClass = "sortdown";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            try
            {
                if (this.VirtualItemCount > 0 || this.Rows.Count > 0)
                {
                    bool ShowTop = false, ShowBottom = false;

                    if (AllowPaging && Visible && VirtualItemCount > 0)
                    {
                        PagerPosition CurrentPosition = this.PagerSettings.Position;
                        ShowTop = CurrentPosition == PagerPosition.Top || CurrentPosition == PagerPosition.TopAndBottom;
                        ShowBottom = CurrentPosition == PagerPosition.Bottom || CurrentPosition == PagerPosition.TopAndBottom;
                    }

                    string PagingText = string.Format(ResourceManager.GetResource("ui.gridview.pagertext"),
                            this.VirtualItemCount, this.PageCount, this.CurrentPageIndex + 1);

                    if (ShowTop) writer.Write(PagingText);
                    base.Render(writer);
                    if (ShowBottom) writer.Write(PagingText);
                }
                else
                {
                    base.Render(writer);
                    writer.Write(ResourceManager.GetResource("ui.gridview.nodata"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                base.Render(writer);
            }
        }
        #endregion
    }
}
