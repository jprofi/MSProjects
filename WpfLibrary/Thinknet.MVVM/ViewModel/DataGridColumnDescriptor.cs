namespace Thinknet.MVVM.ViewModel
{
    /// <summary>
    /// Description for a single data grid column.
    /// Is used to autogenerate the data grid columns.
    /// Functionality to propagate changes to the datagrid is not yet implemented.
    /// </summary>
    public class DataGridColumnDescriptor
    {
        private readonly string _name;
        private readonly string _binding;
        private readonly DataGridColumnType _columnType;
        private readonly bool _isReadonly;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGridColumnDescriptor"/> class.
        /// </summary>
        /// <param name="name">The column name shown in the datagrid.</param>
        /// <param name="binding">The binding to the property for the specific data grid column.</param>
        /// <param name="columnType">The column type for instantiation of a correct data grid column.</param>
        /// <param name="isIsReadonly">Is the datagrid column readonly.</param>
        public DataGridColumnDescriptor(string name, string binding, DataGridColumnType columnType, bool isIsReadonly)
        {
            _name = name;
            _binding = binding;
            _columnType = columnType;
            _isReadonly = isIsReadonly;
        }

        /// <summary>
        /// Gets the name of the data grid column.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the binding to the property on which this column belongs.
        /// </summary>
        public string Binding
        {
            get { return _binding; }
        }

        /// <summary>
        /// Gets the type of the data shown in this data grid column.
        /// </summary>
        public DataGridColumnType ColumnType
        {
            get { return _columnType; }
        }

        /// <summary>
        /// Gets a value indicating whether the column is readonly.
        /// </summary>
        public bool IsReadonly
        {
            get { return _isReadonly; }
        }
    }
}
