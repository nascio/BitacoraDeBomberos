using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitacoraDeBomberos.BLL.Bases {

    public sealed class TextoEnum<T> {

        #region Atributos

        private String texto;
        private T valor;

        #endregion

        #region Constructores

        public TextoEnum (T valor, String texto) {
            this.valor = valor;
            this.texto = texto;
        }

        public TextoEnum (T valor) : this (valor, valor.ToString ( )) {
        }

        #endregion

        #region Propiedades

        public String Texto {
            get {
                return this.texto;
            }
        }

        public T Valor {
            get {
                return this.valor;
            }
        }

        #endregion
    }
}