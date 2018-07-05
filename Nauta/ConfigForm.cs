using Nauta.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nauta
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();

            var data = ConfigFile.Read();
            textBoxUser.Text = data.UserName;
            textBoxPassword.Text = data.Password;
            textBoxProxy.Text = data.ProxyServer;
            textBoxPort.Text = data.ProxyPort;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var data = new ConfigData
            {
                UserName = textBoxUser.Text,
                Password = textBoxPassword.Text,
                ProxyServer = textBoxProxy.Text,
                ProxyPort = textBoxPort.Text
            };
            ConfigFile.Write(data);
            Close();
        }

    }
}
