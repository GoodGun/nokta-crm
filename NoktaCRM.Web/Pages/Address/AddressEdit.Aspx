<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="AddressEdit.aspx.cs" Inherits="AddressEdit" %>

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
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.CustomerID" ID="mlCustomerID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlCustomerID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvCustomerID" runat="server" ControlToValidate="ddlCustomerID" keyError="req.CustomerID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.AddressName" ID="mlAddressName" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.AddressName" ID="txtAddressName" runat="server" MaxLength="100"></web:MTextbox><web:MReqValidator CssClass="vld" ID="rfvAddressName" runat="server" ControlToValidate="txtAddressName" keyError="req.AddressName"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.IsBillingAddress" ID="mlIsBillingAddress" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MCheckBox id="chkIsBillingAddress" CssClass="rbl fancy" keyText="form.IsBillingAddress" Checked="False" runat="server"></web:MCheckBox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.IsUsed" ID="mlIsUsed" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MCheckBox id="chkIsUsed" CssClass="rbl fancy" keyText="form.IsUsed" Checked="False" runat="server"></web:MCheckBox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Name" ID="mlName" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.Name" ID="txtName" runat="server" MaxLength="100"></web:MTextbox><web:MReqValidator CssClass="vld" ID="rfvName" runat="server" ControlToValidate="txtName" keyError="req.Name"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.AddressLine" ID="mlAddressLine" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.AddressLine" ID="txtAddressLine" runat="server" TextMode="MultiLine" Rows="5" MaxLength="1000"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.CityID" ID="mlCityID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlCityID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvCityID" runat="server" ControlToValidate="ddlCityID" keyError="req.CityID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.DistrictID" ID="mlDistrictID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlDistrictID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvDistrictID" runat="server" ControlToValidate="ddlDistrictID" keyError="req.DistrictID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.AreaID" ID="mlAreaID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlAreaID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvAreaID" runat="server" ControlToValidate="ddlAreaID" keyError="req.AreaID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.TaxOffice" ID="mlTaxOffice" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.TaxOffice" ID="txtTaxOffice" runat="server" MaxLength="40"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.TaxNo" ID="mlTaxNo" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.TaxNo" ID="txtTaxNo" runat="server" MaxLength="25"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.ZipCode" ID="mlZipCode" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.ZipCode" ID="txtZipCode" runat="server" MaxLength="10"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Phone" ID="mlPhone" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.Phone" ID="txtPhone" runat="server" MaxLength="20"></web:MTextbox></div>
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
