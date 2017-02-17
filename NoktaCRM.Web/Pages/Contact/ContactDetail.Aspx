<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="ContactDetail.aspx.cs" Inherits="ContactDetail" %>
<%@ MasterType VirtualPath="~/Main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="span12">
            <a class="btn btn-primary btn-mini" href="/contact-list"><%= res("nav.back.to.list") %></a>
            <a class="btn btn-primary btn-mini" href="/contact-edit/<%= ContactID%>"><%= res("nav.edit") %></a>
            <a class="btn btn-primary btn-mini" href="/new-contact"><%= res("nav.add.new") %></a>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <table id="tblData" runat="server" class="Form">

	<tr>
		<td><%=res("form.ContactID")%></td>
		<td><asp:Literal id="ltrContactID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.ContactCode")%></td>
		<td><asp:Literal id="ltrContactCode" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.MemberID")%></td>
		<td><asp:Literal id="ltrMemberID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Name")%></td>
		<td><asp:Literal id="ltrName" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Surname")%></td>
		<td><asp:Literal id="ltrSurname" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Title")%></td>
		<td><asp:Literal id="ltrTitle" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Phone")%></td>
		<td><asp:Literal id="ltrPhone" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.GSM")%></td>
		<td><asp:Literal id="ltrGSM" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.CustomerID")%></td>
		<td><asp:Literal id="ltrCustomerID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.AddressID")%></td>
		<td><asp:Literal id="ltrAddressID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Gender")%></td>
		<td><asp:Literal id="ltrGender" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Email")%></td>
		<td><asp:Literal id="ltrEmail" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Description")%></td>
		<td><asp:Literal id="ltrDescription" runat="server"></asp:Literal></td>
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
