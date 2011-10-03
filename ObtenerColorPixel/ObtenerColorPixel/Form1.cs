using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ObtenerColorPixel
{
    public partial class Form1 : Form
    {
        Win32APICall win32 = new Win32APICall();

        public Form1()
        {
            InitializeComponent();
           
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text =  win32.GetPixelColor( Int16.Parse( tbxX.Text) ,Int32.Parse( tbxY.Text) ).Name;
        }

       
    }
}
