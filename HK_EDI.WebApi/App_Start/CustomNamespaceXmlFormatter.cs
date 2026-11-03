using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace HK_EDI.WebApi.App_Start
{
    public class CustomNamespaceXmlFormatter : XmlMediaTypeFormatter
    {
        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,
                                            TransportContext transportContext)
        {
            try
            {
                var xns = new XmlSerializerNamespaces();
                foreach (var attribute in type.GetCustomAttributes(true))
                {
                    var xmlRootAttribute = attribute as XmlRootAttribute;
                    if (xmlRootAttribute != null)
                    {
                        xns.Add(string.Empty, xmlRootAttribute.Namespace);
                    }
                }

                if (xns.Count == 0)
                {
                    xns.Add(string.Empty, string.Empty);
                }

                var task = Task.Factory.StartNew(() =>
                {
                    var serializer = new XmlSerializer(type);
                    serializer.Serialize(writeStream, value, xns);
                });

                return task;
            }
            catch (Exception)
            {
                return base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
            }
        }
    }
}