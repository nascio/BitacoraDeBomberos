using BitacoraDeBomberos.DAL;
using BitacoraDeBomberos.DAL.AccesosDataSetTableAdapters;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitacoraDeBomberos.BLL.Datos.Accesos {

    public class RolAcceso : Bases.DatosBase {

        #region Atributos

        private RolAcceso copia;

        #endregion

        #region Propiedades

        /// <summary>
        ///	Obtiene o establece el identificador del rol de acceso.
        /// </summary>
        public Int32 ID {
            get;
            set;
        }

        /// <summary>
        /// Obtiene o establece el nombre del rol de acceso.
        /// </summary>
        public string Nombre {
            get;
            set;
        }

        #endregion

        #region BD


        int a;

        #endregion


        #region Metodos

        private static RolAcceso FilaAClase (AccesosDataSet.rol_accesoRow fila) {
            var item = new RolAcceso ( ) {
                ID = fila.id,
                Nombre = fila.nombre
            };

            item.copia = item.MemberwiseClone ( ) as RolAcceso;

            return item;
        }

        /// <summary>
        ///     Obtiene una lista de los roles de acceso que existen. 
        /// </summary>
        /// <returns> Lista de roles de acceso. </returns>
        public static List<RolAcceso> ObtenerRolesDeAcceso ( ) {
            try {
                using (var consulta = new rol_accesoTableAdapter ( )) {
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