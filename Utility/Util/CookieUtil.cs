using System;
using System.Web;
using Utility;

namespace Utility
{
    public sealed class CookieUtil
    {
        public static void SetCookieItem<T>(string key, T Value)
        {
            SetCookieItem<T>(key, Value, DateTime.MinValue);
        }
        public static void SetCookieItem<T>(string key, T Value, DateTime ExpireDate)
        {
            if (Value == null)
                DeleteCookie(key);
            else
            {
                string v = Value.ToString();
                SetCookieValue(key, v, ExpireDate);
                Context.Items[key] = Value.ToString();
            }
        }
        public static T GetCookieItem<T>(string key, T DefaultValue)
        {
            return GetCookieItem<T>(key, DefaultValue, DateTime.MinValue);
        }

        public static T GetCookieItem<T>(string key, T DefaultValue, DateTime ExpireDate)
        {
            T oResult = DefaultValue;

            if (Context.Items.Contains(key))
            {
                oResult = (T)Convert.ChangeType(Context.Items[key], typeof(T));
            }
            else
            {
                string val = GetCookieValue(key, DefaultValue.ToString(), true);

                if (!string.IsNullOrEmpty(val))
                {
                    try
                    {
                        oResult = (T)Convert.ChangeType(val, typeof(T));
                    }
                    catch
                    { }//Buraya düşmüşse cookie değeri sorunludur.
                }
            }
            return oResult;
        }
        private static HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        public static HttpCookie GetCookie(string CookieName)
        {
            return Context.Request.Cookies[CookieName];
        }
        public static string GetCookieValue(string CookieName)
        {
            return GetCookieValue(CookieName, null);
        }
        public static string GetCookieValue(string CookieName, string DefaultValue, bool WriteDefaultIfEmtpy = false)
        {
            HttpCookie oCookie = GetCookie(CookieName);
            bool isNull = oCookie == null;

            string result = isNull ? DefaultValue : oCookie.Value;
            
            if (WriteDefaultIfEmtpy && isNull)
                SetCookieValue(CookieName, result);

            return result;
        }

        public static void DeleteCookie(string CookieName)
        {
            HttpCookie oc = new HttpCookie(CookieName);

            oc.Expires = DateTime.Now.AddYears(-10);
            oc.Value = null;
            Context.Response.Cookies.Set(oc);
        }

        public static void SetCookieValue(string CookieName, string Value)
        {
            SetCookieValue(CookieName, Value, DateTime.MinValue);
        }
        public static void SetCookieValue(string CookieName, string Value, DateTime ExpireDate)
        {
            if (Value.NullOrEmpty()) { DeleteCookie(CookieName); return; }
            HttpCookie oCookie = new HttpCookie(CookieName, Value);
            oCookie.HttpOnly = CookieName != "lng";//client side'da dil desteği için
            oCookie.Path = "/";
            oCookie.Domain = ConfigManager.Current.CookieDomain;

            if (ExpireDate == DateTime.MinValue) 
                ExpireDate = DateTime.Today.AddYears(10);
            
            oCookie.Expires = ExpireDate;
            Context.Response.Cookies.Set(oCookie);
        }

        public static void ClearCookies()
        {
            for (int i = 0; i < Context.Request.Cookies.Count; i++)
            {
                HttpCookie oc = Context.Request.Cookies[i];
                if (!oc.Name.Contains("SessionId"))
                {
                    oc.Expires = DateTime.Now.AddDays(-1d);
                    oc.Value = null;
                    oc.Domain = ConfigManager.Current.CookieDomain;
                    oc.Path = "/";
                    Context.Response.Cookies.Set(oc);
                }
            }
        }
    }
}
