using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Restringidor_Acceso_a_Aplicaciones
{
    static class CierraAplicaciones
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);

        const int WM_COMMAND = 0x111;
        const int MIN_ALL = 419;
        const int MIN_ALL_UNDO = 416;

        public static void cerrarNavegadoresAbiertos()
        {
            {
                
                System.Diagnostics.Process[] procesos;

                procesos = System.Diagnostics.Process.GetProcessesByName("firefox");
                foreach (System.Diagnostics.Process proceso in procesos)
                    proceso.Kill();

                procesos = System.Diagnostics.Process.GetProcessesByName("chrome");
                foreach (System.Diagnostics.Process proceso in procesos)
                    proceso.Kill();

                procesos = System.Diagnostics.Process.GetProcessesByName("msnmsgr");
                foreach (System.Diagnostics.Process proceso in procesos)
                    proceso.Kill();

                //IntPtr lHwnd = FindWindow("Shell_TrayWnd", null);
                //SendMessage(lHwnd, WM_COMMAND, (IntPtr)MIN_ALL, IntPtr.Zero);
                //System.Threading.Thread.Sleep(2000);
                //SendMessage(lHwnd, WM_COMMAND, (IntPtr)MIN_ALL_UNDO, IntPtr.Zero);
            }

        }
    }
}
