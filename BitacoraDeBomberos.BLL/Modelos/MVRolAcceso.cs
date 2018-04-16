using BitacoraDeBomberos.BLL.Bases;
using BitacoraDeBomberos.BLL.Datos.Accesos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitacoraDeBomberos.BLL.Modelos {
    public class MVRolAcceso : ModelosBase<RolAcceso> {


        public override void ActualizarLista ( ) {
            try {
                var lista = RolAcceso.ObtenerRolesDeAcceso ( );
                if (lista == null) {
                    throw new Exepciones.SinConexionException ( );
                }

                base.OnAccionesUI (( ) => {
                    base.coleccion = new ObservableCollection<RolAcceso> (lista);
                    base.OnPropertyChanged ("Coleccion");
                });
            }
            catch (Exepciones.SinConexionException) {
                base.OnMensajesConCierreTiempo (Mensaje.SinConexion);
            }
        }




        Mensaje msgError = new MSG ("El campo del 'Nombre' no puede estar en blanco.");

        private String actualNombre;

        public String ActualNombre {
            get {
                return this.actualNombre;
            }
            set {
                this.actualNombre = Ayuda.ToCamelCase (value);

                base.OnPropertyChanged ( );
                base.RemoveMensajeAdvertencia ( );
                base.OnErrorsChanged ( );
                base.OnQuitarMensajes (this.msgError);
            }
        }

        private List<Control> actualControles;
        bool listaControles = false;
        public List<Control> ActualControles {
            get {
                if (this.actualControles == null) {
                    if (this.listaControles) {
                        return actualControles;
                    }
                    this.listaControles = true;
                    var task = Task.Run (async ( ) => {
                        await Task.Delay (10);
                        this.crearListaControles ( );
                    });
                }

                return this.actualControles;
            }
        }

        private void crearListaControles ( ) {
            try {
                var lista = Control.ObtenerControles ( );
                if (lista == null) {
                    throw new Exepciones.SinConexionException ( );
                }

                base.OnAccionesUI (( ) => {
                    this.actualControles = new List<Control> (lista);
                    base.OnPropertyChanged ("ActualControles");
                });

            }
            catch (Exepciones.SinConexionException) {
                this.listaControles = false;
                base.OnMensajesConCierreTiempo (Mensaje.SinConexion);
            }
        }


        ObservableCollection<MVAcceso> accesos;
        List<Control> c;

        protected override async Task OnAceptarNuevoTaskAsync ( ) {
            if (base.HasErrors) {
                base.OnMensajesConCierreTiempo (Mensaje.HayErrores, 2);
                return;
            }

            var responsable = base.ObtenerResponsableID ( );

            var actual = base.Actual;

            actual.Nombre = this.ActualNombre;
            //var msg = await Task.Run (( ) => actual.Ingresar (responsable));

            //if (msg.Advertencia != MensajeAdvertencia.Bueno) {
            //    base.OnMensajesConCierreTiempo (msg, 20);
            //    return;
            //}

            //if (base.coleccion != null) {
            //    base.coleccion.Add (actual);
            //}

            //foreach (var item in this.accesos) {
            //    var mm = await Task.Run (( ) => item.Aplicar (responsable));
            //    if (mm.Advertencia != MensajeAdvertencia.Bueno) {
            //        base.OnMensajesConCierreTiempo (mm);
            //    }
            //}

            await Task.Delay (10);
        }


        protected void onNuevoElemento ( ) {
            if (this.c == null) {

                try {
                    var lista = Control.ObtenerControles ( );
                    if (lista == null) {
                        throw new Exepciones.SinConexionException ( );
                    }
                    this.c = lista;
                }
                catch (Exepciones.SinConexionException) {
                    //this.listaControles = false;
                    base.OnMensajesConCierreTiempo (Mensaje.SinConexion);
                    return;
                }
            }

            if (this.accesos == null) {
                this.accesos = new ObservableCollection<MVAcceso> ( );
            }
            this.accesos.Clear ( );
            foreach (var control in this.c) {
                this.accesos.Add (new MVAcceso (this, control));
            }
            base.Actual = new RolAcceso ( );
            base.OnNuevoElementoUI ( );
        }

        protected override void OnCmdErroresExecuted (String propertyName) {
            switch (propertyName) {
                case "ActualNombre":
                    if (StringIsNullOrEmpty (this.ActualNombre)) {
                        base.OnMensajes (this.msgError);
                        base.AddMensajeAdvertencia (propertyName, MensajeAdvertencia.Error);
                        base.HasErrors = true;
                        base.OnErrorsChanged (propertyName);
                    }
                    else {
                        base.HasErrors = false;
                    }

                    break;
            }
        }

        protected override Boolean EsPropiedadValidaError (String propertyName) {
            switch (propertyName) {
                case "ActualNombre":
                    return true;
            }
            return false;
        }



        protected override void OnNuevoElementoUI ( ) {
            Console.WriteLine ("Nuevo rol");
            base.Actual = new RolAcceso ( );
            base.OnNuevoElementoUI ( );
        }

        protected override void OnSetActual (RolAcceso value) {
            Console.WriteLine ("Establecer valores actuales");
            this.HasErrors = false;
            this.ActualNombre = value.Nombre;
            base.OnSetActual (value);
        }

        protected override void OnClearActual ( ) {
            Console.WriteLine ("Limpiar todo");
            this.ActualNombre = string.Empty;
            base.OnClearActual ( );
        }








    }


    public class MVAcceso : Notificador {
        Control control;
        bool estado;
        MVRolAcceso modelo;

        public MVAcceso (MVRolAcceso modelo, Control control, Boolean estado) {
            this.modelo = modelo;
            this.control = control;
            this.estado = estado;
        }

        public MVAcceso (MVRolAcceso modelo, Control control) : this (modelo, control, false) {
        }

        public bool Estado {
            get {
                return this.estado;
            }
            set {
                this.estado = value;
                base.OnPropertyChanged ( );
            }
        }

        public Control Control {
            get {
                return this.control;
            }
        }

        public Mensaje Aplicar (int id_responsable) {
            var acceso = new Acceso ( ) {
                ControlID = this.Control.ID,
                RolAccesoID = this.modelo.Actual.ID,
                Estado = this.Estado
            };

            if (this.Estado) {
                return acceso.Activar (id_responsable);
            }
            else {
                return acceso.Desactivar (id_responsable);
            }
        }



    }


}
