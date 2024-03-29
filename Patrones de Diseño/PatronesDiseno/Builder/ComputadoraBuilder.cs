///////////////////////////////////////////////////////////
//  ComputadoraBuilder.cs
//  Implementation of the Class ComputadoraBuilder
//  Generated by Enterprise Architect
//  Created on:      15-sep-2009 22:11:33
//  Original author: nbortolotti
///////////////////////////////////////////////////////////




using Builder;
namespace Builder {
	public abstract class ComputadoraBuilder {


        protected Computadora m_Computadora;

        public Computadora Computadora
        {
            get { return m_Computadora; }
           
        }

		public ComputadoraBuilder(){

		}

		~ComputadoraBuilder(){

		}

		public virtual void Dispose(){

		}
        public abstract void ConstruirEquipo();

        public abstract void ConstruirDisco();

        public abstract void ConstruirMemoria();

        public abstract void ConstruirMonitor();

		

	}//end ComputadoraBuilder

}//end namespace Builder