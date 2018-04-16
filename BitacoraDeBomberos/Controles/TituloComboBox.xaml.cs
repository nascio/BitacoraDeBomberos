using BitacoraDeBomberos.WPF;
using System;
using System.Collections;
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
    public partial class TituloComboBox : UserControl {

        #region Constructores

        public TituloComboBox ( ) {
            InitializeComponent ( );
        }

        #endregion

        #region Titulo

        public static readonly DependencyProperty TituloProperty = TituloLabel.TituloProperty.AddOwner (
            typeof (TituloComboBox)
        );

        public string Titulo {
            get => (string) base.GetValue (TituloProperty);
            set => base.SetValue (TituloProperty, value);
        }

        #endregion

        #region ItemContainerStyle

        public static readonly DependencyProperty ItemContainerStyleProperty = ComboBox.ItemContainerStyleProperty.AddOwner (
            typeof (TituloComboBox)
        );

        public Style ItemContainerStyle {
            get => (Style) base.GetValue (ItemContainerStyleProperty);
            set => base.SetValue (ItemContainerStyleProperty, value);
        }

        #endregion

        #region ItemsPanel

        public static readonly DependencyProperty ItemsPanelProperty = ComboBox.ItemsPanelProperty.AddOwner (
            typeof (TituloComboBox)
        );

        public ItemsPanelTemplate ItemsPanel {
            get => (ItemsPanelTemplate) base.GetValue (ItemsPanelProperty);
            set => base.SetValue (ItemsPanelProperty, value);
        }

        #endregion

        #region ItemsSource

        public static readonly DependencyProperty ItemsSourceProperty = ComboBox.ItemsSourceProperty.AddOwner (
            typeof (TituloComboBox)
        );

        public IEnumerable ItemsSource {
            get => (IEnumerable) base.GetValue (ItemsSourceProperty);
            set => base.SetValue (ItemsSourceProperty, value);
        }

        #endregion

        #region ItemTemplate

        public static readonly DependencyProperty ItemTemplateProperty = ComboBox.ItemTemplateProperty.AddOwner (
            typeof (TituloComboBox)
        );

        public DataTemplate ItemTemplate {
            get => (DataTemplate) base.GetValue (ItemTemplateProperty);
            set => base.SetValue (ItemTemplateProperty, value);
        }

        #endregion

        #region SelectedItem

        public static readonly DependencyProperty SelectedItemProperty = ComboBox.SelectedItemProperty.AddOwner (
            typeof (TituloComboBox)
        );

        public Object SelectedItem {
            get => base.GetValue (SelectedItemProperty);
            set => base.SetValue (SelectedItemProperty, value);
        }

        #endregion

        #region SelectedItem

        public static readonly DependencyProperty SelectedValueProperty = ComboBox.SelectedValueProperty.AddOwner (
            typeof (TituloComboBox)
        );

        public Object SelectedValue {
            get => base.GetValue (SelectedValueProperty);
            set => base.SetValue (SelectedValueProperty, value);
        }

        #endregion




        #region SelectedValuePath

        public static readonly DependencyProperty SelectedValuePathProperty = ComboBox.SelectedValuePathProperty.AddOwner (
            typeof (TituloComboBox)
        );

        public String SelectedValuePath {
            get => (String) base.GetValue (SelectedValuePathProperty);
            set => base.SetValue (SelectedValuePathProperty, value);
        }

        #endregion


        #region TabOnEnter

        public bool TabOnEnter {
            get;
            set;
        }

        protected override void OnPreviewKeyDown (KeyEventArgs e) {
            if (e.Key == Key.Enter && TabOnEnter) {
                e.Handled = true;
                base.MoveFocus (new TraversalRequest (FocusNavigationDirection.Next));
            }
        }

        #endregion
    }
}