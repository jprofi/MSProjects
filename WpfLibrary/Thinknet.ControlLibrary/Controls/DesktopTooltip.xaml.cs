namespace Thinknet.ControlLibrary.Controls
{
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for MyPopUp.xaml
    /// Popup Implementation for use as klickable tooltip.
    /// Showing and hiding of the tooltip is implemented in the specific style./>
    /// </summary>
    public partial class DesktopToolTip : Popup
    {
        public static readonly DependencyProperty CustomToolTipProperty =
            DependencyProperty.Register("XHtmlToolTip", typeof(string), typeof(DesktopToolTip), new PropertyMetadata(string.Empty, OnXHtmlContentChanged));        
        
        public static readonly DependencyProperty CustomToolTipHyperlinkCommad =
            DependencyProperty.Register("HyperLinkCommand", typeof(ICommand), typeof(DesktopToolTip), new PropertyMetadata(null, OnHyperlinkCommandChanged));

        /// <summary>
        /// Initializes a new instance of the <see cref="DesktopToolTip"/> class.
        /// Loading of styles done in Konstruktor.
        /// </summary>
        public DesktopToolTip()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the command to be processed for Hyperlink Commands.
        /// </summary>
        public ICommand HyperLinkCommand
        {
            get { return (ICommand)GetValue(CustomToolTipHyperlinkCommad); }
            set { SetValue(CustomToolTipHyperlinkCommad, value); }
        }

        /// <summary>
        /// Gets or sets the XHtml for showing in the BindingRichTextBox of the Popup.
        /// </summary>
        public string XHtmlToolTip
        {
            get { return (string)GetValue(CustomToolTipProperty); }
            set { SetValue(CustomToolTipProperty, value); }
        }

        /// <summary>
        /// Event Handler for changes of <see cref="XHtmlToolTip"/> property.
        /// Delegates the changed Information to the containing RichtextBox.
        /// </summary>
        /// <param name="obj">The sender of the object usually this instance.</param>
        /// <param name="e">The parameter containing the changed value.</param>
        private static void OnXHtmlContentChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            DesktopToolTip desktopToolTip = (DesktopToolTip)obj;
            desktopToolTip.RichtextBoxForTooltip.Text = e.NewValue.ToString();
        }

        /// <summary>
        /// Event Handler for changes of <see cref="HyperLinkCommand"/> property.
        /// Delegates the changed information to the containing RichtextBox.
        /// </summary>
        /// <param name="obj">The sender of the object usually this instance.</param>
        /// <param name="e">The parameter containing the changed value.</param>
        private static void OnHyperlinkCommandChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            DesktopToolTip desktopToolTip = (DesktopToolTip)obj;
            desktopToolTip.RichtextBoxForTooltip.HyperLinkCommand = e.NewValue as ICommand;
        }
    }
}
