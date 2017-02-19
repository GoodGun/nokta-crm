<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="ContactDetail.aspx.cs" Inherits="ContactDetail" %>

<%@ MasterType VirtualPath="~/Main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section id="page-content">

        <div class="row-fluid">
            <div class="span12">
                <a class="btn btn-primary btn-mini" href="/contact-list"><%= res("nav.back.to.list") %></a>
                <a class="btn btn-primary btn-mini" href="/contact-edit/<%= ContactID%>"><%= res("nav.edit") %></a>
                <a class="btn btn-primary btn-mini" href="/new-contact"><%= res("nav.add.new") %></a>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-6">
                <div class="panel">
                    <div class="panel-body form-horizontal">
                        <div class="form-group">
                            <label class="col-md-3 control-label"><%=res("form.ContactID")%></label>
                            <div class="col-md-9">
                                <asp:Literal ID="ltrContactID" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label"><%=res("form.ContactCode")%></label>
                            <div class="col-md-9">
                                <asp:Literal ID="ltrContactCode" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label"><%=res("form.MemberID")%></label>
                            <div class="col-md-9">
                                <asp:Literal ID="ltrMemberID" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label"><%=res("form.Name")%></label>
                            <div class="col-md-9">
                                <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label"><%=res("form.Surname")%></label>
                            <div class="col-md-9">
                                <asp:Literal ID="ltrSurname" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label"><%=res("form.Title")%></label>
                            <div class="col-md-9">
                                <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label"><%=res("form.Phone")%></label>
                            <div class="col-md-9">
                                <asp:Literal ID="ltrPhone" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label"><%=res("form.GSM")%></label>
                            <div class="col-md-9">
                                <asp:Literal ID="ltrGSM" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label"><%=res("form.CustomerID")%></label>
                            <div class="col-md-9">
                                <asp:Literal ID="ltrCustomerID" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label"><%=res("form.AddressID")%></label>
                            <div class="col-md-9">
                                <asp:Literal ID="ltrAddressID" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label"><%=res("form.Gender")%></label>
                            <div class="col-md-9">
                                <asp:Literal ID="ltrGender" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label"><%=res("form.Email")%></label>
                            <div class="col-md-9">
                                <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label"><%=res("form.Description")%></label>
                            <div class="col-md-9">
                                <asp:Literal ID="ltrDescription" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label"><%=res("form.CreateDate")%></label>
                            <div class="col-md-9">
                                <asp:Literal ID="ltrCreateDate" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label"><%=res("form.UpdateDate")%></label>
                            <div class="col-md-9">
                                <asp:Literal ID="ltrUpdateDate" runat="server"></asp:Literal>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
