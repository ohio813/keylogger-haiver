using System;

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mailer;

namespace EnvioDeMail
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
            Application.Run(new Form1());
            EnviarMail senderMail = new EnviarMail();

            if (senderMail.enviar())
            {
                MessageBox.Show("Mensaje Enviado");
            }
            else
            {
                MessageBox.Show("Mensaje no Enviado");
            }


        }
    }
}
