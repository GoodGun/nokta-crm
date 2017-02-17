using System;
using System.Threading;
using System.Globalization;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Xml;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;
using System.Reflection;

namespace Utility
{
    public static class CommonExtensions
    {
        private static readonly Regex mobBrowser = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private static readonly Regex mobVersions = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        //private static readonly Regex browser = new Regex(@"android.+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        //private static readonly Regex version = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(di|rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        /// <summary>
        /// Generic olarak objeyi kopyalar
        /// </summary>
        /// <typeparam name="T">Kopya türü</typeparam>
        /// <param name="instance">Veri kaynağı</param>
        /// <returns>Kopyayı gönderir</returns>
        public static T CopyObject<T>(this T instance)
        {
            T result = Activator.CreateInstance<T>();

            foreach (FieldInfo field in typeof(T).GetFields())
                field.SetValue(result, field.GetValue(instance));

            foreach (PropertyInfo prop in typeof(T).GetProperties())
                if (prop.CanRead && prop.CanWrite)
                    prop.SetValue(result, prop.GetValue(instance, null), null);

            return result;
        }

        public static Dictionary<string, object> AddX(this Dictionary<string, object> filter, string key, object val)
        {
            filter.Add(key, val);
            return filter;
        }
        public static string Checksum<T>(this T instance)
        {
            int result = 0;
            object val = null;
            foreach (FieldInfo field in typeof(T).GetFields())
            {
                val = field.GetValue(instance);
                result += val.ToSureString().GetHashCode();
            }

            foreach (PropertyInfo prop in typeof(T).GetProperties())
                if (prop.CanRead && prop.CanWrite)
                {
                    val = prop.GetValue(instance, null);
                    result += val.ToSureString().GetHashCode();
                }
            return result.ToString("X");
        }
        public static T FromString<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static DateTime ToExpireDate(this DateTime campaignEndDate, byte FCType)
        {
            // 0 Kapalı
            // 1 Kampanya boyunca
            // 2 Haftada
            // 3 Günde
            // 4 Saatte
            DateTime dtNow = DateTime.Now;
            DateTime dtResult = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, dtNow.Hour, dtNow.Minute, 0);
            switch (FCType)
            {
                case 1:
                    dtResult = campaignEndDate;
                    break;
                case 2:
                    dtResult = dtResult.AddDays(7);
                    break;
                case 3:
                    dtResult = dtResult.AddDays(1);
                    break;
                case 4:
                    dtResult = dtResult.AddHours(1);
                    break;
            }
            return dtResult;
        }

        /// <summary>
        /// Virgülle ayrılmış tarihleri parse ederek array döndürür
        /// </summary>
        /// <param name="value">Virgülle ayrılmış tarih(ler)</param>
        /// <returns>Virgülle ayrılmış tarihleri parse ederek array döndürür</returns>
        public static List<DateTime> ToDateList(this string value)
        {
            var arr = new List<DateTime>();
            DateTime dt = DateTime.MinValue;

            value.ToStringList().ForEach(delegate(string s)
            {
                dt = s.StringToDate();
                if (dt != DateTime.MinValue)
                    arr.Add(dt);
            });
            return arr;
        }

