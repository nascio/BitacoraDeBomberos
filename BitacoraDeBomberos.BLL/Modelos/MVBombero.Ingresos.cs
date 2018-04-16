using BitacoraDeBomberos.BLL.Bases;
using BitacoraDeBomberos.BLL.Datos.Personas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Input;
using BitacoraDeBomberos.BLL.Datos.Accesos;

namespace BitacoraDeBomberos.BLL.Modelos {

    partial class MVBombero : INotifyDataErrorInfo {
        private List<TextoEnum<BomberoRol>> roles;

        MSG[] mensajeserrores = new MSG[] {
            new MSG ("El campo del DPI no puede estar en blanco."),
            new MSG ("El formato del DPI no valido."),
            new MSG ("El campo del Nombre no puede estar en blanco."),
            new MSG ("El campo del Apellido no puede estar en blanco."),
            new MSG ("El campo del Numero de carné no puede estar en blanco."),
            new MSG("El campo de la Fecha de nacimiento no puede estar en blanco."),
            new MSG("El formato de la Fecha de nacimiento es de dia / mes / año")
        };

        public List<TextoEnum<BomberoRol>> ListaRoles {
            get {
                return this.roles ?? (this.roles = new List<TextoEnum<BomberoRol>> ( ) {
                    new TextoEnum<BomberoRol>(BomberoRol.SinRol, "Sin especificar"),
                    new TextoEnum<BomberoRol>(BomberoRol.Jefe, "Jefe de servicio"),
                    new TextoEnum<BomberoRol>(BomberoRol.Secretario, "Secretario"),
                    new TextoEnum<BomberoRol>(BomberoRol.Piloto, "Piloto")
                });
            }
        }


        private Comando cmdNuevo;
        public ICommand CmdNuevo {
            get {
                return this.cmdNuevo ?? (this.cmdNuevo = new Comando (this.cmdNuevoTask));
            }
        }

        private Comando cmdNuevoAG;
        public ICommand CmdNuevoAG {
            get {
                return this.cmdNuevoAG ?? (this.cmdNuevoAG = new Comando (this.cmdNuevoAGTask));
            }
        }

        private void cmdNuevoAGTask ( ) {
            var s = 0;
        }

        bool agregar;
        private void cmdNuevoTask ( ) {
            if (agregar) {
                return;
            }

            var t = Task.Run (async ( ) => {
                await Task.Delay (10);
                OnUIIII ( );
            }).ConfigureAwait (false);
        }

        private void OnUIIII ( ) {
            base.OnAccionesUI (( ) => {
                this.Actual = new Bombero ( ) {
                    UsuarioID = 1,
                    Nombre = "Adda",
                    Apellido = "Pappa Maldonado",
                    DPI = "1000  12345  1001",
                    FechaNacimiento = DateTime.Now.AddYears (-18),
                    NoCarne = 20
                };
            }, TipoAccion.Nuevo);
        }



        private Bombero actual;
        public Bombero Actual {
            get {
                return this.actual;
            }
            set {
                this.actual = value;
                base.OnPropertyChanged ( );

                if (value == null) {
                    this.ActualRol = BomberoRol.SinRol;
                    this.ActualNombre = null;
                    this.ActualApellido = null;
                    this.ActualNoCarne = null;
                    this.ActualDPI = null;
                    this.ActualFechaNacimiento = null;

                    this.ActualAgregarUsuario = false;
                    this.ActualUsuario = null;
                    this.ActualContrasenia = null;
                    this.ActualHayUsuario = false;
                    this.actualUsuarioID = -1;
                    this.cargoUsuario = false;
                    base.HasErrors = false;
                }
                else {
                    this.ActualRol = value.Rol;
                    this.ActualNombre = value.Nombre;
                    this.ActualApellido = value.Apellido;
                    this.ActualNoCarne = value.NoCarne.ToString ( );
                    this.ActualDPI = value.DPI;
                    this.ActualFechaNacimiento = value.FechaNacimiento.ToString ("dd / MM / yyyy");

                    this.ActualAgregarUsuario = false;

                    this.ActualHayUsuario = value.UsuarioID.HasValue;
                    this.actualUsuarioID = value.UsuarioID.HasValue ? value.UsuarioID.Value : -1;
                    this.cargoUsuario = false;
                    this.ActualUsuario = null;

                }
            }
        }


        private BomberoRol actualRol;
        public BomberoRol ActualRol {
            get {
                return this.actualRol;
            }
            set {
                this.actualRol = value;
                base.OnPropertyChanged ( );
            }
        }

