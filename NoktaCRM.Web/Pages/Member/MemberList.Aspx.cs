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

public partial class MemberList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Authorize();
        this.Master.SetTitles("List", "Member");
        
        if (!this.IsPostBack)
        {
            InitForm();
            Search();
        }
    }
    private void InitForm()
    {
        var Filters = Populator.GetFilter();
        Filters.Add("TypeCode", "MemberTypeID");
        var arrType = ParamsManager.GetParamssByFilter(Filters);
        ddlType.BindData(arrType, "TypeName", "ObjectValue");
        ddlType.InsertChooseItem();
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
        if (!string.IsNullOrEmpty(txtName.Text)) filter.Add("Name LIKE '%" + txtName.Text + "%' AND 1", 1);
        if (!string.IsNullOrEmpty(txtSurname.Text)) filter.Add("Surname LIKE '%" + txtSurname.Text + "%' AND 1", 1);
        if (!string.IsNullOrEmpty(txtEmail.Text)) filter.Add("Email LIKE '%" + txtEmail.Text + "%' AND 1", 1);
        if (ddlType.SelectedIndex > 0) filter.Add("MemberTypeID", ddlType.SelectedValue);

        return filter;
    }
    private DataTable GetData(string sortExpression, int pageIndex, int pageSize, out int totalRows)
    {
        bool forExport = pageSize == 0;
        if (forExport) pageSize = ConfigManager.Current.MaxExportRows;
        
        DataTable dt = CustomQueries.GetStuffByPage("admGetMembersByPage", GetFilter(), sortExpression, pageIndex + 1, pageSize, "*", out totalRows);

        if (forExport) grdData.GlobalizeDataTable(ref dt);
        return dt;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
	{
		Search();
	}
    #region Grid and Form functions
    private const string DefaultSort = "MemberID DESC";
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
        string filePath = Exporter.Export(dt, Exporter.FileType.PDF, "list.Member");
        spanExport.InnerHtml = filePath;
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        int totalRows = 0;
        DataTable dt = GetData(grdData.OrderBy ?? DefaultSort, 0, 0, out totalRows);
        string filePath = Exporter.Export(dt, Exporter.FileType.Excel, "list.Member");
        spanExport.InnerHtml = filePath;
    }
    #endregion
}
