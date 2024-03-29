///////////////////////////////////////////////////////////
//  DataAccesObject.cs
//  Implementation of the Class DataAccesObject
//  Generated by Enterprise Architect
//  Created on:      16-sep-2009 17:53:13
//  Original author: nbortolotti
///////////////////////////////////////////////////////////




namespace Template {
	public abstract class DataAccesObject {

		public DataAccesObject(){

		}

		~DataAccesObject(){

		}

		public virtual void Dispose(){

		}

		public void Conectar()
        {
            //Establecer conexion
		}

		public void Desconectar()
        {
            //Desconectar
		}

		public abstract void Procesar();

		/// <summary>
		/// Template
		/// </summary>
		public void Run()
        {
            Conectar();
            Seleccionar();
            Procesar();
            Desconectar();
		}

		public abstract void Seleccionar();

	}//end DataAccesObject

}//end namespace Template