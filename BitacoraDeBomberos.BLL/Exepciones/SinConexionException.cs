using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitacoraDeBomberos.BLL.Exepciones {
    public class SinConexionException : BitacoraBomberosException {

        public SinConexionException ( ) : base ("No se puede conectar al servidor.") { }

        public SinConexionException (Exception innerException) : base ("No se puede conectar al servidor.", innerException) { }
    }
}
