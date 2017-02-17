<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="CityEdit.aspx.cs" Inherits="CityEdit" %>

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
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.CountryID" ID="mlCountryID" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MListBox CssClass="form-control" Rows="1" id="ddlCountryID" runat="server"></web:MListBox><web:MReqValidator CssClass="vld" ID="rfvCountryID" runat="server" ControlToValidate="ddlCountryID" keyError="req.CountryID"></web:MReqValidator></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.CityName" ID="mlCityName" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MTextbox CssClass="form-control" keyTooltip="tooltip.CityName" ID="txtCityName" runat="server" MaxLength="100"></web:MTextbox></div>
		</div>		<div class="form-group">
			<label class="col-md-3 control-label"><web:MLiteral keyData="form.Status" ID="mlStatus" runat="server"></web:MLiteral></label>
			<div class="col-md-9"><web:MCheckBox id="chkStatus" CssClass="rbl fancy" keyText="form.Status" Checked="False" runat="server"></web:MCheckBox></div>
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
