using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Get_Music.Handlers.Helpers
{
    public class XmlHandler
    {        
        public static XmlDocument LoadXmlDocument(string fileFullName, bool ifNoExistCreate = true)
        {
            if ((!File.Exists(fileFullName)) && (ifNoExistCreate))
            {
                FileHandler.CreateFile(fileFullName);
            }

            var xmlDocument = new XmlDocument();

            try
            {
                xmlDocument.Load(fileFullName);
            }
            catch (XmlException)
            {
            }

            return xmlDocument;
        }


        public static void WriteObjectToFile(string fileFullName, object obj)
        {
            using (DataSet ds = new DataSet())
            {
                using (DataSet ds2 = new DataSet())
                {
                    var doc = XmlSerializerHelper.Serialize(obj);
                    var xmlReader = new XmlNodeReader(doc);

                    try
                    {
                        ds.ReadXml(fileFullName);

                        ds2.ReadXml(xmlReader);
                        ds.Merge(ds2);
                        ds.WriteXml(fileFullName);
                    }
                    catch (System.Xml.XmlException)
                    {
                        ds2.ReadXml(xmlReader);
                        ds2.WriteXml(fileFullName);
                    }
                }                
            }
        }

        public static void WriteObjectToFile(string fileFullName, IEnumerable<object> listObjs)
        {
            using (DataSet ds = new DataSet())
            {
                using (DataSet ds2 = new DataSet())
                {
                    foreach (var obj in listObjs)
                    {
                        using (DataSet ds3 = new DataSet())
                        {
                            var doc = XmlSerializerHelper.Serialize(obj);
                            var xmlReader = new XmlNodeReader(doc);
                            ds3.ReadXml(xmlReader);

                            ds2.Merge(ds3);
                        }
                    }

                    try
                    {
                        var t = ds.ReadXml(fileFullName);
                       
                        ds.Merge(ds2);

                        ds.WriteXml(fileFullName);
                    }
                    catch (System.Xml.XmlException)
                    {
                        ds2.WriteXml(fileFullName);
                    }
                }
            }
        }
    }
}
