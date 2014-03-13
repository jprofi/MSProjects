namespace WpfDemo.DataGrid
{
    using Thinknet.MVVM.ViewModel;

    /// <summary>
    /// Test view model for data grid.
    /// </summary>
    public class PersonViewModel : DataGridRowViewModel
    {
        private string _lastName;
        private string _firstName;
        private int _age;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonViewModel"/> class.
        /// </summary>
        /// <param name="lastName">
        /// The last name.
        /// </param>
        /// <param name="firstName">
        /// The first name.
        /// </param>
        /// <param name="age">
        /// The age.
        /// </param>
        public PersonViewModel(string lastName, string firstName, int age)
        {
            _lastName = lastName;
            _firstName = firstName;
            _age = age;
        }

        /// <summary>
        /// Gets or sets the lastname.
        /// </summary>
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the firstname
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                NotifyPropertyChanged();
            }
        }
    }
}
