using System;
using System.Xml;
using System.Web.Caching;
using System.IO;
using System.Web;

using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;

namespace Utility
{
	public class ResourceManager
	{
		private static object _locker = 1;
        private const string cacheKey = "resContainer";
        private static Dictionary<string, Dictionary<string, string>> _resources = null;

		public static Dictionary<string, Dictionary<string, string>> Resources
		{
			get
			{
                if (CacheUtil.GlobalCache[cacheKey] != null)
                    return _resources;

                lock (_locker)
                {
                    if (CacheUtil.GlobalCache[cacheKey] == null)
                    {
                        string langs = ConfigManager.Current.AvailableLanguages, resourcePath = ConfigManager.Current.pathForResources;

                        char[] Seperators = { ',' };

                        string[] arrLangs = langs.Split(Seperators);
                        _resources = GetResourceContainer(arrLangs);

                        for (int i = 0; i < arrLangs.Length; i++)
                            arrLangs[i] = string.Format(ConfigManager.Current.pathForResources, arrLangs[i]);

                        CacheUtil.GlobalCache.Add(cacheKey, "exists",
                            new CacheDependency(arrLangs),
                            DateTime.Now.AddYears(1),
                            Cache.NoSlidingExpiration,
                            CacheItemPriority.NotRemovable,
                            null);
                    }
                }
                return _resources;
			}
		}

		private static Dictionary<string, Dictionary<string, string>> GetResourceContainer(string [] Languages, 
            string xpath = "Items/Item", string nameKey = "@Name", string valKey = "@Value")
		{
            Dictionary<string, Dictionary<string, string>> Container = new Dictionary<string, Dictionary<string, string>>();

            foreach (string lang in Languages)
            {
                XmlDocument wordDoc = CachedDocumentManager.GetCachedDocument(string.Format(ConfigManager.Current.pathForResources, lang));
                Dictionary<string, string> words = new Dictionary<string, string>();

                foreach (XmlNode oNode in wordDoc.SelectNodes(xpath))
                {
                    try { words.Add(oNode.SelectSingleNode(nameKey).InnerText, oNode.SelectSingleNode(valKey).InnerText); }
                    catch (Exception) { }
                }

                Container.Add(lang, words);
            }
			return Container;
		}

		public static string GetResource(string key, string lang = null)
		{
			try
			{
                if (string.IsNullOrEmpty(lang))
                    lang = Util.CurrentUserLang;
                if (key == null) return "null";
                return Resources[lang].ContainsKey(key) ? Resources[lang][key] : key;
			}
			catch (KeyNotFoundException)
			{
                return key;
			}
		}
	}
}
