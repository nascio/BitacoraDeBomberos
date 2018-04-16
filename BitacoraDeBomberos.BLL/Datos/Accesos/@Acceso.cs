using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using BitacoraDeBomberos.BLL.Bases;
using BitacoraDeBomberos.BLL.Datos.Ingresos;
using BitacoraDeBomberos.DAL;
using BitacoraDeBomberos.DAL.AccesosDataSetTableAdapters;

using MySql.Data.MySqlClient;

namespace BitacoraDeBomberos.BLL.Datos.Accesos {

    public class Acceso : DatosBase {

        #region Propiedades

        /// <summary>
        ///     Obtiene o establece el identificador del control que posee el acceso. 
        /// </summary>
        public Int32 ControlID {
            get;
            set;
        }

        /// <summary>
        ///     Obtiene o establece el estado del acceso. 
        /// </summary>
        public Boolean Estado {
            get;
            set;
        }

        /// <summary>
        ///     Obtiene o establece el identificador del rol que posee el acceso. 
        /// </summary>
        public Int32 RolAccesoID {
            get;
            set;
        }

        #endregion

        #region BD

        private MSG activarDesactivar (Int32 id_responsable, Boolean estado) {
            if (this.Estado == estado) {
                return (MSG) Mensaje.SinCambios;
            }

            try {
                var rem = REM.sp_accion_acceso (estado ? Opcion.Activar : Opcion.Desactivar, this.RolAccesoID, this.ControlID, null, id_responsable);

                return this.mensajePorResolucion (rem, estado);
            }
            catch (MySqlException ex) {
                Debug.WriteLine ("Error> " + ex.Number);
                var mysqlErrors = MySqlErrores (ex);

                switch (mysqlErrors) {
                    case ErrorMySql.SinConexion:
                        return (MSG) Mensaje.SinConexion;

                    case ErrorMySql.ElementoDuplicado:
                        return new MSG ("Elemento duplicado.");
                }
            }

            return (MSG) Mensaje.NoPasara;
        }

        private MSG mensajePorResolucion (REM rem, Boolean activar) {
            switch (rem.Estado) {
                case EstadoResolucion.Error_ingresado:
                    return (MSG) MSG.ErrorIngresoSinEspecificar;

                case EstadoResolucion.Actualizado:
                    this.Estado = activar;

                    return new MSG (MensajeAdvertencia.Bueno, "La actualizacion del acceso fue un exito.");

                case EstadoResolucion.primer_id_invalido:
                    return new MSG ("El rol de acceso no existe.");

                case EstadoResolucion.segundo_id_invalido:
                    return new MSG ("El control no existe.");
            }

            return (MSG) Mensaje.NoPasara;
        }

        /// <summary>
        ///     Activa el acceso con los valores establecidos en <see cref="RolAccesoID" /> y
        ///     <see cref="ControlID" />.
        /// </summary>
        /// <param name="id_responsable"> Responsable del cambio. </param>
        /// <returns> Mensaje que contiene informacion del resultado. </returns>
        public Mensaje Activar (Int32 id_responsable) {
            return this.activarDesactivar (id_responsable, estado: true);
        }

        /// <summary>
        ///     Desactiva el acceso con los valores establecidos en <see cref="RolAccesoID" /> y
        ///     <see cref="ControlID" />.
        /// </summary>
        /// <param name="id_responsable"> Responsable del cambio. </param>
        /// <returns> Mensaje que contiene informacion del resultado. </returns>
        public Mensaje Desactivar (Int32 id_responsable) {
            return this.activarDesactivar (id_responsable, estado: false);
        }

        #endregion

        #region Metodos

        private static Acceso FilaAClase (AccesosDataSet.accesoRow fila) {
            var item = new Acceso ( ) {
                RolAccesoID = fila.rol_acceso_id,
                ControlID = fila.control_id,
                Estado = Convert.ToBoolean (fila.estado)
            };

            return item;
        }

        /// <summary>
        ///     Obtiene una lista de accesos que posea un rol de acceso. 
        /// </summary>
        /// <param name="rol_acceso_id"> Identificador del rol. </param>
        /// <returns> Lista de accesos. </returns>
        public static List<Acceso> ObtenerAccesoPorRol (Int32 rol_acceso_id) {
            try {
                using (var consulta = new accesoTableAdapter ( )) {
                    var tabla = consulta.GetData (rol_acceso_id);

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