using System;
using System.Xml.Serialization;
using System.IO;
using System.Web.Caching;
using System.Web;

namespace Utility
{
    public sealed class CacheUtil
    {
        private static object _locker = 1;
        public static Cache GlobalCache { get { return HttpContext.Current != null ? HttpContext.Current.Cache : HttpRuntime.Cache; } }

        public static void Set<T>(T instance, string dependencyFile)
        {
            lock (_locker)
            {
                File.WriteAllText(dependencyFile, ObjectToXML<T>(instance));
            }
        }
        public static T Get<T>(string dependencyFile)
        {
            T result = (T)GlobalCache[dependencyFile];

            if (result == null)
            {
                lock (_locker)
                {
                    if (!File.Exists(dependencyFile))
                    {
                        result = Activator.CreateInstance<T>();
                        File.WriteAllText(dependencyFile, ObjectToXML<T>(result));
                    }
                    else
                        result = XMLToObject<T>(File.ReadAllText(dependencyFile));

                    GlobalCache.Add(dependencyFile,
                        result,
                        new CacheDependency(dependencyFile),
                        DateTime.Now.AddYears(1),
                        Cache.NoSlidingExpiration,
                        CacheItemPriority.NotRemovable,
                        null);
                }
            }
            return result;
        }
        public static T XMLToObject<T>(string serializedString)
        {
            T objResult = Activator.CreateInstance<T>();

            try
            {
                objResult = (T)new XmlSerializer(typeof(T)).Deserialize(new StringReader(serializedString));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objResult;
        }
        public static string ObjectToXML<T>(T instance)
        {
            string s = "";
            XmlSerializer xs = new XmlSerializer(typeof(T));

            using (TextWriter oWriter = new StringWriter())
            {
                xs.Serialize(oWriter, instance);
                oWriter.Close();
                s = oWriter.ToString();
            }
            return s.Replace("utf-16", "utf-8");
        }
    }
}