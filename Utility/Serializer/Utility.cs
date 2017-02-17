using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Text.RegularExpressions;

namespace Framework.Serializer
{

	public class Utility
	{

		public static IDictionary<string, object> ConvertJsonToDictionary(string json)
		{
			System.Web.Script.Serialization.JavaScriptSerializer jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
			IDictionary<string, object> dictionary = (IDictionary<string, object>)jsonSerializer.DeserializeObject(json);
			return dictionary;
		}

		public static string FormatXml(string xml)
		{
			return FormatXml(xml, false, null);
		}

		/// <summary>
		/// Strips the XML namespaces.
		/// </summary>
		/// <param name="xml">The XML.</param>
		/// <returns></returns>
		public static string StripXmlNameSpaces(string xml)
		{
			string strXMLPattern = @"xmlns(:\w+)?="".+""";
			string result = Regex.Replace(xml, strXMLPattern, "");

			strXMLPattern = " d2p1:nil=\"true\"  ";
			result = Regex.Replace(result, strXMLPattern, "");

			return result;
		}

		/// <summary>
		/// Formats the provided XML so it's indented and humanly-readable.
		/// </summary>
		/// <param name="inputXml">The input XML to format.</param>
		/// <returns></returns>
		public static string FormatXml(string xml, bool omitXmlDeclaration, string indentChars)
		{

			// XmlDocument.LoadXml sadece tam ve net sentaxl� xml ler i�in
			// string den olu�an ve xml gibi g�r�nen t�m genel xml ler i�in a�a��daki gibi kullanmak gerekiyormu�
			// ��nk� stringler UTF-16 ile �al���rlarm��, sorun ��kmamas� i�in a�a��daki gibi yap�yoruz. 
			// http://stackoverflow.com/questions/310669/why-does-c-xmldocument-loadxmlstring-fail-when-an-xml-header-is-included
			// A�a��daki implementasyon bu soruna ��z�m oluyor

			byte[] encodedString = System.Text.Encoding.UTF8.GetBytes(xml);

			System.Xml.XmlDocument document = new System.Xml.XmlDocument();

			// Put the byte array into a stream and rewind it to the beginning
			using (MemoryStream ms = new MemoryStream(encodedString))
			{
				ms.Flush();
				ms.Position = 0;
				document.Load(ms);
			}

			System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
			
			settings.Indent = true;
			if ( indentChars != null )
				settings.IndentChars = indentChars; // settings.IndentChars = "\t";
			settings.OmitXmlDeclaration = omitXmlDeclaration; // �st xml deklarasyonu kald�r�r
			//settings.NewLineOnAttributes = false;
			//settings.Encoding = encoding;

			System.Text.StringBuilder output = new System.Text.StringBuilder();

			using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(output, settings))
			{
				document.WriteContentTo(writer);
				writer.Flush();
			}

			return output.ToString();
		}

		public static object Clone(object objectToClone)
		{
			try
			{
				MemoryStream mem = new System.IO.MemoryStream();
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				formatter.Serialize(mem, objectToClone);
				mem.Position = 0;
				object newObject = formatter.Deserialize(mem);
				mem.Close();
				return newObject;
			}
			catch (System.Runtime.Serialization.SerializationException ex)
			{
				throw new Exception("Serialization Error: " + ex.Message, ex);
			}
			catch (Exception ex)
			{
				throw new Exception("Serialization Error: " + ex.Message, ex);
			}
		}
	}
}
