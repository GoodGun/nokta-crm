<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="OrderMainEdit.aspx.cs" Inherits="OrderMainEdit" %>

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
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.OrderCode" ID="mlOrderCode" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.OrderCode" ID="txtOrderCode" runat="server" MaxLength="25"></web:MTextbox></div>
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
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.AddressID" ID="mlAddressID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlAddressID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvAddressID" runat="server" ControlToValidate="ddlAddressID" keyError="req.AddressID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.ReferenceNo" ID="mlReferenceNo" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.ReferenceNo" ID="txtReferenceNo" runat="server" MaxLength="50"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.OrderTypeID" ID="mlOrderTypeID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlOrderTypeID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvOrderTypeID" runat="server" ControlToValidate="ddlOrderTypeID" keyError="req.OrderTypeID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.OrderContent" ID="mlOrderContent" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.OrderContent" ID="txtOrderContent" runat="server" TextMode="MultiLine" Rows="5" MaxLength="4000"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.CargoName" ID="mlCargoName" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.CargoName" ID="txtCargoName" runat="server" MaxLength="150"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.BuyerOrderNo" ID="mlBuyerOrderNo" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.BuyerOrderNo" ID="txtBuyerOrderNo" runat="server" MaxLength="50"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Description" ID="mlDescription" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.Description" ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" MaxLength="4000"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.TotalPrice" ID="mlTotalPrice" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MNumberbox CssClass="form-control" ID="txtTotalPrice" numberType="Decimal" runat="server"></web:MNumberbox><web:MReqValidator CssClass="vld" ID="rfvTotalPrice" runat="server" ControlToValidate="txtTotalPrice" keyError="req.TotalPrice"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.TaxAmount" ID="mlTaxAmount" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MNumberbox CssClass="form-control" ID="txtTaxAmount" numberType="Decimal" runat="server"></web:MNumberbox><web:MReqValidator CssClass="vld" ID="rfvTaxAmount" runat="server" ControlToValidate="txtTaxAmount" keyError="req.TaxAmount"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.DiscountAmount" ID="mlDiscountAmount" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MNumberbox CssClass="form-control" ID="txtDiscountAmount" numberType="Decimal" runat="server"></web:MNumberbox><web:MReqValidator CssClass="vld" ID="rfvDiscountAmount" runat="server" ControlToValidate="txtDiscountAmount" keyError="req.DiscountAmount"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.FinalPrice" ID="mlFinalPrice" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MNumberbox CssClass="form-control" ID="txtFinalPrice" numberType="Decimal" runat="server"></web:MNumberbox><web:MReqValidator CssClass="vld" ID="rfvFinalPrice" runat="server" ControlToValidate="txtFinalPrice" keyError="req.FinalPrice"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.OrderStatusID" ID="mlOrderStatusID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlOrderStatusID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvOrderStatusID" runat="server" ControlToValidate="ddlOrderStatusID" keyError="req.OrderStatusID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.FileName" ID="mlFileName" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.FileName" ID="txtFileName" runat="server" MaxLength="150"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.OrderDate" ID="mlOrderDate" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MDatePicker ID="dtOrderDate" runat="server" AllowNull="False" Width="75px"></web:MDatePicker></div>
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
