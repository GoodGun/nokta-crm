<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="OrderDetailDetail.aspx.cs" Inherits="OrderDetailDetail" %>
<%@ MasterType VirtualPath="~/Main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="span12">
            <a class="btn btn-primary btn-mini" href="/orderdetail-list"><%= res("nav.back.to.list") %></a>
            <a class="btn btn-primary btn-mini" href="/orderdetail-edit/<%= OrderDetailID%>"><%= res("nav.edit") %></a>
            <a class="btn btn-primary btn-mini" href="/new-orderdetail"><%= res("nav.add.new") %></a>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <table id="tblData" runat="server" class="Form">

	<tr>
		<td><%=res("form.OrderDetailID")%></td>
		<td><asp:Literal id="ltrOrderDetailID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.OrderID")%></td>
		<td><asp:Literal id="ltrOrderID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.ProductID")%></td>
		<td><asp:Literal id="ltrProductID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Quantity")%></td>
		<td><asp:Literal id="ltrQuantity" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.TaxRate")%></td>
		<td><asp:Literal id="ltrTaxRate" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.UnitPrice")%></td>
		<td><asp:Literal id="ltrUnitPrice" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.DiscountAmount")%></td>
		<td><asp:Literal id="ltrDiscountAmount" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.TotalPrice")%></td>
		<td><asp:Literal id="ltrTotalPrice" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.FinalPrice")%></td>
		<td><asp:Literal id="ltrFinalPrice" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.CurrencyID")%></td>
		<td><asp:Literal id="ltrCurrencyID" runat="server"></asp:Literal></td>
	</tr>    
            </table>
        </div>
    </div>
</asp:Content>
