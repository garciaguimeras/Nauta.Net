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
        private long time = 0;

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
            var result = await actions.GetAvailableTime(request.GetParamString());
            if (result != null)
            {
                availTimeLabel.Text = "Tiempo disponible: " + result;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            var result = await actions.Logout(request.GetParamString());

            button1.Enabled = true;

            if (!result)
            {
                MessageBox.Show("No se pudo salir de la sesión. Inténtelo de nuevo, o apague su equipo.");
                return;
            }

            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;

            long minutes = time / 60;
            long seconds = time % 60;

            long hours = minutes / 60;
            minutes = minutes % 60;

            var result = string.Format("{0}:{1:00}:{2:00}", hours, minutes, seconds);

            timeLabel.Text = "Tiempo transcurrido: " + result;
        }
    }
}
