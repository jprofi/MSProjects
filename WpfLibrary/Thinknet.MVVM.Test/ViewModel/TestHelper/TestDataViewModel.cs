namespace Thinknet.MVVM.Test.ViewModel.TestHelper
{
    using Thinknet.MVVM.ViewModel;

    public class TestDataViewModel : DataGridRowViewModel
    {
        private string _index;
        private string _name;

        public TestDataViewModel(string index, string name)
        {
            _index = index;
            _name = name;
        }

        public string Index
        {
            get { return _index; }
            set
            {
                _index = value;
                NotifyPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        public new TestDataViewModel Clone()
        {
            return new TestDataViewModel(_index, _name);
        }
    }
}
