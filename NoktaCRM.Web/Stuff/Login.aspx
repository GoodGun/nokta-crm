<%@ Page Language="C#" AutoEventWireup="false" CodeFile="Login.aspx.cs" Inherits="Stuff_Login" %>

<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>CRM - Nokta Bilişim</title>
    <link rel="shortcut icon" href="/assets/img/favicon.png">
    <!--STYLESHEET-->
    <!--=================================================-->
    <!--Roboto Slab Font [ OPTIONAL ] -->
    <link href="https://fonts.googleapis.com/css?family=Roboto+Slab:300,400,700|Roboto:300,400,700" rel="stylesheet">
    <!--Bootstrap Stylesheet [ REQUIRED ]-->
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet">
    <!--Jasmine Stylesheet [ REQUIRED ]-->
    <link href="/assets/css/style.css" rel="stylesheet">
    <!--Font Awesome [ OPTIONAL ]-->
    <link href="/assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <!--Switchery [ OPTIONAL ]-->
    <link href="/assets/plugins/switchery/switchery.min.css" rel="stylesheet">
    <!--Bootstrap Select [ OPTIONAL ]-->
    <link href="plugins/bootstrap-select/bootstrap-select.min.css" rel="stylesheet">
    <!--Demo [ DEMONSTRATION ]-->
    <link href="/assets/css/demo/jasmine.css" rel="stylesheet">
    <!--SCRIPT-->
    <!--=================================================-->
    <!--Page Load Progress Bar [ OPTIONAL ]-->
    <link href="plugins/pace/pace.min.css" rel="stylesheet">
    <script src="plugins/pace/pace.min.js"></script>
</head>
<!-- END HEAD -->
<!-- BEGIN BODY -->
<body>
    <!-- BEGIN LOGIN -->
    <div id="container" class="cls-container">
        <div class="lock-wrapper">
            <div class="panel lock-box">
                <div class="center">
                    <img alt="" src="/assets/img/firm_logo.png" height="50" />
                </div>
                <div class="row">
                    <!-- BEGIN LOGIN FORM -->
                    <form id="form1" runat="server" class="form-inline" enctype="multipart/form-data">
                        <div class="form-group col-md-12 col-sm-12 col-xs-12 pad-5">
                            <div class="text-left">
                                <web:MTextbox CssClass="form-control" ID="txtEmail" runat="server" MaxLength="50" autocomplete="off" placeholder="E-Posta" required></web:MTextbox>
                                <web:MReqValidator ValidationGroup="login" CssClass="vld" ID="rl1" runat="server" ControlToValidate="txtEmail" keyError="req.Email" Display="None"></web:MReqValidator>
                            </div>
                            <div class="text-left">
                                <web:MTextbox ID="txtPassword" CssClass="form-control lock-input" runat="server" TextMode="Password" MaxLength="16" placeholder="Şifre" required></web:MTextbox>
                                <web:MReqValidator ValidationGroup="login" CssClass="vld" ID="rl2" runat="server" ControlToValidate="txtPassword" keyError="req.Password" Display="None"></web:MReqValidator>
                            </div>
                            <web:MButton ID="btnSubmit" ValidationGroup="login" CssClass="btn btn-block btn-primary" runat="server" keyText="nav.login" OnClick="btnSubmit_Click" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
        <!-- END LOGIN FORM -->
    </div>
    <!-- END LOGIN -->
    <!--===================================================-->
    <!-- END OF CONTAINER -->
    <!--JAVASCRIPT-->
    <!--=================================================-->
    <!--jQuery [ REQUIRED ]-->
    <script src="/assets/js/jquery-2.1.1.min.js"></script>
    <!--BootstrapJS [ RECOMMENDED ]-->
    <script src="/assets/js/bootstrap.min.js"></script>
    <!--Fast Click [ OPTIONAL ]-->
    <script src="/assets/plugins/fast-click/fastclick.min.js"></script>
    <!--Jasmine Admin [ RECOMMENDED ]-->
    <script src="/assets/js/scripts.js"></script>
    <!--Switchery [ OPTIONAL ]-->
    <script src="/assets/plugins/switchery/switchery.min.js"></script>
    <!--Bootstrap Select [ OPTIONAL ]-->
    <script src="/assets/plugins/bootstrap-select/bootstrap-select.min.js"></script>
</body>
<!-- END BODY -->
</html>
