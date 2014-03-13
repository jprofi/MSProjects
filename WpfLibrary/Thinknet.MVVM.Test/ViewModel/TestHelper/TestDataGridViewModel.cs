namespace Thinknet.MVVM.Test.ViewModel.TestHelper
{
    using System.Windows.Input;

    using Thinknet.MVVM.ViewModel;

    /// <summary>
    /// Helper model for testing the datagrid.
    /// </summary>
    public class TestDataGridViewModel : DataGridViewModel<TestDataViewModel>
    {
        public TestDataGridViewModel()
        {
            AddColumnDescription(DataGridColumnDescriptor.CreateNewInstance<TestDataViewModel>("Id", model => model.Index, DataGridColumnType.Text, DataGridColumnAlignment.Left, false, false, false));
            AddColumnDescription(DataGridColumnDescriptor.CreateNewInstance("Name", "Name", DataGridColumnType.Text, DataGridColumnAlignment.Left, false, false, false));
            AddColumnDescription(DataGridColumnDescriptor.CreateNewInstance("Timestamp", "Timestamp", DataGridColumnType.Text, DataGridColumnAlignment.Left, false, false, false));

            Data.Add(new TestDataViewModel("0", "Thomas"));
            
            TestDataViewModel ralfViewModel = new TestDataViewModel("1", "Ralf");
            ralfViewModel.IsSelected = true;
            ralfViewModel.IsReadOnly = true;
            Data.Add(ralfViewModel);
           
            Data.Add(new TestDataViewModel("2", "Reto"));
        }

        public override ICommand DeleteRowsCommand
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
