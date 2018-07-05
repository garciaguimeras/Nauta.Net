using Nauta.Config;
using Nauta.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nauta
{
    public partial class MainForm : Form
    {

        ConfigData data;

        public MainForm()
        {
            InitializeComponent();

            ReadConfigFileAndInitWidgets();
        }

        private void ReadConfigFileAndInitWidgets()
        {
            data = ConfigFile.Read();
            userLabel.Text = data.UserName;
            if (string.IsNullOrEmpty(data.ProxyServer))
                proxyLabel.Text = "No hay proxy definido";
            else
                proxyLabel.Text = "Proxy: http://" + data.ProxyServer + ":" + data.ProxyPort;

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var actions = new Actions(data.UserName, data.Password, data.ProxyServer, data.ProxyPort);

            var dict = await actions.HomePage();
            var result = await actions.Login(dict);   
            if (result == null)
            {
                MessageBox.Show("No se pudo conectar. Revise su conexión, y la configuración de su usuario y contraseña.");
                return;
            }

            if (result.AlreadyConnected)
            {
                MessageBox.Show("Ya existe un usuario conectado previamente. Puedes navegar sin necesidad de entrar con esta cuenta.");
                return;
            }

            SessionForm form = new SessionForm(result);
            form.ShowDialog();
        }

        private void configurarCuentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm form = new ConfigForm();
            form.ShowDialog();
            ReadConfigFileAndInitWidgets();
        }
    }
}
