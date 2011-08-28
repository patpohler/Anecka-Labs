using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Anecka.Labs
{
    public class XMLHelper
    {
        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                obj = (T)serializer.Deserialize(ms);
                ms.Close();
            }
            return obj;

        }

        public static T DeserializeList<T>(string xml, string rootElement, string ns)
        {
            T obj = Activator.CreateInstance<T>();
            using (System.IO.StringReader sr = new System.IO.StringReader(xml))
            {

                //XmlRootAttribute root = new XmlRootAttribute(rootElement);
                //root.Namespace = ns;

                XmlSerializer serializer = new XmlSerializer(obj.GetType(), ns);
                
                obj =(T) serializer.Deserialize(sr);
                sr.Close();
            }
            return obj;
        }
    }
}
