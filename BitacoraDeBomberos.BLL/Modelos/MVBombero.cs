using BitacoraDeBomberos.BLL.Bases;
using BitacoraDeBomberos.BLL.Datos.Personas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BitacoraDeBomberos.BLL.Modelos {

    public partial class MVBombero : ModelosBase {

        #region Constructores

        public MVBombero ( ) {
            this.actualizarListaTask ( );
        }

        #endregion

        #region Coleccion

        private ObservableCollection<Bombero> coleccion;
        private Bombero seleccion;

        public ObservableCollection<Bombero> Coleccion {
            get {
                return this.coleccion;
            }
        }

        public Bombero Seleccion {
            get {
                return this.seleccion;
            }
            set {
                this.seleccion = value;
                base.OnPropertyChanged ( );
            }
        }

        #endregion

        #region CMD Actualizar lista

        private Comando cmdActualizarLista;

        public ICommand CmdActualizarLista {
            get {
                return this.cmdActualizarLista ?? (this.cmdActualizarLista = new Comando (this.actualizarListaTask));
            }
        }

        private void actualizarListaTask ( ) {
            if (base.ContieneEstado (EstadosCMD.actualizar_lista___)) {
                base.OnMensajesConCierreTiempo (Mensaje.Procesando, 1);
                return;
            }

            base.AgregarEstadosCMD (EstadosCMD.actualizar_lista___);

            var t = Task.Run (async ( ) => {
                var mensaje = new MVMensaje (Mensaje.ActualizandoLista, this);
                base.OnMensajes (mensaje);
                await Task.Delay (1250);

                this.ActualizarLista ( );

                await Task.Delay (250);
                base.QuitarMensaje (mensaje);

                base.QuitarEstadosCMD (EstadosCMD.actualizar_lista___);
            }).ConfigureAwait (false);
        }

        public void ActualizarLista ( ) {
            try {
                var lista = Bombero.ObtenerBomberos ( );
                if (lista == null) {
                    throw new Exepciones.SinConexionException ( );
                }

                base.OnAccionesUI (( ) => {
                    this.coleccion = new ObservableCollection<Bombero> (lista);
                    base.OnPropertyChanged ("Coleccion");
                });
                Console.WriteLine ("mens1");
            }
            catch (Exepciones.SinConexionException) {
                Console.WriteLine ("mens2");
                base.OnMensajesConCierreTiempo (Mensaje.SinConexion);
            }
        }

        #endregion

        #region CMD Desactivar

        private Comando cmdDesactivar;

        public ICommand CmdDesactivar {
            get {
                return this.cmdDesactivar ?? (this.cmdDesactivar = new Comando (this.desactivarTask));
            }
        }

        private void desactivarTask ( ) {
            if (base.ContieneEstado (EstadosCMD.desactivar_elemento)) {
                base.OnMensajesConCierreTiempo (Mensaje.Procesando, 2);
                return;
            }

            base.AgregarEstadosCMD (EstadosCMD.desactivar_elemento);
            var t = Task.Run (async ( ) => {
                var mensaje = this.msgDesactivar (this.Seleccion);

                if (mensaje != null && base.OnMensajesConCierreTiempo (mensaje)) {
                    await Task.Delay (5000);
                }

                base.QuitarEstadosCMD (EstadosCMD.desactivar_elemento);
            }).ConfigureAwait (false);
        }

        private Mensaje msgDesactivar (Bombero bombero) {
            if (bombero == null) {
                return null;
            }

            var responsable = this.ObtenerResponsableID ( );
            var mensaje = bombero.Desactivar (responsable);

            if (mensaje.Advertencia == MensajeAdvertencia.Bueno) {
                var index = this.Coleccion.IndexOf (bombero);

                base.OnAccionesUI (( ) => {
                    this.Coleccion[index] = null;
                    this.Coleccion[index] = bombero;
                    this.Seleccion = bombero;
                });
            }

            return mensaje;
        }

        public void Desactivar (Bombero bombero) {
            var mensaje = this.msgDesactivar (bombero);
            if (mensaje != null) {
                base.OnMensajesConCierreTiempo (mensaje);
            }
        }

        #endregion

        #region CMD Activar

        private Comando cmdActivar;

        public ICommand CmdActivar {
            get {
                return this.cmdActivar ?? (this.cmdActivar = new Comando (this.activarTask));
            }
        }

        private void activarTask ( ) {
            if (base.ContieneEstado (EstadosCMD.activar_elemento___)) {
                base.OnMensajesConCierreTiempo (Mensaje.Procesando, 2);
                return;
            }

            base.AgregarEstadosCMD (EstadosCMD.activar_elemento___);
            var t = Task.Run (async ( ) => {
                var mensaje = this.msgActivar (this.Seleccion);

                if (mensaje != null && base.OnMensajesConCierreTiempo (mensaje)) {
                    await Task.Delay (5000);
                }

                base.QuitarEstadosCMD (EstadosCMD.activar_elemento___);
            }).ConfigureAwait (false);
        }

        private Mensaje msgActivar (Bombero bombero) {
            if (bombero == null) {
                return null;
            }

            var responsable = this.ObtenerResponsableID ( );
            var mensaje = bombero.Activar (responsable);

            if (mensaje.Advertencia == MensajeAdvertencia.Bueno) {
                var index = this.Coleccion.IndexOf (bombero);

                base.OnAccionesUI (( ) => {
                    this.Coleccion[index] = null;
                    this.Coleccion[index] = bombero;
                    this.Seleccion = bombero;
                });
            }

            return mensaje;
        }

       
        public void Activar (Bombero bombero) {
            var mensaje = this.msgDesactivar (bombero);
            if (mensaje != null) {
                base.OnMensajesConCierreTiempo (mensaje);
            }
        }

        #endregion
    }
}