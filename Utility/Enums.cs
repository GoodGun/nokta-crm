using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utility;

public class Current
{
    public static int PageSize { get { return CookieUtil.GetCookieItem<int>("ps", ConfigManager.Current.DefaultPageSize); } set { CookieUtil.SetCookieItem<int>("ps", value); } }
}
public enum UserRole
{
    Guest = 0, // Ziyaretçi
    User = 1, // Üye
    SuperUser = 2, // Süper Kullanıcı
    Editor = 3, // Editör
    Moderator = 4, // Moderatör
    Admin = 5, // Administrator
    SuperAdmin = 6, //Süper admin - sadece yazılım
    WebServiceUser = 7 //Service User
}
public class OperationResult
{
    private bool _success = false;
    private string _message;
    private string _param;

    public bool Success { get { return _success; } set { _success = value; } }
    public string Message { get { return !string.IsNullOrEmpty(_message) ? ResourceManager.GetResource(_message) : ""; } set { _message = value; } }
    public string Param { get { return _param; } set { _param = value; } }
}
public enum WebUserType
{
    Visitor = 0,
    Member = 1,
    Admin = 2
}
public enum AuditAction
{
    Insert = 1,
    Update = 2,
    Delete = 3,
    Select = 4
}
public enum Status
{
    Passive = 0, //Pasif
    Active = 1, //Aktif
    Preparing = 2, //Taslak
    Deleted = 3, //Silindi
    WaitingAnApproval = 4 //Onay Bekleniyor
}
public enum PostType
{
    Post = 1, //Yazı
    Page = 2, //Sayfa
    Product = 3 //Ürün
}
public enum MenuItemType
{
    Post = 1,
    Page = 2,
    Category = 3,
    CustomUrl = 4 //özel bağlantılar
}
public enum PostStatus
{
    Draft = 0, //Taslak
    Active = 1, //Yayında
    Recycle = 2 //Çöp
}
public enum CategoryType
{
    Blog = 1,
    Products = 2
}