using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);
        //private const int MOUSEEVENTF_LEFTDOWN = 0x02; 
        //private const int MOUSEEVENTF_LEFTUP = 0x04;
        //private const int MOUSEEVENTF_RIGHTDOWN = 0x08; 
        //private const int MOUSEEVENTF_RIGHTUP = 0x10;
        public Form1()
        {
            LeftClick(1350, 5);
        }
        //public void DoMouseClick()
        //{
        //    //Call the imported function with the cursor's current position      int X = Cursor.Position.X;      int Y = Cursor.Position.Y;      
        //    mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
        //}   //...other code needed for the application


        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        public void LeftClick(int x, int y)
        {
            Cursor.Position = new System.Drawing.Point(x, y);
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
        }
    }
}
