<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="ProductDetail.aspx.cs" Inherits="ProductDetail" %>
<%@ MasterType VirtualPath="~/Main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="span12">
            <a class="btn btn-primary btn-mini" href="/product-list"><%= res("nav.back.to.list") %></a>
            <a class="btn btn-primary btn-mini" href="/product-edit/<%= ProductID%>"><%= res("nav.edit") %></a>
            <a class="btn btn-primary btn-mini" href="/new-product"><%= res("nav.add.new") %></a>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <table id="tblData" runat="server" class="Form">

	<tr>
		<td><%=res("form.ProductID")%></td>
		<td><asp:Literal id="ltrProductID" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.ProductCode")%></td>
		<td><asp:Literal id="ltrProductCode" runat="server"></asp:Literal></td>
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
		<td><%=res("form.ProductName")%></td>
		<td><asp:Literal id="ltrProductName" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.ProductGroup")%></td>
		<td><asp:Literal id="ltrProductGroup" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.ProductType")%></td>
		<td><asp:Literal id="ltrProductType" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Unit")%></td>
		<td><asp:Literal id="ltrUnit" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Tax")%></td>
		<td><asp:Literal id="ltrTax" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Producer")%></td>
		<td><asp:Literal id="ltrProducer" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Price1")%></td>
		<td><asp:Literal id="ltrPrice1" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Price2")%></td>
		<td><asp:Literal id="ltrPrice2" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.CurrencyPrice")%></td>
		<td><asp:Literal id="ltrCurrencyPrice" runat="server"></asp:Literal></td>
	</tr>
	<tr>
		<td><%=res("form.Description")%></td>
		<td><asp:Literal id="ltrDescription" runat="server"></asp:Literal></td>
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
		<td><%=res("form.UpdateDate")%></td>
		<td><asp:Literal id="ltrUpdateDate" runat="server"></asp:Literal></td>
	</tr>    
            </table>
        </div>
    </div>
</asp:Content>
