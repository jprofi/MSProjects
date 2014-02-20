namespace Thinknet.ControlLibrary.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Navigation;
    using System.Windows.Threading;

    using Thinknet.ControlLibrary.Utilities;
    using Thinknet.MVVM.Command;

    /// <summary>
    /// Enhanced WPF RichtextBox with binding for Text property and binding to hyperlink command.
    /// </summary>
    public class BindingRichTextBox : RichTextBox
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(BindingRichTextBox),
            new FrameworkPropertyMetadata(
                string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnTextPropertyChanged,
                CoerceTextProperty,
                true,
                UpdateSourceTrigger.LostFocus));

        public static readonly DependencyProperty HyperlinkCommandProperty =
            DependencyProperty.Register(
                "HyperLinkCommand",
                typeof(ICommand),
                typeof(BindingRichTextBox)
            );

        private bool _isInvokePending;
        private ITextFormatter _textFormatter;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingRichTextBox"/> class.
        /// </summary>
        public BindingRichTextBox()
        {
            _textFormatter = new RtfFormatter();
            Loaded += RichTextBox_Loaded;
            AddHandler(Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(RequestNavigateHandler));
            //AddHandler(Button.ClickEvent, xxx);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingRichTextBox"/> class.
        /// </summary>
        /// <param name="document">The flow document.</param>
        public BindingRichTextBox(FlowDocument document) : base(document)
        {
        }

        /// <summary>
        /// The ITextFormatter the is used to format the text of the RichTextBox.
        /// Deafult formatter is the RtfFormatter
        /// </summary>
        public ITextFormatter TextFormatter
        {
            get { return _textFormatter; }

            set
            {
                _textFormatter = value;
            }
        }

        /// <summary>
        /// Gets or sets the text of the RichtextBox control.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command for processing a hyperlink of the RichtextBox control.
        /// </summary>
        public ICommand HyperLinkCommand
        {
            get { return (ICommand)GetValue(HyperlinkCommandProperty); }
            set { SetValue(HyperlinkCommandProperty, value); }
        }

        /// <summary>
        /// Called when the text has changed.
        /// </summary>
        /// <param name="d">The dependency object on which the text has been changed.</param>
        /// <param name="e">The event with the changed text.</param>
        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BindingRichTextBox rtb = (BindingRichTextBox)d;
            rtb.TextFormatter.SetText(rtb.Document, (string)e.NewValue);
        }

        /// <summary>
        /// Ensures that the value object is not null.
        /// </summary>
        /// <param name="d">The dependency object.</param>
        /// <param name="value">The value to ensure not null.</param>
        /// <returns>Returns the value or string empty, if the value is null.</returns>
        private static object CoerceTextProperty(DependencyObject d, object value)
        {
            return value ?? string.Empty;
        }

        /// <summary>
        ///  Updates the text in dipatcher with low priority.
        /// </summary>
        private void InvokeUpdateText()
        {
            if (!_isInvokePending)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(UpdateText));
                _isInvokePending = true;
            }
        }

        /// <summary>
        /// Updates the text in the formatter.
        /// </summary>
        private void UpdateText()
        {
            Text = TextFormatter.GetText(Document);
            _isInvokePending = false;
        }

        /// <summary>
        /// Called when the RichTextBox is ready for interaction.
        /// Registers the bindings on the text property.
        /// </summary>
        /// <param name="sender">The sender, actually not used.</param>
        /// <param name="e">The event args, actually not used.</param>
        private void RichTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            Binding binding = BindingOperations.GetBinding(this, TextProperty);

            if (binding != null)
            {
                if (binding.UpdateSourceTrigger == UpdateSourceTrigger.Default || binding.UpdateSourceTrigger == UpdateSourceTrigger.LostFocus)
                {
                    LostFocus += (o, ea) => UpdateText(); //do this synchronously
                }
                else
                {
                    TextChanged += (o, ea) => InvokeUpdateText(); //do this async
                }
            }
        }

        /// <summary>
        /// Default Action when Navigation Link is clicked.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The navigation event.</param>
        private void RequestNavigateHandler(object sender, RequestNavigateEventArgs e)
        {
            if (HyperLinkCommand != null && HyperLinkCommand.CanExecute(e.Uri))
            {
                HyperLinkCommand.Execute(e.Uri);
            }
        }
    }
}
