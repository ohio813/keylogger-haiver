using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;



namespace IniciarConWindows
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 formulario = new Form1();
            Application.Run(formulario);
            formulario.Dispose();
            Application.Exit();
        }
    }


   

  
}
