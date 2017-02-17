<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="ContactEdit.aspx.cs" Inherits="ContactEdit" %>

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
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.ContactCode" ID="mlContactCode" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.ContactCode" ID="txtContactCode" runat="server" MaxLength="10"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.MemberID" ID="mlMemberID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlMemberID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvMemberID" runat="server" ControlToValidate="ddlMemberID" keyError="req.MemberID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Name" ID="mlName" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.Name" ID="txtName" runat="server" MaxLength="30"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Surname" ID="mlSurname" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.Surname" ID="txtSurname" runat="server" MaxLength="30"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Title" ID="mlTitle" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.Title" ID="txtTitle" runat="server" MaxLength="150"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Phone" ID="mlPhone" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.Phone" ID="txtPhone" runat="server" MaxLength="30"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.GSM" ID="mlGSM" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.GSM" ID="txtGSM" runat="server" MaxLength="30"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.CustomerID" ID="mlCustomerID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlCustomerID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvCustomerID" runat="server" ControlToValidate="ddlCustomerID" keyError="req.CustomerID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.AddressID" ID="mlAddressID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlAddressID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvAddressID" runat="server" ControlToValidate="ddlAddressID" keyError="req.AddressID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Gender" ID="mlGender" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MNumberbox CssClass="form-control" ID="txtGender" numberType="Int" runat="server"></web:MNumberbox><web:MReqValidator CssClass="vld" ID="rfvGender" runat="server" ControlToValidate="txtGender" keyError="req.Gender"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Email" ID="mlEmail" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.Email" ID="txtEmail" runat="server" MaxLength="60"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Description" ID="mlDescription" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.Description" ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" MaxLength="4000"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.UpdateDate" ID="mlUpdateDate" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MDatePicker ID="dtUpdateDate" runat="server" AllowNull="False" Width="75px"></web:MDatePicker></div>
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
