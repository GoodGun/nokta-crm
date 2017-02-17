<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="OrderMainList.aspx.cs"
    Inherits="OrderMainList" %>

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
                                        <label class="col-md-3 control-label">Teklif No</label>
                                        <div class="col-md-9">
                                            <web:MTextbox CssClass="form-control" keyTooltip="tooltip.CustomerCode" ID="txtOrderNo" runat="server"></web:MTextbox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Teklif Tarihi</label>
                                        <div class="col-md-9">
                                            <div id="demo-dp-range">
                                                <div class="input-daterange input-group" id="datepicker">
                                                    <web:MDatePicker ID="dt1" runat="server" AllowNull="False" CssClass="form-control" name="start"></web:MDatePicker>
                                                    <span class="input-group-addon">-</span>
                                                    <web:MDatePicker ID="dt2" runat="server" AllowNull="False" CssClass="form-control" name="end"></web:MDatePicker>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Müşteri</label>
                                        <div class="col-md-9">
                                            <web:MTextbox CssClass="form-control" keyTooltip="tooltip.CustomerCode" ID="txtCustomerName" runat="server"></web:MTextbox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Yetkili Kişi</label>
                                        <div class="col-md-9">
                                            <web:MTextbox CssClass="form-control" keyTooltip="tooltip.CustomerCode" ID="txtContactName" runat="server"></web:MTextbox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Teklif Türü</label>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control selectpicker"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Durum</label>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control selectpicker"></asp:DropDownList>
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
                        <h3 class="panel-title">Teklif Listesi</h3>
                    </div>
                    <div class="panel-body">
                        <div class="pad-btm form-inline">
                            <div class="row">
                                <div class="col-sm-6 text-xs-center">
                                    <div class="form-group">
                                        <a class="btn btn-danger" href="/new-ordermain"><%= res("nav.add.new")%></a>
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
                            <PagerStyle CssClass="footable-visible" />
                            <PagerSettings FirstPageText="&laquo;&laquo;" PreviousPageText="&laquo;" NextPageText="&raquo;" LastPageText="&raquo;&raquo;"
                                Mode="NumericFirstLast" PageButtonCount="10" Position="Bottom" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <a data-toggle="dropdown" class="btn btn-mini btn-info dropdown-toggle">
                                                <%= res("form.operations") %>
                                                <span class="caret"></span></a>
                                            <ul class="dropdown-menu">
                                                <li><a href="/ordermain-detail/<%# Eval("OrderID")%>">
                                                    <%= res("form.detail") %></a></li>
                                                <li><a href="/ordermain-edit/<%# Eval("OrderID")%>">
                                                    <%= res("form.edit") %></a></li>
                                            </ul>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Teklif No" SortExpression="OrderCode">
                                    <ItemTemplate>
                                        <%# Eval("OrderCode")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Teklif Tarihi" SortExpression="OrderDate">
                                    <ItemTemplate>
                                        <%# ShowDate(Eval("OrderDate")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Müşteri" SortExpression="CustomerName">
                                    <ItemTemplate>
                                        <%# Eval("CustomerName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Yetkili Kişi" SortExpression="ContactName">
                                    <ItemTemplate>
                                        <%# Eval("ContactName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Teklif Türü" SortExpression="OrderType">
                                    <ItemTemplate>
                                        <%# Eval("OrderType")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Durum" SortExpression="OrderStatus">
                                    <ItemTemplate>
                                        <%# Eval("OrderStatus")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Toplam Tutar" SortExpression="FinalPrice">
                                    <ItemTemplate>
                                        <%# Eval("FinalPrice")%>
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
