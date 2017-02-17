using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Caching;
using Utility;

namespace BusinessObjects.Common
{
    public class Cacher
    {
        private static Dictionary<string, CachedItem> Items = new Dictionary<string, CachedItem>();
        private static object Locker = 1;
        private static Dictionary<string, object> keyLocks = new Dictionary<string, object>();

        /// <summary>
        /// Kaç saniyede bir expire item'ları kontrol etsin? (default 60)
        /// </summary>
        public static double SyncIntervalSecs = 60d;

        /// <summary>
        /// Get'te expireDate verilmemişse, kaç saniyeliğine cache'lensin? (default 300)
        /// </summary>
        public static double DefaultCacheSecs = 300d;

        /// <summary>
        /// Son senkronizasyon / expire zamanı.
        /// </summary>
        public static DateTime LastSync = DateTime.Now;

        /// <summary>
        /// Cache key collection
        /// </summary>
        public static List<string> CacheKeys { get { return Items.Keys.ToList<string>(); } }

        /// <summary>
        /// Cache'teki item sayısı
        /// </summary>
        public static int Count { get { return Items.Count; } }

        #region Get Operations
        private static string CombineCacheKeys(Type classType, string functionName, string propertyName, params object[] functionParams)
        {
            return string.Join(":", classType, functionName, propertyName, string.Join("|", functionParams));
        }
        
        public static T Get<T>(Type classType, string functionName, params object[] functionParams)
        {
            return Get<T>(classType, functionName, null, DateTime.MinValue, functionParams);
        }
        public static T Get<T>(Type classType, string functionName, string propertyName, params object[] functionParams)
        {
            return Get<T>(classType, functionName, propertyName, DateTime.MinValue, functionParams);
        }

        public static T Get<T>(Type classType, string functionName, int cacheMinutes, params object[] functionParams)
        {
            return Get<T>(classType, functionName, null, DateTime.Now.AddMinutes(cacheMinutes), functionParams);
        }

        public static T Get<T>(Type classType, string functionName, string propertyName, int cacheMinutes, params object[] functionParams)
        {
            return Get<T>(classType, functionName, propertyName, DateTime.Now.AddMinutes(cacheMinutes), functionParams);
        }
        
        public static T Get<T>(Type classType, string functionName, string propertyName, DateTime expireDate, params object[] functionParams)
        {
            string key = CombineCacheKeys(classType, functionName, propertyName, functionParams);

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
                        Result = Execute<T>(key, classType, functionName, propertyName, functionParams);
                        Add(key, Result, expireDate);
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
        private static void Add(string key, object data)
        {
            Add(key, data, DateTime.Now.AddSeconds(DefaultCacheSecs));
        }
        private static void Add(string key, object data, DateTime expireDate)
        {
            bool exists = Exists(key);

            lock (Locker)
            {
                exists = Exists(key);

                if (exists)
                {
                    Items[key].Data = data;
                    Items[key].ExpireDate = (expireDate == DateTime.MinValue ? DateTime.Now.AddSeconds(DefaultCacheSecs) : expireDate);
                    Items[key].Accessed();
                }
                else
                    Items.Add(key, new CachedItem(key, data, expireDate));
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
        
        public static bool Exists(string Key)
        {
            return Items.ContainsKey(Key);
        }

        private static T Execute<T>(string key, Type container, string function, string propertyName, params object[] parameters)
        {
            keyLocks[key] = true;

            try
            {
                lock (keyLocks[key])
                {
                    if (!string.IsNullOrEmpty(propertyName))
                    {
                        object Instance = container.GetMethod(function).Invoke(container.GetConstructor(Type.EmptyTypes), parameters);
                        Type objectType = Instance.GetType();
                        PropertyInfo oInfo = objectType.GetProperty(propertyName);
                        return (T)oInfo.GetValue(Instance, null);
                    }
                    else
                        return (T)container.GetMethod(function).Invoke(container.GetConstructor(Type.EmptyTypes), parameters);

                }
            }
            catch (Exception)
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


        public static bool RemoveItem(Type classType, string functionName, string propertyName, params object[] functionParams)
        {
            string Key = CombineCacheKeys(classType, functionName, propertyName, functionParams);
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
}
