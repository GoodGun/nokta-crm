using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Xml;
using System.Security.Cryptography;
using System.Collections;

namespace Utility
{
    public static class StringExtensions
    {
        private static readonly char[] TrimmedChars = { ' ', '=' };
        private static readonly Regex STRIP_HTML = new Regex("<[^>]*>", RegexOptions.Compiled);
        private static readonly Regex REGEX_BETWEEN_TAGS = new Regex(@">\s+", RegexOptions.Compiled);
        private static readonly Regex REGEX_LINE_BREAKS = new Regex(@"\n\s+", RegexOptions.Compiled);
        private static readonly Regex MOBILE_REGEX = new Regex(@"(nokia|sonyericsson|blackberry|samsung|sec\-|windows ce|motorola|mot\-|up.b|midp\-)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static readonly char[] comma = { ',' };

        [Flags, Serializable]
        public enum CreditCardType
        {
            MasterCard = 0x0001,
            VISA = 0x0002,
            Amex = 0x0004,
            DinersClub = 0x0008,
            enRoute = 0x0010,
            Discover = 0x0020,
            JCB = 0x0040,
            Unknown = 0x0080,
            All = CreditCardType.Amex | CreditCardType.DinersClub | CreditCardType.Discover | CreditCardType.Discover |
                CreditCardType.enRoute | CreditCardType.JCB | CreditCardType.MasterCard | CreditCardType.VISA
        }
        public static CreditCardType _CreditCardTypes = CreditCardType.VISA | CreditCardType.MasterCard | CreditCardType.Amex;

        public static bool IsValidCreditCardType(string cardNumber)
        {
            // AMEX -- 34 or 37 -- 15 length
            if ((Regex.IsMatch(cardNumber, "^(34|37)")) && ((_CreditCardTypes & CreditCardType.Amex) != 0))
                return (15 == cardNumber.Length);

                // MasterCard -- 51 through 55 -- 16 length
            else if ((Regex.IsMatch(cardNumber, "^(51|52|53|54|55)")) && ((_CreditCardTypes & CreditCardType.MasterCard) != 0))
                return (16 == cardNumber.Length);

                // VISA -- 4 -- 13 and 16 length
            else if ((Regex.IsMatch(cardNumber, "^(4)")) && ((_CreditCardTypes & CreditCardType.VISA) != 0))
                return (13 == cardNumber.Length || 16 == cardNumber.Length);

                // Diners Club -- 300-305, 36 or 38 -- 14 length
            else if ((Regex.IsMatch(cardNumber, "^(300|301|302|303|304|305|36|38)")) && ((_CreditCardTypes & CreditCardType.DinersClub) != 0))
                return (14 == cardNumber.Length);

                // enRoute -- 2014,2149 -- 15 length
            else if ((Regex.IsMatch(cardNumber, "^(2014|2149)")) && ((_CreditCardTypes & CreditCardType.DinersClub) != 0))
                return (15 == cardNumber.Length);

                // Discover -- 6011 -- 16 length
            else if ((Regex.IsMatch(cardNumber, "^(6011)")) && ((_CreditCardTypes & CreditCardType.Discover) != 0))
                return (16 == cardNumber.Length);

                // JCB -- 3 -- 16 length
            else if ((Regex.IsMatch(cardNumber, "^(3)")) && ((_CreditCardTypes & CreditCardType.JCB) != 0))
                return (16 == cardNumber.Length);

                // JCB -- 2131, 1800 -- 15 length
            else if ((Regex.IsMatch(cardNumber, "^(2131|1800)")) && ((_CreditCardTypes & CreditCardType.JCB) != 0))
                return (15 == cardNumber.Length);
            else
            {
                // Card type wasn't recognised, provided Unknown is in the CreditCardTypes property, then
                // return true, otherwise return false.
                return ((_CreditCardTypes & CreditCardType.Unknown) != 0);
            }
        }
        public static bool ValidateCardNumber(string cardNumber)
        {
            try
            {
                // Array to contain individual numbers
                ArrayList CheckNumbers = new ArrayList();

                // So, get length of card
                int CardLength = cardNumber.Length;

                // Double the value of alternate digits, starting with the second digit
                // from the right, i.e. back to front.

                // Loop through starting at the end
                for (int i = CardLength - 2; i >= 0; i = i - 2)
                {
                    // Now read the contents at each index, this
                    // can then be stored as an array of integers

                    // Double the number returned
                    CheckNumbers.Add(Int32.Parse(cardNumber[i].ToString()) * 2);
                }

                int CheckSum = 0;	// Will hold the total sum of all checksum digits

                // Second stage, add separate digits of all products
                for (int iCount = 0; iCount <= CheckNumbers.Count - 1; iCount++)
                {
                    int _count = 0;	// will hold the sum of the digits

                    // determine if current number has more than one digit
                    if ((int)CheckNumbers[iCount] > 9)
                    {
                        int _numLength = ((int)CheckNumbers[iCount]).ToString().Length;
                        // add count to each digit
                        for (int x = 0; x < _numLength; x++)
                        {
                            _count = _count + Int32.Parse(((int)CheckNumbers[iCount]).ToString()[x].ToString());
                        }
                    }
                    else
                    {
                        _count = (int)CheckNumbers[iCount];	// single digit, just add it by itself
                    }

                    CheckSum = CheckSum + _count;	// add sum to the total sum
                }

                // Stage 3, add the unaffected digits
                // Add all the digits that we didn't double still starting from the right
                // but this time we'll start from the rightmost number with alternating digits
                int OriginalSum = 0;
                for (int y = CardLength - 1; y >= 0; y = y - 2)
                {
                    OriginalSum = OriginalSum + Int32.Parse(cardNumber[y].ToString());
                }

                // Perform the final calculation, if the sum Mod 10 results in 0 then
                // it's valid, otherwise its false.
                return (((OriginalSum + CheckSum) % 10) == 0);
            }
            catch
            {
                return false;
            }
        }
        public static string ToSHA1(this string sha1Data)
        {
            if (!string.IsNullOrEmpty(sha1Data))
            {
                SHA1 sha = new SHA1CryptoServiceProvider();
                byte[] hashbytes = Encoding.GetEncoding("ISO-8859-9").GetBytes(sha1Data);
                byte[] inputbytes = sha.ComputeHash(hashbytes);
                return GetHexaDecimal(inputbytes);
            }
            return string.Empty;
        }
        private static string GetHexaDecimal(byte[] bytes)
        {
            var s = new StringBuilder();
            var length = bytes.Length;
            for (var n = 0; n <= length - 1; n++)
            {
                s.Append(String.Format("{0,2:x}", bytes[n]).Replace(" ", "0"));
            }
            return s.ToString();
        }
        public static string GetXMLNodeText(ref XmlDocument document, string xpath)
        {
            string result = string.Empty;
            try
            {
                XmlNode oNode = document.SelectSingleNode(xpath);
                if (oNode != null && !string.IsNullOrEmpty(oNode.InnerText))
                    result = oNode.InnerText;
            }
            catch { }
            return result;
        }
        public static string ToReadableFormat(this Dictionary<string, object> filter)
        {
            if (filter == null || filter.Count == 0) return "EMPTY";
            string result = "";

            foreach (string key in filter.Keys)
            {
                object o = filter[key];
                if (o != null && o is int && ((int)o == Param.Leave))
                    result += string.Concat(" ", key, " ");
                else
                    result += string.Format("{0} = '{1}' AND ", key, filter[key]);
            }
            result = string.Concat(result, "1=1").Replace(" AND 1=1", "");

            return string.IsNullOrEmpty(result) ? "EMPTY" : result;
        }
        public static string Add(this string value, string what)
        {
            return string.Concat(value, what);
        }
        public static string Clean(this string value)
        {
            return (string.IsNullOrEmpty(value)) ? string.Empty : REGEX_BETWEEN_TAGS.Replace(STRIP_HTML.Replace(value.Trim(TrimmedChars), string.Empty), string.Empty).Replace("'", "");
        }
        /// <summary>
        /// String'i geriden yazdırır. Ali => ilA
        /// </summary>
        /// <param name="Value">Değer</param>
        /// <returns></returns>
        public static string Reverse(this string Value)
        {
            if (string.IsNullOrEmpty(Value)) return string.Empty;
            string Result = string.Empty;

            for (int i = 0; i < Value.Length; i++)
                Result += Value.Substring(Value.Length - i - 1, 1);

            return Result;
        }
        /// <summary>
        /// DB'deki CONVERT(VARCHAR(8), GETDATE(), 112) formatına çevirir.
        /// dt parametresi DateTime.MinValue ise NULL yazan bir değer döndürür.
        /// </summary>
        /// <param name="dt">Tarih parametresi</param>
        /// <returns>20130314</returns>
        public static string ToVarchar112(this DateTime dt)
        {
            return dt == DateTime.MinValue ? "NULL" :
                string.Format("{0}{1}{2}", dt.Year, dt.Month.ToString().PadLeft(2, '0'), dt.Day.ToString().PadLeft(2, '0'));
        }
        public static DateTime To2359(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 0);
        }

