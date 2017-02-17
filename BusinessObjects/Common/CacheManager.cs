using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Caching;
using Utility;

namespace BusinessObjects.Common
{
    public class CacheManager
    {
        private static Dictionary<string, CachedItem> Items = new Dictionary<string, CachedItem>();
        private static object Locker = 1;
        private static Dictionary<string, object> keyLocks = new Dictionary<string, object>();

        /// <summary>
        /// Kaç saniyede bir expire item'larý kontrol etsin? (default 60)
        /// </summary>
        public static double SyncIntervalSecs = 60d;

        /// <summary>
        /// Get'te expireDate verilmemiþse, kaç saniyeliðine cache'lensin? (default 300)
        /// </summary>
        public static double DefaultCacheSecs = 300d;

        /// <summary>
        /// Son senkronizasyon / expire zamaný.
        /// </summary>
        public static DateTime LastSync = DateTime.Now;

        /// <summary>
        /// Cache key collection
        /// </summary>
        public static List<string> CacheKeys { get { return Items.Keys.ToList<string>(); } }

        /// <summary>
        /// Cache'teki item sayýsý
        /// </summary>
        public static int Count { get { return Items.Count; } }

        #region Get Operations
        /// <summary>
        /// Cache'ten nesne döner, cache'te yoksa execute eder, cache atar ve nesneyi döner.
        /// </summary>
        /// <typeparam name="T">Nesne türü (string, int, custom class)</typeparam>
        /// <param name="key">cache key (homepageproducts)</param>
        /// <param name="subKey">cache sub key (product id)</param>
        /// <param name="classType">Cache'te nesne yoksa, nesneyi dönecek statik fonksiyonu içeren class türü</param>
        /// <param name="functionName">Cache'te nesne yoksa, nesneyi dönecek statik fonksiyonun adý</param>
        /// <param name="functionParams">Cache'te nesne yoksa, nesneyi dönecek statik fonksiyona gönderilecek parametreler (Önemli: optional parametreler boþ geçilemiyor generic nedeniyle)</param>
        /// <returns></returns>
        public static T Get<T>(string key, object subKey, Type classType, string functionName, params object[] functionParams)
        {
            return Get<T>(CombineCacheKeys(key, subKey), DateTime.Now.AddSeconds(DefaultCacheSecs), CacheItemPriority.Normal, classType, functionName, functionParams);
        }
        /// <summary>
        /// Cache'ten nesne döner, cache'te yoksa execute eder, cache atar ve nesneyi döner.
        /// </summary>
        /// <typeparam name="T">Nesne türü (string, int, custom class)</typeparam>
        /// <param name="key">cache key (homepageproducts)</param>
        /// <param name="classType">Cache'te nesne yoksa, nesneyi dönecek statik fonksiyonu içeren class türü</param>
        /// <param name="functionName">Cache'te nesne yoksa, nesneyi dönecek statik fonksiyonun adý</param>
        /// <param name="functionParams">Cache'te nesne yoksa, nesneyi dönecek statik fonksiyona gönderilecek parametreler (Önemli: optional parametreler boþ geçilemiyor generic nedeniyle)</param>
        /// <returns></returns>
        public static T Get<T>(string key, Type classType, string functionName, params object[] functionParams)
        {
            return Get<T>(key, DateTime.Now.AddSeconds(DefaultCacheSecs), CacheItemPriority.Normal, classType, functionName, functionParams);
        }
        public static T Get<T>(string key, object subKey, CacheItemPriority priority, Type classType, string functionName, params object[] functionParams)
        {
            return Get<T>(CombineCacheKeys(key, subKey), DateTime.Now.AddSeconds(DefaultCacheSecs), priority, classType, functionName, functionParams);
        }
        public static T Get<T>(string key, CacheItemPriority priority, Type classType, string functionName, params object[] functionParams)
        {
            return Get<T>(key, DateTime.Now.AddSeconds(DefaultCacheSecs), priority, classType, functionName, functionParams);
        }
        public static T Get<T>(string key, object subKey, DateTime expireDate, Type classType, string functionName, params object[] functionParams)
        {
            return Get<T>(CombineCacheKeys(key, subKey), expireDate, CacheItemPriority.Normal, classType, functionName, functionParams);
        }
        public static T Get<T>(string key, DateTime expireDate, Type classType, string functionName, params object[] functionParams)
        {
            return Get<T>(key, expireDate, CacheItemPriority.Normal, classType, functionName, functionParams);
        }
        public static T Get<T>(string key, object subKey, DateTime expireDate, CacheItemPriority priority, Type classType, string functionName, params object[] functionParams)
        {
            return Get<T>(CombineCacheKeys(key, subKey), expireDate, priority, classType, functionName, functionParams);
        }
        public static T Get<T>(string key, DateTime expireDate, CacheItemPriority priority, Type classType, string functionName, params object[] functionParams)
        {
            T Result = default(T);
            CheckSync();

            if (Exists(key))
            {
                Result = (T)Items[key].Data;
                if (Result != null)
                    Items[key].Accessed();
            }
            else
            {
                keyLocks[key] = true;

                lock (keyLocks[key]) // Avoiding simultaneous execution on expire.
                {
                    if (!Exists(key))
                    {
                        Result = Execute<T>(key, classType, functionName, functionParams);
                        Add(key, Result, expireDate, priority);
                    }
                    else
                    {
                        Result = (T)Items[key].Data;
                        if (Result != null)
                            Items[key].Accessed();
                    }
                }
            }

            return Result;
        }
        #endregion

