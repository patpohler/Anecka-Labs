using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Anecka.Labs
{
    public class JSONHelper
    {
        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(ms);
                ms.Close();
                return obj;
            }
            
        }

        public static List<T> DeserializeList<T>(string json)
        {
            List<T> obj = Activator.CreateInstance<List<T>>();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                obj = (List<T>)serializer.ReadObject(ms);
                ms.Close();
                return obj;
            }
        }
    }
}
