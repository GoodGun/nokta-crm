<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="OrderMainDetail.aspx.cs" Inherits="OrderMainDetail" %>
<%@ MasterType VirtualPath="~/Main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="span12">
            <a class="btn btn-primary btn-mini" href="/ordermain-list"><%= res("nav.back.to.list") %></a>
            <a class="btn btn-primary btn-mini" href="/ordermain-edit/<%= OrderMainID%>"><%= res("nav.edit") %></a>
            <a class="btn btn-primary btn-mini" href="/new-ordermain"><%= res("nav.add.new") %></a>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <table id="tblData" runat="server" class="Form">

	<tr>
		<td><%=res("form.OrderID")%></td>
		<td><asp:Literal id="ltrOrderID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.OrderCode")%></td>
		<td><asp:Literal id="ltrOrderCode" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.MemberID")%></td>
		<td><asp:Literal id="ltrMemberID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.CustomerID")%></td>
		<td><asp:Literal id="ltrCustomerID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.ContactID")%></td>
		<td><asp:Literal id="ltrContactID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.AddressID")%></td>
		<td><asp:Literal id="ltrAddressID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.ReferenceNo")%></td>
		<td><asp:Literal id="ltrReferenceNo" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.OrderTypeID")%></td>
		<td><asp:Literal id="ltrOrderTypeID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.OrderContent")%></td>
		<td><asp:Literal id="ltrOrderContent" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.CargoName")%></td>
		<td><asp:Literal id="ltrCargoName" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.BuyerOrderNo")%></td>
		<td><asp:Literal id="ltrBuyerOrderNo" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Description")%></td>
		<td><asp:Literal id="ltrDescription" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.TotalPrice")%></td>
		<td><asp:Literal id="ltrTotalPrice" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.TaxAmount")%></td>
		<td><asp:Literal id="ltrTaxAmount" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.DiscountAmount")%></td>
		<td><asp:Literal id="ltrDiscountAmount" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.FinalPrice")%></td>
		<td><asp:Literal id="ltrFinalPrice" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.OrderStatusID")%></td>
		<td><asp:Literal id="ltrOrderStatusID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.FileName")%></td>
		<td><asp:Literal id="ltrFileName" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.OrderDate")%></td>
		<td><asp:Literal id="ltrOrderDate" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.CreateDate")%></td>
		<td><asp:Literal id="ltrCreateDate" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.UpdateDate")%></td>
		<td><asp:Literal id="ltrUpdateDate" runat="server"></asp:Literal></td>
	</tr>    
            </table>
        </div>
    </div>
</asp:Content>
