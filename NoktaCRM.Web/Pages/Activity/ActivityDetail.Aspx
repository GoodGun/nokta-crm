<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="ActivityDetail.aspx.cs" Inherits="ActivityDetail" %>
<%@ MasterType VirtualPath="~/Main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="span12">
            <a class="btn btn-primary btn-mini" href="/activity-list"><%= res("nav.back.to.list") %></a>
            <a class="btn btn-primary btn-mini" href="/activity-edit/<%= ActivityID%>"><%= res("nav.edit") %></a>
            <a class="btn btn-primary btn-mini" href="/new-activity"><%= res("nav.add.new") %></a>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <table id="tblData" runat="server" class="Form">

	<tr>
		<td><%=res("form.ActivityID")%></td>
		<td><asp:Literal id="ltrActivityID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.OrderID")%></td>
		<td><asp:Literal id="ltrOrderID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.ActivityCode")%></td>
		<td><asp:Literal id="ltrActivityCode" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Name")%></td>
		<td><asp:Literal id="ltrName" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Description")%></td>
		<td><asp:Literal id="ltrDescription" runat="server"></asp:Literal></td>
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
		<td><%=res("form.ActivityTypeID")%></td>
		<td><asp:Literal id="ltrActivityTypeID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.ActivityStatusID")%></td>
		<td><asp:Literal id="ltrActivityStatusID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.CreatedBy")%></td>
		<td><asp:Literal id="ltrCreatedBy" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.ActivityDate")%></td>
		<td><asp:Literal id="ltrActivityDate" runat="server"></asp:Literal></td>
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
