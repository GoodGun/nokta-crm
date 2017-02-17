using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web.Caching;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Drawing;
using System.Web;

namespace Utility
{
    public class CachedDocumentManager
    {
        public static object _locker = 1;

        public static string GetString(string KeyField, string FilePath, string xQuery, string ValueField)
        {
            string Result = string.Empty;

            XmlDocument Document = GetCachedDocument(FilePath);

            try
            {
                XmlNode node = Document.SelectSingleNode(String.Format(xQuery, KeyField));

                if (node == null)
                    Result = "Bilinmiyor";
                else
                    Result = node.SelectSingleNode(ValueField).InnerText;
            }
            catch (Exception)
            {
            }

            if (Result == "Bilinmiyor")
                throw (new Exception(KeyField + " bulunamadý"));
            return Result;
        }


        public static XmlNodeList GetXmlNodeList(string FilePath, string xQuery)
        {
            string Result = string.Empty;
            XmlNodeList oList = null;

            XmlDocument Document = GetCachedDocument(FilePath);
            try
            {
                oList = Document.SelectNodes(xQuery);
            }
            catch (Exception)
            {
            }
            return oList;
        }

        public static bool GetBool(string KeyField, string FilePath, string xQuery, string ValueField)
        {
            bool blnResult = false;
            try
            {
                blnResult = GetString(KeyField, FilePath, xQuery, ValueField).ToLower().Equals("true");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return blnResult;
        }


        public static int GetInt32(string KeyField, string FilePath, string xQuery, string ValueField)
        {
            int intResult = 0;
            try
            {
                intResult = Convert.ToInt32(GetString(KeyField, FilePath, xQuery, ValueField));
            }
            catch (Exception ex)
            {
                string sMessage = string.Format("KeyField={0}, FilePath={1}, xQuery={2}, ValueField={3}", KeyField, FilePath, xQuery, ValueField);
                ExceptionManager.Publish(new Exception(sMessage, ex));
            }
            return intResult;
        }

        public static long GetLong(string KeyField, string FilePath, string xQuery, string ValueField)
        {
            int lResult = 0;
            try
            {
                lResult = Convert.ToInt16(GetString(KeyField, FilePath, xQuery, ValueField));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lResult;
        }

        /// <summary>
        /// Bu fonksiyon istenen xml dokümanýný döner.
        /// </summary>
        public static XmlDocument GetCachedDocument(string FilePath)
        {
            XmlDocument doc = (XmlDocument)CacheUtil.GlobalCache[FilePath];

            if (doc == null)
            {
                lock (_locker)
                {
                    doc = (XmlDocument)CacheUtil.GlobalCache[FilePath];
                    if (doc == null)
                    {
                        doc = new XmlDocument();
                        doc.Load(FilePath);
                        CacheUtil.GlobalCache.Insert(FilePath, doc, new CacheDependency(FilePath));
                    }
                }
            }

            return doc;
        }
    }
}
