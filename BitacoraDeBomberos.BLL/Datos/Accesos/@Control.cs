using System;
using System.Collections.Generic;
using System.Linq;

using BitacoraDeBomberos.BLL.Bases;
using BitacoraDeBomberos.DAL;
using BitacoraDeBomberos.DAL.AccesosDataSetTableAdapters;

using MySql.Data.MySqlClient;

namespace BitacoraDeBomberos.BLL.Datos.Accesos {

    public class Control : DatosBase {

        #region Atributos

        private Int32 id;
        private String nombre;

        #endregion

        #region Constructores

        private Control (Int32 id, String nombre) {
            this.id = id;
            this.nombre = nombre;
        }

        #endregion

        #region Propiedades

        /// <summary>
        ///     Obtiene el identificador del control. 
        /// </summary>
        public Int32 ID {
            get {
                return this.id;
            }
        }

        /// <summary>
        ///     Obtiene el nombre del control. 
        /// </summary>
        public String Nombre {
            get {
                return this.nombre;
            }
        }

        #endregion

        #region Metodos

        private static Control FilaAClase (AccesosDataSet.controlRow fila) {
            var item = new Control (fila.id, fila.nombre);

            return item;
        }

        /// <summary>
        ///     Obtiene una lista de los controles que existen. 
        /// </summary>
        /// <returns> Lista de controles. </returns>
        public static List<Control> ObtenerControles ( ) {
            try {
                using (var consulta = new controlTableAdapter ( )) {
                    var tabla = consulta.GetData ( );

                    var lista = tabla.Select (FilaAClase).ToList ( );

                    return lista;
                }
            }
            catch (MySqlException ex) {
                var r = MySqlExceptions (ex);
                if (r != null) {
                    throw r;
                }
            }

            return null;
        }

        #endregion
    }
}