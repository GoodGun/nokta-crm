using System;
using System.IO;
using System.Text;

namespace Framework.Serializer
{

	public class SoapSerializer
	{

		public static string Serialize(object objectToSerialize, bool formatXml)
		{
			return Serialize(objectToSerialize, System.Text.Encoding.UTF8, formatXml);
		}

		public static string Serialize(object objectToSerialize, System.Text.Encoding encoding, bool formatXml)
		{
			try
			{
				MemoryStream mem = new System.IO.MemoryStream();
				System.Runtime.Serialization.Formatters.Soap.SoapFormatter formatter = new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();
				formatter.Serialize(mem,objectToSerialize);
				mem.Flush();
				mem.Position = 0;

				byte[] data =  mem.ToArray();

				string xml = encoding.GetString(data,0,data.Length);

				if (formatXml)
					return Utility.FormatXml(xml);
				else
					return xml;
			}
			catch(System.Runtime.Serialization.SerializationException ex)
			{
				throw new Exception("Serialization Error: " + ex.Message, ex);
			}
			catch(Exception ex)
			{
				throw new Exception("Serialization Error: " + ex.Message, ex);
			}
		}
	}
}
