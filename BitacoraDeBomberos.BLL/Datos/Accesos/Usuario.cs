using BitacoraDeBomberos.BLL.Bases;
using BitacoraDeBomberos.DAL;
using BitacoraDeBomberos.DAL.AccesosDataSetTableAdapters;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitacoraDeBomberos.BLL.Datos.Accesos {

    public class Usuario : DatosBase {
        private Usuario copia;

        public Int32 ID {
            get;
            set;
        }

        public UsuarioEstado Estado {
            get;
            set;
        }

        public String Nombre {
            get;
            set;
        }

        public Int32 RolAccesoID {
            get;
            set;
        }

        #region Metodos

        private static Usuario FilaAClase (AccesosDataSet.usuarioRow fila) {
            var item = new Usuario ( ) {
                ID = fila.id,
                Nombre = fila.nombre,
                Estado = (UsuarioEstado) fila.estado,
                RolAccesoID = fila.rol_acceso_id
            };

            item.copia = item.MemberwiseClone ( ) as Usuario;

            return item;
        }

        public static List<Usuario> ObtenerUsuarios ( ) {
            try {
                using (var consulta = new usuarioTableAdapter ( )) {
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

        public static Usuario ObtenerUsuarioPorID (int id_usuario) {
            try {
                using (var consulta = new usuarioTableAdapter ( )) {
                    var tabla = consulta.GetDataByID (id_usuario);

                    var lista = FilaAClase (tabla.First ( ));

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