        /// <summary>
        /// Nesne null ise "", değilse ToString() döndürür, if kontrolünden kurtarır.
        /// </summary>
        /// <param name="o">nesne</param>
        /// <returns></returns>
        public static string ToSureString(this object o, string defValue = "--")
        {
            return o == null ? defValue : o.ToString();
        }
        /// <summary>
        /// Bir string'i string array'ine çevirir.
        /// </summary>
        /// <param name="value">Değer</param>
        /// <returns></returns>
        public static List<string> ToStringList(this string value, char [] seperator = null)
        {
            var arr = new List<string>();

            try
            {
                if (seperator == null) seperator = comma;
                if (!string.IsNullOrEmpty(value) && value != "%00")
                    arr.AddRange(value.Split(seperator, StringSplitOptions.RemoveEmptyEntries));
            }
            catch { }

            return arr;
        }
        public static string TekTirnakDegistir(this string val)
        {
            return !string.IsNullOrEmpty(val) ? val.Replace("'", "’") : "";
        }
        public static string[] SplitToArray(this string val, char seperator = ',', bool trimSpace = true)
        {
            string[] arr = new string[0];
            if (string.IsNullOrEmpty(val)) return arr;

            char[] sep = { seperator };
            List<string> vals = new List<string>(val.Split(sep, StringSplitOptions.RemoveEmptyEntries));
            char[] trim = { ' ' };

            vals.ForEach(delegate(string v) { v = v.Trim(trim); });
            return vals.ToArray();
        }

