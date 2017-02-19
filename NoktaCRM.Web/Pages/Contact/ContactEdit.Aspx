<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="ContactEdit.aspx.cs" Inherits="ContactEdit" %>

<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section id="page-content">
        <div class="row-fluid">
            <div class="span12">
                <a class="btn btn-primary btn-mini" href="/contact-list"><%= res("nav.back.to.list") %></a>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-6">
                <div class="panel">
                    <div class="panel-body form-horizontal">
                        <div class="form-group">
                            <label class="col-md-3 control-label">İsim</label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.Name" ID="txtName" runat="server" MaxLength="30"></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">Soyisim</label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.Surname" ID="txtSurname" runat="server" MaxLength="30"></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">Cinsiyet</label>
                            <div class="col-md-9">
                                <web:MListBox CssClass="form-control" Rows="1" ID="ddlGender" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvGender" runat="server" ControlToValidate="ddlGender" keyError="req.Gender"></web:MReqValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">Şirket</label>
                            <div class="col-md-9">
                                <web:MListBox CssClass="form-control" Rows="1" ID="ddlCustomerID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvCustomerID" runat="server" ControlToValidate="ddlCustomerID" keyError="req.CustomerID"></web:MReqValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">Ünvan</label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.Title" ID="txtTitle" runat="server" MaxLength="150" keyPlaceholder="Firma Sahibi"></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.Email" ID="mlEmail" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.Email" ID="txtEmail" runat="server" MaxLength="60" keyPlaceholder="example@example.com"></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">Sabit Telefon</label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.Phone" ID="txtPhone" runat="server" MaxLength="30" keyPlaceholder="2163334455"></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.GSM" ID="mlGSM" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.GSM" ID="txtGSM" runat="server" MaxLength="30" keyPlaceholder="5954443322"></web:MTextbox>
                            </div>
                        </div>
                        <%--<div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.AddressID" ID="mlAddressID" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MListBox CssClass="form-control" Rows="1" ID="ddlAddressID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvAddressID" runat="server" ControlToValidate="ddlAddressID" keyError="req.AddressID"></web:MReqValidator></div>
                        </div>  --%>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.Description" ID="mlDescription" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.Description" ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" MaxLength="4000"></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.UpdateDate" ID="mlUpdateDate" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MDatePicker ID="dtUpdateDate" runat="server" AllowNull="False" Width="75px"></web:MDatePicker>
                            </div>
                        </div>
                        <div class="panel-footer text-right">
                            <web:MButton ID="btnSubmit" CssClass="btn btn-primary" runat="server" keyText="nav.ok"
                                OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
