namespace Thinknet.MVVM.Test.ViewModel
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using Thinknet.MVVM.Command;
    using Thinknet.MVVM.Test.ViewModel.TestHelper;

    /// <summary>
    /// Tests for the data grid view model.
    /// </summary>
    [TestFixture]
    public class DataGridViewModelTest
    {
        [Test]
        public void TestSelection()
        {
            TestDataGridViewModel dgvm = new TestDataGridViewModel();

            Assert.That(dgvm.Data.Count(), Is.EqualTo(3));
            Assert.That(dgvm.SelectedRows().Count(), Is.EqualTo(1), "One row should be selected");
            Assert.That(dgvm.SelectedRowIndexes().Count(), Is.EqualTo(1), "One row should be selected");
            Assert.That(dgvm.SelectedRows().First().IsSelected, Is.True, "Row wshould be selected");
            Assert.That(dgvm.SelectedRowIndexes().First(), Is.EqualTo(1), "Row with index 1 should be selected");

            RelayCommand<IList> cmdSelect = dgvm.SelectionChangedCommand;
            Assert.That(cmdSelect.CanExecute(null), Is.True);

            IList<TestDataViewModel> toSelect = new List<TestDataViewModel>();
            toSelect.Add(dgvm.Data[0]);
            toSelect.Add(dgvm.Data[2]);

            cmdSelect.Execute(toSelect);

            Assert.That(dgvm.SelectedRows().Count(), Is.EqualTo(2), "Two rows should be selected");
            Assert.That(dgvm.Data[0].IsSelected, Is.True);
            Assert.That(dgvm.Data[1].IsSelected, Is.False);
            Assert.That(dgvm.Data[2].IsSelected, Is.True);

            Assert.That(dgvm.Data[0].IsReadOnly, Is.False);
            Assert.That(dgvm.Data[1].IsReadOnly, Is.True);
            Assert.That(dgvm.Data[2].IsReadOnly, Is.False);

            // Clear selection
            cmdSelect.Execute(new List<TestDataViewModel>());
            Assert.That(dgvm.Data[0].IsSelected, Is.False);
            Assert.That(dgvm.Data[1].IsSelected, Is.False);
            Assert.That(dgvm.Data[2].IsSelected, Is.False);
            Assert.That(dgvm.SelectedRows().Count(), Is.EqualTo(0), "No rows should be selected");
        }
    }
}
