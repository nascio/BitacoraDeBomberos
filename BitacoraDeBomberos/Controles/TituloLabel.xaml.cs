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
    ///     Lógica de interacción para DescripcionControl.xaml 
    /// </summary>
    public partial class TituloLabel : UserControl {

        #region Constructores

        public TituloLabel ( ) {
            InitializeComponent ( );
        }

        #endregion

        #region Titulo

        public static readonly DependencyProperty TituloProperty = DependencyProperty.Register (
            "Titulo",
            typeof (string),
            typeof (TituloLabel),
            new FrameworkPropertyMetadata (String.Empty, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure)
        );

        public string Titulo {
            get => (string) base.GetValue (TituloProperty);
            set => base.SetValue (TituloProperty, value);
        }

        #endregion

        #region Contenido

        public static readonly DependencyProperty ContenidoProperty = DependencyProperty.Register (
            "Contenido",
            typeof (string),
            typeof (TituloLabel),
            new FrameworkPropertyMetadata (String.Empty, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure)
        );

        public string Contenido {
            get => (string) base.GetValue (ContenidoProperty);
            set => base.SetValue (ContenidoProperty, value);
        }

        #endregion
    }
}