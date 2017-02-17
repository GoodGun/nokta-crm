<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderBox.ascx.cs" Inherits="UserControls_HeaderBox" %>
<!--===================================================-->
<header id="navbar">
    <div id="navbar-container" class="boxed">
        <!--Brand logo & name-->
        <!--================================-->
        <div class="navbar-header">
            <a href="<%= ConfigManager.Current.VirtualPath %>" class="navbar-brand">
                <img src="/assets/img/firm_logo.png" width="220" />
            </a>
        </div>
        <!--================================-->
        <!--End brand logo & name-->
        <!--Navbar Dropdown-->
        <!--================================-->
        <div class="navbar-content clearfix">
            <ul class="nav navbar-top-links pull-left">
                <!--Navigation toogle button-->
                <!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
                <li class="tgl-menu-btn">
                    <a class="mainnav-toggle" href="#"><i class="fa fa-navicon fa-lg"></i></a>
                </li>
            </ul>
            <ul class="nav navbar-top-links pull-right">
                <!--Profile toogle button-->
                <!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
                <li class="hidden-xs" id="toggleFullscreen">
                    <a class="fa fa-expand" data-toggle="fullscreen" href="#" role="button">
                        <span class="sr-only">Toggle fullscreen</span>
                    </a>
                </li>
                <!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
                <!--End Profile toogle button-->
                <!--User dropdown-->
                <!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
                <li id="dropdown-user" class="dropdown">
                    <a href="#" data-toggle="dropdown" class="dropdown-toggle text-right">
                        <span class="pull-right">
                            <img class="img-circle img-user media-object" src="/assets/img/user.png" alt="Profile Picture">
                        </span>
                        <div class="username hidden-xs"><%= AdminName %></div>
                    </a>
                </li>
            </ul>
        </div>
        <!--================================-->
        <!--End Navbar Dropdown-->
    </div>
</header>
<!--===================================================-->
