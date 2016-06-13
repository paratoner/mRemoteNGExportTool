using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace mRemoteNGExportTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = args[0];
            if (!File.Exists(file))
                throw new FileNotFoundException(string.Format("File not found! : {0}", file));

            System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
            xdoc.Load(file);
            System.Text.StringBuilder b = new System.Text.StringBuilder();
            GetConnections(b, xdoc.SelectNodes("/Connections/Node"));
            var outputFileName = Path.Combine(new FileInfo(file).Directory.FullName, "mRemoteNGConnections.txt");
            if (File.Exists(outputFileName))
                File.Delete(outputFileName);
            using (StreamWriter writer = new StreamWriter(outputFileName))
            {
                writer.Write(b.ToString());
            }
        }
        private static void GetConnections(StringBuilder b, XmlNodeList nodes)
        {
            foreach (System.Xml.XmlNode item in nodes)
            {
                b.AppendLine(string.Concat("Name : ", item.Attributes["Name"].Value, "\t", "HostName : ", item.Attributes["Hostname"].Value));
                if (item.ChildNodes.Count > 0)
                    GetConnections(b, item.ChildNodes);
            }
        }
    }
}
