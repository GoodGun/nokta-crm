using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Web.UI;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Utility
{
    public static class Util
    {
        //public enum MailSender
        //{
        //    Destek = 1,
        //    Info = 2,
        //    Siparis = 3,
        //    Kayit = 4
        //}
        public static long CurrentUserIPLong { get { return Dot2LongIP(CurrentUserIP); } }
        public static string LongToIP(long longIP)
        {
            string ip = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                int num = (int)(longIP / Math.Pow(256, (3 - i)));
                longIP = longIP - (long)(num * Math.Pow(256, (3 - i)));
                if (i == 0)
                    ip = num.ToString();
                else
                    ip = ip + "." + num.ToString();
            }
            return ip;
        }
        public static bool CheckURL(string url)
        {
            try
            {
                Uri uri = null;
                bool valid = Uri.TryCreate(url, UriKind.Absolute, out uri);
                if (!valid) return false;

                var request = HttpWebRequest.Create(url);
                request.Timeout = 4000;
                using (var response = request.GetResponse())
                {
                    return response.ContentLength > 0;
                }
            }
            catch { return false; }
        }
        public static long Dot2LongIP(string DottedIP)
        {
            double CalculateLongIP = 0;
            try
            {
                int i = 1;
                int pos = 0;
                int PrevPos = 0;
                double num = 0;
                if (DottedIP != "" && DottedIP.IndexOf(".") != -1 && DottedIP.Length < 16)
                    while (i <= 4)
                    {
                        pos = DottedIP.IndexOf(".", pos);
                        if (i == 4) pos = DottedIP.Length;
                        num = Convert.ToInt32(DottedIP.Substring(PrevPos, pos - PrevPos));
                        CalculateLongIP = ((num % 256) * (Math.Pow(256, (4 - i)))) + CalculateLongIP;
                        PrevPos = pos + 1;
                        pos = pos + 1;
                        i = i + 1;
                    }
            }
            catch { }

            return CalculateLongIP.ToLong();
        }

        public static void SaveToFile<T>(T instance, string filePath)
        {
            var oInfo = new FileInfo(filePath);
            if (!Directory.Exists(oInfo.DirectoryName)) Directory.CreateDirectory(oInfo.DirectoryName);

            using (var stream = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(stream, instance);
                var content = stream.GetBuffer();
                stream.Close();
                File.WriteAllBytes(filePath, content);
            }
        }
        public static T ReadFile<T>(string filePath)
        {
            T res = Activator.CreateInstance<T>();

            try
            {
                using (var stream = new MemoryStream(File.ReadAllBytes(filePath)))
                {
                    res = (T)Convert.ChangeType(new BinaryFormatter().Deserialize(stream), typeof(T));
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return res;
        }

        #region Compression and Decompression
        /// <summary>
        /// Converts the date time to specific string 13.07.2008 converts to 20080713
        /// </summary>
        /// <param name="oDateTime">DateTime object that will be converted to specific string</param>
        /// <returns>string</returns>
        public static string ConvertToFormattedDateString(DateTime oDateTime)
        {
            string year = oDateTime.Year.ToString();
            string month = oDateTime.Month < 10 ? "0" + oDateTime.Month.ToString() : oDateTime.Month.ToString();
            string day = oDateTime.Day < 10 ? "0" + oDateTime.Day.ToString() : oDateTime.Day.ToString();
            return year + month + day;
        }
        public static DateTime ConvertToFormattedDateString(string oDateString)
        {
            int year = Convert.ToInt32(oDateString.Substring(0, 4));
            int mount = Convert.ToInt32(oDateString.Substring(4, 2));
            int day = Convert.ToInt32(oDateString.Substring(6, 2));
            return (new DateTime(year, mount, day));
        }
        #endregion

        #region ImageOperations
        /// <summary>
        /// Bu fonksiyon image stream'ini alýp, istenen width e göre 
        /// boyutlarýný deðiþtirir. Boyutunu deðiþtirirken image ýn orantýsýný korur.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="newWidth"></param>
        /// <returns></returns>
        public static byte[] ResizeImagesProportional(Stream str, int newWidth, int newHeight)
        {
            byte[] ImageData = null;
            if (newWidth == 0 && newHeight == 0)//original size 
            {
                ImageData = new byte[str.Length];
                str.Read(ImageData, 0, Convert.ToInt32(str.Length));
            }
            else
            {
                System.Drawing.Image originalImage = System.Drawing.Image.FromStream(str);
                decimal proportion = (newWidth / (decimal)originalImage.Width);
                decimal ProportionalHeight = originalImage.Height * proportion;
                if (ProportionalHeight > newHeight)
                    newWidth = Convert.ToInt32(Convert.ToDecimal(newWidth) * Convert.ToDecimal(newHeight) / ProportionalHeight);
                else
                    newHeight = Convert.ToInt32(ProportionalHeight);

                System.Drawing.Bitmap bm = new System.Drawing.Bitmap(originalImage, newWidth, Convert.ToInt32(newHeight));
                System.IO.MemoryStream Str = new System.IO.MemoryStream();
                bm.Save(Str, originalImage.RawFormat);
                ImageData = Str.ToArray();
            }
            return ImageData;
        }

        /// <summary>
        /// Resmi, MaxWidth x MaxHeight içine sýðabilen hale kadar küçültür.
        /// Bunu yaparken dosya boyutunu da MaxBytes'a sýðdýrmaya çalýþýr.
        /// </summary>
        /// <param name="oStream"></param>
        /// <param name="MaxWidth"></param>
        /// <param name="MaxHeight"></param>
        /// <param name="MaxBytes"></param>
        /// <returns></returns>
        public static System.Drawing.Image ResizeImagesProportional(Stream oStream, int MaxWidth, int MaxHeight, int MaxBytes)
        {
            System.Drawing.Image originalImage = null;

            try
            {
                originalImage = Image.FromStream(oStream);

                // Resmin boyutlarý, izin verilenden zaten küçük ya da eþitse, bir deðiþiklik yapmýyoruz:
                if (originalImage.Width <= MaxWidth && originalImage.Height <= MaxHeight)
                    return originalImage;
                else
                {
                    // Küçültme oraný
                    decimal proportion = (MaxWidth / (decimal)originalImage.Width);
                    decimal ProportionalHeight = originalImage.Height * proportion;

                    if (ProportionalHeight > MaxHeight)
                        MaxWidth = Convert.ToInt32(Convert.ToDecimal(MaxWidth) * Convert.ToDecimal(MaxHeight) / ProportionalHeight);
                    else
                        MaxHeight = Convert.ToInt32(ProportionalHeight);

                    Bitmap bm = new Bitmap(originalImage, MaxWidth, MaxHeight);
                    MemoryStream oMemoryStream = new MemoryStream();
                    bm.Save(oMemoryStream, originalImage.RawFormat);

                    if (oMemoryStream.Length > MaxBytes)
                    {
                        oMemoryStream = new MemoryStream();
                        bm.Save(oMemoryStream, ImageFormat.Jpeg);
                    }

                    if (oMemoryStream.Length > MaxBytes)
                    {
                        oMemoryStream = new MemoryStream();
                        bm.Save(oMemoryStream, ImageFormat.Gif);
                    }

                    long Quality = 80L; // %100 kalite
                    // Resmin boyutu izin verilen boyutun altýna düþene kadar %100-90-80... þeklinde küçültüyoruz:
                    while (oMemoryStream.Length > MaxBytes && Quality > 0)
                    {
                        oMemoryStream = new MemoryStream();
                        ImageCodecInfo[] Info = ImageCodecInfo.GetImageEncoders();
                        EncoderParameters Params = new EncoderParameters(1);
                        Params.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Quality);
                        bm.Save(oMemoryStream, Info[1], Params);
                        Quality -= 10;
                    }
                    originalImage = Image.FromStream(oMemoryStream);
                    bm.Dispose();
                    return originalImage;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(new Exception("Hatalý resim formatý", ex));
                throw (ex);
            }
        }
        public static void SaveImage(System.Drawing.Image imgPhoto, string FilePath, string FileName, System.Drawing.Imaging.ImageFormat Type)
        {
            imgPhoto.Save(FilePath + FileName, Type);
        }
        #endregion

        #region Configuration Section
        public static bool IsWebUse
        {
            get { return System.Web.HttpContext.Current != null; }
        }
        public static string VirtualPath
        {
            get
            {
                string path = HttpContext.Current.Request.ApplicationPath;
                return path == "/" ? "" : path;
            }
        }

        public static string MachineName
        {
            get
            {
                try { return Environment.MachineName; }
                catch (Exception) { return System.Web.HttpContext.Current.Server.MachineName; }
            }
        }
        public static bool IsTurkish
        {
            get { return CurrentUserLang == "TR"; }
        }
        public static string CurrentUserLang
        {
            get
            {
                try
                {
                    return "TR";
                    string lang = CookieUtil.GetCookieValue("lng");
                    if (!string.IsNullOrEmpty(lang)) return lang;
                    lang = CurrentBrowserLang();
                    CookieUtil.SetCookieValue("lng", lang, DateTime.Today.AddYears(1));
                    return lang;
                }
                catch
                {
                    return "TR";
                }
            }
            set
            {
                CookieUtil.SetCookieValue("lng", value);
            }
        }
        public static List<string> AvailableLanguages(string def = "EN,TR")
        {
            char[] Seperators = { ',' };

            try
            {
                string avail = ConfigurationManager.AppSettings["AvailableLanguages"];
                return new List<string>(avail.Split(Seperators, StringSplitOptions.RemoveEmptyEntries));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<string>(def.Split(Seperators, StringSplitOptions.RemoveEmptyEntries));
            }
        }
        public static string CurrentBrowserLang(string defLang = "EN")
        {
            try
            {
                return Thread.CurrentThread.CurrentCulture.ToString();
                

                //return "TR";
                //string[] languages = HttpContext.Current.Request.UserLanguages;

                //if (languages == null || languages.Length == 0)
                //    return defLang;

                //char[] sep = { '-' };
                //string lang = languages[0].Split(sep, StringSplitOptions.RemoveEmptyEntries)[0].ToUpper();
                //List<string> avail = AvailableLanguages();
                //return avail.Contains(lang) ? lang : (avail.Count > 0 ? avail[0] : defLang);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return defLang;
            }
        }
        public static string CurrentUserIP
        {
            get
            {
                try
                {
                    string IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
                    if (string.IsNullOrEmpty(IP))
                        IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (string.IsNullOrEmpty(IP))
                        IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    return IP;
                }
                catch (Exception) { return string.Empty; }
            }
        }
        #endregion

        #region Utility Functions
        private const int EncryptTimes = 2;
        private const int EncryptAddNumber = 7;
        private const int EncryptMultiplyNumber = 5;
        private const int EncryptFinalAddNumber = 9;

        public static string EncryptNumber(long Number)
        {
            long Result = Number;
            for (int i = 0; i < EncryptTimes; i++)
                Result = _encryptNumber(Result);
            return Result.ToString();
        }
        public static long DecryptNumber(string Number)
        {
            long Result = Convert.ToInt64(Number);
            for (int i = 0; i < EncryptTimes; i++)
            {
                Result = _decryptNumber(Result);
                if (Result <= 0) return 0;
            }
            return Result;
        }
        private static long _encryptNumber(long Number)
        {
            long Result = ((Number + EncryptAddNumber) * EncryptMultiplyNumber) + EncryptFinalAddNumber;
            return Convert.ToInt64(ReverseString(Result.ToString()));
        }
        private static long _decryptNumber(long Number)
        {
            try
            {
                long Result = Convert.ToInt64(ReverseString(Number.ToString())) - EncryptFinalAddNumber;
                if (Result % EncryptMultiplyNumber == 0)
                {
                    Result /= EncryptMultiplyNumber;
                    Result -= EncryptAddNumber;
                    return Result > 0 ? Result : 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return 0;
        }

        public static string CheckIban(string iban)
        {
            iban = Regex.Replace(iban, @"\s", "").ToUpper();
            if (Regex.IsMatch(iban, @"\W")) return "err.iban.invalid.chars";
            if (!Regex.IsMatch(iban, @"^\D\D\d\d.+")) return "err.iban.invalid";
            if (Regex.IsMatch(iban, @"^\D\D00.+|^\D\D01.+|^\D\D99.+")) return "err.iban.invalid";
            string countryCode = iban.Substring(0, 2);
            if (iban.Length != 26) return "err.iban.invalid.length";

            if (!Regex.IsMatch(iban.Remove(0, 4), @"\d{5}[a-zA-Z0-9]{17}")) return "err.iban.invalid";
            string modifiedIban = iban.ToUpper().Substring(4) + iban.Substring(0, 4);
            modifiedIban = Regex.Replace(modifiedIban, @"\D", m => ((int)m.Value[0] - 55).ToString());
            int remainer = 0;
            while (modifiedIban.Length >= 7)
            {
                remainer = int.Parse(remainer + modifiedIban.Substring(0, 7)) % 97;
                modifiedIban = modifiedIban.Substring(7);
            }
            remainer = int.Parse(remainer + modifiedIban) % 97;

            if (remainer != 1) return "err.iban.invalid";
            return "OK";
        }

        public static string ReverseString(string Value)
        {
            if (string.IsNullOrEmpty(Value)) return string.Empty;

            string Result = string.Empty;

            for (int i = 0; i < Value.Length; i++)
                Result += Value.Substring(Value.Length - i - 1, 1);

            return Result;
        }
        public static string GetThumbPath(string Path, int Width)
        {
            if (Path.IndexOf(".") <= 0) return Path;

            string Path1 = Path.Substring(0, Path.LastIndexOf(".")),
                Path2 = Path.Substring(Path.LastIndexOf("."));

            return string.Format("{0}_w{1}{2}", Path1, Width, Path2);
        }
        public static string Base64Encode(string Value)
        {
            try
            {
                return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Value));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return "";
            }
        }
        public static string StripHtmlTags(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        public static string ToFriendlyName(this string Value)
        {
            if (string.IsNullOrEmpty(Value)) return "";

            //char[] arrChars = CapitalToLower(Value).ReplaceTurkishChars().Replace("'", "").ToCharArray(), Removals = { '-', ' ' };
            char[] arrChars = Value.ToLower().ReplaceTurkishChars().Replace("'", "").ToCharArray(), Removals = { '-', ' ' };

            string Result = "";

            for (int i = 0; i < arrChars.Length; i++)
            {
                if ((Convert.ToInt16(arrChars[i]) >= Convert.ToInt16('0')) && (Convert.ToInt16(arrChars[i]) <= Convert.ToInt16('9')))
                    Result += arrChars[i].ToString();
                else
                {
                    if (((Convert.ToInt16(arrChars[i]) >= Convert.ToInt16('a')) && (Convert.ToInt16(arrChars[i]) <= Convert.ToInt16('z')))
                        ||
                    (Convert.ToInt16(arrChars[i]) >= Convert.ToInt16('A')) && (Convert.ToInt16(arrChars[i]) <= Convert.ToInt16('Z')))
                        Result += arrChars[i].ToString();
                    else
                        Result += "-";
                }
            }
            while (Result.IndexOf("--") >= 0)
                Result = Result.Replace("--", "-");
            return Result.Trim(Removals);
        }

        public static string CapitalToLower(string str)   // Baþ harf büyük, diðer harfler küçük yapýlýr.
        {
            str = str.ToLower();
            char[] letters = str.ToCharArray();
            letters[0] = Char.ToUpper(letters[0]);
            str = new string(letters);
            return str;
        }

        /// <summary>
        /// http://www.google.com/q.asp => google.com
        /// </summary>
        /// <param name="URL">URL to convert.</param>
        /// <returns></returns>
        public static string ConvertURLToName(string URL)
        {
            if (string.IsNullOrEmpty(URL)) return string.Empty;

            string Result = URL.ToLower();

            if (Result.StartsWith("http://")) Result = Result.Substring(7);
            if (Result.Contains("/")) Result = Result.Substring(0, Result.IndexOf("/"));
            if (Result.StartsWith("www")) Result = Result.Substring(4);

            return Result;
        }
        public static string Base64Decode(string Value)
        {
            try
            {
                System.Text.UTF8Encoding objEncoding = new System.Text.UTF8Encoding();
                System.Text.Decoder objUTF8Decoder = objEncoding.GetDecoder();

                byte[] arrBytes = Convert.FromBase64String(Value);
                return objEncoding.GetString(arrBytes);

                //int charCount = objUTF8Decoder.GetCharCount(arrBytes, 0, arrBytes.Length);
                //char[] arrChars = new char[charCount];
                //objUTF8Decoder.GetChars(arrBytes, 0, arrBytes.Length, arrChars, 0);
                //return new String(arrChars);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return "";
            }
        }

        public static DateTime DateTimeTo235959(DateTime Value)
        {
            return new DateTime(Value.Year, Value.Month, Value.Day, 23, 59, 0);
        }

        public static string ToMoney(decimal Value)
        {
            return Value.ToString("#,000.00");
        }

        public static string ToRGB(Color oColor)
        {
            return string.Concat("#", (oColor.ToArgb() & 0x00FFFFFF).ToString("X6"));
        }

        public static string DateDiff(DateTime dtStart)
        {
            return DateDiff(dtStart, DateTime.Now);
        }

        public static string DateDiff(DateTime dtStart, DateTime dtEnd)
        {
            System.Text.StringBuilder oResult = new System.Text.StringBuilder();
            TimeSpan diff = dtEnd.Subtract(dtStart);

            int iDays = Convert.ToInt32(Math.Floor(diff.TotalDays));
            int iHours = Convert.ToInt32(Math.Floor(diff.TotalHours % 24));
            int iMinutes = Convert.ToInt32(Math.Floor(diff.TotalMinutes % 60));

            if (iDays > 0)
                oResult.AppendFormat("{0} {1}, ", iDays, ResourceManager.GetResource("l_gun"));
            if (iHours > 0)
                oResult.AppendFormat("{0} {1}, ", iHours, ResourceManager.GetResource("l_saat"));
            oResult.AppendFormat("{0} {1} ", iMinutes > 0 ? iMinutes : 1, ResourceManager.GetResource("l_dakika"));

            string Result = oResult.ToString();
            if (Result.EndsWith(", "))
                Result = Result.Substring(0, Result.Length - 2);

            return Result + " " + ResourceManager.GetResource("l_once");
        }

        public static string GetSqlCommandDetails(SqlCommand cmd)
        {
            return GetSqlCommandDetails(cmd, true, false);
        }

        public static string GetSqlCommandDetails(SqlCommand cmd, bool ShowCommandText, bool GetFirst500chars)
        {
            System.Text.StringBuilder Result = new System.Text.StringBuilder();

            if (cmd != null)
            {
                string NewLine = System.Environment.NewLine;
                if (ShowCommandText)
                    Result.AppendFormat("{0}{1}", cmd.CommandText, NewLine);

                try
                {
                    if (cmd.Parameters != null)
                        for (int i = 0; i < cmd.Parameters.Count; i++)
                        {
                            if (!GetFirst500chars)
                                Result.AppendFormat("{0}= {1},{2}", cmd.Parameters[i].ParameterName, cmd.Parameters[i].Value.ToString(), NewLine);
                            else
                            {
                                if (cmd.Parameters[i].Value.ToString().Length > 500 &&
                                    (cmd.Parameters[i].SqlDbType == SqlDbType.Char ||
                                    cmd.Parameters[i].SqlDbType == SqlDbType.NChar ||
                                    cmd.Parameters[i].SqlDbType == SqlDbType.NText ||
                                    cmd.Parameters[i].SqlDbType == SqlDbType.NVarChar ||
                                    cmd.Parameters[i].SqlDbType == SqlDbType.Text ||
                                    cmd.Parameters[i].SqlDbType == SqlDbType.VarChar))
                                    Result.AppendFormat("{0}= {1},{2}",
                                        cmd.Parameters[i].ParameterName,
                                        cmd.Parameters[i].Value.ToString().Substring(0, 500),
                                        NewLine);
                                else
                                    Result.AppendFormat("{0}= {1},{2}",
                                        cmd.Parameters[i].ParameterName,
                                        cmd.Parameters[i].Value.ToString(),
                                        NewLine);
                            }
                        }
                }
                catch (Exception)
                {
                }
            }
            return Result.ToString();
        }

        public static string GenerateRandomString(int Length)
        {
            string Result = "";
            string arr = "abcdefghijklmnopqrstuvyzxABCDEFGHIJKLMNOPQRSTVYZX012345678";

            Random a = new Random(Guid.NewGuid().ToString().GetHashCode());

            for (int i = 0; i < Length; i++)
            {
                Result += arr[a.Next(arr.Length - 1)].ToString();
            }
            return Result;
        }

        public static string GenerateCodeString(int Length)
        {
            string Result = "";
            string arr = "ABCDEFGHIJKLMNOPQRSTVYZX012345678";

            Random a = new Random(Guid.NewGuid().ToString().GetHashCode());

            for (int i = 0; i < Length; i++)
            {
                Result += arr[a.Next(arr.Length - 1)].ToString();
            }
            return Result;
        }


        /// <summary>
        /// 12 Ekim 2008
        /// </summary>
        /// <param name="DateToParse"></param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime DateToParse)
        {
            return string.Format("{0} {1}", DateToParse.ToShortDateString(), DateToParse.ToShortTimeString());
            //return DateTimeToString(DateToParse, true, true);
        }

        /// <summary>
        /// 12 Ekim 2008 Çarþamba
        /// </summary>
        /// <param name="DateToParse"></param>
        /// <param name="IncludeDay"></param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime DateToParse, bool IncludeDay)
        {
            return DateTimeToString(DateToParse, IncludeDay, true);
        }

        /// <summary>
        /// Sun, 16 Aug 2009 15:03:35 GMT
        /// </summary>
        /// <param name="dtNow"></param>
        /// <returns></returns>
        public static string DateTimeToShortStringToGMT(DateTime dtNow)
        {
            string[] arrMonthNames = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            string Result = string.Format("{0}, {1} {2} {3} {4}:{5}:{6} GMT",
                dtNow.DayOfWeek.ToString().Substring(0, 3),
                dtNow.Day,
                arrMonthNames[dtNow.Month - 1],
                dtNow.Year,
                dtNow.Hour,
                dtNow.Minute,
                dtNow.Second);
            Result = Result.Replace(":0:", ":00:");
            Result = Result.Replace(" 0:", " 00:");
            return Result;
        }
        public static string ToRfc822DateTime(DateTime dateTime)
        {
            int offset = (int)(TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).TotalHours + 8);
            string timeZone = "+" + offset.ToString(NumberFormatInfo.InvariantInfo).PadLeft(2, '0');

            //------------------------------------------------------------
            //	Adjust time zone based on offset
            //------------------------------------------------------------
            if (offset < 0)
            {
                int i = offset * -1;
                timeZone = "-" + i.ToString(NumberFormatInfo.InvariantInfo).PadLeft(2, '0');

            }

            return dateTime.ToString("ddd, dd MMM yyyy HH:mm:ss " + timeZone.PadRight(5, '0'), DateTimeFormatInfo.InvariantInfo);
        }
        /// <summary>
        /// 12 Ekim 2008
        /// </summary>
        /// <param name="DateToParse"></param>
        /// <param name="IncludeDay"></param>
        /// <param name="IncludeMonth"></param>
        /// <param name="Language"></param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime DateToParse, bool IncludeDay, bool IncludeMonth)
        {
            string Result = string.Empty;
            try
            {
                if (IncludeDay == true)
                    Result += DateToParse.Day.ToString() + " ";

                if (IncludeMonth == true)
                    Result += ResourceManager.GetResource("month" + DateToParse.Month.ToString());

                Result += string.Format(" {0}, {1}", DateToParse.Year, DateToParse.ToShortTimeString());
            }
            catch (Exception)
            {
                Result = "Bilinmiyor";	//Null Value?
            }

            return Result;
        }
        public static string DateTimeToShortString(DateTime DateToParse)
        {
            string Result = string.Empty;
            try
            {
                Result = string.Format("{0} {1} {2}",
                    DateToParse.Day,
                    ResourceManager.GetResource(string.Format("month{0}", DateToParse.Month)),
                    DateToParse.Year);
            }
            catch (Exception)
            {
                Result = "Bilinmiyor";	//Null Value?
            }

            return Result;
        }
        /// <summary>
        /// Tarihin saatlerini yok eder.
        /// <example>
        /// 18.06.2008 11:22:44:0000 => 18.06.2008 00:00:00:0000
        /// </example>
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static DateTime RoundDate(DateTime Value)
        {
            return new DateTime(Value.Year, Value.Month, Value.Day, 0, 0, 0);
        }

        public static int StripNonNumeric(string Value)
        {
            string Result = string.Empty;
            string Numbers = "0123456789";
            for (int i = 0; i < Value.Length; i++)
            {
                if (Numbers.IndexOf(Value.Substring(i, 1)) != -1)
                    Result += Value.Substring(i, 1);
            }

            return Int16.Parse(Result);
        }
        public static void SendMailAsnyc(string From, string To, string Subject, string Body, string Bcc = null)
        {
            //ThreadPool.QueueUserWorkItem(delegate { SendMail(From, To, Subject, Body, Bcc); });
        }

        //public static bool SendMail(MailSender From, string To, string Subject, string Body, string bcc = null)
        //{
        //    string from = string.Concat(From.ToString().ToLowerInvariant(), "@meramobile.com.pk");

        //    return SendMail(from, To, Subject, Body, bcc);
        //}
        public static bool SendMail(string From, string To, string Subject, string Body, int mailType, string bcc = null)
        {
            return SendMail(From, To, Subject, Body);
        }
        public static bool SendMail(string From, string To, string Subject, string Body, bool EnableSsl = false, bool Async = false)
        {
            return SendMail(From, To, Subject, Body, true, EnableSsl, Async);
        }

        public static bool SendMail(string From, string To, string Subject, string Body, bool Format, bool EnableSsl = false, bool Async = false)
        {
            return SendMail(From, To, null, Subject, Body, null, Format, EnableSsl, Async);
        }

        public static bool SendMail(string From, string To, string Bcc, string Subject, string Body, string attachment, bool Format, bool EnableSsl = false, bool Async = false)
        {
            if (!ConfigManager.Current.AllowSendingEmail) return true;

            try
            {
                MailMessage email = new MailMessage();

                foreach (string s in To.SplitToArray(';', true))
                    email.To.Add(s);

                if (!string.IsNullOrEmpty(Bcc))
                    foreach (string s in Bcc.SplitToArray(';', true))
                        email.Bcc.Add(s);

                if (!string.IsNullOrEmpty(attachment))
                    email.Attachments.Add(new Attachment(attachment));

                email.From = new MailAddress(From, "No Reply - getit.com.tr");
                email.Subject = Subject;
                email.Priority = MailPriority.High;
                email.Body = Body;
                email.IsBodyHtml = Format;

                SmtpClient client = null;
                if (To.Contains("hurriyettv.com"))
                    client = new SmtpClient(ConfigManager.Current.SmtpServerAddress, ConfigManager.Current.SmtpServerPort);
                else
                {
                    client = new SmtpClient(ConfigManager.Current.SmtpHostAddress, ConfigManager.Current.SmtpServerPort);
                    client.Credentials = null;
                    client.EnableSsl = true;                    
                    client.Credentials = new NetworkCredential(ConfigManager.Current.SmtpUser, ConfigManager.Current.SmtpPassword);
                }

                if (Async)
                    client.SendAsync(email, "hurriyettv");
                else
                    client.Send(email);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GetExceptionMessageRecursive(Exception ex)
        {
            string sMessage = string.Empty;

            if (ex != null)
            {
                sMessage = string.Concat(ex.Message, "<br>", ex.StackTrace, "<br>");

                if (ex.InnerException != null)
                    sMessage += GetExceptionMessageRecursive(ex.InnerException);
            }
            return string.IsNullOrEmpty(sMessage) ? "exception empty" : sMessage.Replace(" at ", "<br> at ");
        }



        /// <summary>
        /// Replaces \r\n by HTML equivalent (<br />).
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string FormatMultiline(string text)
        {
            if (text == null)
                return "";

            string result = text;
            if (result.Length > 0)
            {
                result = result.Replace("\r\n", "\n");
                result = result.Replace("\r", "\n");
                result = result.Replace("\n", "<br />");
                result = result.Replace(Convert.ToChar(10).ToString(), "<br />");
                result = result.Replace(System.Environment.NewLine, "<br />");
                return result;
            }
            else
                return text;
        }

        /// <summary>
        /// Reads from a file and returns the content by using default Encoding.
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string ReadFromGivenFile(string PathToFile)
        {
            return File.ReadAllText(PathToFile, Encoding.Default);
        }
        #endregion
        /// <summary>
        /// Splits by space character if not specified otherwise by other overloads.
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static string[] SplitForFullTextSearch(string strInput)
        {
            return SplitForFullTextSearch(strInput, " ");
        }

        /// <summary>
        /// Splits by space character if not specified otherwise by other overloads.
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="Seperator"></param>
        /// <returns></returns>
        public static string[] SplitForFullTextSearch(string strInput, char Seperator)
        {
            return SplitForFullTextSearch(strInput, Seperator.ToString());
        }

        /// <summary>
        /// Splits by space character if not specified otherwise by other overloads.
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="Seperator"></param>
        /// <returns></returns>
        public static string[] SplitForFullTextSearch(string strInput, string Seperator)
        {
            // Satýþ Pazarlama "Deneme Yanýlma" Uzmaný
            // =>
            // {"Satýþ", "Pazarlama", "Deneme Yanýlma", "Uzmaný"}
            string[] arrResult = Split(strInput.Trim(' '), Seperator);

            if (strInput.IndexOf("\"") < 0)	// (") karakteri içermiyorsa, Split deðerini döndür.
                return arrResult;

            bool bSplit = false;
            string sResult = string.Empty;
            string sDelimeter = "¿";	// Must be unique
            string sSpacer = " ", Quot = "\"";

            for (int i = 0; i < arrResult.Length; i++)
            {
                if (bSplit == false)
                {
                    if (arrResult[i].IndexOf(Quot) < 0)
                        sResult += arrResult[i] + sDelimeter;
                    else
                    {
                        sResult += arrResult[i];
                        bSplit = true;
                    }
                }
                else
                {
                    if (arrResult[i].IndexOf(Quot) < 0)
                        sResult += sSpacer + arrResult[i];
                    else
                    {
                        sResult += sSpacer + arrResult[i] + sDelimeter;
                        bSplit = false;
                    }
                }
            }
            return Util.Split(sResult.Replace("\"", string.Empty), sDelimeter);
        }

        /// <summary>
        /// Trims and splits the Value and returns as a string array.
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Delimeters"></param>
        /// <returns></returns>
        public static string[] Split(string Value, char[] Delimeters)
        {
            return Value.Trim(Delimeters).Split(Delimeters);
        }


        /// <summary>
        /// Splits the value by commas (,) and returns the result as a string array.
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string[] Split(string Value)
        {
            return Split(Value, ",");
        }

        /// <summary>
        /// Splits the value by Delimeter parameter and returns the result as a string array.
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Delimeter"></param>
        /// <returns></returns>
        public static string[] Split(string Value, string Delimeter)
        {
            char[] delimeter = { Convert.ToChar(Delimeter) };

            return Value.Trim(delimeter).Split(delimeter);
        }
        /// <summary>
        /// Without Extra Comma
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string ArrayToString(ArrayList arr)
        {
            string str = string.Empty;
            for (int i = 0; i < arr.Count; i++)
                str += (i == 0 ? "" : ",") + arr[i].ToString();

            return str.Trim(',');
        }

        public static string ArrayToCommaString(int[] arr)
        {
            string str = "";
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == 0)
                    str += "," + arr[i].ToString() + ",";
                else
                    str += arr[i].ToString() + ",";
            }
            return str.Trim(',');
        }

        public static int[] CommaStringToArray(string value)
        {
            char[] delimeter = { ',' };
            string[] content;
            content = value.Trim(',').Trim(' ').Split(delimeter);
            int[] arr = new int[content.Length];
            int cnt = 0;

            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] != null && content[i] != string.Empty)
                    arr[cnt++] = Int32.Parse(content[i]);
            }
            int[] Result = new int[cnt];
            for (int i = 0; i < cnt; i++)
                Result[i] = arr[i];
            return Result;
        }

        public static string[] CommaStringToStringArray(string value)
        {
            char[] delimeter = { ',' };
            string[] content;
            content = value.Trim(',').Trim(' ').Split(delimeter);
            string[] arr = new string[content.Length];
            int cnt = 0;

            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] != null && content[i] != string.Empty)
                    arr[cnt++] = (content[i]);
            }
            string[] Result = new string[cnt];
            for (int i = 0; i < cnt; i++)
                Result[i] = arr[i];
            return Result;
        }

        public static IQueryable<TSource> Between<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, TKey low, TKey high) where TKey : IComparable<TKey>
        {
            Expression key = Expression.Invoke(keySelector, keySelector.Parameters.ToArray());
            Expression lowerBound = Expression.GreaterThanOrEqual(Expression.Constant(low), key);
            Expression upperBound = Expression.LessThanOrEqual(key, Expression.Constant(high));
            Expression and = Expression.AndAlso(lowerBound, upperBound);
            Expression<Func<TSource, bool>> lambda = Expression.Lambda<Func<TSource, bool>>(and, keySelector.Parameters);
            return source.Where(lambda);
        }

        public static bool CheckIsValidFileName(string value)
        {
            string[] arrTemp = value.Split('_');
            string pattern = string.Empty;
            if (arrTemp.Count() > 2)
            {
                pattern = @"htv_([0-9]+)_([0-9]+).(MP4|mp4|FLV|flv|WMA|wma)$";
            }
            else
            {
                pattern = @"htv_([0-9]+).(MP4|mp4|FLV|flv|WMA|wma)$";
            }

            return (Regex.IsMatch(value, pattern));
        }
    }
}
