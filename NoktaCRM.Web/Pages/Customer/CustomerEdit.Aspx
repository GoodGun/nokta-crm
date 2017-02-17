<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="CustomerEdit.aspx.cs" Inherits="CustomerEdit" %>

<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section id="page-content">
        <div class="row">
            <div class="col-lg-6">
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <asp:Literal ID="ltrTitle" runat="server"></asp:Literal></h3>
                    </div>
                    <div class="panel-body form-horizontal">
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.CustomerCode" ID="mlCustomerCode" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.CustomerCode" ID="txtCustomerCode" runat="server" MaxLength="10"></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.ReferenceNo" ID="mlReferenceNo" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.ReferenceNo" ID="txtReferenceNo" runat="server" MaxLength="50"></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.MemberID" ID="mlMemberID" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MListBox CssClass="form-control" Rows="1" ID="ddlMemberID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvMemberID" runat="server" ControlToValidate="ddlMemberID" keyError="req.MemberID"></web:MReqValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.SellerMemberID" ID="mlSellerMemberID" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MListBox CssClass="form-control" Rows="1" ID="ddlSellerMemberID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvSellerMemberID" runat="server" ControlToValidate="ddlSellerMemberID" keyError="req.SellerMemberID"></web:MReqValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.Name" ID="mlName" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.Name" ID="txtName" runat="server" MaxLength="150"></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.CustomerTypeID" ID="mlCustomerTypeID" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MListBox CssClass="form-control" Rows="1" ID="ddlCustomerTypeID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvCustomerTypeID" runat="server" ControlToValidate="ddlCustomerTypeID" keyError="req.CustomerTypeID"></web:MReqValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.SectorTypeID" ID="mlSectorTypeID" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MListBox CssClass="form-control" Rows="1" ID="ddlSectorTypeID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvSectorTypeID" runat="server" ControlToValidate="ddlSectorTypeID" keyError="req.SectorTypeID"></web:MReqValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.MainReseller" ID="mlMainReseller" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.MainReseller" ID="txtMainReseller" runat="server" MaxLength="50"></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.Reseller" ID="mlReseller" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.Reseller" ID="txtReseller" runat="server" MaxLength="50"></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.Description" ID="mlDescription" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.Description" ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" MaxLength="4000"></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.Code1" ID="mlCode1" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.Code1" ID="txtCode1" runat="server" MaxLength="150"></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.Code2" ID="mlCode2" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.Code2" ID="txtCode2" runat="server" MaxLength="150"></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.Code3" ID="mlCode3" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.Code3" ID="txtCode3" runat="server" MaxLength="150"></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.BillingAddressID" ID="mlBillingAddressID" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MListBox CssClass="form-control" Rows="1" ID="ddlBillingAddressID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvBillingAddressID" runat="server" ControlToValidate="ddlBillingAddressID" keyError="req.BillingAddressID"></web:MReqValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.DeliveryAddressID" ID="mlDeliveryAddressID" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MListBox CssClass="form-control" Rows="1" ID="ddlDeliveryAddressID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvDeliveryAddressID" runat="server" ControlToValidate="ddlDeliveryAddressID" keyError="req.DeliveryAddressID"></web:MReqValidator>
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
