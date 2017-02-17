<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SidebarMenu.ascx.cs" Inherits="UserControls_SidebarMenu" %>
<!--MAIN NAVIGATION-->
<!--===================================================-->
<nav id="mainnav-container">
    <div id="mainnav">
        <!--Menu-->
        <!--================================-->
        <div id="mainnav-menu-wrap">
            <div class="nano">
                <div class="nano-content">
                    <ul id="mainnav-menu" class="list-group">
                        <!--Category name-->
                        <li class="list-header">Menü</li>
                        <!--Menu list item-->
                        <li>
                            <a href="<%= ConfigManager.Current.VirtualPath %>/customer-list">
                                <i class="fa fa-venus-mars"></i>
                                <span class="menu-title">Müşteriler</span>
                            </a>
                        </li>
                        <li>
                            <a href="<%= ConfigManager.Current.VirtualPath %>/contact-list">
                                <i class="fa fa-user"></i>
                                <span class="menu-title">Yetkili Kişiler</span>
                            </a>
                        </li>
                        <li>
                            <a href="<%= ConfigManager.Current.VirtualPath %>/activity-list">
                                <i class="fa fa-pencil-square-o"></i>
                                <span class="menu-title">Aktiviteler</span>
                            </a>
                        </li>
                        <li>
                            <a href="<%= ConfigManager.Current.VirtualPath %>/product-list">
                                <i class="fa fa-tags"></i>
                                <span class="menu-title">Ürünler</span>
                            </a>
                        </li>
                        <li>
                            <a href="<%= ConfigManager.Current.VirtualPath %>/ordermain-list">
                                <i class="fa fa-suitcase"></i>
                                <span class="menu-title">Teklifler</span>
                            </a>
                        </li>
                        <li>
                            <a href="javascript:void(0)">
                                <i class="fa fa-shopping-cart"></i>
                                <span class="menu-title">Satın Almalar</span>
                            </a>
                        </li>
                        <li>
                            <a href="javascript:void(0)">
                                <i class="fa fa-bar-chart"></i>
                                <span class="menu-title">Raporlar</span>
                            </a>
                        </li>
                        <li>
                            <a href="<%= ConfigManager.Current.VirtualPath %>/member-list">
                                <i class="fa fa-users"></i>
                                <span class="menu-title">Kullanıcılar</span>
                            </a>
                        </li>
                        <li>
                            <a href="<%= ConfigManager.Current.VirtualPath %>?Logout=1">
                                <i class="fa fa-power-off"></i>
                                <span class="menu-title">Çıkış</span>
                            </a>
                        </li>
                    </ul>
                </div>
                <!--================================-->
                <!--End widget-->
            </div>
        </div>
    </div>
    <!--================================-->
    <!--End menu-->
</nav>
<!--===================================================-->
