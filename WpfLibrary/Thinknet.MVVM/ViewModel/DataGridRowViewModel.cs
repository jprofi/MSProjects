namespace Thinknet.MVVM.ViewModel
{
    /// <summary>
    /// Base for a single row in a data grid, contains base functionality used by every row.
    /// </summary>
    public abstract class DataGridRowViewModel : ViewModel
    {
        private bool _isReadonly;
        private bool _isSelected;
        private bool _isModified;
        private bool _suspendSelectionPropertyChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the table row is readonly or not.
        /// </summary>
        public bool IsReadOnly
        {
            get { return _isReadonly; }
            set
            {
                _isReadonly = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this table row is selected or not.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    
                    if (!SuspendSelectionPropertyChanged)
                    {
                        NotifyPropertyChanged();
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether a datagrid row has been modified or not.
        /// </summary>
        public bool IsModified
        {
            get { return _isModified; }
            set
            {
                _isModified = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the event for <see cref="IsSelected"/> should be fired or not.
        /// Is needed to prevent event loops when updating selected items over relay from xaml.
        /// </summary>
        internal bool SuspendSelectionPropertyChanged
        {
            get { return _suspendSelectionPropertyChanged; }
            set { _suspendSelectionPropertyChanged = value; }
        }

        /// <summary>
        /// Implementor should overwrite this method when copy functionality on the table is required.
        /// E.g. Drag copy or things like that.
        /// </summary>
        /// <returns>A usually deep copy of the table row.</returns>
        public object Clone()
        {
            return this;
        }
    }
}
