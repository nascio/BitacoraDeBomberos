using BitacoraDeBomberos.BLL.Datos.Personas;
using BitacoraDeBomberos.DAL.IngresosDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitacoraDeBomberos.BLL.Datos.Ingresos {
    partial class REM {

        public static REM sp_accion_bombero (Opcion opcion, Int32? id,
            Tuple<Boolean?, String> dpi,
            Tuple<Boolean?, String> nombre,
            Tuple<Boolean?, String> apellido,
            Tuple<Boolean?, DateTime?> fecha_nacimiento,
            Tuple<Boolean?, Int32?> estado,
            Tuple<Boolean?, Int32?> rol,
            Tuple<Boolean?, Int32?> carne,
            Tuple<Boolean?, String> foto,
            Tuple<Boolean?, Int32?> usuario_id,

            Int32 id_responsable

            ) {

            dpi = FixC (dpi);
            nombre = FixC (nombre);
            apellido = FixC (apellido);
            fecha_nacimiento = Fix (fecha_nacimiento);
            estado = Fix (estado);
            rol = Fix (rol);
            carne = Fix (carne);
            usuario_id = Fix (usuario_id);
            foto = FixC (foto);

            using (var consulta = new accionesTableAdapter ( )) {
                var tabla = consulta.sp_accion_bombero (
                    (Int32) opcion,
                    id,
                    dpi.Item1, dpi.Item2,
                    nombre.Item1, nombre.Item2,
                    apellido.Item1, apellido.Item2,
                    fecha_nacimiento.Item1, fecha_nacimiento.Item2,
                    estado.Item1, estado.Item2,
                    rol.Item1, rol.Item2,
                    carne.Item1, carne.Item2,
                    foto.Item1, null,
                    usuario_id.Item1, usuario_id.Item2,
                    id_responsable);

                var fila = tabla.First ( );

                return FilaAClase (fila);
            }
        }


        public static REM sp_accion_acceso (Opcion opcion,
            Int32 rol_acceso_id,
            int control_id,
            Tuple<bool?, bool?> estado,

            Int32 id_responsable

            ) {

            estado = Fix (estado);

            using (var consulta = new accionesTableAdapter ( )) {
                var tabla = consulta.sp_accion_acceso (
                    (int) opcion,
                    rol_acceso_id,
                    control_id,
                    id_responsable
                    );

                var fila = tabla.First ( );

                return FilaAClase (fila);
            }
        }




        private static Tuple<Boolean?, T> FixC<T> (Tuple<Boolean?, T> c) where T : class {
            return c ?? Tuple.Create<Boolean?, T> (false, null);
        }
        private static Tuple<Boolean?, T?> Fix<T> (Tuple<Boolean?, T?> c) where T : struct {
            return c ?? Tuple.Create<Boolean?, T?> (false, null);
        }

    }
}
