<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="ParamsDetail.aspx.cs" Inherits="ParamsDetail" %>
<%@ MasterType VirtualPath="~/Main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="span12">
            <a class="btn btn-primary btn-mini" href="/params-list"><%= res("nav.back.to.list") %></a>
            <a class="btn btn-primary btn-mini" href="/params-edit/<%= ParamsID%>"><%= res("nav.edit") %></a>
            <a class="btn btn-primary btn-mini" href="/new-params"><%= res("nav.add.new") %></a>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <table id="tblData" runat="server" class="Form">

	<tr>
		<td><%=res("form.TypeID")%></td>
		<td><asp:Literal id="ltrTypeID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.TypeCode")%></td>
		<td><asp:Literal id="ltrTypeCode" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.TypeName")%></td>
		<td><asp:Literal id="ltrTypeName" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Description")%></td>
		<td><asp:Literal id="ltrDescription" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.ObjectValue")%></td>
		<td><asp:Literal id="ltrObjectValue" runat="server"></asp:Literal></td>
	</tr>    
            </table>
        </div>
    </div>
</asp:Content>
