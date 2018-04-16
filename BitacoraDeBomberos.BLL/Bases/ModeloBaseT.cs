using BitacoraDeBomberos.BLL.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections;

namespace BitacoraDeBomberos.BLL.Bases {

    public abstract class ModelosBase<T> : ModelosBase {

        #region Coleccion

        protected ObservableCollection<T> coleccion;
        private T seleccion;

        public ObservableCollection<T> Coleccion {
            get {
                return this.coleccion;
            }
        }

        public T Seleccion {
            get {
                return this.seleccion;
            }
            set {
                this.seleccion = value;
                base.OnPropertyChanged ( );
            }
        }

        #endregion

        #region Actual

        private T actual;

        public T Actual {
            get {
                return this.actual;
            }
            set {
                this.actual = value;
                base.OnPropertyChanged ( );
                if (value == null) {
                    this.OnClearActual ( );
                }
                else {
                    this.OnSetActual (value);
                }
            }
        }

        protected virtual void OnClearActual ( ) {
            base.ClearErrors ( );
            base.HasErrors = false;
        }

        protected virtual void OnSetActual (T value) {

        }

        #endregion


        #region CMD Actualizar lista

        private Comando cmdActualizarLista;

        public ICommand CmdActualizarLista {
            get {
                return this.cmdActualizarLista ??
                    (this.cmdActualizarLista = new Comando (this.cmdActualizarListaTask));
            }
        }

        private void cmdActualizarListaTask ( ) {
            if (base.ContieneEstado (EstadosCMD.actualizar_lista___)) {
                base.OnMensajesConCierreTiempo (Mensaje.Procesando, 1);
                return;
            }

            base.AgregarEstadosCMD (EstadosCMD.actualizar_lista___);
            var task = Task.Run (async ( ) => {
                var mensaje = new MVMensaje (Mensaje.ActualizandoLista, this);
                base.OnMensajes (mensaje);
                await Task.Delay (1250);

                this.ActualizarLista ( );

                await Task.Delay (250);
                base.QuitarMensaje (mensaje);
                base.QuitarEstadosCMD (EstadosCMD.actualizar_lista___);
            }).ConfigureAwait (false);
        }

        public virtual void ActualizarLista ( ) {

        }

        #endregion

        #region CMD Nuevo elemento

        private Comando cmdNuevoElemento;

        public ICommand CmdNuevoElemento {
            get {
                return this.cmdNuevoElemento ?? (this.cmdNuevoElemento = new Comando (this.cmdNuevoElementoTask));
            }
        }

        private void cmdNuevoElementoTask ( ) {
            if (base.ContieneEstado (EstadosCMD.nuevo_elemento_____)) {
                return;
            }

            base.AgregarEstadosCMD (EstadosCMD.nuevo_elemento_____);
            var task = Task.Run (async ( ) => {
                await Task.Delay (10);
                base.OnAccionesUI (this.OnNuevoElementoUI, TipoAccion.Nuevo);
            }).ConfigureAwait (false);
        }

        protected virtual void OnNuevoElementoUI ( ) {
            base.QuitarEstadosCMD (EstadosCMD.nuevo_elemento_____);
        }

        #endregion

        #region CMD Editar elemento

        private Comando cmdEditarElemento;

        public ICommand CmdEditarElemento {
            get {
                return this.cmdEditarElemento ?? (this.cmdEditarElemento = new Comando (this.cmdEditarElementoTask));
            }
        }

        private void cmdEditarElementoTask ( ) {
            if (base.ContieneEstado (EstadosCMD.editar_elemento____)) {
                return;
            }

            base.AgregarEstadosCMD (EstadosCMD.editar_elemento____);
            var task = Task.Run (async ( ) => {
                await Task.Delay (10);
                base.OnAccionesUI (this.OnEditarElementoUI, TipoAccion.Editar);
            }).ConfigureAwait (false);
        }

        protected virtual void OnEditarElementoUI ( ) {

        }

        #endregion

        #region CMD Aceptar (Nuevo y editar)

        private Comando<TipoAccion> cmdAceptar;
        private Comando cmdAceptarYNuevo;

        public ICommand CmdAceptar {
            get {
                return this.cmdAceptar ?? (this.cmdAceptar = new Comando<TipoAccion> (this.cmdAceptarTask, x => x == TipoAccion.Nuevo || x == TipoAccion.Editar));
            }
        }

        private void cmdAceptarTask (TipoAccion tipoAccion) {
            if (tipoAccion == TipoAccion.Nuevo) {
                if (base.ContieneEstado (EstadosCMD.aceptar_nuevo______)) {
                    return;
                }
                base.AgregarEstadosCMD (EstadosCMD.aceptar_nuevo______);
            }
            else if (tipoAccion == TipoAccion.Editar) {
                if (base.ContieneEstado (EstadosCMD.aceptar_editar_____)) {
                    return;
                }
                base.AgregarEstadosCMD (EstadosCMD.aceptar_editar_____);
            }
            else {
                return;
            }

            var task = Task.Run (async ( ) => {
                await Task.Delay (10);
                if (tipoAccion == TipoAccion.Editar) {
                    await this.OnAceptarEditarTask ( );
                    base.QuitarEstadosCMD (EstadosCMD.aceptar_editar_____);
                }
                else if (tipoAccion == TipoAccion.Nuevo) {
                    await this.OnAceptarNuevoTaskAsync ( );
                    base.QuitarEstadosCMD (EstadosCMD.aceptar_nuevo______);
                }
            }).ConfigureAwait (false);
        }

        protected virtual Task OnAceptarNuevoTaskAsync ( ) {
            return Task.CompletedTask;
        }

        protected virtual Task OnAceptarEditarTask ( ) {
            return Task.CompletedTask;
        }

        #endregion




        #region Errores

        public override IEnumerable GetErrors (String propertyName) {
            if (string.IsNullOrEmpty (propertyName)) {
                return null;
            }

            var temp = null as MensajeAdvertencia?;

            if (EsPropiedadValidaError (propertyName) && (temp = base.TryGetMensajeAdvertencia (propertyName)).HasValue) {
                return Enumerable.Repeat (temp.Value, 1);
            }

            return null;
        }

        protected virtual bool EsPropiedadValidaError (String propertyName) {
            return false;
        }

        protected void OnQuitarMensajes (params Mensaje[] mensajes) {
            foreach (var item in mensajes) {
                base.QuitarMensaje (new MVMensaje (item, null));
            }
        }

        #endregion







    }
}