using System;
using System.IO;
using System.Text;

namespace Framework.Serializer
{
	public class JsonSerializer
	{
		private static readonly object lockDeserialize = new object();
		private static readonly object lockSerialize = new object();

		public static string Serialize(object item)
		{
			return Serialize(item, System.Text.Encoding.UTF8);
		}

		public static string Serialize(object item, System.Text.Encoding encoding)
		{
			if (item == null)
				return "null";

			using (MemoryStream writer = new MemoryStream())
			{
				System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(item.GetType());
				serializer.WriteObject(writer, item);
				writer.Position = 0;
				using (StreamReader reader = new StreamReader(writer, encoding))
				{
					return reader.ReadToEnd();
				}
			}
		}

		public static T Deserialize<T>(string item)
		{
			return Deserialize<T>(item, System.Text.Encoding.UTF8);
		}

		public static T Deserialize<T>(string item, System.Text.Encoding encoding)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				byte[] bytes = encoding.GetBytes(System.Web.HttpUtility.HtmlDecode(item));
				stream.Write(bytes, 0, bytes.Length);
				stream.Position = 0;
				System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
				object value = serializer.ReadObject(stream);
				if ( value == null )
					return default(T);
				else
					return (T)value;
			}
		}

		public static object Deserialize(Type type, string item)
		{
			return Deserialize(type, item, System.Text.Encoding.UTF8);
		}

		public static object Deserialize(Type type, string item, System.Text.Encoding encoding)
		{
			using(MemoryStream stream = new MemoryStream())
			{
				byte[] bytes = encoding.GetBytes(System.Web.HttpUtility.HtmlDecode(item));
				stream.Write(bytes, 0, bytes.Length);
				stream.Position = 0;
				System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(type);
				object value = serializer.ReadObject(stream);
				if(value == null)
					return null;
				else
					return value;
			}
		}
	}
}
