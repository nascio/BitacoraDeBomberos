using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitacoraDeBomberos.BLL.Exepciones;

namespace BitacoraDeBomberos.BLL.Bases {

    public abstract class DatosBase : Notificador {

        internal enum ErrorMySql {
            SinIndicar = -1,

            SinConexion = 1042,

            ElementoDuplicado = 1062,
        }


        internal static ErrorMySql MySqlErrores (MySqlException e) {
            switch (e.Number) {
                case 1042:
                case 1062:
                    return (ErrorMySql) e.Number;
            }

            return ErrorMySql.SinIndicar;
        }



        public bool StringCompare (String a, String b) {
            return a == null || b == null ? a == b : string.Compare (a.Trim ( ), b.Trim ( )) == 0;
        }



        public static Exception MySqlExceptions (MySqlException e) {
            switch (e.Number) {
                case 1042:
                    return new SinConexionException (e);
            }

            return null;
        }
    }
}