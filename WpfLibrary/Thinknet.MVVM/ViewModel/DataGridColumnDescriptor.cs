namespace Thinknet.MVVM.ViewModel
{
    using System;
    using System.Linq.Expressions;

    using Thinknet.MVVM.Helper;

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
        private readonly bool _canSort;
        private readonly bool _canReorder;

        private readonly DataGridColumnAlignment _columnAlignment;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGridColumnDescriptor"/> class.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        /// <param name="propertyName">The property on which the colum belongs.</param>
        /// <param name="columnType">The column type.</param>
        /// <param name="columnAlignment">The alignment of the column.</param>
        /// <param name="isReadonly">Whether the column should be readonly or not.</param>
        /// <param name="canSort">Whether the column content can be sorted or not.</param>
        /// <param name="canReorder">Whether the column can be rearranged or not.</param>
        private DataGridColumnDescriptor(string name, string propertyName, DataGridColumnType columnType, DataGridColumnAlignment columnAlignment, bool isReadonly, bool canSort, bool canReorder)
        {
            _name = name;
            _binding = propertyName;
            _columnType = columnType;
            _columnAlignment = columnAlignment;
            _isReadonly = isReadonly;
            _canSort = canSort;
            _canReorder = canReorder;
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
        /// Gets the alignment of the data grid column.
        /// </summary>
        public DataGridColumnAlignment ColumnAlignment
        {
            get { return _columnAlignment; }
        }

        /// <summary>
        /// Gets a value indicating whether the column is readonly.
        /// </summary>
        public bool IsReadonly
        {
            get { return _isReadonly; }
        }

        /// <summary>
        /// Gets a value indicating whether the column content can be sorted or not.
        /// </summary>
        public bool CanSort
        {
            get { return _canSort; }
        }

        /// <summary>
        /// Gets a value indicating whether the column can be rearranged or not. 
        /// </summary>
        public bool CanReorder
        {
            get { return _canReorder; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGridColumnDescriptor"/> class.
        /// </summary>
        /// <typeparam name="TModel">The type of the table row view model for databinding.</typeparam>
        /// <param name="name">The name of the column.</param>
        /// <param name="property">The path and property on which the colum belongs.</param>
        /// <param name="columnType">The column type.</param>
        /// <param name="columnAlignment">The alignment of the column.</param>
        /// <param name="isReadonly">Whether the column should be readonly or not.</param>
        /// <param name="canSort">Whether the column content can be sorted or not.</param>
        /// <param name="canReorder">Whether the column can be rearranged or not.</param>
        public static DataGridColumnDescriptor CreateNewInstance<TModel>(string name, Expression<Func<TModel, object>> property, DataGridColumnType columnType, DataGridColumnAlignment columnAlignment, bool isReadonly, bool canSort, bool canReorder) where TModel : class
        {
            string propertyPath = ReflectionHelper.GetPropertyName(property);
            return CreateNewInstance(name, propertyPath, columnType, columnAlignment, isReadonly, canSort, canReorder);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGridColumnDescriptor"/> class.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        /// <param name="propertyPath">The path and property on which the colum belongs.</param>
        /// <param name="columnType">The column type.</param>
        /// <param name="columnAlignment">The alignment of the column.</param>
        /// <param name="isReadonly">Whether the column should be readonly or not.</param>
        /// <param name="canSort">Whether the column content can be sorted or not.</param>
        /// <param name="canReorder">Whether the column can be rearranged or not.</param>
        public static DataGridColumnDescriptor CreateNewInstance(string name, string propertyPath, DataGridColumnType columnType, DataGridColumnAlignment columnAlignment, bool isReadonly, bool canSort, bool canReorder)
        {
            return new DataGridColumnDescriptor(name, propertyPath, columnType, columnAlignment, isReadonly, canSort, canReorder);
        }
    }
}
