namespace Thinknet.MVVM.ViewModel
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Windows.Input;

    using Thinknet.MVVM.Command;

    /// <summary>
    /// The model for the implementation of the generic data grid in Xaml.
    /// </summary>
    /// <typeparam name="T">The type of a data row.</typeparam>
    public abstract class DataGridViewModel<T> : ViewModel where T : DataGridRowViewModel
    {
        private readonly RelayCommand<IList> _selectionChangedCommand;
        private readonly RelayCommand _deleteRowsCommand;
        private readonly ObservableCollection<DataGridColumnDescriptor> _columnsDescriptions;
        private ObservableCollection<T> _data;
        private bool _isModified;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGridViewModel{T}"/> class.
        /// </summary>
        protected DataGridViewModel()
        {
            // Generic list can't be used, because selection can contains other types than view model, e.g. newly created item.
            _selectionChangedCommand = new RelayCommand<IList>(items => SelectRows(items));
            _deleteRowsCommand = new RelayCommand(OnDeleteRows);

            _columnsDescriptions = new ObservableCollection<DataGridColumnDescriptor>();
            Data = new ObservableCollection<T>();
        }

        /// <summary>
        /// Gets the description for the single columns of a data grid.
        /// </summary>
        public IEnumerable<DataGridColumnDescriptor> ColumnsDescriptions
        {
            get { return _columnsDescriptions; }
        }

        /// <summary>
        /// Gets the rows for showing in the data grid.
        /// </summary>
        public ObservableCollection<T> Data
        {
            get { return _data; }
            set
            {
                DeregisterDataOnCollectionChangedHandler();
                _data = value;
                RegisterDataOnCollectionChangedHandler();
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the command for the handling for multiple row selection, is called from event to command.
        /// </summary>
        public RelayCommand<IList> SelectionChangedCommand
        {
            get { return _selectionChangedCommand; }
        }

        /// <summary>
        /// Gets the command for deletion operation. 
        /// Implementor is responsible for removing the desired items and modifying the collection of data after deletion.<see cref="Data"/>
        /// </summary>
        public ICommand DeleteRowsCommand
        {
            get { return _deleteRowsCommand; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a sample has been added, removed, moved or modified.
        /// Delegates the call to it's data collection. Sets the value to false, also propagates this information to it's children.
        /// </summary>
        public bool IsModified
        {
            get
            {
                return _isModified || Data.Any(arg => arg.IsModified);
            }

            protected set
            {
                // Reset modified state for all children.
                if (value == false)
                {
                    Data.ToList().ForEach(obj => obj.IsModified = false);
                }

                _isModified = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the indexes of the selected rows in the datagrid.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/> of all selected Rows.</returns>
        public IEnumerable<T> SelectedRows()
        {
            return _data.Where(row => row.IsSelected).ToList();
        }

        /// <summary>
        /// The selected row indexes.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/> indexes of all selected rows.
        /// </returns>
        public IEnumerable<int> SelectedRowIndexes()
        {
            List<int> selectedRowIndexes = new List<int>();
            for (int rowIdx = 0; rowIdx < _data.Count; rowIdx++)
            {
                if (Data[rowIdx].IsSelected)
                {
                    selectedRowIndexes.Add(rowIdx);
                }
            }

            return selectedRowIndexes;
        }

        /// <summary>
        /// Adds a column description.
        /// </summary>
        /// <param name="columnDescription">The column description</param>
        public void AddColumnDescription(DataGridColumnDescriptor columnDescription)
        {
            _columnsDescriptions.Add(columnDescription);
        }

        /// <summary>
        /// Update the selection states of the single rows and the list with the indexes of the selected rows.
        /// </summary>
        /// <param name="selectedItems">The selected rows.</param>
        protected void SelectRows(IList selectedItems)
        {
            // todo tb: Optimize when a lot of items ares selcted, problem of cartesian product.
            if (selectedItems != null)
            {
                // Check casting for the case of newitemplaceholder and copy for concurrent modification
                List<T> selectedRowsOfT = selectedItems.OfType<T>().ToList();

                foreach (T dataRow in Data)
                {
                    dataRow.SuspendSelectionPropertyChanged = true;
                    dataRow.IsSelected = false;
                    dataRow.SuspendSelectionPropertyChanged = false;
                }

                selectedRowsOfT.ForEach(obj =>
                {
                    obj.SuspendSelectionPropertyChanged = true;
                    obj.IsSelected = true;
                    obj.SuspendSelectionPropertyChanged = false;
                });
            }
        }

        protected virtual void OnDeleteRows()
        {
        }

        private void RegisterDataOnCollectionChangedHandler()
        {
            _data.CollectionChanged += DataOnCollectionChanged;
        }

        private void DeregisterDataOnCollectionChangedHandler()
        {
            if (_data != null)
            {
                _data.CollectionChanged -= DataOnCollectionChanged;
            }
        }

        private void DataOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            IsModified = true;
        }
    }
}
