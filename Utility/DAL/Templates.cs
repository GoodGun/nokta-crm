using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public class DBObject
    {
        public string TableName { get; set; }
        public string IDColumn { get; set; }
        public string NameColumn { get; set; }
        public bool FromResource { get; set; }
    }
    public enum OrderBy
    {
        Name = 1,
        Value = 2,
        Default = 0
    }

    public struct Templates
    {
        public static DBObject Address = new DBObject { TableName = "Address", IDColumn = "AddressID", NameColumn = "PersonName" };
        public static DBObject Admin = new DBObject { TableName = "Admin", IDColumn = "AdminID", NameColumn = "Email" };
        public static DBObject Bank = new DBObject { TableName = "Bank", IDColumn = "BankID", NameColumn = "BankName" };
        public static DBObject BankAccount = new DBObject { TableName = "BankAccount", IDColumn = "BankAccountID", NameColumn = "DepartmentCode" };
        public static DBObject Brand = new DBObject { TableName = "Brand", IDColumn = "BrandID", NameColumn = "BName" };
        public static DBObject Carousel = new DBObject { TableName = "Carousel", IDColumn = "CarouselID", NameColumn = "Name" };
        public static DBObject Category = new DBObject { TableName = "Category", IDColumn = "CategoryID", NameColumn = "CatName" };
        public static DBObject CarouselItem = new DBObject { TableName = "CarouselItem", IDColumn = "CarouselItemID", NameColumn = "Spot" };
        public static DBObject Charity = new DBObject { TableName = "Charity", IDColumn = "CharityID", NameColumn = "CharityName" };
        public static DBObject City = new DBObject { TableName = "City", IDColumn = "CityID", NameColumn = "CityName" };
        public static DBObject ContactLog = new DBObject { TableName = "ContactLog", IDColumn = "ContactLogID", NameColumn = "Name" };
        public static DBObject Content = new DBObject { TableName = "Content", IDColumn = "ContentID", NameColumn = "Title" };
        public static DBObject Country = new DBObject { TableName = "Country", IDColumn = "CountryID", NameColumn = "CountryName" };
        public static DBObject luAccessory = new DBObject { TableName = "luAccessory", IDColumn = "AccessoryID", NameColumn = "AccessoryDetail" };
        public static DBObject luColor = new DBObject { TableName = "luColor", IDColumn = "ColorID", NameColumn = "ColorName" };
        public static DBObject luCondition = new DBObject { TableName = "luCondition", IDColumn = "ConditionID", NameColumn = "ConditionDetail" };
        public static DBObject luDeviceType = new DBObject { TableName = "luDeviceType", IDColumn = "DeviceTypeID", NameColumn = "ResTypeName" };
        public static DBObject Member = new DBObject { TableName = "Member", IDColumn = "MemberID", NameColumn = "Email" };
        public static DBObject Message = new DBObject { TableName = "Message", IDColumn = "MessageID", NameColumn = "Subject" };
        public static DBObject Model = new DBObject { TableName = "Model", IDColumn = "ModelID", NameColumn = "MName" };
        public static DBObject ModelPrice = new DBObject { TableName = "ModelPrice", IDColumn = "PriceID", NameColumn = "Options" };
        public static DBObject Option = new DBObject { TableName = "Option", IDColumn = "OptionID", NameColumn = "BgImage" };
        public static DBObject Pagelet = new DBObject { TableName = "Pagelet", IDColumn = "PageletID", NameColumn = "PageKey" };
        public static DBObject Payment = new DBObject { TableName = "Payment", IDColumn = "PaymentID", NameColumn = "ProvisionCode" };
        public static DBObject PaymentType = new DBObject { TableName = "PaymentType", IDColumn = "PaymentTypeID", NameColumn = "ResPaymentType" };
        public static DBObject Property = new DBObject { TableName = "Property", IDColumn = "PropertyID", NameColumn = "ResDescription" };
        public static DBObject Serial = new DBObject { TableName = "Serial", IDColumn = "SerialID", NameColumn = "DefImagePath" };
        public static DBObject SerialDonation = new DBObject { TableName = "SerialDonation", IDColumn = "DonationID", NameColumn = "CharityName" };
        public static DBObject SerialNote = new DBObject { TableName = "SerialNote", IDColumn = "NoteID", NameColumn = "Note" };
        public static DBObject SerialPicture = new DBObject { TableName = "SerialPicture", IDColumn = "PictureID", NameColumn = "PPath" };
    }
}
