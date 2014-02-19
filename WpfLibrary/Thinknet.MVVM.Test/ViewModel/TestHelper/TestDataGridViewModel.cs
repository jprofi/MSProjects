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
            ColumnsDescriptions.Add(new DataGridColumnDescriptor("Id", "Index", DataGridColumnType.Text, false));
            ColumnsDescriptions.Add(new DataGridColumnDescriptor("Name", "Name", DataGridColumnType.Text, false));
            ColumnsDescriptions.Add(new DataGridColumnDescriptor("Timestamp", "Timestamp", DataGridColumnType.Text, false));

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
