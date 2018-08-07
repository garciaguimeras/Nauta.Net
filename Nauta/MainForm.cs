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

            if (string.IsNullOrEmpty(data.UserName))
                userLabel.Text = "No hay usuario definido";
            else
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
                MessageBox.Show("No se pudo conectar. Revise su conexión.");
                return;
            }

            if (result.AlreadyConnected)
            {
                MessageBox.Show("Ya existe un usuario conectado previamente. Puedes navegar sin necesidad de entrar con esta cuenta.");
                return;
            }
            if (result.NoMoney)
            {
                MessageBox.Show("No tienes saldo. Necesitas recargar tu cuenta.");
                return;
            }
            if (result.BadPassword)
            {
                MessageBox.Show("Contraseña incorrecta. Revisa la opción de Configurar cuenta.");
                return;
            }
            if (result.BadUsername)
            {
                MessageBox.Show("Nombre de usuario incorrecto. Revisa la opción de Configurar cuenta.");
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

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox form = new AboutBox();
            form.ShowDialog();
        }
    }
}
