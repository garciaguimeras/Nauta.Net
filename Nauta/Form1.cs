﻿using Nauta.Config;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ConfigFile.Write(new ConfigData { UserName = "prueba", Password = "prueba", ProxyServer = "", ProxyPort = "" });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var actions = new Actions("", "", "", "");
            var dict = await actions.HomePage();
            var result = await actions.Login(dict);
            
            if (result == null)
            {
                return;
            }
        }

        private void configurarCuentaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}