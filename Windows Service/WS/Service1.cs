using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;

namespace WS
{
    public partial class Service1 : ServiceBase
    {
        private System.Timers.Timer myTimer;
        static StreamWriter sw;


        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            myTimer = new System.Timers.Timer();
            myTimer.Interval = 100;
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(myTimer_Elapsed);
            myTimer.Enabled = true;
        }

        protected override void OnStop()
        {
        }

        public void myTimer_Elapsed(object sender, EventArgs e)
        {
            myTimer.Enabled = false;
            escribir("Escribiendo");

            // Revisar si el proceso se encuentra activo, si no iniciarlo

            myTimer.Enabled = true;

        }


        public void escribirArchivo()
        {
            try
            {
                DateTime fecha = DateTime.Now;
                string sfecha = "";
                sfecha = Convert.ToString(fecha.ToString("yyyyMMdd"));
                string FILE_NAME = "hola.txt";
                string Directorio = @"D:/";

                if (File.Exists(Directorio + FILE_NAME))
                {
                    sw = File.AppendText(Directorio + FILE_NAME);
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine(" --------------------------------------------------------");
                    sw.WriteLine(" --------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine("SE EJECUTA NUEVAMENTE LA APLICACION");
                    sw.WriteLine("Inicia programa Extracción: " + DateTime.Now);
                    sw.WriteLine();
                    sw.WriteLine(" --------------------------------------------------------");
                    sw.WriteLine();
                }
                else
                {
                    sw = File.CreateText(Directorio + FILE_NAME);

                    sw.WriteLine("estamos reunidos en viernes a las 9 ");
                    sw.WriteLine();
                    sw.Write("------------------- ");
                    sw.Write(DateTime.Now);
                    sw.Write(" ------------------- ");
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("Inicia programa Extracción: " + DateTime.Now);
                    sw.WriteLine(" --------------------------------------------------------");
                    sw.WriteLine();
                }

            }
            catch (Exception Excepcion)
            {
                string error = Excepcion.Message.ToString();
                sw.WriteLine("Catch Main Error -> " + error);
            }
            finally
            {
                sw.WriteLine("Fin programa Extracción: " + DateTime.Now);
                sw.Close();
            }
        }

         public void escribir(string texto)
            {
            try
            {
                DateTime fecha = DateTime.Now;
                string sfecha = "";
                sfecha = Convert.ToString(fecha.ToString("yyyyMMdd"));
                string FILE_NAME = "hola.txt";
                string Directorio = @"D:/";

                if (File.Exists(Directorio + FILE_NAME))
                {
                    sw = File.AppendText(Directorio + FILE_NAME);
                    sw.WriteLine(" --------------------------------------------------------");
                    sw.WriteLine(texto);
                    sw.WriteLine(" --------------------------------------------------------");
                    sw.WriteLine();
                }
                else
                {
                    sw = File.CreateText(Directorio + FILE_NAME);

                    sw = File.AppendText(Directorio + FILE_NAME);
                    sw.WriteLine(" --------------------------------------------------------");
                    sw.WriteLine(texto);
                    sw.WriteLine(" --------------------------------------------------------");
                    sw.WriteLine();
                }

            }
            catch (Exception Excepcion)
            {
                string error = Excepcion.Message.ToString();
                sw.WriteLine("Catch Main Error -> " + error);
            }
            finally
            {
                sw.Close();
            }
        }
    }
}