        #region Add Operations
        public static void Add(string key, object data)
        {
            Add(key, data, DateTime.Now.AddSeconds(DefaultCacheSecs));
        }
        public static void Add(string key, object data, CacheItemPriority priority)
        {
            Add(key, data, DateTime.Now.AddSeconds(DefaultCacheSecs), priority);
        }
        public static void Add(string key, object data, DateTime expireDate)
        {
            Add(key, data, expireDate, CacheItemPriority.Normal);
        }
        public static void Add(string key, object data, DateTime expireDate, CacheItemPriority priority)
        {
            bool exists = Exists(key);

            lock (Locker)
            {
                exists = Exists(key);

                if (exists)
                {
                    Items[key].Data = data;
                    Items[key].Priority = priority;
                    Items[key].ExpireDate = expireDate;
                    Items[key].Accessed();
                }
                else
                    Items.Add(key, new CachedItem(key, data, expireDate, priority));
            }
        }
        #endregion

        #region Utility Functions
        public static List<CachedItem> GetStats()
        {
            List<CachedItem> arr = new List<CachedItem>();
            foreach (string key in CacheKeys)
                arr.Add(Items[key]);

            return arr;
        }
        public static bool Exists(string BaseKey, object SubKey)
        {
            return Exists(CombineCacheKeys(BaseKey, SubKey));
        }

        public static bool Exists(string Key)
        {
            return Items.ContainsKey(Key);
        }

        private static T Execute<T>(string key, Type container, string function, params object[] parameters)
        {
            keyLocks[key] = true;

            try
            {
                lock (keyLocks[key])
                {
                    string propertyName = null;
                    char[] sep = { '.', ':' };

                    if (function.IndexOfAny(sep) > 0)
                    {
                        string[] arr = function.Split(sep);
                        function = arr[0];
                        propertyName = arr[1];

                        object Instance = container.GetMethod(function).Invoke(container.GetConstructor(Type.EmptyTypes), parameters);
                        Type objectType = Instance.GetType();
                        PropertyInfo oInfo = objectType.GetProperty(propertyName);
                        return (T)oInfo.GetValue(Instance, null);
                    }
                    else
                        return (T)container.GetMethod(function).Invoke(container.GetConstructor(Type.EmptyTypes), parameters);

                }
            }
            catch (Exception )
            {
                //string message = string.Format("CacheManager.Execute({0}.{1}) {2} için null döndü.", container.ToString(), function, key);
                
                //ExceptionManager.Publish(new Exception(message, ex));
                return default(T);
            }
        }
        private static void CheckSync()
        {
            if (LastSync.AddSeconds(SyncIntervalSecs) >= DateTime.Now) return;

            bool callGC = false;

            lock (Locker)
            {
                foreach (string key in CacheKeys)
                {
                    if (Items[key].Expired)
                    {
                        Items.Remove(key);
                        callGC = true;
                    }
                }
            }
            LastSync = DateTime.Now;
            if (callGC) GC.Collect();
        }

        private static string CombineCacheKeys(object BaseKey, object SubKey)
        {
            return string.Format("{0}:{1}", BaseKey, SubKey);
        }

        public static bool RemoveItem(string Key, object subKey = null)
        {
            if (subKey != null) Key = CombineCacheKeys(Key, subKey);
            bool Result = false;

            lock (Locker)
            {
                Result = Items.Remove(Key) && keyLocks.Remove(Key);
            }
            return Result;
        }

        public static void RestartCache()
        {
            lock (Locker)
            {
                Items.Clear();
                keyLocks.Clear();
                GC.Collect();
            }
        }
        #endregion
    }

    public class CachedItem
    {
        private string _CacheKey;
        private object _Data;
        private int _HitCount;
        private DateTime _CreateDate;
        private DateTime _ExpireDate;
        private DateTime _LastAccess;
        private CacheItemPriority _Priority;

        public string CacheKey { get { return _CacheKey; } set { _CacheKey = value; } }
        public object Data { get { return _Data; } set { _Data = value; } }
        public int HitCount { get { return _HitCount; } set { _HitCount = value; } }
        public DateTime CreateDate { get { return _CreateDate; } set { _CreateDate = value; } }
        public DateTime ExpireDate { get { return _ExpireDate; } set { _ExpireDate = value; } }
        public DateTime LastAccess { get { return _LastAccess; } set { _LastAccess = value; } }
        public CacheItemPriority Priority { get { return _Priority; } set { _Priority = value; } }

        public CachedItem(string cacheKey, object data) : this(cacheKey, data, CacheItemPriority.Normal) { }
        public CachedItem(string cacheKey, object data, CacheItemPriority priority) : this(cacheKey, data, DateTime.MinValue, priority) { }
        public CachedItem(string cacheKey, object data, DateTime expireDate) : this(cacheKey, data, expireDate, CacheItemPriority.Normal) { }
        public CachedItem(string cacheKey, object data, DateTime expireDate, CacheItemPriority priority)
        {
            this.CacheKey = cacheKey;
            this.Data = data;
            this.HitCount = 1;
            this.CreateDate = DateTime.Now;
            this.ExpireDate = expireDate;
            this.Priority = priority;
        }
        public void Accessed()
        {
            this.HitCount++;
            this.LastAccess = DateTime.Now;
        }
        public bool Expired { get { return this.ExpireDate != DateTime.MinValue && this.ExpireDate <= DateTime.Now; } }
        public double Importance { get { return Math.Pow(Convert.ToDouble(this.Priority), 1d) * this.HitCount; } }
    }
}
