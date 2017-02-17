<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="MemberEdit.aspx.cs" Inherits="MemberEdit" %>

<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <section id="page-content">
        <div class="row">
            <div class="col-lg-6">
                <div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title"><asp:Literal ID="ltrTitle" runat="server"></asp:Literal></h3>
                    </div>
                    <div class="panel-body form-horizontal">
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.Name" ID="mlName" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.Name" ID="txtName" runat="server" MaxLength="30" required></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.Surname" ID="mlSurname" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.Surname" ID="txtSurname" runat="server" MaxLength="30" required></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.Email" ID="mlEmail" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.Email" ID="txtEmail" runat="server" MaxLength="60" required></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                Şifre</label>
                            <div class="col-md-9">
                                <web:MTextbox CssClass="form-control" keyTooltip="tooltip.PasswordHashed" ID="txtPasswordHashed" runat="server" MaxLength="50" required></web:MTextbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                Üyelik Tipi</label>
                            <div class="col-md-9">
                                <web:MListBox CssClass="form-control" Rows="1" ID="ddlMemberTypeID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvMemberTypeID" runat="server" ControlToValidate="ddlMemberTypeID" keyError="req.MemberTypeID"></web:MReqValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">
                                <web:MLiteral keyData="form.Status" ID="mlStatus" runat="server"></web:MLiteral></label>
                            <div class="col-md-9">
                                <web:MCheckbox ID="chkStatus" CssClass="rbl fancy" keyText="form.Status" Checked="False" runat="server"></web:MCheckbox>
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
