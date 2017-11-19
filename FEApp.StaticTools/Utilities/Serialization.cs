using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FEApp.StaticTools.Utilities
{
    public static class Serialization
    {
        public static string DataContractSerializeObject<T>(T objectToSerialize)
        {
            using (var output = new StringWriter())
            using (var writer = new XmlTextWriter(output) { Formatting = System.Xml.Formatting.Indented })
            {
                new DataContractSerializer(typeof(T)).WriteObject(writer, objectToSerialize);
                var result = output.GetStringBuilder().ToString();
                return result;
            }
        }

        public static T DataContractDeserializeObject<T>(MemoryStream ms)
        {
            var ser = new DataContractSerializer(typeof(T));

            return (T)ser.ReadObject(ms);
        }
    }
}
