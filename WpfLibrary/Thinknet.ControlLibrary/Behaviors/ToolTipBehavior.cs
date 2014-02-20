namespace Thinknet.ControlLibrary.Behaviors
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    using Thinknet.ControlLibrary.Controls;

    /// <summary>
    /// Behavior for showing a custom tooltip on any <see cref="FrameworkElement"/>.
    /// The tooltip contains formatted text as XHtml and it's content is clickable.
    /// Popup is closing when focus on popup or control is lost.
    /// </summary>
    public class ToolTipBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty XHtmlToolTipProperty =
            DependencyProperty.Register("XHtmlToolTip", typeof(string), typeof(ToolTipBehavior), new PropertyMetadata(string.Empty, OnCustomToolTipChanged));       
        
        public static readonly DependencyProperty CustomToolTipWidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(ToolTipBehavior), new PropertyMetadata(double.NaN, OnCustomToolTipWidthChanged));

        public static readonly DependencyProperty CustomToolTipHyperlinkCommad =
            DependencyProperty.Register("HyperLinkCommand", typeof(ICommand), typeof(ToolTipBehavior), new PropertyMetadata(null, OnHyperlinkCommandChanged));
        
        public static readonly DependencyProperty ToolTipPlacementProperty =
            DependencyProperty.Register("Placement", typeof(PlacementMode), typeof(ToolTipBehavior), new FrameworkPropertyMetadata(PlacementMode.Mouse, OnPlacementChanged));

        private readonly DesktopToolTip _popup = new DesktopToolTip();

        /// <summary>
        /// Gets or sets the content of the tooltip as XHtml.
        /// </summary>
        public string XHtmlToolTip
        {
            get { return (string)GetValue(XHtmlToolTipProperty); }
            set { SetValue(XHtmlToolTipProperty, value); }
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
        /// Gets or sets the placement of the tooltip.
        /// </summary>
        public PlacementMode Placement
        {
            get { return (PlacementMode)GetValue(ToolTipPlacementProperty); }
            set { SetValue(ToolTipPlacementProperty, value); }
        }

        /// <summary>
        /// Gets or sets the width of the tooltip.
        /// </summary>
        public double Width
        {
            get { return (double)GetValue(CustomToolTipWidthProperty); }
            set { SetValue(CustomToolTipWidthProperty, value); }
        }

        /// <summary>
        /// Event Handler for changes of the <see cref="XHtmlToolTip"/> property.
        /// Delegates the changed value to the Popup control.
        /// </summary>
        /// <param name="d">Usually this behavior.</param>
        /// <param name="e">The event with the changed parameter value.</param>
        private static void OnCustomToolTipChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ToolTipBehavior behavior = d as ToolTipBehavior;
            if (behavior != null)
            {
                behavior._popup.XHtmlToolTip = e.NewValue.ToString();
            }
        }       
        
        private static void OnCustomToolTipWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ToolTipBehavior behavior = d as ToolTipBehavior;
            if (behavior != null)
            {
                behavior._popup.Width = Convert.ToDouble(e.NewValue);
            }
        }

        /// <summary>
        /// Event Handler for changes of the <see cref="HyperLinkCommand"/> property.
        /// </summary>
        /// <param name="d">Usually this behavior</param>
        /// <param name="e">The event with the changed parameter value.</param>
        private static void OnHyperlinkCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ToolTipBehavior behavior = d as ToolTipBehavior;
            if (behavior != null)
            {
                behavior._popup.HyperLinkCommand = e.NewValue as ICommand;
            }
        }

        /// <summary>
        /// Called when tooltip placement has changed.
        /// </summary>
        /// <param name="d">Usually this behavior.</param>
        /// <param name="e">The event with the new Placement.</param>
        private static void OnPlacementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ToolTipBehavior behavior = d as ToolTipBehavior;
            if (behavior != null)
            {
                behavior._popup.Placement = (PlacementMode)e.NewValue;
            }
        }

        /// <inheritdoc />
        /// Sets the Popup placement with the instance of the to this behavior attached FrameworkElement.
        protected override void OnAttached()
        {
            base.OnAttached();
            _popup.PlacementTarget = AssociatedObject;
        }
    }
}
