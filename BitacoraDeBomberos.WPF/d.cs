using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace BitacoraDeBomberos.WPF {
    public class d : MarkupExtension, IValueConverter {
        public override Object ProvideValue (IServiceProvider serviceProvider) => this;

        public Object Convert (Object value, Type targetType, Object parameter, CultureInfo culture) {
            //var s = 0;
            return value;
        }
        public Object ConvertBack (Object value, Type targetType, Object parameter, CultureInfo culture) => throw new NotImplementedException ( );
    }
}
