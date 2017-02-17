<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="ContentEdit.aspx.cs" Inherits="ContentEdit" %>

<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
      <section id="page-content">
                        <div class="row">
                        <div class="col-lg-6">
                        <div class="panel">
            <div class="panel-body form-horizontal">
		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Title" ID="mlTitle" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.Title" ID="txtTitle" runat="server" MaxLength="100"></web:MTextbox><web:MReqValidator CssClass="vld" ID="rfvTitle" runat="server" ControlToValidate="txtTitle" keyError="req.Title"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Body" ID="mlBody" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.Body" ID="txtBody" runat="server" TextMode="MultiLine" Rows="5" MaxLength="-1"></web:MTextbox><web:MReqValidator CssClass="vld" ID="rfvBody" runat="server" ControlToValidate="txtBody" keyError="req.Body"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.MemberID" ID="mlMemberID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlMemberID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvMemberID" runat="server" ControlToValidate="ddlMemberID" keyError="req.MemberID"></web:MReqValidator></div>
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
