<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="CityList.aspx.cs"
    Inherits="CityList" %>

<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="scrMain" runat="server" EnablePageMethods="true" EnableScriptGlobalization="true"
	EnableScriptLocalization="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upMain" runat="server" ClientIDMode="Static" UpdateMode="Conditional">
	<ContentTemplate>
	    <span id="spanExport" runat="server" class="export" enableviewstate="false"></span>
	    <div id="page-content">
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Kullanıcı Listesi</h3>
                    </div>
                    <div class="panel-body">
                        <div class="pad-btm form-inline">
                            <div class="row">
                                <div class="col-sm-6 text-xs-center">
                                    <div class="form-group">
                                        <a class="btn btn-danger" href="/new-city"><%= res("nav.add.new")%></a>
                                    </div>
                                    <div class="btn-group">
                                        <a id="lnkExport" runat="server" data-toggle="dropdown" class="btn btn-primary btn-mini dropdown-toggle">
                                            <%= res("export") %>
                                            <span class="caret"></span></a>
                                        <ul class="dropdown-menu">
                                            <li>
                                                <asp:LinkButton ID="btnExportPDF" runat="server" OnClick="btnExportPDF_Click" Text="PDF" /></li>
                                            <li>
                                                <asp:LinkButton ID="btnExportExcel" runat="server" OnClick="btnExportExcel_Click" Text="Excel" /></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
		    <web:MGridView ID="grdData" ClientIDMode="Static" EnableCustomPaging="true" runat="server" AllowPaging="True"
			CellPadding="1" CellSpacing="1" AutoGenerateColumns="False" OnPageIndexChanging="grdData_PageIndexChanging"
			OnSorting="grdData_Sorting" CssClass="Grid table table-striped table-bordered table-hover table-condensed"
			GridLines="None" PagerStyle-CssClass="GridPager">
			<HeaderStyle CssClass="GridTitleRow" />
			<PagerSettings FirstPageText="&laquo;&laquo;" PreviousPageText="&laquo;" NextPageText="&raquo;" LastPageText="&raquo;&raquo;"
			    Mode="NumericFirstLast" PageButtonCount="10" Position="Top" />
			<Columns>
			    <asp:TemplateField HeaderStyle-Width="50px">
				<ItemTemplate>
				    <div class="btn-group">
					<a data-toggle="dropdown" class="btn btn-mini btn-info dropdown-toggle">
					    <%= res("form.operations") %>
					    <span class="caret"></span></a>
					<ul class="dropdown-menu">
					    <li><a href="/city-detail/<%# Eval("CityID")%>">
						<%= res("form.detail") %></a></li>
					    <li><a href="/city-edit/<%# Eval("CityID")%>">
						<%= res("form.edit") %></a></li>
					</ul>
				    </div>
				</ItemTemplate>
			    </asp:TemplateField>

	<asp:TemplateField HeaderText="CityID" SortExpression="CityID">
		<itemtemplate>
			<a href="/city-detail/<%# Eval("CityID")%>"># <%# Eval("CityID")%></a>
		</itemtemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="CountryID" SortExpression="CountryID">
		<itemtemplate>
			<%# Eval("CountryID")%>
		</itemtemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="CityName" SortExpression="CityName">
		<itemtemplate>
			<%# Eval("CityName")%>
		</itemtemplate>
	</asp:TemplateField>
	<asp:TemplateField HeaderText="Status" SortExpression="Status">
		<itemtemplate>
			<%# ShowBool(Eval("Status"))%>
		</itemtemplate>
	</asp:TemplateField>                        </Columns>
                    </web:MGridView>
                </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