        private string actualNombre;
        public string ActualNombre {
            get {
                return this.actualNombre;
            }
            set {
                this.actualNombre = Ayuda.ToCamelCase (value);
                base.OnPropertyChanged ( );
                base.RemoveMensajeAdvertencia ( );
                base.OnErrorsChanged ( );
                this.quitarMensajes (this.mensajeserrores[2]);
            }
        }

        private string actualApellido;
        public string ActualApellido {
            get {
                return this.actualApellido;
            }
            set {
                this.actualApellido = Ayuda.ToCamelCase (value);
                base.OnPropertyChanged ( );
                base.RemoveMensajeAdvertencia ( );
                base.OnErrorsChanged ( );
                this.quitarMensajes (this.mensajeserrores[3]);
            }
        }

        private string actualNoCarne;
        public string ActualNoCarne {
            get {
                return this.actualNoCarne;
            }
            set {
                var cambios = false;
                if (string.IsNullOrEmpty (value)) {
                    this.actualNoCarne = value;
                    cambios = true;
                }
                else {
                    var ii = 0;
                    if (int.TryParse (value, out ii)) {
                        this.actualNoCarne = ii.ToString ( );
                        cambios = true;
                    }
                }

                if (cambios) {
                    base.OnPropertyChanged ( );
                    base.RemoveMensajeAdvertencia ( );
                    base.OnErrorsChanged ( );
                    this.quitarMensajes (this.mensajeserrores[4]);
                }

            }
        }

        private string actualDPI;
        public string ActualDPI {
            get {
                return this.actualDPI;
            }
            set {
                this.actualDPI = value;
                base.OnPropertyChanged ( );
                base.RemoveMensajeAdvertencia ( );
                base.OnErrorsChanged ( );
                this.quitarMensajes (this.mensajeserrores[0], this.mensajeserrores[1]);
            }
        }

        private string actualFechaNacimiento;
        public string ActualFechaNacimiento {
            get {
                return this.actualFechaNacimiento;
            }
            set {
                this.actualFechaNacimiento = value;
                base.OnPropertyChanged ( );
                base.RemoveMensajeAdvertencia ( );
                base.OnErrorsChanged ( );
                this.quitarMensajes (this.mensajeserrores[5], this.mensajeserrores[6]);

            }
        }







        private Boolean actualAgregarUsuario = true;
        public Boolean ActualAgregarUsuario {
            get {
                return this.actualAgregarUsuario;
            }
            set {
                this.actualAgregarUsuario = value;
                base.OnPropertyChanged ( );
            }
        }

        private Boolean actualHayUsuario;
        public Boolean ActualHayUsuario {
            get {
                return this.actualHayUsuario;
            }
            set {
                this.actualHayUsuario = value;
                base.OnPropertyChanged ( );
            }
        }

        private int actualUsuarioID = 1;
        private bool cargoUsuario;

        private string actualUsuario;
        public string ActualUsuario {
            get {
                return this.actualHayUsuario && !this.cargoUsuario ? "Nombre de usuario sin cargar." : this.actualUsuario;
            }
            set {
                this.actualUsuario = value;
                base.OnPropertyChanged ( );
                if (ActualHayUsuario) {
                    return;
                }

                base.RemoveMensajeAdvertencia ( );
                base.OnErrorsChanged ( );
                //this.quitarMensajes();
            }
        }

        private string actualContrasenia;
        public string ActualContrasenia {
            get {
                return this.actualContrasenia;
            }
            set {
                this.actualContrasenia = value;
                base.OnPropertyChanged ( );
                base.RemoveMensajeAdvertencia ( );
                base.OnErrorsChanged ( );
                //this.quitarMensajes();
            }
        }

        private Comando cmdCargarInfoUsuario;
        public ICommand CmdCargarInfoUsuario {
            get {
                return this.cmdCargarInfoUsuario ?? (this.cmdCargarInfoUsuario = new Comando (this.cargarInfoUsuarioCMD));
            }
        }

        private void cargarInfoUsuarioCMD ( ) {
            if (this.cargoUsuario) {
                base.OnMensajesConCierreTiempo (Mensaje.InfoDeUsuarioActualizada);
                return;
            }

            this.cargoUsuario = true;
            var t = Task.Run (( ) => {
                this.cargarInfoUsuario ( );

            }).ConfigureAwait (false);

        }

