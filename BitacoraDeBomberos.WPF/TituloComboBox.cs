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

    public class TituloComboBox : ComboBox {
        static TituloComboBox ( ) {
            DefaultStyleKeyProperty.OverrideMetadata (typeof (TituloComboBox), new FrameworkPropertyMetadata (typeof (TituloComboBox)));
        }

        public TituloComboBox ( ) {
            this.TabOnEnter = true;
        }


        #region Titulo

        public static readonly DependencyProperty TituloProperty = TituloLabel.TituloProperty.AddOwner (
            typeof (TituloComboBox)
        );

        public string Titulo {
            get => (string) base.GetValue (TituloProperty);
            set => base.SetValue (TituloProperty, value);
        }

        #endregion

        #region TabOnEnter

        public bool TabOnEnter {
            get;
            set;
        }



        protected override void OnPreviewKeyDown (KeyEventArgs e) {
            Console.WriteLine ("Key");
            if (e.Key == Key.Enter && TabOnEnter && !base.IsEditable && !base.IsDropDownOpen) {
                e.Handled = true;
                base.MoveFocus (new TraversalRequest (FocusNavigationDirection.Next));
            }
        }



        protected override void OnAccessKey (AccessKeyEventArgs e) {
            
            Console.WriteLine ("Key");
            base.OnAccessKey (e);
        }

        protected override void OnTextInput (TextCompositionEventArgs e) {
            Console.WriteLine ("Key");
            Console.WriteLine (e.ToString ( ));
            base.OnTextInput (e);
        }
        protected override void OnKeyDown (KeyEventArgs e) {
            Console.WriteLine ("Key");
            base.OnKeyDown (e);
        }

        #endregion



    }
}