        /// <summary>
        /// ddMMyyyy formatında bitişik yazılmış bir DateTime'ı parse eder. 
        /// Herhangi bir tutarsızlıkta DateTime.MinValue döndürür.
        /// </summary>
        /// <param name="value">Değer</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value)
        {
            try
            {
                int day = Convert.ToInt32(value.Substring(0, 2)),
                    month = Convert.ToInt32(value.Substring(2, 2)),
                    year = Convert.ToInt32(value.Substring(4, 4));

                return new DateTime(year, month, day);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        public static string Left(this string str, int size)
        {
            return !string.IsNullOrEmpty(str)
                       ? (str.Length > size ? str.Substring(0, size) : str)
                       : string.Empty;
        }

        public static string Right(this string str, int size)
        {
            return !string.IsNullOrEmpty(str)
                       ? (str.Length > size ? str.Substring((str.Length - size), size) : str)
                       : string.Empty;
        }
        public static string ReplaceTurkishChars(this string sInput)
        {
            string sResult = sInput;
            string sTurkishChars = "ğüşıöçĞÜŞİÖÇ";
            string sReplacedChars = "gusiocGUSIOC";

            for (int i = 0; i < sTurkishChars.Length; i++)
                if (sResult.IndexOf(sTurkishChars[i].ToString()) >= 0)
                    sResult = sResult.Replace(sTurkishChars[i].ToString(), sReplacedChars[i].ToString());

            return sResult;
        }

        public static string ToSecureFileName(this string val)
        {
            val = val.ReplaceTurkishChars();
            var reg = new Regex(@"([^a-zA-Z0-9\-._])", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);
            val = reg.Replace(val, string.Empty);
            return val;
        }
        /// <summary>
        /// URL türündeki bir string'i alış encode eder.
        /// Örnek http://www.yahoo.com?a=ali@  => http:%2F%2Fwww.yahoo.com%3Fa%3Dali%40
        /// </summary>
        /// <param name="link">Link parametresi</param>
        /// <returns></returns>
        public static string EncodeURL(this string link)
        {
            link = link.ToSureString();
            link = link.Replace("'", "%27");
            link = link.Replace("&", "%26");
            link = link.Replace("$", "%24");
            link = link.Replace("?", "%3F");
            link = link.Replace("#", "%23");
            link = link.Replace("<", "%3C");
            link = link.Replace(">", "%3E");
            link = link.Replace(@"\", "%5C");
            link = link.Replace("/", "%2F");
            link = link.Replace("+", "%2B");
            link = link.Replace(",", "%2C");
            link = link.Replace(";", "%3B");
            link = link.Replace("=", "%3D");
            link = link.Replace("@", "%40");

            return link;
        }
        

        /// <summary>
        /// Strips all illegal characters from the specified title.
        /// </summary>
        public static string RemoveIllegalCharacters(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";
            char[] arr = { ' ', ',' };
            text = text.ToLower().Trim(arr).ReplaceTurkishChars();
            text = text.Replace(":", string.Empty);
            text = text.Replace("/", string.Empty);
            text = text.Replace("?", string.Empty);
            text = text.Replace("#", string.Empty);
            text = text.Replace("[", string.Empty);
            text = text.Replace("]", string.Empty);
            text = text.Replace("@", string.Empty);
            text = text.Replace("*", string.Empty);
            text = text.Replace(".", string.Empty);
            text = text.Replace(",", string.Empty);
            text = text.Replace("\"", string.Empty);
            text = text.Replace("&", string.Empty);
            text = text.Replace("'", string.Empty);
            text = text.Replace(" ", "-");
            return HttpUtility.UrlEncode(text).Replace("%", string.Empty);
        }

        /// <summary>
        /// Strips all HTML tags from the specified string.
        /// </summary>
        /// <param name="html">The string containing HTML</param>
        /// <returns>A string without HTML tags</returns>
        public static string Strip(this string html, bool RemoveHTML, bool RemoveWhiteSpace)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            if (RemoveHTML) html = STRIP_HTML.Replace(html, string.Empty);
            if (RemoveWhiteSpace)
            {
                html = REGEX_BETWEEN_TAGS.Replace(html, "> ");
                html = REGEX_LINE_BREAKS.Replace(html, string.Empty).Trim();
            }

            return html;
        }
        /// <summary>
        /// String'i şifreler
        /// </summary>
        /// <param name="Value">şifrelenecek metin</param>
        /// <returns></returns>
        public static string Encrypt(this string Value)
        {
            Value = Value.NullToString();
            try
            {
                return CryptographyService.EncDec.Encrypt(Value, "aDM3nT@L", "adnone", "SHA1", 2, "@1B2c3D4e5F6g7H8", 256).Replace("+", "_").Replace("=", "");
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// Şifrelenmiş string'in şifresini çözer.
        /// </summary>
        /// <param name="Value">Çözülecek metin</param>
        /// <returns></returns>
        public static string Decrypt(this string Value)
        {
            string Result = _decrypt(Value);
            if (Result == "") Result = _decrypt(Value + "=");
            if (Result == "") Result = _decrypt(Value + "==");
            return Result;
        }
        private static string _decrypt(string Value)
        {
            try
            {
                return CryptographyService.EncDec.Decrypt(Value.Replace("_", "+"), "aDM3nT@L", "adnone", "SHA1", 2, "@1B2c3D4e5F6g7H8", 256);
            }
            catch (Exception)
            {
                return "";
            }
        }
        
    }
}
