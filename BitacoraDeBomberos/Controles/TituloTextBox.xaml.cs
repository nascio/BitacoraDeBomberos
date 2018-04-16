using BitacoraDeBomberos.WPF;
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
    public partial class TituloTextBox : UserControl {

        #region Constructores

        public TituloTextBox ( ) {
            InitializeComponent ( );
        }

        #endregion

        #region Titulo

        public static readonly DependencyProperty TituloProperty = TituloLabel.TituloProperty.AddOwner (
            typeof (TituloTextBox)
        );

        public string Titulo {
            get => (string) base.GetValue (TituloProperty);
            set => base.SetValue (TituloProperty, value);
        }

        #endregion

        #region Contenido

        public static readonly DependencyProperty ContenidoProperty = TituloLabel.ContenidoProperty.AddOwner (
            typeof (TituloTextBox)
        );

        public string Contenido {
            get => (string) base.GetValue (ContenidoProperty);
            set => base.SetValue (ContenidoProperty, value);
        }

        #endregion

        #region TextAligment

        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register (
            "TextAlignment",
            typeof (TextAlignment),
            typeof (TituloTextBox),
            new FrameworkPropertyMetadata (TextAlignment.Left, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure)
        );

        public TextAlignment TextAlignment {
            get => (TextAlignment) base.GetValue (TextAlignmentProperty);
            set => base.SetValue (TextAlignmentProperty, value);
        }

        #endregion
    }
}