<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="MemberDetail.aspx.cs" Inherits="MemberDetail" %>
<%@ MasterType VirtualPath="~/Main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="span12">
            <a class="btn btn-primary btn-mini" href="/member-list"><%= res("nav.back.to.list") %></a>
            <a class="btn btn-primary btn-mini" href="/member-edit/<%= MemberID%>"><%= res("nav.edit") %></a>
            <a class="btn btn-primary btn-mini" href="/new-member"><%= res("nav.add.new") %></a>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <table id="tblData" runat="server" class="Form">

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
		<td><%=res("form.Username")%></td>
		<td><asp:Literal id="ltrUsername" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Email")%></td>
		<td><asp:Literal id="ltrEmail" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.PasswordHashed")%></td>
		<td><asp:Literal id="ltrPasswordHashed" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.MemberTypeID")%></td>
		<td><asp:Literal id="ltrMemberTypeID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Status")%></td>
		<td><asp:Literal id="ltrStatus" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.CreateDate")%></td>
		<td><asp:Literal id="ltrCreateDate" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.LastUpdate")%></td>
		<td><asp:Literal id="ltrLastUpdate" runat="server"></asp:Literal></td>
	</tr>    
            </table>
        </div>
    </div>
</asp:Content>
