using BitacoraDeBomberos.BLL.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitacoraDeBomberos.DAL;
using BitacoraDeBomberos.DAL.PersonasDataSetTableAdapters;
using BitacoraDeBomberos.BLL.Datos.Ingresos;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace BitacoraDeBomberos.BLL.Datos.Personas {

    public class Bombero : DatosBase {

        public enum M {
            ksdmk,
            smdkmskd
        }

        #region Atributos

        private Bombero copia;

        #endregion

        #region Constructores

        public Bombero ( ) {
            this.ID = -1;
            this.DPI = "";
            this.Nombre = "";
            this.Apellido = "";
            this.FechaNacimiento = DateTime.Now;
            this.Estado = BomberoEstado.Activo;
            this.Rol = BomberoRol.SinRol;
            this.Foto = "";
        }

        #endregion

        #region Propiedades

        public Int32 ID {
            get;
            set;
        }

        public String DPI {
            get;
            set;
        }

        public String Nombre {
            get;
            set;
        }

        public String Apellido {
            get;
            set;
        }

        public String NombreCompleto {
            get {
                return string.Concat (this.Nombre, " ", this.Apellido);
            }
        }

        public DateTime FechaNacimiento {
            get;
            set;
        }

        public BomberoEstado Estado {
            get;
            set;
        }

        public BomberoRol Rol {
            get;
            set;
        }

        public String Carne {
            get {
                return String.Concat ("PGP-019-", this.NoCarne);
            }
        }

        public Int32 NoCarne {
            get;
            set;
        }

        public String Foto {
            get;
            set;
        }

        public Int32? UsuarioID {
            get;
            set;
        }

        #endregion

        #region BD

        private Mensaje activarDesactivar (Int32 id_responsable, BomberoEstado estado) {
            // this.Activo = activo | this.Inactivo = inactivo
            if (this.Estado == estado) {
                return Mensaje.SinCambios;
            }

            var @return = tareas (estado == BomberoEstado.Activo ? Opcion.Activar : Opcion.Desactivar,
                this.ID, null, null, null, null, null, null, null, null, null, id_responsable);

            if (@return.Estado == EstadoResolucion.Actualizado) {
                this.Estado = estado;
                this.copia.Estado = estado;
            }

            return @return;

            //IResolucion rem = null;
            //ErrorMySql mysqlrem = ErrorMySql.SinIndicar;

            //try {
            //    var fila = REM.sp_accion_bombero (estado == BomberoEstado.Activo ? Opcion.Activar : Opcion.Desactivar, this.ID, null, null, null, null, null, null, null, null, null, id_responsable);

            // if (fila.Estado == EstadoResolucion.Actualizado) { this.Estado = BomberoEstado.Activo;
            // this.copia.Estado = BomberoEstado.Activo; } else if (fila.Estado ==
            // EstadoResolucion.primer_id_invalido) { this.ID = -1; this.copia = null; }

            //    rem = fila;
            //}
            //catch (MySql.Data.MySqlClient.MySqlException ex) {
            //    mysqlrem = MySqlErrores (ex);
            //    Console.WriteLine ("Error> " + ex.Number);
            //}

            ////if (mysqlrem == ErrorMySql.SinConexion) {
            ////    return Mensaje.SinConexion;
            ////}
            ////if (rem == null) {
            ////    return Mensaje.ErrorActualizarSinEspecificar;
            ////}

            //switch (rem.Estado) {
            //    //case EstadoResolucion.Actualizado:
            //    //    return new Mensaje (MensajeAdvertencia.Bueno, string.Concat ("La actualizacion del bombero fue un exito."));

            // //case EstadoResolucion.primer_id_invalido: // return new Mensaje ("El bombero no
            // existe, eliminando referencias.");

            // //case EstadoResolucion.segundo_id_invalido: // return new Mensaje ("El usuario
            // especificado al bombero no esta disponible.");

            //    default:
            //        return Mensaje.NoPasara;
            //}
        }

        private MSG mensajePorResolucion (REM rem) {
            switch (rem.Estado) {
                case EstadoResolucion.Ingresado:
                    this.ID = rem.OutID.Value;
                    this.copia = base.MemberwiseClone ( ) as Bombero;
                    return new MSG (MensajeAdvertencia.Bueno, string.Concat ("El ingreso del bombero ", this.NombreCompleto, " fue exitoso."));

                case EstadoResolucion.Error_ingresado:
                    return (MSG) MSG.ErrorIngresoSinEspecificar;

                case EstadoResolucion.Error_eliminado:
                    return (MSG) MSG.ErrorEliminarSinEspecificar;

                case EstadoResolucion.segundo_id_invalido:
                    return new MSG ("El usuario especificado al bombero no esta disponible.");

                case EstadoResolucion.Eliminado:
                    this.ID = -1;
                    this.copia = null;
                    return new MSG (MensajeAdvertencia.Bueno, string.Concat ("El bombero <b>", this.NombreCompleto, "</b> fue dado de baja satisfactoriamente."));

                case EstadoResolucion.Error_eliminar_referencias:
                    return new MSG ("El bombero no se eliminara, es aconsejable desactivar el usuario.");

                case EstadoResolucion.Actualizado:
                    this.copia = base.MemberwiseClone ( ) as Bombero;
                    return new MSG (MensajeAdvertencia.Bueno, "La actualizacion del bombero fue un exito.") {
                        Estado = EstadoResolucion.Actualizado
                    };

                case EstadoResolucion.primer_id_invalido:
                    this.ID = -1;
                    this.copia = null;
                    return new MSG (MensajeAdvertencia.Advertencia1, "El bombero no existe, eliminando referencias.");

                case EstadoResolucion.Sin_cambios:
                    return (MSG) Mensaje.SinCambios;
            }

            return (MSG) Mensaje.NoPasara;
        }

        private MSG tareas (Opcion opcion, Int32? id, Tuple<Boolean?, String> dpi, Tuple<Boolean?, String> nombre, Tuple<Boolean?, String> apellido, Tuple<Boolean?, DateTime?> fecha_nacimiento, Tuple<Boolean?, Int32?> estado, Tuple<Boolean?, Int32?> rol, Tuple<Boolean?, Int32?> carne, Tuple<Boolean?, String> foto, Tuple<Boolean?, Int32?> usuario_id, Int32 id_responsable) {
            try {
                var rem = REM.sp_accion_bombero (opcion, id, dpi, nombre, apellido, fecha_nacimiento, estado, rol, carne, foto, usuario_id, id_responsable);

                return mensajePorResolucion (rem);
            }
            catch (MySqlException ex) {
                Debug.WriteLine ("Error> " + ex.Number);
                var mysqlErrors = MySqlErrores (ex);

                switch (mysqlErrors) {
                    case ErrorMySql.SinConexion:
                        return (MSG) Mensaje.SinConexion;

                    case ErrorMySql.ElementoDuplicado:
                        return new MSG ("El numero DPI del bombero ya esta ingresado.");
                }
            }

            return (MSG) Mensaje.NoPasara;
        }

        public Mensaje Activar (Int32 id_responsable) {
            return this.activarDesactivar (id_responsable, BomberoEstado.Activo);
        }

        public Mensaje Actualizar (Int32 id_responsable) {
            var @return = tareas (Opcion.Actualizar, this.ID,
                                        this.DPI.ToTupleEdicionC (this.copia.DPI, StringCompare),

                                        this.Nombre.ToTupleEdicionC (this.copia.Nombre, StringCompare),
                                        this.Apellido.ToTupleEdicionC (this.copia.Apellido, StringCompare),
                                        this.FechaNacimiento.Date.ToTupleEdicion (this.copia.FechaNacimiento.Date),
                                        ((Int32) this.Estado).ToTupleEdicion ((Int32) this.copia.Estado),
                                        (this.Rol == BomberoRol.SinRol ? null : (Int32?) this.Rol).ToTupleEdicion (this.copia.Rol == BomberoRol.SinRol ? null : (Int32?) this.copia.Rol),
                                        this.NoCarne.ToTupleEdicion (this.copia.NoCarne),
                                        this.Foto.ToTupleEdicionC (this.copia.Foto),
                                        this.UsuarioID.ToTupleEdicion (this.copia.UsuarioID),
                                        id_responsable);

            return @return;

            //IResolucion rem = null;
            //ErrorMySql mysqlrem = ErrorMySql.SinIndicar;

            //try {
            //    var fila = REM.sp_accion_bombero (Opcion.Actualizar, this.ID,
            //                            this.DPI.ToTupleEdicionC (this.copia.DPI, StringCompare),

            // this.Nombre.ToTupleEdicionC (this.copia.Nombre, StringCompare),
            // this.Apellido.ToTupleEdicionC (this.copia.Apellido, StringCompare),
            // this.FechaNacimiento.Date.ToTupleEdicion (this.copia.FechaNacimiento.Date), ((Int32)
            // this.Estado).ToTupleEdicion ((Int32) this.copia.Estado), (this.Rol ==
            // BomberoRol.SinRol ? null : (Int32?) this.Rol).ToTupleEdicion (this.copia.Rol ==
            // BomberoRol.SinRol ? null : (Int32?) this.copia.Rol), this.NoCarne.ToTupleEdicion
            // (this.copia.NoCarne), this.Foto.ToTupleEdicionC (this.copia.Foto),
            // this.UsuarioID.ToTupleEdicion (this.copia.UsuarioID), id_responsable);

            // //if (fila.Estado == EstadoResolucion.Actualizado) { // this.copia =
            // base.MemberwiseClone ( ) as Bombero; //} //else

            // //if (fila.Estado == EstadoResolucion.primer_id_invalido) { // this.ID = -1; //
            // this.copia = null; //}

            //    rem = fila;
            //}
            //catch (MySql.Data.MySqlClient.MySqlException ex) {
            //    mysqlrem = MySqlErrores (ex);
            //    Console.WriteLine ("Error> " + ex.Number);
            //}

            ////switch (mysqlrem) {
            ////    case ErrorMySql.SinConexion:
            ////        return Mensaje.SinConexion;

            ////    case ErrorMySql.ElementoDuplicado:
            ////        return new Mensaje ("El numero DPI del bombero ya esta ingresado.");
            ////}

            ////if (rem == null) {
            ////    return Mensaje.ErrorActualizarSinEspecificar;
            ////}

            //switch (rem.Estado) {
            //    //case EstadoResolucion.Actualizado:
            //    //    return new Mensaje (MensajeAdvertencia.Bueno, "La actualizacion del bombero fue un exito.");

            // //case EstadoResolucion.segundo_id_invalido: // return new Mensaje ("El usuario
            // especificado al bombero no esta disponible.");

            //    default:
            //        return Mensaje.NoPasara;
            //}
        }

        public Mensaje Desactivar (Int32 id_responsable) {
            return this.activarDesactivar (id_responsable, BomberoEstado.Inactivo);
        }

        public Mensaje Eliminar (Int32 id_responsable) {
            var @return = tareas (Opcion.Eliminar, this.ID, null, null, null, null, null, null, null, null, null, id_responsable);

            return @return;

            //IResolucion rem = null;
            //ErrorMySql mysqlrem = ErrorMySql.SinIndicar;

            //try {
            //    var fila = REM.sp_accion_bombero (Opcion.Eliminar, this.ID, null, null, null, null, null, null, null, null, null, id_responsable);

            // //if (fila.Estado == EstadoResolucion.Eliminado || fila.Estado ==
            // EstadoResolucion.primer_id_invalido) { // this.ID = -1; // this.copia = null; //}

            //    rem = fila;
            //}
            //catch (MySql.Data.MySqlClient.MySqlException ex) {
            //    mysqlrem = MySqlErrores (ex);
            //    Console.WriteLine ("Error> " + ex.Number);
            //}

            ////switch (mysqlrem) {
            ////    case ErrorMySql.SinConexion:
            ////        return Mensaje.SinConexion;
            ////}

            ////if (rem == null || rem.Estado == EstadoResolucion.Error_eliminado) {
            ////    return Mensaje.ErrorEliminarSinEspecificar;
            ////}

            //switch (rem.Estado) {
            //    //case EstadoResolucion.Eliminado:
            //    //    return new Mensaje (MensajeAdvertencia.Bueno, string.Concat ("El bombero ", this.NombreCompleto, " fue dado de baja satisfactoriamente."));

            // //case EstadoResolucion.Error_eliminar_referencias: // return new Mensaje ("El bombero
            // no se eliminara, es aconsejable desactivar el usuario.");

            //    default:
            //        return Mensaje.NoPasara;
            //}
        }

        public Mensaje Ingresar (Int32 id_responsable) {
            var @return = tareas (Opcion.Ingresar, null,
                                        this.DPI.ToTupleIngreso ( ),
                                        this.Nombre.ToTupleIngreso ( ),
                                        this.Apellido.ToTupleIngreso ( ),
                                        ((DateTime?) this.FechaNacimiento).ToTupleIngreso ( ),
                                        ((Int32?) this.Estado).ToTupleIngreso ( ),
                                        (this.Rol == BomberoRol.SinRol ? null : (Int32?) this.Rol).ToTupleIngreso ( ),
                                        ((Int32?) this.NoCarne).ToTupleIngreso ( ),
                                        this.Foto.ToTupleIngreso ( ),
                                        this.UsuarioID.ToTupleIngreso ( ),
                                        id_responsable);

            return @return;

            //IResolucion rem = null;
            //ErrorMySql mysqlrem = ErrorMySql.SinIndicar;

            //try {
            //    var fila = REM.sp_accion_bombero (Opcion.Ingresar, null,
            //                            this.DPI.ToTupleIngreso ( ),
            //                            this.Nombre.ToTupleIngreso ( ),
            //                            this.Apellido.ToTupleIngreso ( ),
            //                            ((DateTime?) this.FechaNacimiento).ToTupleIngreso ( ),
            //                            ((Int32?) this.Estado).ToTupleIngreso ( ),
            //                            (this.Rol == BomberoRol.SinRol ? null : (Int32?) this.Rol).ToTupleIngreso ( ),
            //                            ((Int32?) this.NoCarne).ToTupleIngreso ( ),
            //                            this.Foto.ToTupleIngreso ( ),
            //                            this.UsuarioID.ToTupleIngreso ( ),
            //                            id_responsable);

            // //if (fila.Estado == EstadoResolucion.Ingresado) { // this.ID = fila.OutID.Value; //
            // this.copia = base.MemberwiseClone ( ) as Bombero; //}

            //    rem = fila;
            //}
            //catch (MySql.Data.MySqlClient.MySqlException ex) {
            //    mysqlrem = MySqlErrores (ex);
            //    Console.WriteLine ("Error> " + ex.Number);
            //}

            ////switch (mysqlrem) {
            ////    case ErrorMySql.SinConexion:
            ////        return Mensaje.SinConexion;

            ////    case ErrorMySql.ElementoDuplicado:
            ////        return new Mensaje ("El numero DPI del bombero ya esta ingresado.");
            ////}

            ////if (rem == null || rem.Estado == EstadoResolucion.Error_ingresado) {
            ////    return Mensaje.ErrorIngresoSinEspecificar;
            ////}

            //switch (rem.Estado) {
            //    //case EstadoResolucion.Ingresado:
            //    //    return new Mensaje (MensajeAdvertencia.Bueno, string.Concat ("El ingreso del bombero ", this.NombreCompleto, " fue exitoso."));

            // //case EstadoResolucion.segundo_id_invalido: // return new Mensaje ("El usuario
            // especificado al bombero no esta disponible.");

            //    default:
            //        return Mensaje.NoPasara;
            //}
        }

        #endregion



        #region Metodos 

        private static Bombero FilaAClase (PersonasDataSet.bomberoRow fila) {
            var item = new Bombero ( ) {
                ID = fila.id,
                DPI = fila.dpi,
                Nombre = fila.nombre,
                Apellido = fila.apellido,
                FechaNacimiento = fila.fecha_nacimiento,
                Estado = (BomberoEstado) fila.estado,
                Rol = fila.IsrolNull ( ) ? BomberoRol.SinRol : (BomberoRol) fila.rol,
                NoCarne = fila.carne,
                Foto = "",
                UsuarioID = fila.Isusuario_idNull ( ) ? (Int32?) null : fila.usuario_id
            };

            item.copia = item.MemberwiseClone ( ) as Bombero;

            return item;
        }

        public static List<Bombero> ObtenerBomberos ( ) {
            try {
                using (var consulta = new bomberoTableAdapter ( )) {
                    var tabla = consulta.GetData ( );

                    var lista = tabla.Select (FilaAClase).ToList ( );

                    return lista;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex) {
                var r = MySqlExceptions (ex);
                if (r != null) {
                    throw r;
                }
            }

            return null;
        }

        public static List<Bombero> ObtenerBomberosActivos ( ) {
            try {
                using (var consulta = new bomberoTableAdapter ( )) {
                    var tabla = consulta.GetDataActivos ( );

                    var lista = tabla.Select (FilaAClase).ToList ( );

                    return lista;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex) {
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