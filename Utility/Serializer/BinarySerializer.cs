using System;
using System.IO;
using System.Text;

namespace Framework.Serializer
{

	public class BinarySerializer
	{
		
		public static byte[] Serialize(object objectToSerialize)
		{
			MemoryStream mem = null;
			try
			{
				mem = new System.IO.MemoryStream();
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				formatter.Serialize(mem,objectToSerialize);
				byte[] data = mem.ToArray();
				return data;
			}
			catch(System.Runtime.Serialization.SerializationException ex)
			{
				throw new Exception("Serialization Error: " + ex.Message, ex);
			}
			catch(Exception ex)
			{
				throw new Exception("Serialization Error: " + ex.Message, ex);
			}
			finally
			{
				if ( mem != null )	mem.Close();
			}
		}

		
		public static object Deserialize(byte[] data)
		{
			MemoryStream mem = null;
			try
			{
				mem = new System.IO.MemoryStream(data);
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				object newObject = formatter.Deserialize(mem);
				return newObject;
			}
			catch(System.Runtime.Serialization.SerializationException ex)
			{
				throw new Exception("Serialization Error: " + ex.Message, ex);
			}
			catch(Exception ex)
			{
				throw new Exception("Serialization Error: " + ex.Message, ex);
			}
			finally{
				if ( mem != null )	mem.Close();
			}
		}
	}

}
