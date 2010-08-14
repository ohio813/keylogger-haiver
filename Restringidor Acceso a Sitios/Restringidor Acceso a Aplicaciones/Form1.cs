using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;

namespace Restringidor_Acceso_a_Aplicaciones
{
    public partial class Form1 : Form
    {
        Restringidor_Acceso_a_Aplicaciones.ReestringeAcceso restringidor;

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MAXIMIZE = 0xF030;
        const int WM_COMMAND = 0x111;
        const int MIN_ALL = 419;
        const int MIN_ALL_UNDO = 416;

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);

        public Form1()
        {
           InitializeComponent();
           restringidor = new Restringidor_Acceso_a_Aplicaciones.ReestringeAcceso(l_tiempoTranscurrido, l_tiempoRestante, this, lMensaje);
           l_tiempoRestante.Text = restringidor.getTiempoRestante();
           l_tiempoTranscurrido.Text = restringidor.getTiempoTranscurrido();
           if (restringidor.tiempoTerminado())
               button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Iniciar")
            {
                button1.Text = "Pausar";
                restringidor.iniciarControlTiempo();
                lMensaje.Text = "";

            }
            else
            {
                restringidor.detenerControlTiempo();
                button1.Text = "Iniciar";
                CierraAplicaciones.cerrarNavegadoresAbiertos();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            terminarAplicacion();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            terminarAplicacion();
        }

        public void terminarAplicacion()
        {
            if (restringidor != null)
                restringidor.detenerControlTiempo();
            this.Dispose();
        }
    }
}
