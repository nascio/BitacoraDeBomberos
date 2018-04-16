using BitacoraDeBomberos.BLL.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BitacoraDeBomberos.BLL.Modelos {
    public class MVMensaje : Notificador {

        Mensaje mensaje;
        ModelosBase modelo;
        private string segundos;
        private bool cerrado;

        public MVMensaje (Mensaje mensaje, ModelosBase modelo) {
            this.mensaje = mensaje;
            this.modelo = modelo;
        }

        public Mensaje Mensaje {
            get {
                return this.mensaje;
            }
        }

        public String Segundos {
            get {
                return this.segundos;
            }
            set {
                this.segundos = value;
                base.OnPropertyChanged ( );
            }
        }

        public MVMensaje CerrarConTiempo (byte segundos) {
            var tiempo = TimeSpan.FromSeconds (segundos);
            this.Segundos = "Cerrando mensaje: " + tiempo.Seconds + " segs.";

            var t = Task.Run (async ( ) => {
                while (tiempo.Seconds > 0) {
                    await Task.Delay (1000);
                    this.modelo.OnAccionesUI (( ) => this.Segundos = "Cerrando mensaje en: " + tiempo.Seconds + " segs.");
                    tiempo = tiempo.Add (TimeSpan.FromSeconds (-1));
                }

                this.cerrarTask ( );
            }).ConfigureAwait (false);

            return this;
        }


        Comando cmdCerrar;
        public ICommand CmdCerrar {
            get {
                return this.cmdCerrar ?? (this.cmdCerrar = new Comando (this.cerrarTask));
            }
        }

        private void cerrarTask ( ) {
            if (this.cerrado) {
                return;
            }

            var t = Task.Run (async ( ) => {
                await Task.Delay (100);

                this.modelo.QuitarMensaje (this);
                this.cerrado = true;
            }).ConfigureAwait (false);
        }





        public override Boolean Equals (Object obj) {
            return (obj is MVMensaje) && (obj as MVMensaje).mensaje.Equals (this.mensaje);
        }

        public override Int32 GetHashCode ( ) {
            return base.GetHashCode ( );
        }




    }
}
