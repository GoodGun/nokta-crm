<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="AddressDetail.aspx.cs" Inherits="AddressDetail" %>
<%@ MasterType VirtualPath="~/Main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="span12">
            <a class="btn btn-primary btn-mini" href="/address-list"><%= res("nav.back.to.list") %></a>
            <a class="btn btn-primary btn-mini" href="/address-edit/<%= AddressID%>"><%= res("nav.edit") %></a>
            <a class="btn btn-primary btn-mini" href="/new-address"><%= res("nav.add.new") %></a>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <table id="tblData" runat="server" class="Form">

	<tr>
		<td><%=res("form.AddressID")%></td>
		<td><asp:Literal id="ltrAddressID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.CustomerID")%></td>
		<td><asp:Literal id="ltrCustomerID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.AddressName")%></td>
		<td><asp:Literal id="ltrAddressName" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.IsBillingAddress")%></td>
		<td><asp:Literal id="ltrIsBillingAddress" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.IsUsed")%></td>
		<td><asp:Literal id="ltrIsUsed" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Name")%></td>
		<td><asp:Literal id="ltrName" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.AddressLine")%></td>
		<td><asp:Literal id="ltrAddressLine" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.CityID")%></td>
		<td><asp:Literal id="ltrCityID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.DistrictID")%></td>
		<td><asp:Literal id="ltrDistrictID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.AreaID")%></td>
		<td><asp:Literal id="ltrAreaID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.TaxOffice")%></td>
		<td><asp:Literal id="ltrTaxOffice" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.TaxNo")%></td>
		<td><asp:Literal id="ltrTaxNo" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.ZipCode")%></td>
		<td><asp:Literal id="ltrZipCode" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Phone")%></td>
		<td><asp:Literal id="ltrPhone" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.CreateDate")%></td>
		<td><asp:Literal id="ltrCreateDate" runat="server"></asp:Literal></td>
	</tr>    
            </table>
        </div>
    </div>
</asp:Content>
