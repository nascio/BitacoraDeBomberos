using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitacoraDeBomberos.BLL.Bases {

    internal class Ayuda {

        public static string ToCamelCase (string value) {
            if (String.IsNullOrEmpty (value)) {
                return value;
            }

            var result = new StringBuilder (value.Length);
            var lastWasBreak = true;
            for (var i = 0; i < value.Length; i++) {
                var c = value[i];

                if (!char.IsLetter (c)) {
                    result.Append (c);
                    lastWasBreak = true;
                    continue;
                }

                if (lastWasBreak) {
                    result.Append (char.ToUpper (c));
                    lastWasBreak = false;
                }
                else {
                    result.Append (char.ToLower (c));
                }
            }

            return result.ToString ( );
        }
    }
}