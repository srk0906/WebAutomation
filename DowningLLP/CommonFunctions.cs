using FluentAssertions;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;


namespace DowningLLP
{
    class CommonFunctions
    {
        /// <summary>
        /// To get the assembly directory
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        /// <summary>
        /// To get the xml node value
        /// </summary>
        /// <param name="xmlPage">XML Page</param>
        /// <param name="nodeName">XML node name</param>
        /// <returns></returns>
        public static string GetXMLNode(string xmlPage, string nodeName)
        {
            var xdoc = new XmlDocument();
            xdoc.Load(AssemblyDirectory + xmlPage);
            var node = ((XmlElement)xdoc.GetElementsByTagName(nodeName)[0]);
            return node.Cast<XmlNode>().FirstOrDefault().InnerText;
        }

        public static string GetXMLNodeAttribute(string xmlPage, string nodeName, string attributeName)
        {
            var xdoc = new XmlDocument();
            xdoc.Load(AssemblyDirectory + xmlPage);
            var node = ((XmlElement)xdoc.GetElementsByTagName(nodeName)[0]);
            return node.Cast<XmlNode>().FirstOrDefault().Attributes[attributeName].Value;
        }

        public static void IsFileDownloaded_Ext(string fileName)
        {
            string downloadsPath = Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
               "Downloads");
            DirectoryInfo directory = new DirectoryInfo(downloadsPath);
            directory.GetFiles(fileName).Count().Should().BeGreaterOrEqualTo(1);
        }

    }
}
