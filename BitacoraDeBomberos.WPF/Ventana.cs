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

namespace BitacoraDeBomberos.WPF {

    public class Ventana : ContentControl {

        #region Constructores

        static Ventana ( ) {
            DefaultStyleKeyProperty.OverrideMetadata (typeof (Ventana), new FrameworkPropertyMetadata (typeof (Ventana)));
        }

        #endregion

        #region CloseCommand

        public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register (
            "CloseCommand",
            typeof (ICommand),
            typeof (Ventana),
            new FrameworkPropertyMetadata (null)

        );

        public ICommand CloseCommand {
            get => (ICommand) base.GetValue (CloseCommandProperty);
            set => base.SetValue (CloseCommandProperty, value);
        }

        #endregion
    }
}