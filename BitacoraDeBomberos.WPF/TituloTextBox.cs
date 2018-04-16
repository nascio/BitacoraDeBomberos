using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BitacoraDeBomberos.WPF {

    public class TituloTextBox : TextBox {

        #region Constructores

        static TituloTextBox ( ) {
            DefaultStyleKeyProperty.OverrideMetadata (typeof (TituloTextBox), new FrameworkPropertyMetadata (typeof (TituloTextBox)));


            //TextProperty.OverrideMetadata (typeof (TituloTextBox), new FrameworkPropertyMetadata (
            //    null, new CoerceValueCallback (mksmdkmskd)
            //    ));
        }

        //private static Object mksmdkmskd (DependencyObject d, Object baseValue) {
        //    return (d is TituloTextBox) ? (d as TituloTextBox).CoerceText (baseValue as String) : "";
        //}

        //protected String CoerceText (String newS) {
        //    return string.IsNullOrEmpty (newS) ? "" : maskProvider.ToDisplayString ( );
        //}


        public TituloTextBox ( ) {
            this.TabOnEnter = true;
        }

        #endregion

        #region Titulo

        public static readonly DependencyProperty TituloProperty = TituloLabel.TituloProperty.AddOwner (
            typeof (TituloTextBox)
        );

        public String Titulo {
            get {
                return (String) base.GetValue (TituloProperty);
            }
            set {
                base.SetValue (TituloProperty, value);
            }
        }

        #endregion

        #region ErrorBrush

        public static readonly DependencyProperty ErrorBrushProperty = DependencyProperty.Register (
            "ErrorBrush",
            typeof (Brush),
            typeof (TituloTextBox),
            new FrameworkPropertyMetadata (Brushes.Red, FrameworkPropertyMetadataOptions.AffectsRender)
        );

        public Brush ErrorBrush {
            get {
                return (Brush) base.GetValue (ErrorBrushProperty);
            }
            set {
                base.SetValue (ErrorBrushProperty, value);
            }
        }

        public static readonly DependencyProperty ShowErrorProperty = DependencyProperty.Register (
            "ShowError",
            typeof (Boolean),
            typeof (TituloTextBox),
            new FrameworkPropertyMetadata (false, FrameworkPropertyMetadataOptions.AffectsRender)
        );

        public Boolean ShowError {
            get {
                return (Boolean) base.GetValue (ShowErrorProperty);
            }
            set {
                base.SetValue (ShowErrorProperty, value);
            }
        }

        #endregion

        #region MarcaAguaContent

        public static readonly DependencyProperty MarcaAguaContentProperty = DependencyProperty.Register (
            "MarcaAguaContent",
            typeof (Object),
            typeof (TituloTextBox),
            new FrameworkPropertyMetadata (null, FrameworkPropertyMetadataOptions.AffectsRender)
        );

        public Object MarcaAguaContent {
            get {
                return (Object) base.GetValue (MarcaAguaContentProperty);
            }
            set {
                base.SetValue (MarcaAguaContentProperty, value);
            }
        }

        #endregion


        #region MarcaAguaTemplate

        public DataTemplate MarcaAguaTemplate {
            get {
                return (DataTemplate) base.GetValue (MarcaAguaTemplateProperty);
            }
            set {
                base.SetValue (MarcaAguaTemplateProperty, value);
            }
        }

        public static readonly DependencyProperty MarcaAguaTemplateProperty = DependencyProperty.Register (
            "MarcaAguaTemplate",
            typeof (DataTemplate),
            typeof (TituloTextBox),
            new FrameworkPropertyMetadata (null, FrameworkPropertyMetadataOptions.AffectsRender)
        );

        #endregion

        #region MarcaAguaStringFormat


        public String MarcaAguaStringFormat {
            get {
                return (String) base.GetValue (MarcaAguaStringFormatProperty);
            }
            set {
                base.SetValue (MarcaAguaStringFormatProperty, value);
            }
        }

        public static readonly DependencyProperty MarcaAguaStringFormatProperty = DependencyProperty.Register (
            "MarcaAguaStringFormat",
            typeof (String),
            typeof (TituloTextBox),
            new FrameworkPropertyMetadata (null, FrameworkPropertyMetadataOptions.AffectsRender)
        );

        #endregion

        #region MarcaAguaPadding


        public Thickness MarcaAguaPadding {
            get {
                return (Thickness) base.GetValue (MarcaAguaPaddingProperty);
            }
            set {
                base.SetValue (MarcaAguaPaddingProperty, value);
            }
        }

        public static readonly DependencyProperty MarcaAguaPaddingProperty = DependencyProperty.Register (
            "MarcaAguaPadding",
            typeof (Thickness),
            typeof (TituloTextBox),
            new FrameworkPropertyMetadata (new Thickness (0), FrameworkPropertyMetadataOptions.AffectsRender)
        );
        #endregion



        #region TabOnEnter

        public bool TabOnEnter {
            get;
            set;
        }

        protected override void OnTextChanged (TextChangedEventArgs e) {
            base.OnTextChanged (e);
            if (manejar || !this.UsarFormato) {
                return;
            }

            var text = base.Text;
            var m = this.maskProvider.ToDisplayString ( );
            this.maskProvider.Set (text);
            if (this.maskProvider.ToDisplayString ( ).Equals (text)) {
                return;
            }

            this.contenido = this.maskProvider.ToDisplayString ( );

        }

        bool manejar;
        //private void settext (string text) {
        //    manejar = true;
        //    base.Text = text;
        //    manejar = false;
        //}

        private string contenido {
            set {
                manejar = true;
                base.Text = value;
                manejar = false;
            }
        }



        protected override void OnPreviewKeyDown (KeyEventArgs e) {
            var modifiers = e.KeyboardDevice.Modifiers;
            var caretIndex = base.CaretIndex;
            var provider = this.maskProvider;

            switch (e.Key) {
                case Key.Enter:
                    if (!this.TabOnEnter) {
                        break;
                    }

                    if (!base.AcceptsReturn || modifiers == ModifierKeys.Control) {
                        e.Handled = true;
                        base.MoveFocus (new TraversalRequest (FocusNavigationDirection.Next));
                    }

                    break;

                case Key.X:
                case Key.V:
                    if (modifiers != ModifierKeys.Control || !this.UsarFormato) {
                        goto default;
                    }

                    var selectedText = base.SelectedText;

                    switch (e.Key) {
                        case Key.X:
                            Clipboard.SetText (selectedText);
                            var newCaretIndex1 = caretIndex;
                            for (var x = 0; x < base.SelectionLength; x++) {
                                provider.Replace (' ', caretIndex + x);
                            }

                            this.contenido = provider.ToDisplayString ( );
                            base.CaretIndex = caretIndex;

                            break;
                        case Key.V:
                            if (!Clipboard.ContainsText (TextDataFormat.Text)) {
                                return;
                            }
                            selectedText = Clipboard.GetText (TextDataFormat.Text);
                            var backup = provider.ToString ( );
                            if (!provider.Replace (selectedText, caretIndex)) {
                                provider.Set (backup);
                                this.contenido = provider.ToDisplayString ( );
                                base.CaretIndex = caretIndex;
                            }
                            else {
                                this.contenido = provider.ToDisplayString ( );
                                base.CaretIndex = caretIndex + selectedText.Length;
                            }

                            break;
                    }

                    e.Handled = true;

                    break;

                case Key.Left:
                case Key.Right:
                case Key.Delete:
                case Key.Back:
                    if (modifiers != ModifierKeys.None || !this.UsarFormato) {
                        break;
                    }

                    var newCaretIndex = caretIndex;
                    var selectionLength = base.SelectionLength;
                    var repeticion = Math.Max (1, selectionLength);

                    switch (e.Key) {
                        case Key.Left:
                            newCaretIndex = Math.Max (0, newCaretIndex - 2);
                            if (newCaretIndex >= 0 && provider.FindNonEditPositionFrom (newCaretIndex, false) == newCaretIndex) {
                                newCaretIndex = provider.FindEditPositionFrom (newCaretIndex, false);

                                if (newCaretIndex >= 0 || (newCaretIndex = provider.FindEditPositionFrom (newCaretIndex, true)) >= 0) {
                                    base.CaretIndex = newCaretIndex;
                                }

                                e.Handled = true;
                            }
                            break;
                        case Key.Right:
                            newCaretIndex++;
                            if (provider.FindNonEditPositionFrom (newCaretIndex, true) == newCaretIndex) {
                                newCaretIndex = provider.FindEditPositionFrom (newCaretIndex, true);

                                if (newCaretIndex >= 0) {
                                    base.CaretIndex = newCaretIndex;
                                }

                                e.Handled = true;
                            }

                            break;
                        case Key.Delete:
                            for (var x = 0; x < repeticion; x++) {
                                this.replaceChar (' ', caretIndex + x);
                            }

                            e.Handled = true;

                            break;
                        case Key.Back:
                            newCaretIndex -= (selectionLength == 0 ? 1 : 0);
                            if (newCaretIndex >= 0) {
                                if (provider.FindNonEditPositionFrom (newCaretIndex, false) == newCaretIndex &&
                                    (newCaretIndex = provider.FindEditPositionFrom (newCaretIndex, false)) == -1) {
                                    newCaretIndex = provider.FindEditPositionFrom (newCaretIndex, true);
                                }

                                if (newCaretIndex >= 0) {
                                    for (var x = 0; x < repeticion; x++) {
                                        provider.Replace (' ', newCaretIndex + x);
                                    }

                                    this.contenido = provider.ToDisplayString ( );
                                    base.CaretIndex = newCaretIndex;
                                }
                                e.Handled = true;
                            }
                            break;
                    }

                    break;

                default:
                    base.OnPreviewKeyDown (e);
                    break;
            }





        }



        #endregion


        #region Propiedad LostKeyboardFocusCommand

        public static readonly DependencyProperty LostKeyboardFocusCommandProperty = DependencyProperty.Register (
            "LostKeyboardFocusCommand",
            typeof (ICommand),
            typeof (TituloTextBox),
            new FrameworkPropertyMetadata (null)
        );

        public ICommand LostKeyboardFocusCommand {
            get {
                return (ICommand) base.GetValue (LostKeyboardFocusCommandProperty);
            }
            set {
                base.SetValue (LostKeyboardFocusCommandProperty, value);
            }
        }

        //protected override void OnPreviewLostKeyboardFocus (KeyboardFocusChangedEventArgs e) {
        //    try {
        //        //if (!BindingOperations.IsDataBound (this, TextProperty)) {
        //        //    return;
        //        //}

        //        //var binExp = BindingOperations.GetBindingExpression (this, TextProperty);
        //        //var data = binExp.DataItem as IBasicValidation;

        //        //if (data == null) {
        //        //    return;
        //        //}


        //        //data.Validar (binExp.ResolvedSourcePropertyName, out var s, out var er);

        //        ExecuteLostKeyboardFocusCommand (null);

        //        //this.ShowError = er;
        //        //this.ErrorBrush = s == null ? null : new BrushConverter ( ).ConvertFromString (s) as Brush;

        //    }
        //    catch { }
        //    finally {
        //        base.OnPreviewLostKeyboardFocus (e);
        //    }


        //}

        protected virtual void ExecuteLostKeyboardFocusCommand (Object parameter) {
            var cmd = LostKeyboardFocusCommand;
            if (cmd == null) {
                return;
            }
            if (cmd.CanExecute (parameter)) {
                cmd.Execute (parameter);
            }
        }


        #endregion





        MaskedTextProvider maskProvider;

        public string Formato {
            get {
                return this.maskProvider == null ? "" : this.maskProvider.Mask;
            }
            set {
                if (string.IsNullOrEmpty (value)) {
                    this.maskProvider = null;
                }
                else {
                    this.maskProvider = new MaskedTextProvider (value);
                }
            }
        }

        public bool UsarFormato {
            get {
                return this.maskProvider != null;
            }
        }


        //private bool textIsOk = false;
        //public bool NewTextIsOk {
        //    get {
        //        return textIsOk;
        //    }
        //    set {
        //        textIsOk = value;
        //    }
        //}


        protected override void OnGotFocus (RoutedEventArgs e) {
            if (this.UsarFormato) {
                if (this.Text == "") {
                    this.contenido = this.maskProvider.ToDisplayString ( );
                }
                var c = this.maskProvider.FindUnassignedEditPositionFrom (0, true);
                this.CaretIndex = Math.Max (c, 0);
            }
            else {
                this.SelectAll ( );
            }

            base.OnGotFocus (e);
        }


        protected override void OnLostFocus (RoutedEventArgs e) {
            if (this.UsarFormato && this.maskProvider.AssignedEditPositionCount == 0) {
                this.contenido = "";
            }


            try {
                var name = null as string;

                if (BindingOperations.IsDataBound (this, TextProperty)) {
                    var expr = BindingOperations.GetBindingExpression (this, TextProperty);
                    name = expr.ResolvedSourcePropertyName;
                }


                //if (!BindingOperations.IsDataBound (this, TextProperty)) {
                //    return;
                //}

                //var binExp = BindingOperations.GetBindingExpression (this, TextProperty);
                //var data = binExp.DataItem as IBasicValidation;

                //if (data == null) {
                //    return;
                //}


                //data.Validar (binExp.ResolvedSourcePropertyName, out var s, out var er);

                ExecuteLostKeyboardFocusCommand (name);

                //this.ShowError = er;
                //this.ErrorBrush = s == null ? null : new BrushConverter ( ).ConvertFromString (s) as Brush;

            }
            catch { }
            finally {
                base.OnLostFocus (e);
            }

        }


        private void replaceChar (char c, int caretIndex) {
            var provider = this.maskProvider;

            provider.Replace (c, caretIndex);
            this.contenido = provider.ToDisplayString ( );

            var newCaretIndex = caretIndex + 1;
            if (provider.FindNonEditPositionFrom (newCaretIndex, true) == newCaretIndex) {
                newCaretIndex = provider.FindEditPositionFrom (newCaretIndex, true);
            }

            if (newCaretIndex >= 0) {
                this.CaretIndex = newCaretIndex;
            }
        }

        private int getNewValidCharIndex (int caretIndex) {
            var provider = this.maskProvider;

            if (provider.FindNonEditPositionFrom (caretIndex, true) == caretIndex) {
                var newCaretIndex = provider.FindEditPositionFrom (caretIndex, true);

                if (newCaretIndex >= 0) {
                    return newCaretIndex;
                }
            }

            return -1;
        }



        protected override void OnPreviewTextInput (TextCompositionEventArgs e) {
            try {
                if (String.IsNullOrEmpty (e.Text) || !this.UsarFormato) {
                    return;
                }

                var hint = MaskedTextResultHint.NoEffect;
                var provider = this.maskProvider;

                var valid = provider.VerifyChar (e.Text[0], base.CaretIndex, out hint);

                if (!valid) {
                    var newCaretIndex = this.getNewValidCharIndex (base.CaretIndex);
                    if (newCaretIndex >= 0) {
                        base.CaretIndex = newCaretIndex;
                        valid = provider.VerifyChar (e.Text[0], newCaretIndex, out hint);
                    }
                }

                if (valid) {
                    replaceChar (e.Text[0], base.CaretIndex);
                }
            }
            finally {
                base.OnPreviewTextInput (e);

                e.Handled = this.UsarFormato;
            }
        }



        //    protected override Boolean ShouldSerializeProperty (DependencyProperty dp) => base.ShouldSerializeProperty (dp);
        //    protected override void OnVisualChildrenChanged (DependencyObject visualAdded, DependencyObject visualRemoved) => base.OnVisualChildrenChanged (visualAdded, visualRemoved);
        //    protected override void OnDpiChanged (DpiScale oldDpi, DpiScale newDpi) => base.OnDpiChanged (oldDpi, newDpi);

        //    protected override void OnPreviewKeyUp (KeyEventArgs e) => base.OnPreviewKeyUp (e);
        //    protected override void OnPreviewQueryContinueDrag (QueryContinueDragEventArgs e) => base.OnPreviewQueryContinueDrag (e);
        //    protected override void OnPreviewGiveFeedback (GiveFeedbackEventArgs e) => base.OnPreviewGiveFeedback (e);
        //    protected override void OnPreviewDragEnter (DragEventArgs e) => base.OnPreviewDragEnter (e);
        //    protected override void OnPreviewDragOver (DragEventArgs e) => base.OnPreviewDragOver (e);
        //    protected override void OnPreviewDragLeave (DragEventArgs e) => base.OnPreviewDragLeave (e);
        //    protected override void OnPreviewDrop (DragEventArgs e) => base.OnPreviewDrop (e);
        //    protected override void OnPreviewTouchDown (TouchEventArgs e) => base.OnPreviewTouchDown (e);
        //    protected override void OnTouchDown (TouchEventArgs e) => base.OnTouchDown (e);
        //    protected override void OnPreviewTouchMove (TouchEventArgs e) => base.OnPreviewTouchMove (e);
        //    protected override void OnTouchMove (TouchEventArgs e) => base.OnTouchMove (e);
        //    protected override void OnPreviewTouchUp (TouchEventArgs e) => base.OnPreviewTouchUp (e);
        //    protected override void OnTouchUp (TouchEventArgs e) => base.OnTouchUp (e);
        //    protected override void OnGotTouchCapture (TouchEventArgs e) => base.OnGotTouchCapture (e);
        //    protected override void OnLostTouchCapture (TouchEventArgs e) => base.OnLostTouchCapture (e);
        //    protected override void OnTouchEnter (TouchEventArgs e) => base.OnTouchEnter (e);
        //    protected override void OnTouchLeave (TouchEventArgs e) => base.OnTouchLeave (e);
        //    protected override void OnChildDesiredSizeChanged (UIElement child) => base.OnChildDesiredSizeChanged (child);
        //    protected override void OnRender (DrawingContext drawingContext) => base.OnRender (drawingContext);
        //    protected override void OnAccessKey (AccessKeyEventArgs e) => base.OnAccessKey (e);
        //    protected override HitTestResult HitTestCore (PointHitTestParameters hitTestParameters) => base.HitTestCore (hitTestParameters);
        //    protected override GeometryHitTestResult HitTestCore (GeometryHitTestParameters hitTestParameters) => base.HitTestCore (hitTestParameters);
        //    protected override void OnManipulationStarting (ManipulationStartingEventArgs e) => base.OnManipulationStarting (e);
        //    protected override void OnManipulationStarted (ManipulationStartedEventArgs e) => base.OnManipulationStarted (e);
        //    protected override void OnManipulationDelta (ManipulationDeltaEventArgs e) => base.OnManipulationDelta (e);
        //    protected override void OnManipulationInertiaStarting (ManipulationInertiaStartingEventArgs e) => base.OnManipulationInertiaStarting (e);
        //    protected override void OnManipulationBoundaryFeedback (ManipulationBoundaryFeedbackEventArgs e) => base.OnManipulationBoundaryFeedback (e);
        //    protected override void OnManipulationCompleted (ManipulationCompletedEventArgs e) => base.OnManipulationCompleted (e);
        //    protected override void ParentLayoutInvalidated (UIElement child) => base.ParentLayoutInvalidated (child);
        //    protected override Visual GetVisualChild (Int32 index) => base.GetVisualChild (index);
        //    protected override void OnRenderSizeChanged (SizeChangedInfo sizeInfo) => base.OnRenderSizeChanged (sizeInfo);
        //    protected override Geometry GetLayoutClip (Size layoutSlotSize) => base.GetLayoutClip (layoutSlotSize);
        //    public override void BeginInit ( ) => base.BeginInit ( );
        //    public override void EndInit ( ) => base.EndInit ( );
        //    protected override void OnInitialized (EventArgs e) => base.OnInitialized (e);
        //    protected override void OnContextMenuClosing (ContextMenuEventArgs e) => base.OnContextMenuClosing (e);
        //    protected override Size ArrangeOverride (Size arrangeBounds) => base.ArrangeOverride (arrangeBounds);
        //    public override void OnApplyTemplate ( ) => base.OnApplyTemplate ( );
        //    protected override void OnSelectionChanged (RoutedEventArgs e) => base.OnSelectionChanged (e);

        //    protected override void OnKeyDown (KeyEventArgs e) => base.OnKeyDown (e);
        //    protected override void OnKeyUp (KeyEventArgs e) => base.OnKeyUp (e);
        //    protected override void OnTextInput (TextCompositionEventArgs e) => base.OnTextInput (e);

        //    protected override void OnQueryCursor (QueryCursorEventArgs e) => base.OnQueryCursor (e);
        //    protected override void OnQueryContinueDrag (QueryContinueDragEventArgs e) => base.OnQueryContinueDrag (e);
        //    protected override void OnGiveFeedback (GiveFeedbackEventArgs e) => base.OnGiveFeedback (e);
        //    protected override void OnDragEnter (DragEventArgs e) => base.OnDragEnter (e);
        //    protected override void OnDragOver (DragEventArgs e) => base.OnDragOver (e);
        //    protected override void OnDragLeave (DragEventArgs e) => base.OnDragLeave (e);
        //    protected override void OnDrop (DragEventArgs e) => base.OnDrop (e);
        //    protected override void OnContextMenuOpening (ContextMenuEventArgs e) => base.OnContextMenuOpening (e);

        //    protected override AutomationPeer OnCreateAutomationPeer ( ) => base.OnCreateAutomationPeer ( );
        //    protected override void OnPropertyChanged (DependencyPropertyChangedEventArgs e) => base.OnPropertyChanged (e);
        //    protected override Size MeasureOverride (Size constraint) => base.MeasureOverride (constraint);
        //
    }
}