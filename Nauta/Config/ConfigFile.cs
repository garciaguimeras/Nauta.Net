using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Nauta.Config
{

    class ConfigData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ProxyServer { get; set; }
        public string ProxyPort { get; set; }
    }

    class ConfigFile
    {

        public const string FILENAME = "config.xml";

        public static ConfigData Read()
        {
            if (!File.Exists(FILENAME))
            {
                Write(new ConfigData { UserName = "", Password = "", ProxyServer = "", ProxyPort = "" });
            }

            XDocument xDoc = XDocument.Load(FILENAME);
            var root = xDoc.Elements("config").FirstOrDefault();
            if (root == null)
                return new ConfigData();

            var children = root.Elements();
            var username = children.Where(e => e.Name.ToString().Equals("username")).FirstOrDefault();
            var password = children.Where(e => e.Name.ToString().Equals("password")).FirstOrDefault();
            var proxyServer = children.Where(e => e.Name.ToString().Equals("proxy-server")).FirstOrDefault();
            var proxyPort = children.Where(e => e.Name.ToString().Equals("proxy-port")).FirstOrDefault();

            return new ConfigData
            {
                UserName = username != null ? username.Value : "",
                Password = password != null ? password.Value : "",
                ProxyServer = proxyServer != null ? proxyServer.Value : "",
                ProxyPort = proxyPort != null ? proxyPort.Value : ""
            };
        }

        public static void Write(ConfigData data)
        {
            XDocument xDoc = new XDocument();

            XElement root = new XElement(XName.Get("config"));

            XElement username = new XElement(XName.Get("username"));
            username.Value = data.UserName;
            root.Add(username);

            XElement password = new XElement(XName.Get("password"));
            password.Value = data.Password;
            root.Add(password);

            XElement proxyServer = new XElement(XName.Get("proxy-server"));
            proxyServer.Value = data.ProxyServer;
            root.Add(proxyServer);

            XElement proxyPort = new XElement(XName.Get("proxy-port"));
            proxyPort.Value = data.ProxyPort;
            root.Add(proxyPort);

            xDoc.Add(root);
            xDoc.Save(FILENAME);
        }

    }

}
