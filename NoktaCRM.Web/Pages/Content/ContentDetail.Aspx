<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="ContentDetail.aspx.cs" Inherits="ContentDetail" %>
<%@ MasterType VirtualPath="~/Main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="span12">
            <a class="btn btn-primary btn-mini" href="/content-list"><%= res("nav.back.to.list") %></a>
            <a class="btn btn-primary btn-mini" href="/content-edit/<%= ContentID%>"><%= res("nav.edit") %></a>
            <a class="btn btn-primary btn-mini" href="/new-content"><%= res("nav.add.new") %></a>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <table id="tblData" runat="server" class="Form">

	<tr>
		<td><%=res("form.ContentID")%></td>
		<td><asp:Literal id="ltrContentID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Title")%></td>
		<td><asp:Literal id="ltrTitle" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Body")%></td>
		<td><asp:Literal id="ltrBody" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.MemberID")%></td>
		<td><asp:Literal id="ltrMemberID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.CreateDate")%></td>
		<td><asp:Literal id="ltrCreateDate" runat="server"></asp:Literal></td>
	</tr>    
            </table>
        </div>
    </div>
</asp:Content>
