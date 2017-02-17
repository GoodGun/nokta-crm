<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="ActivityEdit.aspx.cs" Inherits="ActivityEdit" %>

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
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.OrderID" ID="mlOrderID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlOrderID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvOrderID" runat="server" ControlToValidate="ddlOrderID" keyError="req.OrderID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.ActivityCode" ID="mlActivityCode" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.ActivityCode" ID="txtActivityCode" runat="server" MaxLength="15"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Name" ID="mlName" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.Name" ID="txtName" runat="server" MaxLength="50"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Description" ID="mlDescription" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.Description" ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" MaxLength="4000"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.MemberID" ID="mlMemberID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlMemberID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvMemberID" runat="server" ControlToValidate="ddlMemberID" keyError="req.MemberID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.CustomerID" ID="mlCustomerID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlCustomerID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvCustomerID" runat="server" ControlToValidate="ddlCustomerID" keyError="req.CustomerID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.ContactID" ID="mlContactID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlContactID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvContactID" runat="server" ControlToValidate="ddlContactID" keyError="req.ContactID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.ActivityTypeID" ID="mlActivityTypeID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlActivityTypeID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvActivityTypeID" runat="server" ControlToValidate="ddlActivityTypeID" keyError="req.ActivityTypeID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.ActivityStatusID" ID="mlActivityStatusID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlActivityStatusID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvActivityStatusID" runat="server" ControlToValidate="ddlActivityStatusID" keyError="req.ActivityStatusID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.CreatedBy" ID="mlCreatedBy" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MNumberbox CssClass="form-control" ID="txtCreatedBy" numberType="Int" runat="server"></web:MNumberbox><web:MReqValidator CssClass="vld" ID="rfvCreatedBy" runat="server" ControlToValidate="txtCreatedBy" keyError="req.CreatedBy"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.ActivityDate" ID="mlActivityDate" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MDatePicker ID="dtActivityDate" runat="server" AllowNull="False" Width="75px"></web:MDatePicker></div>
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
