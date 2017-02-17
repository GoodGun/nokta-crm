using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;
using System.Linq;

namespace Framework.Serializer
{

	public class XmlSrlzr
	{

		public static string Serialize(object objectToSerialize)
		{
			return Serialize(objectToSerialize, System.Text.Encoding.UTF8, false, false);
		}

		public static string Serialize(object objectToSerialize, System.Text.Encoding encoding)
		{
			return Serialize(objectToSerialize, encoding, false, false);
		}
		
		/*
		    // use reflection to get all derived types
			var knownTypes = Assembly.GetExecutingAssembly().GetTypes().Where( t => typeof(Car).IsAssignableFrom(t) || typeof(Wheel).IsAssignableFrom(t) || typeof(Door).IsAssignableFrom(t)).ToArray();
 
			// prepare to serialize a car object
			XmlSerializer serializer = new XmlSerializer(typeof(Car), knownTypes);
		 */

		public static string Serialize(object objectToSerialize, System.Text.Encoding encoding, bool formatXml, bool removeNamespaces)
		{
			System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(objectToSerialize.GetType());
			
			string xml = "";

			using (MemoryStream memoryStream = new MemoryStream())
			{
				ModifiedXmlTextWriter xmlTextWriter = new ModifiedXmlTextWriter(memoryStream, encoding);
				if (removeNamespaces)
				{
					System.Xml.Serialization.XmlSerializerNamespaces namespaces = new System.Xml.Serialization.XmlSerializerNamespaces();
					namespaces.Add("", "");
					xs.Serialize(xmlTextWriter, objectToSerialize, namespaces);
				}
				else
				{
					xs.Serialize(xmlTextWriter, objectToSerialize);
				}
				byte[] bytes = memoryStream.ToArray();
				xml = encoding.GetString(bytes);
			}

			if (formatXml)
				return Utility.FormatXml(xml);
			else
				return xml;
		}

		public static T Deserialize<T>(string xml) where T : class
		{
			return Deserialize<T>(xml, System.Text.Encoding.UTF8);
		}

		public static T Deserialize<T>(string xml, System.Text.Encoding encoding) where T : class
		{

			System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(T));

			byte[] bytes = encoding.GetBytes(xml);

			T instance = null;
			
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, encoding);
				instance = s.Deserialize(memoryStream) as T;
			}

			return instance;
		}

		public static T Deserialize<T>(Stream stream) where T : class
		{
			var serializer = new XmlSerializer(typeof(T));
			return serializer.Deserialize(stream) as T;
		}

		public static object Deserialize(Type type, Stream stream, Type[] knownTypes)
		{ 
			var serializer = new XmlSerializer(type, knownTypes);
			//var serializer = new XmlSerializer(type);
			return serializer.Deserialize(stream);

			//System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(type);
			//using ( MemoryStream memoryStream = new MemoryStream() )
			//{
			//    stream.CopyTo(memoryStream);
			//    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, System.Text.Encoding.UTF8);
			//    return s.Deserialize(memoryStream);
			//}
		}

		
		/// <summary>
		/// http://weblogs.asp.net/cazzu/archive/2004/01/23/62141.aspx
		/// </summary>
		public class ModifiedXmlTextWriter : XmlTextWriter
		{
			public ModifiedXmlTextWriter(TextWriter w) : base(w) { }

			public ModifiedXmlTextWriter(Stream w, Encoding encoding) : base(w, encoding) { }

			public ModifiedXmlTextWriter(string filename, Encoding encoding) : base(filename, encoding) { }

			bool _skip = false;

			/*
			 
			 <Tab Type="YayinAkisi" ID="YayinAkisi">
				<Title>Yayýn Akýþý</Title>
				<TabImagePath>Resources/VirginRadio/Tabs/YayinAkisi.png</TabImagePath>
				<TabImageHoverPath>Resources/VirginRadio/Tabs/YayinAkisiHover.png</TabImageHoverPath>
			 </Tab>
			 
			 <Tab d6p1:type="Html5Tab" Type="Html5" ID="Etkinlikler">
				<Title>Etkinlikler</Title>
				<TabImagePath>Resources/VirginRadio/Tabs/YayinAkisi.png</TabImagePath>
				<TabImageHoverPath>Resources/VirginRadio/Tabs/YayinAkisiHover.png</TabImageHoverPath>
				<Url>http://test.virginradioturkiye.com</Url>
			 </Tab>

			 */

			public override void WriteStartAttribute(string prefix, string localName, string ns)
			{
				if ( prefix == "xmlns" && ( localName == "xsd" || localName == "xsi" ) ) // Omits XSD and XSI declarations. 
				{
					_skip = true;
					return;
				}

				if ( !String.IsNullOrEmpty(ns) && localName == "type" ) // Omits Derived ":type" declarations like "d6p1:type"
				{ 
					_skip = true; 
					return; 
				}

				base.WriteStartAttribute(prefix, localName, ns);
			}

			public override void WriteString(string text)
			{
				if ( _skip )
				{
					return;
				}
				base.WriteString(text);
			}

			public override void WriteEndAttribute()
			{
				if ( _skip )
				{	
					// Reset the flag, so we keep writing. 
					_skip = false; 
					return;
				}
				base.WriteEndAttribute();
			}
		}
	}
}
