namespace Thinknet.MVVM.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

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
                SetProperty(ref _expanded, value);
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
                SetProperty(ref _selected, value);
            }
        }
    }
}
