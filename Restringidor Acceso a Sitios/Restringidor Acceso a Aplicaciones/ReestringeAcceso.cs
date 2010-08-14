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
    class ReestringeAcceso
    {
        private int minutosPermitidos = 120;
        private string rutaHost = @"C:\Windows\System32\drivers\etc\hosts";
        private string rutaControlTiempo = @"C:\Windows\System32\drivers\etc\time_control";
        private int segundosTranscurridos;
        private Thread th1;
        private delegate void SetTextCallback(string text);
        private Label lTiempoTranscurrido, lTiempoRestante, lMensaje;
        private Form formularioPrincipal;
        private int segundosParaPreaviso = 180;

        public const int MAXIMIZE = 0xF030;
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);


        public ReestringeAcceso(Label lTiempoTranscurrido, Label lTiempoRestante, Form formularioPrincipal, Label lMensaje)
        {
            this.lTiempoTranscurrido = lTiempoTranscurrido;
            this.lTiempoRestante = lTiempoRestante;
            this.formularioPrincipal = formularioPrincipal;
            this.lMensaje = lMensaje;
        }

        public void iniciarControlTiempo()
        {
            if (!File.Exists(rutaControlTiempo))
                File.Create(rutaControlTiempo).Close();
            
            
            string cadenaControlTiempo = File.ReadAllText(rutaControlTiempo);
            
            string date = "";
            if(cadenaControlTiempo!= null &&  cadenaControlTiempo.IndexOf("-") != -1)
                date = cadenaControlTiempo.Substring(0, cadenaControlTiempo.IndexOf("-"));

            if (date == DateTime.Now.ToShortDateString())
            {
                segundosTranscurridos = int.Parse(cadenaControlTiempo.Substring(cadenaControlTiempo.IndexOf("-")));
            }
            else {                
                string cadenaTiempoHoy = DateTime.Now.Date.ToShortDateString() + "-0";
                File.WriteAllText(rutaControlTiempo, cadenaTiempoHoy);
                segundosTranscurridos = 0;
            }
            permitirAccesso();
            th1 = new Thread(new ThreadStart(this.iniciarContador));
            th1.Start();
        }
        public void detenerControlTiempo()
        {
            denegarAcceso();
            if (th1 != null)
            {
                th1.Abort();
            }
        }
        public string getTiempoTranscurrido()
        {
            try
            {
                string cadenaTiempo = File.ReadAllText(rutaControlTiempo);
                segundosTranscurridos = int.Parse(cadenaTiempo.Substring(cadenaTiempo.IndexOf("-") + 1)) + 1;
                return formatearSegundos(segundosTranscurridos);
            }
            catch {
                return "";
            }
        }
        public string getTiempoRestante()
        {
            try
            {
                string cadenaTiempo = File.ReadAllText(rutaControlTiempo);
                segundosTranscurridos = int.Parse(cadenaTiempo.Substring(cadenaTiempo.IndexOf("-") + 1)) + 1;
                return formatearSegundos((minutosPermitidos * 60) - segundosTranscurridos);
            }
            catch {
                return "";
            }
        }
        public bool tiempoTerminado()
        {
            try
            {
                string cadenaTiempo = File.ReadAllText(rutaControlTiempo);
                segundosTranscurridos = int.Parse(cadenaTiempo.Substring(cadenaTiempo.IndexOf("-") + 1)) + 1;
                if (segundosTranscurridos >= (minutosPermitidos * 60))
                    return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }


        private void iniciarContador()
        {
            int segundosPermitidos = minutosPermitidos*60;
            bool preavisoMostrado = false;
            while (segundosTranscurridos < segundosPermitidos)
            {
                string cadenaTiempo = File.ReadAllText(rutaControlTiempo);
                segundosTranscurridos = int.Parse(cadenaTiempo.Substring(cadenaTiempo.IndexOf("-")+1)) + 1;
                cadenaTiempo = DateTime.Now.Date.ToShortDateString() + "-" + segundosTranscurridos;
                File.WriteAllText(rutaControlTiempo, cadenaTiempo);
                this.mostrarTiempoTranscurrido( formatearSegundos( segundosTranscurridos));
                this.mostrarTiempoRestante(formatearSegundos((minutosPermitidos * 60) - segundosTranscurridos));

                //si faltan 2 minutos para terminar tiempo
                if (segundosPermitidos - segundosTranscurridos < segundosParaPreaviso && preavisoMostrado == false)
                {
                    WindowHandler manejador = new WindowHandler();

                    System.Diagnostics.Process[] procesos = System.Diagnostics.Process.GetProcesses();
                    IntPtr handle;
                    foreach (System.Diagnostics.Process proceso in procesos)
                    {
                        handle = proceso.MainWindowHandle;
                        if (handle != IntPtr.Zero)
                            manejador.Minimize((int)handle);
                    }

                    int hand = manejador.getIdManejadorVentana("system");
                    manejador.Maximize(hand);
                    preavisoMostrado = true;
                }

                Thread.Sleep(1000);
            }
            denegarAcceso();
            CierraAplicaciones.cerrarNavegadoresAbiertos();
            formularioPrincipal.Close();
        }

        #region Funciondes de Obtención y Muestra de Tiempo
        private string formatearSegundos(int segundosTranscurridos)
        {
            TimeSpan t = new TimeSpan(0,0, segundosTranscurridos);
            return t.Hours.ToString().PadLeft(2, char.Parse("0")) + ":" + t.Minutes.ToString().PadLeft(2, char.Parse("0")) + ":" + t.Seconds.ToString().PadLeft(2, char.Parse("0"));

        }
        private void mostrarTiempoTranscurrido(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.lTiempoTranscurrido.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(mostrarTiempoTranscurrido);
                formularioPrincipal.Invoke(d, new object[] { text });
            }
            else
            {
                this.lTiempoTranscurrido.Text = text;
            }
        }
        private void mostrarTiempoRestante(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.lTiempoRestante.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(mostrarTiempoRestante);
                formularioPrincipal.Invoke(d, new object[] { text });
            }
            else
            {
                this.lTiempoRestante.Text = text;
            }
        }
        #endregion

        #region Accesso a Archivo Hosts
        private ArrayList obtenerTextoHost()
        {
            StreamReader objReader = new StreamReader(rutaHost);
            string sLine = "";
            ArrayList arrText = new ArrayList();

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    arrText.Add(sLine);
            }
            objReader.Close();
            return arrText;
        }
        private void denegarAcceso()
        {
            ArrayList arrText = obtenerTextoHost();

            bool ejecutarAccion = false;
            string nuevoArchivoHost = "";
            string nuevaLinea = "";

            foreach (string sOutput in arrText)
            {
                if (sOutput.StartsWith("##begin restriction"))
                    ejecutarAccion = true;

                if (ejecutarAccion)
                    nuevaLinea = sOutput.Substring(1);
                else
                    nuevaLinea = sOutput;

                nuevoArchivoHost += nuevaLinea + "\r\n";
            }
            guardarArchivoHost(nuevoArchivoHost);
        }
        private void permitirAccesso()
        {
            ArrayList arrText = obtenerTextoHost();

            bool ejecutarAccion = false;
            string nuevoArchivoHost = "";
            string nuevaLinea = "";

            foreach (string sOutput in arrText)
            {
                if (sOutput.StartsWith("#begin restriction"))
                    ejecutarAccion = true;



                if (ejecutarAccion)
                    nuevaLinea = "#" + sOutput;
                else
                    nuevaLinea = sOutput;

                nuevoArchivoHost += nuevaLinea + "\r\n";
            }
            guardarArchivoHost(nuevoArchivoHost);
        }
        private void guardarArchivoHost(string nuevoArchivoHost)
        {
            StreamWriter sw = new StreamWriter(@"C:\Windows\System32\drivers\etc\hosts");
            sw.Write(nuevoArchivoHost);
            sw.Close();
        }
        #endregion
    }
}
