using BitacoraDeBomberos.BLL.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BitacoraDeBomberos {
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window {
        public Window1 ( ) {
            InitializeComponent ( );
        }

        private void MVRolAcceso_AccionesUI (Action accion, TipoAccion tipo) {
            if (App.Current.Dispatcher.Thread == Thread.CurrentThread) {
                accion ( );

                switch (tipo) {
                    case TipoAccion.Nuevo:
                        //this.kw.Visibility = Visibility.Visible;
                        //this.kw.Children.Clear ( );
                        //this.kw.Children.Add (this.FindResource ("mm") as UIElement);
                        break;

                    case TipoAccion.Ejecutar:
                        break;
                }

            }
            else {
                App.Current.Dispatcher.BeginInvoke (System.Windows.Threading.DispatcherPriority.Background,
                    (Action<Action, TipoAccion>) MVRolAcceso_AccionesUI, accion, tipo);
            }
        }
    }
}
