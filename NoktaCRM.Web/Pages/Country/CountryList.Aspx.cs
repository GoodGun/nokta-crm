using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using BusinessObjects.Common;
using Utility; 

public partial class CountryList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles("List", "Country");
        
        if (!this.IsPostBack)
        {
            InitForm();
            Search();
        }
    }
    private void InitForm()
    {
        //dt1.SelectedDate = DateTime.Today.AddDays(-7d);
        //dt2.SelectedDate = DateTime.Today;
        grdData.Visible = false;
    }
    private void Search(int PageIndex = 0, string SortExpression = null)
	{
        SortExpression = SortExpression ?? DefaultSort;
		int TotalRows = 0;
        DataTable dt = GetData(SortExpression, PageIndex, ConfigManager.Current.DefaultPageSize, out TotalRows);
        grdData.ShowData(dt, TotalRows, PageIndex, ConfigManager.Current.DefaultPageSize, SortExpression);
        lnkExport.Visible = TotalRows > 0;
	}

    private Dictionary<string, object> GetFilter()
    {
        var filter = Populator.GetFilter();
        //if (dt1.HasDate) filter.Add("CreateDate>", dt1.SelectedDate.ToDBDate());
        //if (dt2.HasDate) filter.Add("CreateDate<", dt2.SelectedDate.ToDBDate());

        return filter;
    }
    private DataTable GetData(string sortExpression, int pageIndex, int pageSize, out int totalRows)
    {
        bool forExport = pageSize == 0;
        if (forExport) pageSize = ConfigManager.Current.MaxExportRows;
        
        DataTable dt = CustomQueries.GetStuffByPage("admGetCountrysByPage", GetFilter(), sortExpression, pageIndex + 1, pageSize, "*", out totalRows);

        if (forExport) grdData.GlobalizeDataTable(ref dt);
        return dt;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
	{
		Search();
	}
    #region Grid and Form functions
    private const string DefaultSort = "CountryID DESC";
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		Search(e.NewPageIndex, grdData.OrderBy);
	}
	protected void grdData_Sorting(object sender, GridViewSortEventArgs e)
	{
        Search(grdData.PageIndex, e.SortExpression);
	}
    protected void btnExportPDF_Click(object sender, EventArgs e)
    {
        int totalRows = 0;
        DataTable dt = GetData(grdData.OrderBy ?? DefaultSort, 0, 0, out totalRows);
        string filePath = Exporter.Export(dt, Exporter.FileType.PDF, "list.Country");
        spanExport.InnerHtml = filePath;
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        int totalRows = 0;
        DataTable dt = GetData(grdData.OrderBy ?? DefaultSort, 0, 0, out totalRows);
        string filePath = Exporter.Export(dt, Exporter.FileType.Excel, "list.Country");
        spanExport.InnerHtml = filePath;
    }
    #endregion
}
