using BusinessObjects.Common;
using System;
using System.Data;
using Utility;

/// <summary>
/// Summary description for Member
/// </summary>
namespace BusinessObjects
{
    public class Member
    {
        #region Constructors
        public Member()
        {
        }
        #endregion

        #region Variables
        private int _MemberID;
        private string _Name ;
        private string _Surname ;
        private string _Username ;
        private string _Email ;
        private string _PasswordHashed ;
        private byte _MemberTypeID = 0;
        private bool _Status = false;
        private DateTime _CreateDate = System.DateTime.Now;
        private DateTime _LastUpdate = System.DateTime.Now;

        #endregion

        #region Properties
        public int MemberID { get { return _MemberID; } set { _MemberID = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Surname { get { return _Surname; } set { _Surname = value; } }
        public string Username { get { return _Username; } set { _Username = value; } }
        public string Email { get { return _Email; } set { _Email = value; } }
        public string PasswordHashed { get { return _PasswordHashed; } set { _PasswordHashed = value; } }
        public byte MemberTypeID { get { return _MemberTypeID; } set { _MemberTypeID = value; } }
        public bool Status { get { return _Status; } set { _Status = value; } }
        public DateTime CreateDate { get { return _CreateDate; } set { _CreateDate = value; } }
        public DateTime LastUpdate { get { return _LastUpdate; } set { _LastUpdate = value; } }

        #endregion

        #region Extended Properties
        private static string _Language = ConfigManager.Current.DefaultLanguage;
        public static string Language { get { return _Language; } set { _Language = value; } }

        public string FullName { get { return string.Concat(this.Name, " ", this.Surname); } }
        private const string sMemberID = "bcid", sMemberName = "bcnm";
        public static bool LoggedIn
        {
            get { return CurrentMemberID > 0; }
        }
        public static int CurrentMemberID
        {
            get { return CookieUtil.GetCookieItem<string>(sMemberID, "0", ExpireDate).Decrypt().ToInt(); }
            set { CookieUtil.SetCookieItem<string>(sMemberID, value.ToString().Encrypt(), ExpireDate); }
        }
        public static string CurrentName
        {
            get { return CookieUtil.GetCookieItem<string>(sMemberName, "0", ExpireDate).Decrypt(); }
            set { CookieUtil.SetCookieItem<string>(sMemberName, value.Encrypt(), ExpireDate); }
        }
        public static Member Current
        {
            get
            {
                int mid = CurrentMemberID;
                if (mid < 1) return null;
                return CacheManager.Get<Member>("_member", mid, typeof(MemberManager), "GetMemberByID", mid);
            }
        }
        public static bool RememberMe
        {
            get { return CookieUtil.GetCookieItem<string>("bcrm", "0", DateTime.Today.AddYears(1)) == "1"; }
            set { CookieUtil.SetCookieItem<string>("bcrm", value ? "1" : "0", DateTime.Today.AddYears(1)); }
        }
        private static DateTime ExpireDate { get { return RememberMe ? DateTime.Today.AddYears(5) : DateTime.MinValue; } }
        public static void SetMember(int memberID, string name, bool rememberMe = false)
        {
            RememberMe = rememberMe;
            CurrentMemberID = memberID;
            CurrentName = name;
        }
        public static void SetLanguage(string _lang)
        {
            Language = _lang;
        }
        #endregion
    }
}