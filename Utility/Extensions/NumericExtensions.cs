using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public static class NumericExtensions
    {
        private static int encAdd = 3, encMultiply = 15, encFinalAdd = 8;
    
        /// <summary>
        /// Kredi kartlarının çoğunun aldığı formata çevirir: 1.234,56 => 1234.56
        /// </summary>
        /// <param name="input">değer</param>
        /// <returns></returns>
        public static string ToCCSafeString(this decimal input)
        {
            return input.ToString("N2").Replace(".", "").Replace(",", ".");
        }

        /// <summary>
        /// Virgülden sonra 2 basamak olacak şekilde yuvarlar.
        /// </summary>
        /// <param name="str">Değer</param>
        /// <returns>Math.round(değer, 2)</returns>
        public static decimal ToRound(this decimal str)
        {
            return Math.Round(str, 2);
        }
        /// <summary>
        /// Bir sayıyı şifreler ve string olarak döner.
        /// </summary>
        /// <param name="Number">Şifrelenecek sayı.</param>
        /// <returns>Bir sayıyı şifreler ve string olarak döner.</returns>
        public static long EncryptNumber(this object Number)
        {
            long Result = ((Number.ToLong() + encAdd) * encMultiply) + encFinalAdd;
            return Result.ToString().Reverse().ToLong();
        }

        /// <summary>
        /// EncryptNumber ile şifrelenmiş sayıyı eski haline getirir.
        /// </summary>
        /// <param name="Number">Şifresi çözülecek olan sayı</param>
        /// <returns>Çözülebilirse sayı, çözülemezse 0 döner.</returns>
        public static long DecryptNumber(this object Number)
        {
            try
            {
                long Result = Number.ToString().Reverse().ToLong() - encFinalAdd;
                if (Result % encMultiply == 0)
                {
                    Result /= encMultiply;
                    Result -= encAdd;
                    return Result > 0 ? Result : 0;
                }
            }
            catch { return 0; }
            return 0;
        }
        /// <summary>
        /// Virgülle ayrılmış string'i split ederek int array döndürür
        /// </summary>
        /// <param name="value">nesne</param>
        /// <returns></returns>
        public static List<int> ToIntList(this string value)
        {
            List<int> arr = new List<int>();
            int i = 0;
            value.ToStringList().ForEach(delegate(string s)
            {
                if (int.TryParse(s, out i))
                    arr.Add(i);
            });
            return arr;
        }
        /// <summary>
        /// Virgülle ayrılmış string'i split ederek long list döndürür
        /// </summary>
        /// <param name="value">nesne</param>
        /// <returns></returns>
        public static List<long> ToLongList(this string value, char [] seperator = null)
        {
            var arr = new List<long>();
            long i = 0;
            value.ToStringList(seperator).ForEach(delegate(string s)
            {
                if (long.TryParse(s, out i))
                    arr.Add(i);
            });
            return arr;
        }
        /// <summary>
        /// Virgülle ayrılmış tarihleri parse ederek byte array döndürür
        /// </summary>
        /// <param name="value">nesne</param>
        /// <returns></returns>
        public static List<byte> ToByteList(this string value)
        {
            List<byte> arr = new List<byte>();
            try
            {
                byte b = 0;
                value.NullToString().ToStringList().ForEach(delegate(string s)
                {
                    if (byte.TryParse(s, out b))
                        arr.Add(b);
                });
            }
            catch { }

            return arr;
        }
        public static string JoinToString<T>(this List<T> arr, string seperator = ",")
        {
            return string.Join(seperator, arr.ToArray());
        }
        /// <summary>
        /// Bir nesneyi önce long'a çevirir. Bu değer min ve max arasında bir değer çıkarsa bunu; yoksa default değeri döndürür.
        /// </summary>
        /// <param name="value">değer</param>
        /// <param name="defaultValue">Default değer</param>
        /// <param name="minValue">min değer</param>
        /// <param name="maxValue">max değer</param>
        /// <returns></returns>
        public static long ToDefaultLong(this object value, long defaultValue = 0, long minValue = 0, long maxValue = long.MaxValue)
        {
            long res = value.ToLong();
            return (res <= maxValue && res >= minValue) ? res : defaultValue;
        }

        /// <summary>
        /// Bir nesneyi önce decimal'a çevirir. Bu değer min ve max arasında bir değer çıkarsa bunu; yoksa default değeri döndürür.
        /// </summary>
        /// <param name="value">değer</param>
        /// <param name="defaultValue">Default değer</param>
        /// <param name="minValue">min değer</param>
        /// <param name="maxValue">max değer</param>
        /// <returns></returns>
        public static decimal ToDefaultDecimal(this object value, decimal defaultValue = 0, decimal minValue = 0, decimal maxValue = decimal.MaxValue)
        {
            decimal res = value.ToDecimal();
            return (res <= maxValue && res >= minValue) ? res : defaultValue;
        }

        /// <summary>
        /// Bir nesneyi önce int'e çevirir. Bu değer min ve max arasında bir değer çıkarsa bunu; yoksa default değeri döndürür.
        /// </summary>
        /// <param name="value">değer</param>
        /// <param name="defaultValue">Default değer</param>
        /// <param name="minValue">min değer</param>
        /// <param name="maxValue">max değer</param>
        /// <returns></returns>
        public static int ToDefaultInt(this object value, int defaultValue = 0, int minValue = 0, int maxValue = int.MaxValue)
        {
            return (int)ToDefaultLong(value, defaultValue, minValue, maxValue);
        }
        /// <summary>
        /// Bir nesneyi önce short'a çevirir. Bu değer min ve max arasında bir değer çıkarsa bunu; yoksa default değeri döndürür.
        /// </summary>
        /// <param name="value">değer</param>
        /// <param name="defaultValue">Default değer</param>
        /// <param name="minValue">min değer</param>
        /// <param name="maxValue">max değer</param>
        /// <returns></returns>
        public static short ToDefaultShort(this object value, short defaultValue = 0, short minValue = 0, short maxValue = short.MaxValue)
        {
            return (short)ToDefaultLong(value, defaultValue, minValue, maxValue);
        }
        /// <summary>
        /// Bir nesneyi önce byte'a çevirir. Bu değer min ve max arasında bir değer çıkarsa bunu; yoksa default değeri döndürür.
        /// </summary>
        /// <param name="value">değer</param>
        /// <param name="defaultValue">Default değer</param>
        /// <param name="minValue">min değer</param>
        /// <param name="maxValue">max değer</param>
        /// <returns></returns>
        public static byte ToDefaultByte(this object value, byte defaultValue = 0, byte minValue = 0, byte maxValue = byte.MaxValue)
        {
            return (byte)ToDefaultLong(value, defaultValue, minValue, maxValue);
        }

    }
}
