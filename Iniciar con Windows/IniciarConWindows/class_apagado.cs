using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Utilidades
{
    public class ApagarEquipo
    {
        string argumento = null;
        DateTime tmp;
        public ApagarEquipo(string argumento, DateTime tmp)
        {
            this.argumento = argumento;
            this.tmp = tmp;
        }
        public void Shut_Down()
        {
            try
            {
                while (true)
                {
                    if (tmp.ToLongTimeString() == DateTime.Now.ToLongTimeString())
                    {
                        Process proceso = new Process();
                        proceso.StartInfo.UseShellExecute = false;
                        proceso.StartInfo.RedirectStandardOutput = true;
                        proceso.StartInfo.FileName = "shutdown.exe";
                        proceso.StartInfo.Arguments = this.argumento;
                        proceso.Start();
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }

}
