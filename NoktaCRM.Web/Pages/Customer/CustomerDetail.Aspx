<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="CustomerDetail.aspx.cs" Inherits="CustomerDetail" %>
<%@ MasterType VirtualPath="~/Main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="span12">
            <a class="btn btn-primary btn-mini" href="/customer-list"><%= res("nav.back.to.list") %></a>
            <a class="btn btn-primary btn-mini" href="/customer-edit/<%= CustomerID%>"><%= res("nav.edit") %></a>
            <a class="btn btn-primary btn-mini" href="/new-customer"><%= res("nav.add.new") %></a>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <table id="tblData" runat="server" class="Form">

	<tr>
		<td><%=res("form.CustomerID")%></td>
		<td><asp:Literal id="ltrCustomerID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.CustomerCode")%></td>
		<td><asp:Literal id="ltrCustomerCode" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.ReferenceNo")%></td>
		<td><asp:Literal id="ltrReferenceNo" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.MemberID")%></td>
		<td><asp:Literal id="ltrMemberID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.SellerMemberID")%></td>
		<td><asp:Literal id="ltrSellerMemberID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Name")%></td>
		<td><asp:Literal id="ltrName" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.CustomerTypeID")%></td>
		<td><asp:Literal id="ltrCustomerTypeID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.SectorTypeID")%></td>
		<td><asp:Literal id="ltrSectorTypeID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.MainReseller")%></td>
		<td><asp:Literal id="ltrMainReseller" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Reseller")%></td>
		<td><asp:Literal id="ltrReseller" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Description")%></td>
		<td><asp:Literal id="ltrDescription" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Code1")%></td>
		<td><asp:Literal id="ltrCode1" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Code2")%></td>
		<td><asp:Literal id="ltrCode2" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Code3")%></td>
		<td><asp:Literal id="ltrCode3" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.BillingAddressID")%></td>
		<td><asp:Literal id="ltrBillingAddressID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.DeliveryAddressID")%></td>
		<td><asp:Literal id="ltrDeliveryAddressID" runat="server"></asp:Literal></td>
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
