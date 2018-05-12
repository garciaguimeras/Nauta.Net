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

        public LoginResponse LoginResponse { get; set; }

        public SessionForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var data = ConfigFile.Read();
            var actions = new Actions(data.UserName, data.Password, data.ProxyServer, data.ProxyPort);
            var result = await actions.Logout(LoginResponse.Session);
            if (!result)
            {
                return;
            }

            Close();
        }

    }
}
