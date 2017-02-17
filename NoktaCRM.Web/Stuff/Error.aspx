<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Error.aspx.cs" Inherits="Stuff_Error" %>

<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="span12 mt">
            <div class="alert alert-error">
                <asp:Literal ID="ltrMessage" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
</asp:Content>
