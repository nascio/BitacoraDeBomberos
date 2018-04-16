using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace BitacoraDeBomberos.WPF {

    internal class severidadconverter : MarkupExtension, IValueConverter {

        public Object Convert (Object value, Type targetType, Object parameter, CultureInfo culture) {
            var c = System.Convert.ToInt32 (value);
            switch (c) {
                case 0:
                    return Brushes.Red;

                case 1:
                    return Brushes.Orange;

                case 2:
                    return Brushes.Gold;
            }

            return null;
        }

        public Object ConvertBack (Object value, Type targetType, Object parameter, CultureInfo culture) {
            throw new NotImplementedException ( );
        }

        public override Object ProvideValue (IServiceProvider serviceProvider) {
            return this;
        }
    }
}