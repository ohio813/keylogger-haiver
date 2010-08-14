///////////////////////////////////////////////////////////
//  CalculadoraComando.cs
//  Implementation of the Class CalculadoraComando
//  Generated by Enterprise Architect
//  Created on:      16-sep-2009 9:02:38
//  Original author: nbortolotti
///////////////////////////////////////////////////////////




using Command;
namespace Command {
	public class CalculadoraComando : Comando {

        char operador;
        int operando;
		public Calculadora m_Calculadora;

		public CalculadoraComando(Calculadora c,char pOperador,int pOperando)
        {
            this.m_Calculadora = c;
            this.operador = pOperador;
            this.operando = pOperando;
		}

		~CalculadoraComando(){

		}

		public override void Dispose(){

		}

		public override void Deshacer()
        {
            //No implementado
            m_Calculadora.Operación(Undo(operador), operando);
		}

		public override void Hacer()
        {
            m_Calculadora.Operación(operador, operando);
		}

        private char Undo(char pOperator)
        {
            char undo;
            switch (pOperator)
            {
                case '+': 
                    undo = '-'; 
                    break;
                case '-': 
                    undo = '+'; 
                    break;
                case '*': 
                    undo = '/'; 
                    break;
                case '/': 
                    undo = '*'; 
                    break;
                default: 
                    undo = ' '; 
                    break;
            }
            return undo;
        }

	}//end CalculadoraComando

}//end namespace Command