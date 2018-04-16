using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace BitacoraDeBomberos.WPF {

    public class BooleanToVisibilityConverterExtension : MarkupExtension, IValueConverter {

        #region Atributos

        private Visibility falseValue;

        #endregion

        #region Constructores

        public BooleanToVisibilityConverterExtension ( ) {
            this.falseValue = Visibility.Collapsed;
        }

        #endregion

        #region Propiedades

        public Visibility FalseValue {
            get {
                return this.falseValue;
            }
            set {
                if (value == Visibility.Hidden || value == Visibility.Collapsed) {
                    this.falseValue = value;
                }
            }
        }

        public Boolean Negate {
            get;
            set;
        }

        public Object Tag {
            get;set;
        }

        #endregion

        #region Metodos

        public Object Convert (Object value, Type targetType, Object parameter, CultureInfo culture) {
            if (!(value is bool)) {
                return DependencyProperty.UnsetValue;
            }

            var s = (bool) value;
            s = Negate ? !s : s;

            return s ? Visibility.Visible : FalseValue;
        }

        public Object ConvertBack (Object value, Type targetType, Object parameter, CultureInfo culture) {
            throw new NotImplementedException ( );
        }

        public override Object ProvideValue (IServiceProvider serviceProvider) {
            return this;
        }

        #endregion
    }
}