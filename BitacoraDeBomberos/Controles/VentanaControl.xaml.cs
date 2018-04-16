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
    ///     Lógica de interacción para VentanaControl.xaml 
    /// </summary>
    public partial class VentanaControl : UserControl {

        #region Constructores

        public VentanaControl ( ) {
            InitializeComponent ( );
        }

        #endregion

        #region CloseCommand

        public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register (
            "CloseCommand",
            typeof (ICommand),
            typeof (VentanaControl),
            new FrameworkPropertyMetadata (null)

        );

        public ICommand CloseCommand {
            get => (ICommand) base.GetValue (CloseCommandProperty);
            set => base.SetValue (CloseCommandProperty, value);
        }

        #endregion
    }
}