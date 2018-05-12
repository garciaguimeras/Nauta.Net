using Nauta.Config;
using Nauta.Web;
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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var data = ConfigFile.Read();
            var actions = new Actions(data.UserName, data.Password, data.ProxyServer, data.ProxyPort);

            var dict = await actions.HomePage();
            var result = await actions.Login(dict);   
            if (result == null)
            {
                return;
            }

            SessionForm form = new SessionForm();
            form.LoginResponse = result;
            form.ShowDialog();
        }

        private void configurarCuentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm form = new ConfigForm();
            form.ShowDialog();
        }
    }
}
