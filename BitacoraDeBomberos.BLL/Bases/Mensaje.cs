using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitacoraDeBomberos.BLL.Bases {

    public abstract class Mensaje {
        private MensajeAdvertencia advertencia;
        private String contenido;

        public Mensaje (MensajeAdvertencia advertencia, string contenido) {
            this.contenido = contenido;
            this.advertencia = advertencia;
        }

        public Mensaje (string contenido) : this (MensajeAdvertencia.Error, contenido) {
        }

        public String Contenido {
            get {
                return this.contenido;
            }
        }

        public MensajeAdvertencia Advertencia {
            get {
                return this.advertencia;
            }
        }

        /// <summary>
        ///     Mensaje: No se puede conectar al servidor. 
        /// </summary>
        internal static Mensaje SinConexion = new MSG ("No se puede conectar al servidor.");

        /// <summary>
        ///     Mensaje: Error en el ingreso no especificado. 
        /// </summary>
        internal static Mensaje ErrorIngresoSinEspecificar = new MSG ("Error en el ingreso no especificado.");

        /// <summary>
        ///     Mensaje: Esto nunca pasara. 
        /// </summary>
        internal static Mensaje NoPasara = new MSG ("Esto nunca pasara.");

        /// <summary>
        ///     Advertencia2, Mensaje: Sin cambios a realizar. 
        /// </summary>
        internal static Mensaje SinCambios = new MSG (MensajeAdvertencia.Advertencia2, "Sin cambios a realizar.");

        /// <summary>
        ///     Mensaje: Error en la actualizacion no especificada. 
        /// </summary>
        internal static Mensaje ErrorActualizarSinEspecificar = new MSG ("Error en la actualizacion no especificada.");

        /// <summary>
        ///     Mensaje: Error en la eliminacion no especificada. 
        /// </summary>
        internal static Mensaje ErrorEliminarSinEspecificar = new MSG ("Error en la eliminacion no especificada.");




        internal static Mensaje Procesando = new MSG (MensajeAdvertencia.Advertencia2, "Procesando todavia.");

        internal static Mensaje ActualizandoLista = new MSG (MensajeAdvertencia.Bueno, "Actualizando la lista.");
        internal static Mensaje InfoDeUsuarioActualizada = new MSG (MensajeAdvertencia.Bueno, "Ya se cargo la informacion del usuario.");

        internal static Mensaje HayErrores = new MSG ("Hay errores sin resolver.");

    }

    internal class MSG : Mensaje {
        public Datos.Ingresos.EstadoResolucion Estado;

        public MSG (MensajeAdvertencia advertencia, string contenido) : base (advertencia, contenido) {
        }

        public MSG (string contenido) : base (MensajeAdvertencia.Error, contenido) {
        }
    }
}