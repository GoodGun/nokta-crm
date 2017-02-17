<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="MemberList.aspx.cs"
    Inherits="MemberList" %>

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
                <div class="panel-group accordion" id="accordion">
                    <div class="panel" style="overflow: visible !important;">
                        <!--Accordion title-->
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <a data-parent="#accordion" data-toggle="collapse" href="#collapseOne" aria-expanded="false" class="collapsed">Filtre</a>
                            </h3>
                        </div>
                        <!--Accordion content-->
                        <div class="panel-collapse collapse" id="collapseOne" aria-expanded="false" style="height: 0px;">
                            <div class="panel-body form-horizontal">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Ad</label>
                                        <div class="col-md-9">
                                            <web:MTextbox CssClass="form-control" keyTooltip="tooltip.CustomerCode" ID="txtName" runat="server"></web:MTextbox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Soyad</label>
                                        <div class="col-md-9">
                                            <web:MTextbox CssClass="form-control" keyTooltip="tooltip.CustomerCode" ID="txtSurname" runat="server"></web:MTextbox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Email</label>
                                        <div class="col-md-9">
                                            <web:MTextbox CssClass="form-control" keyTooltip="tooltip.CustomerCode" ID="txtEmail" runat="server"></web:MTextbox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Rol</label>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control selectpicker"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="panel-footer text-right">
                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-default" Text="Ara" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">Kullanıcı Listesi</h3>
                    </div>
                    <div class="panel-body">
                        <div class="pad-btm form-inline">
                            <div class="row">
                                <div class="col-sm-6 text-xs-center">
                                    <div class="form-group">
                                        <a class="btn btn-danger" href="/new-member"><%= res("nav.add.new")%></a>
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
                                                <li><a href="/member-detail/<%# Eval("MemberID")%>">
                                                    <%= res("form.detail") %></a></li>
                                                <li><a href="/member-edit/<%# Eval("MemberID")%>">
                                                    <%= res("form.edit") %></a></li>
                                            </ul>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ad" SortExpression="Name">
                                    <ItemTemplate>
                                        <%# Eval("Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Soyad" SortExpression="Surname">
                                    <ItemTemplate>
                                        <%# Eval("Surname")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email" SortExpression="Email">
                                    <ItemTemplate>
                                        <%# Eval("Email")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rol" SortExpression="MemberTypeID">
                                    <ItemTemplate>
                                        <%# Eval("MemberType")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Durum" SortExpression="Status">
                                    <ItemTemplate>
                                        <%# ShowBool(Eval("Status"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kayıt Tarihi" SortExpression="CreateDate">
                                    <ItemTemplate>
                                        <%# ShowDate(Eval("CreateDate")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </web:MGridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
