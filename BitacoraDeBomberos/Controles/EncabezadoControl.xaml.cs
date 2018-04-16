using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BitacoraDeBomberos.Controles {

    /// <summary>
    ///     Lógica de interacción para EncabezadoControl.xaml 
    /// </summary>
    public partial class EncabezadoControl : UserControl {

        #region Constructores

        public EncabezadoControl ( ) {
            InitializeComponent ( );
        }

        #endregion

        #region SubTitulo

        public static readonly DependencyProperty SubTituloProperty = DependencyProperty.Register ("SubTitulo",
            typeof (string),
            typeof (EncabezadoControl),
            new FrameworkPropertyMetadata (String.Empty, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure)
        );

        public string SubTitulo {
            get {
                return (string) GetValue (SubTituloProperty);
            }
            set {
                SetValue (SubTituloProperty, value);
            }
        }

        #endregion
    }
}