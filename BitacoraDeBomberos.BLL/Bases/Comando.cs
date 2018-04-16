using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BitacoraDeBomberos.BLL.Bases {

    internal class Comando : ICommand {

        #region Atributos

        private Action action;

        #endregion

        #region Constructores

        public Comando (Action action) {
            this.action = action;
        }

        #endregion

        #region Delegados + Eventos

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Metodos

        public Boolean CanExecute (Object parameter) {
            return true;
        }

        public void Execute (Object parameter) {
            this.action ( );
        }

        public void OnCanExecuteChanged (Object sender) {
            this.CanExecuteChanged?.Invoke (sender, EventArgs.Empty);
        }

        #endregion
    }

    internal class Comando<T> : ICommand {

        #region Atributos

        private Action<T> action;
        private Func<T, bool> can;

        #endregion

        #region Constructores

        public Comando (Action<T> action, Func<T, bool> can) {
            this.action = action;
            this.can = can;
        }

        #endregion

        #region Delegados + Eventos

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Metodos

        public Boolean CanExecute (Object parameter) {
            return this.can ((T) parameter);
        }

        public void Execute (Object parameter) {
            this.action ((T) parameter);
        }

        public void OnCanExecuteChanged (Object sender) {
            this.CanExecuteChanged?.Invoke (sender, EventArgs.Empty);
        }

        #endregion
    }
}