using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitacoraDeBomberos.BLL.Bases {
    static class CreacionTuples {

        public static Tuple<Boolean?, T> ToTupleIngreso<T> (this T valor) {
            return Tuple.Create<Boolean?, T> (null, valor);
        }
        public static Tuple<Boolean?, T?> ToTupleIngreso<T> (this T? valor) where T : struct {
            return Tuple.Create<Boolean?, T?> (null, valor);
        }







        public static Tuple<Boolean?, T> ToTupleEdicionC<T> (this T valor, T comparar) where T : class {
            var iguales = object.Equals (valor, comparar);
            return Tuple.Create<Boolean?, T> (!iguales, iguales ? null : valor);
        }
        public static Tuple<Boolean?, T?> ToTupleEdicion<T> (this T valor, T comparar) where T : struct {
            var iguales = Object.Equals (valor, comparar);
            return Tuple.Create<Boolean?, T?> (!iguales, iguales ? null : (T?) valor);
        }

        public static Tuple<Boolean?, T> ToTupleEdicionC<T> (this T valor, T comparar, Func<T, T, Boolean> comparer) where T : class {
            var iguales = comparer (valor, comparar);
            return Tuple.Create<Boolean?, T> (!iguales, iguales ? null : valor);
        }
        public static Tuple<Boolean?, T?> ToTupleEdicion<T> (this T valor, T comparar, Func<T, T, Boolean> comparer) where T : struct {
            var iguales = comparer (valor, comparar);
            return Tuple.Create<Boolean?, T?> (!iguales, iguales ? null : (T?) valor);
        }

        public static Tuple<Boolean?, T?> ToTupleEdicion<T> (this T? valor, T? comparar) where T : struct {
            return Tuple.Create<Boolean?, T?> (null, valor);
        }
        public static Tuple<Boolean?, T?> ToTupleEdicion<T> (this T? valor, T? comparar, Func<T?, T?, Boolean> comparer) where T : struct {
            var iguales = comparer (valor, comparar);
            return Tuple.Create<Boolean?, T?> (null, valor);
        }

    }
}
