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

public partial class OrderMainList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles("List", "OrderMain");
        
        if (!this.IsPostBack)
        {
            InitForm();
            Search();
        }
    }
    private void InitForm()
    {
        var Filters = Populator.GetFilter();
        Filters.Add("TypeCode", "OrderTypeID");
        var arrType = ParamsManager.GetParamssByFilter(Filters);
        ddlType.BindData(arrType, "TypeName", "ObjectValue");
        ddlType.InsertChooseItem();

        Filters = Populator.GetFilter();
        Filters.Add("TypeCode", "OrderStatusID");
        var arrStatus = ParamsManager.GetParamssByFilter(Filters);
        ddlStatus.BindData(arrStatus, "TypeName", "ObjectValue");
        ddlStatus.InsertChooseItem();

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
        if (!string.IsNullOrEmpty(txtOrderNo.Text)) filter.Add("OrderCode", txtOrderNo.Text);
        if (!string.IsNullOrEmpty(txtContactName.Text)) filter.Add("ContactName LIKE '%" + txtContactName.Text + "%' AND 1", 1);
        if (!string.IsNullOrEmpty(txtCustomerName.Text)) filter.Add("CustomerName LIKE '%" + txtCustomerName.Text + "%' AND 1", 1);
        if (ddlType.SelectedIndex > 0) filter.Add("OrderTypeID", ddlType.SelectedValue);
        if (ddlStatus.SelectedIndex > 0) filter.Add("OrderStatusID", ddlStatus.SelectedValue);
        if (dt1.HasDate) filter.Add("OrderDate>", dt1.SelectedDate.ToDBDate());
        if (dt2.HasDate) filter.Add("OrderDate<", dt2.SelectedDate.ToDBDate());

        return filter;
    }
    private DataTable GetData(string sortExpression, int pageIndex, int pageSize, out int totalRows)
    {
        bool forExport = pageSize == 0;
        if (forExport) pageSize = ConfigManager.Current.MaxExportRows;
        
        DataTable dt = CustomQueries.GetStuffByPage("admGetOrderMainsByPage", GetFilter(), sortExpression, pageIndex + 1, pageSize, "*", out totalRows);

        if (forExport) grdData.GlobalizeDataTable(ref dt);
        return dt;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
	{
		Search();
	}
    #region Grid and Form functions
    private const string DefaultSort = "OrderID DESC";
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
        string filePath = Exporter.Export(dt, Exporter.FileType.PDF, "list.OrderMain");
        spanExport.InnerHtml = filePath;
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        int totalRows = 0;
        DataTable dt = GetData(grdData.OrderBy ?? DefaultSort, 0, 0, out totalRows);
        string filePath = Exporter.Export(dt, Exporter.FileType.Excel, "list.OrderMain");
        spanExport.InnerHtml = filePath;
    }
    #endregion
}
