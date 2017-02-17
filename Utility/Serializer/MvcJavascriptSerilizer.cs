using System;
using System.IO;
using System.Text;

namespace Framework.Serializer
{	
	public class MvcJavascriptSerilizer
	{

		public static string Serialize(object item)
		{
			if ( item == null )
				return "null";

			System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
			return serializer.Serialize(item);
		}

		public static T Deserialize<T>(string item) where T : class
		{
			System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
			return serializer.Deserialize<T>(item);
		}
	}
}
