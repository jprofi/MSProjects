namespace Thinknet.ControlLibrary.Controls
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    using Thinknet.ControlLibrary.Behaviors;

    /// <summary>
    /// Datagrid with column definition from meta data. Supports viewmodel for selection etc...
    /// </summary>
    public class TDataGrid : DataGrid
    {
        public static readonly DependencyProperty ShowRowNumberProperty =
            DependencyProperty.Register("ShowRowNumber", typeof(bool), typeof(TDataGrid), new PropertyMetadata(true));        
        
        public static readonly DependencyProperty CellCenterAlignStyleProperty =
            DependencyProperty.Register("CellCenterAlignStyle", typeof(Style), typeof(TDataGrid), new PropertyMetadata(null));    
    
        public static readonly DependencyProperty CellRightAlignStyleProperty =
            DependencyProperty.Register("CellRightAlignStyle", typeof(Style), typeof(TDataGrid), new PropertyMetadata(null));    

        public static readonly DependencyProperty SelectionChangedCommandProperty =
            DependencyProperty.Register("SelectionChangedCommand", typeof(ICommand), typeof(TDataGrid), new PropertyMetadata(null));
        
        public static readonly DependencyProperty DeleteRowsCommandProperty =
            DependencyProperty.Register("DeleteRowsCommand", typeof(ICommand), typeof(TDataGrid), new PropertyMetadata(null, OnDeleteRowsCommandChanged));

        private KeyBinding _deleteKeyBinding;


        static TDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TDataGrid), new FrameworkPropertyMetadata(typeof(TDataGrid)));
        }

        public TDataGrid()
        {
            Interaction.GetBehaviors(this).Add(new DataGridRowReadOnlyBehavior());

            SelectionChanged += OnSelectionChanged;

            LoadingRow += DataGrid_OnLoadingRow;
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

        /// <summary>
        /// Gets or sets the command executed when selection changed..
        /// </summary>
        public ICommand SelectionChangedCommand
        {
            get { return (ICommand)GetValue(SelectionChangedCommandProperty); }
            set { SetValue(SelectionChangedCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command for deleting rows.
        /// If this command is set viewmodel is responsible for the deletion of selected rows.
        /// Can be used to make database removes with updating data in viewmodel after successful operation.
        /// </summary>
        public ICommand DeleteRowsCommand
        {
            get { return (ICommand)GetValue(SelectionChangedCommandProperty); }
            set { SetValue(SelectionChangedCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the delete key binding.
        /// </summary>
        private KeyBinding DeleteKeyBinding
        {
            get { return _deleteKeyBinding; }
            set { _deleteKeyBinding = value; }
        }

        private void DataGrid_OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            if ((bool)GetValue(ShowRowNumberProperty))
            {
                e.Row.Header = (e.Row.GetIndex() + 1).ToString(CultureInfo.InvariantCulture);
            }
        }

        private static void OnDeleteRowsCommandChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            TDataGrid dataGrid = dependencyObject as TDataGrid;
            ICommand newCommand = dependencyPropertyChangedEventArgs.NewValue as ICommand;
            if (newCommand == null)
            {
                if (dataGrid.DeleteKeyBinding != null)
                {
                    dataGrid.InputBindings.Remove(dataGrid.DeleteKeyBinding);
                    dataGrid.DeleteKeyBinding = null;
                }
            }
            else if (dataGrid.DeleteKeyBinding == null)
            {
                dataGrid.DeleteKeyBinding = new KeyBinding(newCommand, Key.Delete, ModifierKeys.None);
                dataGrid.InputBindings.Add(dataGrid.DeleteKeyBinding);
            }
            else
            {
                dataGrid.DeleteKeyBinding.Command = newCommand;
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (SelectionChangedCommand != null && SelectionChangedCommand.CanExecute(null))
            {
                SelectionChangedCommand.Execute(SelectedItems);
            }
        }
    }
}