        private void cargarInfoUsuario ( ) {
            try {
                var user = Usuario.ObtenerUsuarioPorID (this.actualUsuarioID);
                if (user == null) {
                    throw new Exepciones.SinConexionException ( );
                }

                this.actualUsuario = user.Nombre;
                base.OnPropertyChanged ("ActualUsuario");
            }
            catch (Exepciones.SinConexionException) {
                Console.WriteLine ("msmsm");
                base.OnMensajesConCierreTiempo (Mensaje.SinConexion);
                this.cargoUsuario = false;
            }
        }




















        private void quitarMensajes (params Mensaje[] mensajes) {
            foreach (var item in mensajes) {
                base.QuitarMensaje (new MVMensaje (item, this));
            }
        }



        public override IEnumerable GetErrors (String propertyName) {
            if (string.IsNullOrEmpty (propertyName)) {
                return null;
            }

            var mm = null as MensajeAdvertencia?;
            switch (propertyName) {
                case "ActualDPI":
                case "ActualNombre":
                case "ActualApellido":
                case "ActualNoCarne":
                case "ActualFechaNacimiento":
                    mm = base.TryGetMensajeAdvertencia (propertyName);
                    if (mm.HasValue) {
                        return Enumerable.Repeat (mm.Value, 1);
                    }
                    break;
            }

            return null;
        }

        protected override void OnCmdErroresExecuted (String propertyName) {
            var hayError = false;
            var advertencia = MensajeAdvertencia.Bueno;

            switch (propertyName) {
                case "ActualUsuario":
                    if (this.ActualHayUsuario) {
                        return;
                    }

                    if (StringIsNullOrEmpty (this.ActualUsuario)) {
                        //base.OnMensajes (this.mensajeserrores[2]);
                        hayError = true;
                        advertencia = MensajeAdvertencia.Error;
                    }
                    break;
                case "ActualContrasenia":
                    if (this.ActualHayUsuario) {
                        return;
                    }

                    if (StringIsNullOrEmpty (this.ActualContrasenia)) {
                        //base.OnMensajes (this.mensajeserrores[2]);
                        hayError = true;
                        advertencia = MensajeAdvertencia.Error;
                    }
                    break;

                case "ActualDPI":
                    hayError = true;
                    advertencia = MensajeAdvertencia.Error;

                    var @long = 0L;
                    if (StringIsNullOrEmpty (this.ActualDPI)) {
                        base.OnMensajes (this.mensajeserrores[0]);
                    }
                    else if (!long.TryParse (this.ActualDPI.Replace (" ", string.Empty), out @long)) {
                        base.OnMensajes (this.mensajeserrores[1]);
                    }
                    else {
                        hayError = false;
                    }
                    break;

                case "ActualNombre":
                    if (StringIsNullOrEmpty (this.ActualNombre)) {
                        base.OnMensajes (this.mensajeserrores[2]);
                        hayError = true;
                        advertencia = MensajeAdvertencia.Error;
                    }
                    break;

                case "ActualApellido":
                    if (StringIsNullOrEmpty (this.ActualApellido)) {
                        base.OnMensajes (this.mensajeserrores[3]);
                        hayError = true;
                        advertencia = MensajeAdvertencia.Error;
                    }
                    break;

                case "ActualNoCarne":
                    if (StringIsNullOrEmpty (this.ActualNoCarne)) {
                        base.OnMensajes (this.mensajeserrores[4]);
                        hayError = true;
                        advertencia = MensajeAdvertencia.Error;
                    }
                    break;

                case "ActualFechaNacimiento":
                    hayError = true;
                    advertencia = MensajeAdvertencia.Error;
                    var dateTime = DateTime.Now;
                    if (StringIsNullOrEmpty (this.ActualFechaNacimiento)) {
                        base.OnMensajes (this.mensajeserrores[5]);
                    }
                    else if (!DateTime.TryParse (this.ActualFechaNacimiento.Replace (" ", String.Empty), out dateTime)) {
                        base.OnMensajes (this.mensajeserrores[6]);
                    }
                    else {
                        hayError = false;
                    }

                    break;

                default:
                    return;
            }


            if (hayError) {
                base.AddMensajeAdvertencia (propertyName, advertencia);
            }

            base.HasErrors |= hayError;
            base.OnErrorsChanged (propertyName);
        }



        //private void erroresCMD ( ) {
        //    Validar ("ActualDPI", out var b, out var er);

        //    if (er) {
        //        base.AddMensajeAdvertencia ("ActualDPI", MensajeAdvertencia.Error);
        //    }
        //    //this.dic["ActualDPI"] = er ? MensajeAdvertencia.Error : MensajeAdvertencia.Bueno;

        //    this.HasErrors = er;
        //    this.OnErrorsChanged ("ActualDPI");
        //}
    }
}