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

    public class TituloLabel : Control {

        #region Constructores

        static TituloLabel ( ) {
            DefaultStyleKeyProperty.OverrideMetadata (typeof (TituloLabel), new FrameworkPropertyMetadata (typeof (TituloLabel)));
        }

        #endregion

        #region Titulo

        public static readonly DependencyProperty TituloProperty = DependencyProperty.Register (
            "Titulo",
            typeof (String),
            typeof (TituloLabel),
            new FrameworkPropertyMetadata (String.Empty, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure)
        );

        public String Titulo {
            get => (String) base.GetValue (TituloProperty);
            set => base.SetValue (TituloProperty, value);
        }

        #endregion

        #region Contenido

        public static readonly DependencyProperty ContenidoProperty = DependencyProperty.Register (
            "Contenido",
            typeof (String),
            typeof (TituloLabel),
            new FrameworkPropertyMetadata (String.Empty, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure)
        );

        public String Contenido {
            get => (String) base.GetValue (ContenidoProperty);
            set => base.SetValue (ContenidoProperty, value);
        }

        #endregion
    }
}