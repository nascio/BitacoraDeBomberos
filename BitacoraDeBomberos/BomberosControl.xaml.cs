using BitacoraDeBomberos.BLL.Bases;
using BitacoraDeBomberos.BLL.Modelos;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BitacoraDeBomberos {
    /// <summary>
    /// Lógica de interacción para BomberosControl.xaml
    /// </summary>
    public partial class BomberosControl : UserControl {
        public BomberosControl ( ) {
            InitializeComponent ( );
        }

        private void MVBombero_AccionesUI (Action accion, TipoAccion tipo) {
            if (App.Current.Dispatcher.Thread == Thread.CurrentThread) {
                Console.WriteLine ("hilo principal UI");
                accion ( );

                switch (tipo) {
                    case TipoAccion.Nuevo:
                        this.kw.Visibility = Visibility.Visible;
                        this.kw.Children.Clear ( );
                        this.kw.Children.Add (this.FindResource ("mm") as UIElement);
                        break;

                    case TipoAccion.Ejecutar:
                        break;
                }

            }
            else {
                Console.WriteLine ("hilo secundario UI");
                App.Current.Dispatcher.BeginInvoke (System.Windows.Threading.DispatcherPriority.Background,
                    (Action<Action, TipoAccion>) MVBombero_AccionesUI, accion, tipo);
            }
        }

        private void Button_Click (Object sender, RoutedEventArgs e) {

        }

        private void kasmdkasmdkasmdakm (Object sender, RoutedEventArgs e) {
            //this.kw.Visibility = this.kw.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            //this.kw.Children.Add (this.FindResource ("nnn") as UIElement);
        }

        private void Button_Click_1 (Object sender, RoutedEventArgs e) {
            //this.kw.Visibility = this.kw.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            //this.kw.Children.Clear ( );
        }

        private void alkdsmaksldmsalkd (Object sender, RoutedEventArgs e) {
            var c = this.DataContext;
        }


        //private void MVBombero_Mensajes (BLL.Bases.Mensaje mensaje) {
        //    if (App.Current.Dispatcher.Thread == Thread.CurrentThread) {
        //        Console.WriteLine ("hilo principal");
        //        this.items.Items.Add (mensaje.Contenido);
        //    }
        //    else {
        //        Console.WriteLine ("hilo secundario");
        //        App.Current.Dispatcher.BeginInvoke (System.Windows.Threading.DispatcherPriority.Background,
        //            (Action) (( ) => {

        //                this.items.Items.Add (mensaje.Contenido);

        //            }));

        //    }
        //}
    }
}