        public static DateTime StringToDate(this string value)
        {
            try
            {
                int day = Convert.ToInt32(value.Substring(0, 2)),
                    month = Convert.ToInt32(value.Substring(2, 2)),
                    year = Convert.ToInt32(value.Substring(4, 4)),
                    hour = Convert.ToInt32(value.Substring(8, 2)),
                    minute = Convert.ToInt32(value.Substring(10, 2)),
                    second = Convert.ToInt32(value.Substring(12, 2));

                return new DateTime(year, month, day, hour, minute, second);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime IFSStringToDate(this string value)
        {
            try
            {
                int year = Convert.ToInt32(value.Substring(0, 4)),
                    month = Convert.ToInt32(value.Substring(4, 2)),
                    day = Convert.ToInt32(value.Substring(6, 2)),
                    hour = Convert.ToInt32(value.Substring(8, 2)),
                    minute = Convert.ToInt32(value.Substring(10, 2)),
                    second = Convert.ToInt32(value.Substring(12, 2));

                return new DateTime(year, month, day, hour, minute, second);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        public static string IFSDateValue(this string value)
        {
        string pDate = value;
        string[] pDateFirstPart = pDate.Split(' ');
        string[] pDateFirstPartSplits = pDateFirstPart[0].Split('-');
        string[] pDateSecondPartSplits = pDateFirstPart[1].Split(':');

        for (int i = 0; i < 3; i++)
	    {
			 if (pDateFirstPartSplits[i].Length % 2 != 0)
                pDateFirstPartSplits[i] = String.Format("0{0}", pDateFirstPartSplits[i]);
             if (pDateSecondPartSplits[i].Length % 2 != 0)
                 pDateSecondPartSplits[i] = String.Format("0{0}", pDateSecondPartSplits[i]);
		}

        pDate = String.Format("{0}{1}{2}{3}{4}{5}", pDateFirstPartSplits[0], pDateFirstPartSplits[1], pDateFirstPartSplits[2], pDateSecondPartSplits[0], pDateSecondPartSplits[1], pDateSecondPartSplits[2]);

            return pDate;
        }

        public static string DateToString(this DateTime value)
        {
            return string.Format("{0}{1}{2}{3}{4}{5}",
                value.Day.ToString().PadLeft(2, '0'),
                value.Month.ToString().PadLeft(2, '0'),
                value.Year.ToString().PadLeft(4, '0'),
                value.Hour.ToString().PadLeft(2, '0'),
                value.Minute.ToString().PadLeft(2, '0'),
                value.Second.ToString().PadLeft(2, '0'));
        }

        /// <summary>
        /// Gets a value indicating whether the client is a mobile device.
        /// </summary>
        /// <value><c>true</c> if this instance is mobile; otherwise, <c>false</c>.</value>
        public static bool IsMobile(this HttpRequest req)
        {
            if (req.Browser.IsMobileDevice) return true;
            string ua = req.UserAgent ?? req.ServerVariables["HTTP_USER_AGENT"];
            if (string.IsNullOrEmpty(ua)) return false;
            if ((mobBrowser.IsMatch(ua) || mobVersions.IsMatch(ua.Substring(0, 4)))) return true;
            return false;
        }
        /// <summary>
        /// Gets a value indicating whether the client is a mobile device.
        /// </summary>
        /// <value><c>true</c> if this instance is mobile; otherwise, <c>false</c>.</value>
        public static bool IsMobile(bool isMobileDevice, string userAgent)
        {
            if (isMobileDevice) return true;
            if (string.IsNullOrEmpty(userAgent)) return false;
            if ((mobBrowser.IsMatch(userAgent) || mobVersions.IsMatch(userAgent.Substring(0, 4)))) return true;
            return false;
        }

        public static bool ContainsColumn(this IDataReader reader, string column)
        {
            return reader.GetSchemaTable().Columns.Contains(column);
        }
        /// <summary>
        /// Writes ETag and Last-Modified headers and sets the conditional get headers.
        /// </summary>
        /// <param name="date">The date.</param>
        public static bool SetConditionalGetHeaders(this HttpResponse res, HttpRequest req, DateTime date)
        {
            // SetLastModified() below will throw an error if the 'date' is a future date.
            if (date > DateTime.Now)
                date = DateTime.Now;

            string etag = "\"" + date.Ticks + "\"";
            string incomingEtag = req.Headers["If-None-Match"];

            res.AppendHeader("ETag", etag);
            res.Cache.SetLastModified(date);

            if (String.Compare(incomingEtag, etag) == 0)
            {
                res.Clear();
                res.StatusCode = (int)System.Net.HttpStatusCode.NotModified;
                return true;
            }

            return false;
        }
        public static string ToChecksum(this object o)
        {
            string x = o.ToString();
            x = string.Concat(x, x.Reverse(), x.GetHashCode());
            return x.GetHashCode().ToString("X");
        }
        /// <summary>
        /// Null olsa da olmasa da string döndürür.
        /// </summary>
        /// <param name="str">parametre</param>
        /// <returns>null ise boş string, değilse .ToString()</returns>
        public static string NullToString(this object str, string defaultValue = "")
        {
            if (str == null) return defaultValue;
            string res = str.ToString();
            return string.IsNullOrEmpty(res) ? defaultValue : res;
        }

        /// <summary>
        /// Bir nesnenin null ya da boş string olup olmadığını döndürür.
        /// Öncesinde null kontrolü yapmaya gerek yoktur.
        /// </summary>
        /// <param name="str">parametre</param>
        /// <returns>Bir nesnenin null ya da boş string olup olmadığını döndürür.</returns>
        public static bool NullOrEmpty(this object str)
        {
            return str == null ? true : string.IsNullOrEmpty(str.ToString());
        }

        public static int Count(this string s, string pattern)
        {
            if (pattern.NullOrEmpty() || s.NullOrEmpty()) return 0;
            return (s.Length - s.Replace(pattern, "").Length) / pattern.Length;
        }
        public static string ISNULL(this string str, string defValue = "--")
        {
            return string.IsNullOrEmpty(str) ? defValue : str;
        }
        /// <summary>
        /// Database'de saklanabilecek formatta short (smallint) formatında getirir.
        /// Saat kısımları dikkate alınmaz.
        /// </summary>
        /// <param name="dt">ilgili tarih</param>
        /// <returns>ilgili tarihin short gösterimi.</returns>
        public static string ToDBDate(this DateTime dt)
        {
            return string.Format("{0}.{1}.{2}", dt.Year, dt.Month, dt.Day);
            //TimeSpan ts = dt.Subtract(new DateTime(1900, 1, 1));
            //double days = Math.Floor(ts.TotalDays) - 30000;
            //return days.ToShort();
        }
        /// <summary>
        /// ToDBDate ile short'a çevrilmiş tarihi normal DateTime'a çevirir.
        /// </summary>
        /// <param name="shortDay">short notasyon</param>
        /// <returns>ilgili tarih</returns>
        public static DateTime DBShortToDate(this short shortDay)
        {
            return new DateTime(1900, 1, 1).AddDays(30000 + shortDay);
        }
        /// <summary>
        /// Herhangi bir nesneyi bit/bool'a çevirir.
        /// </summary>
        /// <param name="str">nesne</param>
        /// <returns>Herhangi bir nesneyi bit/bool'a çevirir.</returns>
        public static bool ToBool(this object str)
        {
            if (str == null) return false;
            bool result = false;
            if (bool.TryParse(str.ToString().Replace("1", "True").Replace("0", "False"), out result))
                return result;

            try { return str.ToInt() > 0; }
            catch { return false; }
        }
        /// <summary>
        /// Herhangi bir nesneyi byte'a çevirir.
        /// </summary>
        /// <param name="str">nesne</param>
        /// <returns>Herhangi bir nesneyi bit/bool'a çevirir.</returns>
        public static byte ToByte(this object str)
        {
            if (str == null) return 0;
            byte result = 0;
            byte.TryParse(str.ToString(), out result);
            return result;
        }
        /// <summary>
        /// Herhangi bir nesneyi short'a çevirir.
        /// </summary>
        /// <param name="str">nesne</param>
        /// <returns>Herhangi bir nesneyi bit/bool'a çevirir.</returns>
        public static short ToShort(this object str)
        {
            if (str == null) return 0;
            short result = 0;
            short.TryParse(str.ToString(), out result);
            return result;
        }
        /// <summary>
        /// Herhangi bir nesneyi int'e çevirir.
        /// </summary>
        /// <param name="str">nesne</param>
        /// <returns>Herhangi bir nesneyi bit/bool'a çevirir.</returns>
        public static int ToInt(this object str)
        {
            if (str == null) return 0;
            int result = 0;
            try { result = Convert.ToInt32(str); }
            catch { }
            //int.TryParse(str.ToString(), out result);
            return result;
        }
        public static int ID(this string str)
        {
            return str.NullOrEmpty() ? 0 : Math.Abs(str.ToLower().GetHashCode());
        }
        /// <summary>
        /// Herhangi bir nesneyi long'a çevirir.
        /// </summary>
        /// <param name="str">nesne</param>
        /// <returns>Herhangi bir nesneyi bit/bool'a çevirir.</returns>
        public static long ToLong(this object str)
        {
            if (str == null) return 0;
            long result = 0;
            long.TryParse(str.ToString(), out result);
            return result;
        }

        public static DateTime FromDBShort(this short days)
        {
            return new DateTime(1900, 1, 1).AddDays(30000 + Convert.ToDouble(days));
        }
        public static short ToDBShort(this DateTime dt)
        {
            DateTime dtZero = new DateTime(1900, 1, 1);
            return (dt.Subtract(dtZero).TotalDays - 30000).ToShort();
        }
        /// <summary>
        /// Herhangi bir nesneyi decimal'a çevirir.
        /// </summary>
        /// <param name="str">nesne</param>
        /// <returns>Herhangi bir nesneyi bit/bool'a çevirir.</returns>
        public static decimal ToDecimal(this object str)
        {
            if (str == null) return 0;
            decimal result = 0;
            decimal.TryParse(str.ToString(), out result);
            return result;
        }
    }
}
