namespace WpfDemo.DataGrid
{
    using System.Windows.Input;

    using Thinknet.MVVM.ViewModel;

    /// <summary>
    /// Data provider for the data grid.
    /// </summary>
    public class TestDataGridViewModel : DataGridViewModel<PersonViewModel>
    {
        private ICommand _deleteRowsCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestDataGridViewModel"/> class.
        /// </summary>
        public TestDataGridViewModel()
        {
            AddColumnDescription(DataGridColumnDescriptor.CreateNewInstance<PersonViewModel>("LastName", model => model.LastName, DataGridColumnType.Text, DataGridColumnAlignment.Left, true, false, true));
            AddColumnDescription(DataGridColumnDescriptor.CreateNewInstance<PersonViewModel>("FirstName", model => model.FirstName, DataGridColumnType.Text, DataGridColumnAlignment.Center, true, false, true));
            AddColumnDescription(DataGridColumnDescriptor.CreateNewInstance<PersonViewModel>("Age", model => model.Age, DataGridColumnType.Text, DataGridColumnAlignment.Right, true, false, true));

            Data.Add(new PersonViewModel("Bühler", "Thomas", 45));
            Data.Add(new PersonViewModel("Bühler", "Ralf", 42));
            Data.Add(new PersonViewModel("Bühler", "Reto", 39));
        }

        /// <inheritdoc />
        public override ICommand DeleteRowsCommand
        {
            get { return _deleteRowsCommand; }
        }
    }
}
