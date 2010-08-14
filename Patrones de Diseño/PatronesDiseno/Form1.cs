using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using Builder;
using Adapter;
using Decorator;
using Command;
using State;
using Visitor;
using Observer;


namespace wfaPatronesDiseno
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ComputadoraBuilder c;

            Produccion p = new Produccion();
            
            c = new PersonalJuegosBuilder();
            p.Construir(c);
            c.Computadora.MostrarPartes();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Compuesto c = new Compuesto("Cualquiera");
            c.Mostrar();

            Compuesto agua = new CompuestoCompleto("Agua");
            agua.Mostrar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Libro objLibro = new Libro("Nicolas");
            objLibro.Mostrar();

            Prestable objPrestable = new Prestable(objLibro);
            objPrestable.PrestarItem("Cliente 1");
            objPrestable.PrestarItem("Cliente 2");

            objPrestable.Mostrar();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Usuario objUsuario = new Usuario();
            objUsuario.Calcular('+', 10);
            objUsuario.Calcular('-', 5);
            objUsuario.Calcular('*', 2);

            objUsuario.Deshacer(1);


        }

        private void button5_Click(object sender, EventArgs e)
        {
            Cuenta objCuenta = new Cuenta("Nicolas");
            objCuenta.Depositar(100);


        }

        private void button6_Click(object sender, EventArgs e)
        {
            Employees empleados = new Employees();
            empleados.Attach(new Director());

            empleados.Accept(new VisitorIngreso());

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Despensa d = new Despensa("Azucar",5);
            d.Attach(new Iversor("Nicolas"));
            d.Attach(new Iversor("Juan"));

            d.Precio = 6;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FactoriaContinente fc = new FactoriaAmerica();
            MundoAnimal a = new MundoAnimal(fc);
            a.CadenaAlimenticia();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Composite.Compuesto c = new Composite.Compuesto("Figuras");
            c.Agregar(new Composite.Primitiva("Auto"));
            c.Agregar(new Composite.Primitiva("Moto"));

            c.Mostrar(1);
            
        }
    }
}
