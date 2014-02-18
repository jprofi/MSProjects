namespace Thinknet.MVVM.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Thinknet.MVVM.Messaging;

    /// <summary>
    /// This class serves as abstract base class for all tree node ViewModels.
    /// </summary>
    /// <typeparam name="T">Type of the children.</typeparam>
    public abstract class TreeNodeViewModel<T> : ViewModel<object> where T : TreeNodeViewModel<T>
    {
        protected readonly ICollection<T> ChildNodes = new ObservableCollection<T>();

        private bool _expanded;
        private bool _selected;

        /// <summary>
        /// Gets the child nodes of this tree node.
        /// </summary>
        public IEnumerable<T> Children
        {
            get { return ChildNodes; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a node is expanded.
        /// </summary>
        public virtual bool Expanded
        {
            get { return _expanded; }

            set
            {
                // Check whether value has changed
                if (value != _expanded)
                {
                    // Store value
                    _expanded = value;

                    // Notify about property change
                    NotifyPropertyChanged(() => Expanded);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a node is selected.
        /// </summary>
        public virtual bool Selected
        {
            get { return _selected; }

            set
            {
                // Check whether value has changed
                if (value != _selected)
                {
                    // Store value
                    _selected = value;

                    // Notify about property change
                    NotifyPropertyChanged(() => Selected);
                    MessengerInstance.Send(new TreeSelectionChangedMessage(this, _selected));
                }
            }
        }
    }
}
