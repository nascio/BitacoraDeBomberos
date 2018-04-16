using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace BitacoraDeBomberos.WPF {
    class errorconverter : MarkupExtension, IMultiValueConverter {

        public override Object ProvideValue (IServiceProvider serviceProvider) {
            return this;
        }

        public Object Convert (Object[] values, Type targetType, Object parameter, CultureInfo culture) {
            if (values.Length != 2) {
                return false;
            }

            var status = values[0];
            var collection = values[1];

            if (!(status is bool) || !(collection == null || collection is ICollection || collection is IEnumerable)) {
                return false;
            }

            if (!((bool) status) || collection == null) {
                return false;
            }

            if (collection is ICollection) {
                var icollection = collection as ICollection;

                return icollection.Count > 0;
            }

            var ienumerable = collection as IEnumerable;
            var ienumerator = null as IEnumerator;

            try {
                ienumerator = ienumerable.GetEnumerator ( );

                if (ienumerator.MoveNext ( )) {
                    return true;
                }
                //return false;
            }
            finally {
                if (ienumerator is IDisposable) {
                    (ienumerator as IDisposable).Dispose ( );
                }
            }



            return false;
        }
        public Object[] ConvertBack (Object value, Type[] targetTypes, Object parameter, CultureInfo culture) {
            throw new NotImplementedException ( );
        }
    }
}
