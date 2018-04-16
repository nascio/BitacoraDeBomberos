using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BitacoraDeBomberos.BLL.Exepciones {

    public abstract class BitacoraBomberosException : ApplicationException {

        #region Constructores

        protected BitacoraBomberosException (SerializationInfo info, StreamingContext context) : base (info, context) {
        }

        public BitacoraBomberosException ( ) {
        }

        public BitacoraBomberosException (String message) : base (message) {
        }

        public BitacoraBomberosException (String message, Exception innerException) : base (message, innerException) {
        }

        #endregion

        #region Propiedades

        public String Titulo {
            get {
                return "Exepcion de la bitacora de bomberos.";
            }
        }

        #endregion
    }
}