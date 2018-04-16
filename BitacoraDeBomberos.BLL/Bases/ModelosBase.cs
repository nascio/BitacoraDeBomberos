using BitacoraDeBomberos.BLL.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BitacoraDeBomberos.BLL.Bases {

    public enum TipoAccion {
        Ejecutar,
        Nuevo,
        Editar
    }

    public delegate void AccionesUIEventHandler (Action accion, TipoAccion tipo);

    //public delegate void MensajesEventHandler (Mensaje mensaje);

    public abstract class ModelosBase : Notificador, INotifyDataErrorInfo {

        #region Constructores

        public ModelosBase ( ) {
            this.coleccionMensajes = new ObservableCollection<MVMensaje> ( );
        }

        #endregion


        #region Estados de CMD

        [Flags]
        protected enum EstadosCMD {
            actualizar_lista___ = 0b00000001,
            desactivar_elemento = 0b00000010,
            activar_elemento___ = 0b00000100,
            eliminar_elemento__ = 0b00001000,
            nuevo_elemento_____ = 0b00010000,
            editar_elemento____ = 0b00100000,

            aceptar_nuevo______ = 0b01000000,
            aceptar_editar_____ = 0b10000000,
        }

        private EstadosCMD estadosCMD;

        protected bool ContieneEstado (EstadosCMD estado) {
            return this.estadosCMD.HasFlag (estado);
        }
        protected void QuitarEstadosCMD (EstadosCMD estado) {
            this.estadosCMD = this.estadosCMD & ~estado;
        }
        protected void AgregarEstadosCMD (EstadosCMD estado) {
            this.estadosCMD = this.estadosCMD | estado;
        }

        #endregion

        #region Coleccion de mensajes

        private ObservableCollection<MVMensaje> coleccionMensajes;

        public ObservableCollection<MVMensaje> ColeccionMensajes {
            get {
                return this.coleccionMensajes;
            }
        }

        public void QuitarMensaje (MVMensaje mensaje) {
            var msg = mensaje;

            if (!this.ColeccionMensajes.Contains (msg)) {
                return;
            }

            this.OnAccionesUI (( ) => {
                this.ColeccionMensajes.Remove (msg);
                msg.Liberar ( );
            });
        }

        #endregion

        #region AccionesUI

        private Object @lock2 = new Object ( );
        private AccionesUIEventHandler accionesUI;

        public event AccionesUIEventHandler AccionesUI {
            add {
                lock (@lock2) {
                    this.accionesUI += value;
                }
            }
            remove {
                lock (@lock2) {
                    this.accionesUI -= value;
                }
            }
        }

        internal protected void OnAccionesUI (Action accion, TipoAccion tipo = TipoAccion.Ejecutar) {
            var handler = null as AccionesUIEventHandler;

            lock (@lock2) {
                handler = this.accionesUI;
            }
            Console.WriteLine ("handler is null: " + (handler == null));

            handler?.Invoke (accion, tipo);
        }

        #endregion

        #region Mensajes

        internal protected void OnMensajes (MVMensaje mvMensaje) {
            var msg = mvMensaje;

            if (this.ColeccionMensajes.Contains (msg)) {
                return;
            }

            this.OnAccionesUI (( ) => {
                this.ColeccionMensajes.Add (msg);
            });
        }

        internal protected void OnMensajes (Mensaje mensaje) {
            var msg = new MVMensaje (mensaje, this);

            if (this.ColeccionMensajes.Contains (msg)) {
                return;
            }

            this.OnAccionesUI (( ) => {
                this.ColeccionMensajes.Add (msg);
            });



            //var handler = null as MensajesEventHandler;

            //lock (@lock) 
            //    handler = this.mensajes;


            //handler?.Invoke (mensaje);
        }

        internal protected bool OnMensajesConCierreTiempo (Mensaje mensaje, byte segundos = 5) {
            var msg = new MVMensaje (mensaje, this);

            if (this.ColeccionMensajes.Contains (msg)) {
                return false;
            }

            this.OnAccionesUI (
                ( ) => {
                    var nu = msg.CerrarConTiempo (segundos);
                    this.ColeccionMensajes.Add (nu);
                }
            );



            //var handler = null as MensajesEventHandler;

            //lock (@lock)- 
            //    handler = this.mensajes;


            //handler?.Invoke (mensaje);
            return true;
        }

        #endregion

        #region Metodos

        public override void Liberar ( ) {
            base.Liberar ( );
            lock (@lock3) {
                this.errorsChanged = null;
            }
            lock (@lock2) {
                this.accionesUI = null;
            }
        }


        #endregion


        #region Errores

        protected virtual void OnCmdErroresExecuted (String propertyName) {

        }



        private Comando<string> cmdErrores;

        public ICommand CmdErrores {
            get {
                return this.cmdErrores ?? (this.cmdErrores = new Comando<String> (this.OnCmdErroresExecuted, x => !string.IsNullOrEmpty (x)));
            }
        }
        public virtual IEnumerable GetErrors (String propertyName) {
            return null;
        }

        private Boolean hasErrors;
        public Boolean HasErrors {
            get {
                return this.hasErrors;
            }
            protected set {
                this.hasErrors = value;
                base.OnPropertyChanged ( );
            }
        }

        Dictionary<String, MensajeAdvertencia> dic = new Dictionary<string, MensajeAdvertencia> ( );
        protected void AddMensajeAdvertencia (string propertyName, MensajeAdvertencia value) {
            this.dic[propertyName] = value;
        }
        protected void RemoveMensajeAdvertencia ([CallerMemberName]string propertyName = "") {
            if (this.dic.ContainsKey (propertyName)) {
                this.dic.Remove (propertyName);
                this.HasErrors = this.dic.Count > 0;
            }
        }
        protected MensajeAdvertencia? TryGetMensajeAdvertencia (string propertyName) {
            MensajeAdvertencia value;
            return this.dic.TryGetValue (propertyName, out value) ? value : (MensajeAdvertencia?) null;
        }

        protected void ClearErrors ( ) {
            this.dic.Clear ( );
        }


        protected void OnErrorsChanged ([CallerMemberName]string propertyName = "") {
            var e = new DataErrorsChangedEventArgs (propertyName);
            var handler = null as EventHandler<DataErrorsChangedEventArgs>;

            lock (@lock3) {
                handler = this.errorsChanged;
            }
            Console.WriteLine ("handler is null: " + (handler == null));

            handler?.Invoke (this, e);
        }

        private Object @lock3 = new Object ( );
        private EventHandler<DataErrorsChangedEventArgs> errorsChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged {
            add {
                lock (lock3) {
                    this.errorsChanged += value;
                }
            }
            remove {
                lock (lock3) {
                    this.errorsChanged -= value;
                }
            }
        }

        protected virtual int ObtenerResponsableID ( ) {
            //FIXME: establecer el usuario responsable de los cambios.
            return 0;
        }




        #endregion
    }
}