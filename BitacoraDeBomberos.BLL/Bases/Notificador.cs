using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BitacoraDeBomberos.BLL.Bases {

    public abstract class Notificador : INotifyPropertyChanged {

        #region Atributos

        private readonly Object @lock = new Object ( );

        private PropertyChangedEventHandler propertyChanged;

        #endregion

        #region Eventos

        public event PropertyChangedEventHandler PropertyChanged {
            add {
                lock (this.@lock) {
                    this.propertyChanged += value;
                }
            }
            remove {
                lock (this.@lock) {
                    this.propertyChanged -= value;
                }
            }
        }

        #endregion

        #region Metodos

        protected void OnPropertyChanged ([CallerMemberName] String propertyName = "") {
            var handler = null as PropertyChangedEventHandler;

            lock (this.@lock) {
                handler = this.propertyChanged;
            }

            handler?.Invoke (this, new PropertyChangedEventArgs (propertyName));
        }

        protected void OnMPropertiesChanged (params String[] propertiesNames) {
            var handler = null as PropertyChangedEventHandler;

            lock (this.@lock) {
                handler = this.propertyChanged;
            }

            if (handler == null) {
                return;
            }

            foreach (var propertyName in propertiesNames) {
                handler (this, new PropertyChangedEventArgs (propertyName));
            }
        }

        public virtual void Liberar ( ) {
            lock (this.@lock) {
                this.propertyChanged = null;
            }
        }


        internal static bool StringIsNullOrEmpty (String value) {
            return value == null || value.Length == 0 || value.Trim ( ).Length == 0;
        }


        #endregion
    }
}