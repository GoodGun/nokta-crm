<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="CountryDetail.aspx.cs" Inherits="CountryDetail" %>
<%@ MasterType VirtualPath="~/Main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="span12">
            <a class="btn btn-primary btn-mini" href="/country-list"><%= res("nav.back.to.list") %></a>
            <a class="btn btn-primary btn-mini" href="/country-edit/<%= CountryID%>"><%= res("nav.edit") %></a>
            <a class="btn btn-primary btn-mini" href="/new-country"><%= res("nav.add.new") %></a>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <table id="tblData" runat="server" class="Form">

	<tr>
		<td><%=res("form.CountryID")%></td>
		<td><asp:Literal id="ltrCountryID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.CountryName")%></td>
		<td><asp:Literal id="ltrCountryName" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Status")%></td>
		<td><asp:Literal id="ltrStatus" runat="server"></asp:Literal></td>
	</tr>    
            </table>
        </div>
    </div>
</asp:Content>
