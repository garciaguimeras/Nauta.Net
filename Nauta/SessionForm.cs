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
    public partial class SessionForm : Form
    {

        private LoginResponse request;
        private ConfigData data;
        private Actions actions;

        public SessionForm(LoginResponse request)
        {
            InitializeComponent();

            this.request = request;
            data = ConfigFile.Read();
            actions = new Actions(data.UserName, data.Password, data.ProxyServer, data.ProxyPort);

            GetAvailableTime();
        }

        private async void GetAvailableTime()
        {
            var result = await actions.GetAvailableTime(request.TimeParams);
            if (result != null)
            {
                availTimeLabel.Text = "Tiempo disponible: " + result;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var result = await actions.Logout(request.Session);
            if (!result)
            {
                MessageBox.Show("No se pudo salir de la sesión. Inténtelo de nuevo, o apague su equipo.");
                return;
            }

            Close();
        }

    }
}
