using System;
using System.IO;
using System.Text;

namespace Framework.Serializer
{
	public class DataContractSrlzr
	{

		public static string Serialize(object objectToSerialize, bool formatXml)
		{
			return Serialize(objectToSerialize, System.Text.Encoding.UTF8, formatXml);
		}

		public static string Serialize(object objectToSerialize, System.Text.Encoding encoding)
		{
			return Serialize(objectToSerialize, encoding, false);
		}

		public static string Serialize(object objectToSerialize, System.Text.Encoding encoding, bool formatXml)
		{
			System.Runtime.Serialization.DataContractSerializer s = new System.Runtime.Serialization.DataContractSerializer(objectToSerialize.GetType());

			string xml = "";

			using (MemoryStream ms = new MemoryStream())
			{
				s.WriteObject(ms, objectToSerialize);
				byte[] bytes = ms.GetBuffer();
				xml = encoding.GetString(bytes);
			}

			if (formatXml)
				return Utility.FormatXml(xml);
			else
				return xml;
		}

		public static object Deserialize(Type type, string xml)
		{
			System.Runtime.Serialization.DataContractSerializer s = new System.Runtime.Serialization.DataContractSerializer(type);

			byte[] bytes = System.Text.Encoding.Unicode.GetBytes(xml);

			object obj = null;

			using (MemoryStream ms = new MemoryStream())
			{
				ms.Write(bytes, 0, bytes.GetLength(0));
				ms.Position = 0;
				obj = s.ReadObject(ms);
			}

			return obj;
		}
	}
}
