namespace Thinknet.ControlLibrary.Behaviors
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    using Thinknet.ControlLibrary.Controls;
    using Thinknet.ControlLibrary.Utilities;
    using Thinknet.MVVM.ViewModel;

    /// <summary>
    /// Attached behavior, for generating datagrid columns from description and the handling of column changes.
    /// In future also persistency of Column settings can maybe implemented here, or delegate it.
    /// Remark: Maybe refactor to Blend compatible behavior. 
    /// </summary>
    public class DataGridColumnsBehavior
    {
        public static readonly DependencyProperty BindableColumnsProperty =
            DependencyProperty.RegisterAttached(
                "BindableColumns",
                typeof(ObservableCollection<DataGridColumnDescriptor>),
                typeof(DataGridColumnsBehavior),
                new UIPropertyMetadata(null, BindableColumnsPropertyChanged)
            );

        /// <summary>Gets the binding to the data grid columns.</summary>
        /// <param name="element">The dependency object.</param>
        /// <returns>The <see cref="ObservableCollection{T}"/> of data grid columns.</returns>
        public static ObservableCollection<DataGridColumn> GetBindableColumns(DependencyObject element)
        {
            return (ObservableCollection<DataGridColumn>)element.GetValue(BindableColumnsProperty);
        }

        /// <summary>Sets the binding to the data grid columns.</summary>
        /// <param name="element">The dependency object.</param>
        /// <param name="value">The <see cref="ObservableCollection{T}"/> of data grid columns.</param>
        public static void SetBindableColumns(DependencyObject element, ObservableCollection<DataGridColumn> value)
        {
            element.SetValue(BindableColumnsProperty, value);
        }
        
        /// <summary>
        /// Handler is called when dependency object has changed.
        /// </summary>
        /// <param name="source">The dependency object.</param>
        /// <param name="e">The change event</param>
        private static void BindableColumnsPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            DataGrid dataGrid = source as DataGrid;
            Contract.Assert(dataGrid != null); 

            dataGrid.Columns.Clear();

            /* 
             * Further development for tracking column changes in view model and view can done here
             * E.g. tracking the observable collection for changes. 
             */

            ObservableCollection<DataGridColumnDescriptor> columnDescriptions = e.NewValue as ObservableCollection<DataGridColumnDescriptor>;
            if (columnDescriptions == null)
            {
                return;
            }

            // Generate DataGridColumns from model.
            foreach (DataGridColumnDescriptor columnDescription in columnDescriptions)
            {
                DataGridColumn dataGridColumn = CreateDataGridColumn(dataGrid, columnDescription);
                dataGrid.Columns.Add(dataGridColumn);
            }

            ////dataGrid.ColumnDisplayIndexChanged += (sender, args) =>
            ////{

            ////    Console.WriteLine("Column Index changed: {0}, {1}", args.Column.Header, args.Column.DisplayIndex);

            ////};

            ////columns.CollectionChanged += (sender, e2) =>
            ////{
            ////    NotifyCollectionChangedEventArgs ne = e2;
            ////    if (ne.Action == NotifyCollectionChangedAction.Reset)
            ////    {
            ////        dataGrid.Columns.Clear();
            ////        if (ne.NewItems != null)
            ////        {
            ////            foreach (ColumnDescriptor column in ne.NewItems)
            ////            {
            ////                DataGridColumn dgc = CreateDataGridColumn(column);
            ////                dataGrid.Columns.Add(dgc);
            ////            }
            ////        }
            ////    }
            ////    else if (ne.Action == NotifyCollectionChangedAction.Add)
            ////    {
            ////        if (ne.NewItems != null)
            ////        {
            ////            foreach (ColumnDescriptor column in ne.NewItems)
            ////            {
            ////                DataGridColumn dgc = CreateDataGridColumn(column);
            ////                dataGrid.Columns.Add(dgc);
            ////            }
            ////        }
            ////    }
            ////    else if (ne.Action == NotifyCollectionChangedAction.Move)
            ////    {
            ////        dataGrid.Columns.Move(ne.OldStartingIndex, ne.NewStartingIndex);
            ////    }
            ////    else if (ne.Action == NotifyCollectionChangedAction.Remove)
            ////    {
            ////        if (ne.OldItems != null)
            ////        {
            ////            foreach (ColumnDescriptor column in ne.OldItems)
            ////            {
            ////                // dataGrid.Columns.Remove(column);
            ////            }
            ////        }
            ////    }
            ////    else if (ne.Action == NotifyCollectionChangedAction.Replace)
            ////    {
            ////        dataGrid.Columns[ne.NewStartingIndex] = ne.NewItems[0] as DataGridColumn;
            ////    }
            ////};
        }

        /// <summary>
        /// Creates a <see cref="DataGridColumn"/> from the description.
        /// </summary>
        /// <param name="dataGrid">The datagrid.</param>
        /// <param name="column">The description of a data grid column.</param>
        /// <returns>The generated <see cref="DataGridColumn"/></returns>
        /// <exception cref="ArgumentException">If column contains a unknown column type.</exception>
        private static DataGridColumn CreateDataGridColumn(DataGrid dataGrid, DataGridColumnDescriptor column)
        {
            DataGridColumn dgc;

            switch (column.ColumnType)
            {
                case DataGridColumnType.Text:
                {
                    Style alignmentStyle = null;
                    DataGridTextColumn dgtc = new DataGridTextColumn();
                    dgtc.Header = column.Name;
                    dgtc.IsReadOnly = column.IsReadonly;
                    dgtc.CanUserSort = column.CanSort;
                    dgtc.CanUserReorder = column.CanReorder;
                    dgtc.Binding = new Binding(column.Binding);

                    switch (column.ColumnAlignment)
                    {
                        case DataGridColumnAlignment.Right:
                        {
                            alignmentStyle = WpfHelper.GetDependencyPropertyValue(dataGrid, StandardDataGrid.CellRightAlignStyleProperty) as Style;
                        }

                        break;

                        case DataGridColumnAlignment.Center:
                        {
                            alignmentStyle = WpfHelper.GetDependencyPropertyValue(dataGrid, StandardDataGrid.CellCenterAlignStyleProperty) as Style;
                        }

                        break;
                    }

                    if (alignmentStyle != null)
                    {
                        dgtc.CellStyle = alignmentStyle;
                    }

                    dgc = dgtc;
                }

                    break;
                default:
                {
                    throw new ArgumentException(string.Format("Unknown column type: {0}", column.ColumnType));
                }
            }

            return dgc;
        }
    }
}
