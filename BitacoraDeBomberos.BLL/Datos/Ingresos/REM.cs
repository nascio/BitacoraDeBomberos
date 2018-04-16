using BitacoraDeBomberos.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitacoraDeBomberos.BLL.Datos.Ingresos {

    internal partial class REM : IResolucion {
        private EstadoResolucion estado;
        public REM ( ) {
        }

        public REM (EstadoResolucion estado, String mensaje) {
            this.estado = estado;
            this.Mensaje = mensaje;
        }
        public REM (String mensaje) : this (EstadoResolucion.SinEspecificar, mensaje) { }


        public String Mensaje {
            get;
            set;
        }

        public EstadoResolucion Estado {
            get {
                return this.estado;
            }
        }

        private Int32? outID;

        public Int32? OutID {
            get {
                return this.outID;
            }
        }

        public static REM FilaAClase (IngresosDataSet.accionesRow fila) {
            var item = new REM ( ) {
                estado = (EstadoResolucion) fila.out_estado,
                outID = fila.Isout_idNull ( ) ? (Int32?) null : fila.out_id
            };

            return item;
        }
    }
}