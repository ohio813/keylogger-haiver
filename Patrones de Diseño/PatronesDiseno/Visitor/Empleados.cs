using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Visitor
{
    class Employees
    {
        private ArrayList employees = new ArrayList();

        public void Attach(Empleado employee)
        {
            employees.Add(employee);
        }

        public void Detach(Empleado employee)
        {
            employees.Remove(employee);
        }

        public void Accept(IVisitor visitor)
        {
            foreach (Empleado e in employees)
            {
                e.Aceptar(visitor);
            }
            //Notificar Pantalla
        }
    }
}
