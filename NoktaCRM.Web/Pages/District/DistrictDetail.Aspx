<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="DistrictDetail.aspx.cs" Inherits="DistrictDetail" %>
<%@ MasterType VirtualPath="~/Main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="span12">
            <a class="btn btn-primary btn-mini" href="/district-list"><%= res("nav.back.to.list") %></a>
            <a class="btn btn-primary btn-mini" href="/district-edit/<%= DistrictID%>"><%= res("nav.edit") %></a>
            <a class="btn btn-primary btn-mini" href="/new-district"><%= res("nav.add.new") %></a>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <table id="tblData" runat="server" class="Form">

	<tr>
		<td><%=res("form.DistrictID")%></td>
		<td><asp:Literal id="ltrDistrictID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.CityID")%></td>
		<td><asp:Literal id="ltrCityID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.DistrictName")%></td>
		<td><asp:Literal id="ltrDistrictName" runat="server"></asp:Literal></td>
	</tr>    
            </table>
        </div>
    </div>
</asp:Content>
