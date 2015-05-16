using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Get_Music.Handlers.Helpers
{
    public class XmlHelper
    {
        public static XElement GetXElement(XmlNode node)
        {
            XDocument xDoc = new XDocument();
            using (XmlWriter xmlWriter = xDoc.CreateWriter())
            {
                node.WriteTo(xmlWriter);
            }

            return xDoc.Root;
        }

        public static XmlNode GetXmlNode(XElement element)
        {
            using (XmlReader xmlReader = element.CreateReader())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);
                return xmlDoc;
            }
        }

        public static bool IsNodeExist(XmlNode xmlNodeRoot, string nodePath, string nodeValue)
        {
            var nodeList = xmlNodeRoot.SelectNodes(nodePath);

            if (nodeList != null)
            {
                foreach (XmlNode node in nodeList)
                {
                    if (node.InnerText == nodeValue)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsNodeExist(XmlNode xmlNodeRoot, string nodePath, XmlNode xmlNode)
        {
            var nodeList = xmlNodeRoot.SelectNodes(nodePath);

            if (nodeList != null)
            {
                foreach (XmlNode node in nodeList)
                {
                    if (node.OuterXml == xmlNode.OuterXml)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
