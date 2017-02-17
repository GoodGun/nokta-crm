<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="ProductEdit.aspx.cs" Inherits="ProductEdit" %>

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
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.ProductCode" ID="mlProductCode" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.ProductCode" ID="txtProductCode" runat="server" MaxLength="15"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.ReferenceNo" ID="mlReferenceNo" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.ReferenceNo" ID="txtReferenceNo" runat="server" MaxLength="25"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.MemberID" ID="mlMemberID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlMemberID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvMemberID" runat="server" ControlToValidate="ddlMemberID" keyError="req.MemberID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.ProductName" ID="mlProductName" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.ProductName" ID="txtProductName" runat="server" MaxLength="150"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.ProductGroup" ID="mlProductGroup" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.ProductGroup" ID="txtProductGroup" runat="server" MaxLength="50"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.ProductType" ID="mlProductType" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.ProductType" ID="txtProductType" runat="server" MaxLength="50"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Unit" ID="mlUnit" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.Unit" ID="txtUnit" runat="server" MaxLength="10"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Tax" ID="mlTax" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MNumberbox CssClass="form-control" ID="txtTax" numberType="Int" runat="server"></web:MNumberbox><web:MReqValidator CssClass="vld" ID="rfvTax" runat="server" ControlToValidate="txtTax" keyError="req.Tax"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Producer" ID="mlProducer" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.Producer" ID="txtProducer" runat="server" MaxLength="10"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Price1" ID="mlPrice1" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MNumberbox CssClass="form-control" ID="txtPrice1" numberType="Decimal" runat="server"></web:MNumberbox><web:MReqValidator CssClass="vld" ID="rfvPrice1" runat="server" ControlToValidate="txtPrice1" keyError="req.Price1"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Price2" ID="mlPrice2" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MNumberbox CssClass="form-control" ID="txtPrice2" numberType="Decimal" runat="server"></web:MNumberbox><web:MReqValidator CssClass="vld" ID="rfvPrice2" runat="server" ControlToValidate="txtPrice2" keyError="req.Price2"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.CurrencyPrice" ID="mlCurrencyPrice" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MNumberbox CssClass="form-control" ID="txtCurrencyPrice" numberType="Decimal" runat="server"></web:MNumberbox><web:MReqValidator CssClass="vld" ID="rfvCurrencyPrice" runat="server" ControlToValidate="txtCurrencyPrice" keyError="req.CurrencyPrice"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Description" ID="mlDescription" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.Description" ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" MaxLength="1500"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Status" ID="mlStatus" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MCheckBox id="chkStatus" CssClass="rbl fancy" keyText="form.Status" Checked="True" runat="server"></web:MCheckBox></div>
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
