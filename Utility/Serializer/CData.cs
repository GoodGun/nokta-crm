using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace Framework.Serialization
{

	/// <summary>
	/// http://geekswithblogs.net/cmartin/archive/2005/11/30/61705.aspx
	/// http://stackoverflow.com/questions/1379888/how-do-you-serialize-a-string-as-cdata-using-xmlserializer
	/// </summary>
	public class CDataSection : IXmlSerializable
	{

		private string text;

		public CDataSection() { }

		public CDataSection(string text)
		{
			this.text = text;
		}

		public string Text
		{
			get { return text; }
		}

		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.text = reader.ReadString();
		}

		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteCData(this.text);
		}

	}

	public class CDataUtility
	{
		public static string GetTextFromXmlNodeArray(XmlNode[] value)
		{
			 if ( value == null )
			 {
				 return null;
			 }

			 if ( value.Length != 1 )
			 {
				 throw new InvalidOperationException(String.Format("Invalid array length {0}", value.Length));
			 }

			 return value[0].Value;
		}

		public static XmlNode[] GetXmlNodeArrayFromText(string text)
		{
			XmlDocument doc = new XmlDocument();
			return new XmlNode[] { doc.CreateCDataSection(text) };
		}

	}

	internal class SampleClass
	{
		//[XmlElement]
		//public XmlCDataSection SongNameCData
		//{
		//    get
		//    {
		//        XmlDocument doc = new XmlDocument();
		//        return doc.CreateCDataSection(SongName);
		//    }
		//    set
		//    {
		//        SongName = value.ToString();
		//    }
		//}

		// veya aşağıdaki bizim CDataSection a örnek

		[XmlElement("SongName")]
		public CDataSection SongNameCData { get { return new CDataSection(SongName); } set { SongName = value.Text; } }

		[XmlIgnore]
		public string SongName { get; set; }

		// veya "XmlText" olarak ihtiyacımız var ise

		//[XmlText]
		//public XmlNode[] CData_SongName
		//{
		//    get
		//    {
		//        XmlDocument doc = new XmlDocument();
		//        return new XmlNode[] { doc.CreateCDataSection(CData_SongName) };
		//    }
		//    set
		//    {
		//        if ( value == null )
		//        {
		//            CData_SongName = null;
		//            return;
		//        }

		//        if ( value.Length != 1 )
		//        {
		//            throw new InvalidOperationException(String.Format("Invalid array length {0}", value.Length));
		//        }

		//        CData_SongName = value[0].Value;
		//    }
		//}
	}
}
