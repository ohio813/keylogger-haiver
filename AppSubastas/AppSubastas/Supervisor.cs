using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Collections;

namespace AppSubastas
{

    

    class Supervisor
    {
        Thread hilo;
        private ThreadStart Supervisar;

        public Supervisor()
        {
            hilo = new Thread(new ThreadStart(SupervisarValores));
        }

        public void SupervisarValores()
        {
            while (true)
            {
                Thread.Sleep(1000);

                //hacer click para generar archivo con la lectura de pantalla



                //abrir archivo para interpretar lectura
                StreamReader objReader = new StreamReader("c:\\test.txt");
                string sLine = "";

                
                string primeraLinea = objReader.ReadLine();
                string segundaLinea = objReader.ReadLine();



                //objReader.Close();

                //foreach (string sOutput in arrText)
                //    Console.WriteLine(sOutput);
                //Console.ReadLine();


                //tomar decicion si aplicar a la oferta
            }
        }
    }
}
