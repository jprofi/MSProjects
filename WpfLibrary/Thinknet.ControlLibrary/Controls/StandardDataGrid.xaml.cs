namespace Thinknet.ControlLibrary.Controls
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Standard Generic Datagrid.
    /// </summary>
    public partial class StandardDataGrid : UserControl
    {
        public static readonly DependencyProperty VerticalScrollBarVisibilityProperty =
            DependencyProperty.Register("VerticalScrollBarVisibility", typeof(ScrollBarVisibility), typeof(StandardDataGrid), new PropertyMetadata(ScrollBarVisibility.Auto, OnVerticalScrollbarVisibilityChanged));

        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty =
            DependencyProperty.Register("HorizontalScrollBarVisibility", typeof(ScrollBarVisibility), typeof(StandardDataGrid), new PropertyMetadata(ScrollBarVisibility.Auto, OnHorizontalScrollbarVisibilityChanged));
        
        public static readonly DependencyProperty ShowRowNumberProperty =
            DependencyProperty.Register("ShowRowNumber", typeof(bool), typeof(StandardDataGrid), new PropertyMetadata(true));        
        
        public static readonly DependencyProperty CellCenterAlignStyleProperty =
            DependencyProperty.Register("CellCenterAlignStyle", typeof(Style), typeof(StandardDataGrid), new PropertyMetadata(null));    
    
        public static readonly DependencyProperty CellRightAlignStyleProperty =
            DependencyProperty.Register("CellRightAlignStyle", typeof(Style), typeof(StandardDataGrid), new PropertyMetadata(null));


        /// <summary>
        /// Initializes a new instance of the <see cref="StandardDataGrid"/> class.
        /// </summary>
        public StandardDataGrid()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the vertical scrollbar visibility of the datagrid.
        /// </summary>
        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(VerticalScrollBarVisibilityProperty); }
            set { SetValue(VerticalScrollBarVisibilityProperty, value); }
        }

        /// <summary>
        /// Gets or sets the horizontal scrollbar visibility of the datagrid.
        /// </summary>
        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(HorizontalScrollBarVisibilityProperty); }
            set { SetValue(HorizontalScrollBarVisibilityProperty, value); }
        }

        /// <summary>
        /// Gets or sets the style for center alignment of table column.
        /// </summary>
        public Style CellCenterAlignStyle
        {
            get { return (Style)GetValue(CellCenterAlignStyleProperty); }
            set { SetValue(CellCenterAlignStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the style for right alignment of table column.
        /// </summary>
        public Style CellRightAlignStyle
        {
            get { return (Style)GetValue(CellRightAlignStyleProperty); }
            set { SetValue(CellRightAlignStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets whether the row header should contain numbering.
        /// </summary>
        public bool ShowRowNumber
        {
            get { return (bool)GetValue(ShowRowNumberProperty); }
            set { SetValue(ShowRowNumberProperty, value); }
        }

        private static void OnVerticalScrollbarVisibilityChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            StandardDataGrid stdg = dependencyObject as StandardDataGrid;
            if (stdg != null)
            {
                stdg.ThinknetDataGrid.VerticalScrollBarVisibility = (ScrollBarVisibility)dependencyPropertyChangedEventArgs.NewValue;
            }
        }

        private static void OnHorizontalScrollbarVisibilityChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            StandardDataGrid stdg = dependencyObject as StandardDataGrid;
            if (stdg != null)
            {
                stdg.ThinknetDataGrid.HorizontalScrollBarVisibility = (ScrollBarVisibility)dependencyPropertyChangedEventArgs.NewValue;
            }
        }

        private void ThinknetDataGrid_OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            if ((bool)GetValue(ShowRowNumberProperty))
            {
                e.Row.Header = (e.Row.GetIndex() + 1).ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
