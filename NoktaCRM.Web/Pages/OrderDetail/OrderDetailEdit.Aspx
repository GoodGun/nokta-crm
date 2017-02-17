<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="OrderDetailEdit.aspx.cs" Inherits="OrderDetailEdit" %>

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
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.ProductID" ID="mlProductID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlProductID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvProductID" runat="server" ControlToValidate="ddlProductID" keyError="req.ProductID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Quantity" ID="mlQuantity" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MNumberbox CssClass="form-control" ID="txtQuantity" numberType="Int" runat="server"></web:MNumberbox><web:MReqValidator CssClass="vld" ID="rfvQuantity" runat="server" ControlToValidate="txtQuantity" keyError="req.Quantity"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.TaxRate" ID="mlTaxRate" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MNumberbox CssClass="form-control" ID="txtTaxRate" numberType="Int" runat="server"></web:MNumberbox><web:MReqValidator CssClass="vld" ID="rfvTaxRate" runat="server" ControlToValidate="txtTaxRate" keyError="req.TaxRate"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.UnitPrice" ID="mlUnitPrice" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MNumberbox CssClass="form-control" ID="txtUnitPrice" numberType="Decimal" runat="server"></web:MNumberbox><web:MReqValidator CssClass="vld" ID="rfvUnitPrice" runat="server" ControlToValidate="txtUnitPrice" keyError="req.UnitPrice"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.DiscountAmount" ID="mlDiscountAmount" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MNumberbox CssClass="form-control" ID="txtDiscountAmount" numberType="Decimal" runat="server"></web:MNumberbox><web:MReqValidator CssClass="vld" ID="rfvDiscountAmount" runat="server" ControlToValidate="txtDiscountAmount" keyError="req.DiscountAmount"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.TotalPrice" ID="mlTotalPrice" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MNumberbox CssClass="form-control" ID="txtTotalPrice" numberType="Decimal" runat="server"></web:MNumberbox><web:MReqValidator CssClass="vld" ID="rfvTotalPrice" runat="server" ControlToValidate="txtTotalPrice" keyError="req.TotalPrice"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.FinalPrice" ID="mlFinalPrice" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MNumberbox CssClass="form-control" ID="txtFinalPrice" numberType="Decimal" runat="server"></web:MNumberbox><web:MReqValidator CssClass="vld" ID="rfvFinalPrice" runat="server" ControlToValidate="txtFinalPrice" keyError="req.FinalPrice"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.CurrencyID" ID="mlCurrencyID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlCurrencyID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvCurrencyID" runat="server" ControlToValidate="ddlCurrencyID" keyError="req.CurrencyID"></web:MReqValidator></div>
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
