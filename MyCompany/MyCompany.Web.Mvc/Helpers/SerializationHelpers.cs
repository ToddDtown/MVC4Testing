using System.IO;
using System.Xml.Serialization;

namespace MyCompany.Web.Mvc.Helpers.Serialization
{
    public static class SerializationHelpers<T>
    {
        public static T FromXml(string input)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            var stringReader = new StringReader(input);
            var obj = (object)default(T);
            try
            {
                obj = xmlSerializer.Deserialize(stringReader);
            }
            finally
            {
                stringReader.Close();
            }
            return (T)obj;
        }

        public static string ToXmlString(T obj)
        {
            var serializer = new XmlSerializer(obj.GetType());
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }
    }
}
