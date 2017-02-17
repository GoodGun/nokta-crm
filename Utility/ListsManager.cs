using System;
using System.Web.Caching;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace Utility
{
	public class ListsManager
	{
        public static string GetLongToString(long sumValue, string dropdownType, string defaultValue = "--")
        {
            var items = GetItemsByListType(dropdownType);
            string result = "";

            foreach (string key in items.Keys)
            {
                long id = items[key].ToLong();
                if ((id & sumValue) == id)
                    result += string.Concat(key, ",");
            }
            if (!result.NullOrEmpty())
                return result.Trim(StringExtensions.comma).Replace(",", ", ");
            else
                return defaultValue;
        }
		public static string GetLookupTexts(string [] SelectedValues, string DropDownType)
		{
			string Result = string.Empty;

			for (int i = 0; i < SelectedValues.Length; i++)
				Result += string.Format("{0}{1}", GetLookupText(SelectedValues[i], DropDownType), (i<SelectedValues.Length -1) ? ",&nbsp;" : string.Empty);

			return Result;
		}

		public static string GetLookupTexts(string sCommaSeperated, string DropDownType)
		{
            char[] sep = { ',' };
            string[] arrValues = sCommaSeperated.Split(sep, StringSplitOptions.RemoveEmptyEntries);
			string Result = string.Empty;

			for (int i = 0; i < arrValues.Length; i++)
				Result += string.Format("{0}{1}", GetLookupText(arrValues[i], DropDownType), (i<arrValues.Length -1) ? ",&nbsp;" : string.Empty);

			return Result;
		}

		public static string GetLookupTexts(int [] SelectedValues, string DropDownType)
		{
			string Result = string.Empty;
			if (SelectedValues != null)
				for (int i = 0; i < SelectedValues.Length; i++)
					Result += GetLookupText(SelectedValues[i], DropDownType) + ((i<SelectedValues.Length -1) ? ",&nbsp;" : string.Empty);
			return Result;
		}

		public static string GetLookupText(int Value, string DropDownType, string defValue="--")
		{
            return GetLookupText(Value.ToString(), DropDownType, defValue);
		}
        public static SortedList GetListItemsByListType(string DropDownType, string defValue="--")
        {
            return GetListItemsByListType(DropDownType, null, defValue);
        }
        public static Dictionary<string, int> GetItemsByListType(string DropDownType)
        {
            string lang = "@Name";
            string xQuery = String.Format("Lists/List[@Key='{0}']/Item", DropDownType);

            var htResult = new Dictionary<string, int>();
            string filePath = string.Format(ConfigManager.Current.pathForLists, Util.CurrentUserLang);

            foreach (XmlNode node in CachedDocumentManager.GetXmlNodeList(filePath, xQuery))
            {
                string v = node.SelectSingleNode("@Value").InnerText;
                XmlNode nameNode = node.SelectSingleNode(lang);
                nameNode = nameNode ?? node.SelectSingleNode("@Name");
                if (nameNode == null || nameNode.InnerText.NullOrEmpty()) continue;

                htResult[nameNode.InnerText] = v.ToInt();
            }
            return htResult;
        }
        
        public static SortedList GetListItemsByListType(string DropDownType, string Filter, string defValue = "--")
        {
            string lang = "@Name";
            string xQuery = String.Format("Lists/List[@Key='{0}']/Item", DropDownType);
            if (!string.IsNullOrEmpty(Filter))
                xQuery += string.Format("[@Relation='{0}']", Filter);
            SortedList htResult = new SortedList();
            string filePath = string.Format(ConfigManager.Current.pathForLists, Util.CurrentUserLang);

            foreach (XmlNode node in CachedDocumentManager.GetXmlNodeList(filePath, xQuery))
            {
                string v = node.SelectSingleNode("@Value").InnerText;
                string n;
                XmlNode nameNode = node.SelectSingleNode(lang);
                if (nameNode == null) nameNode = node.SelectSingleNode("@Name");
                n = (nameNode == null) ? defValue : nameNode.InnerText;

                if (!htResult.ContainsKey(n))
                    htResult.Add(n, v);
            }
            return htResult;
        }
        
        public static string GetLookupText(string Value, string DropDownType, string defValue = "--")
		{
			string text = string.Empty;
			XmlDocument Document = CachedDocumentManager.GetCachedDocument(string.Format(ConfigManager.Current.pathForLists, Util.CurrentUserLang));

			if (Document != null)
			{
				string xPath = "Lists/List[@Key='{0}']/Item[@Value='{1}']";
				string lang = "@Name";

				XmlNode node = Document.SelectSingleNode(String.Format(xPath, DropDownType.ToString(), Value));
				if (node != null)
				{
					XmlNode nLang = node.SelectSingleNode(lang);
                    if (nLang == null)
                        text = defValue;
                    else
                        text = nLang.InnerText.Trim();
				}

				else text = defValue;
			}
			return text.Replace(" ", "");
		}
	}
}
