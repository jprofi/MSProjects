namespace Thinknet.MVVM.Messaging
{
    /// <summary>
    /// Message for broadcasting that the selection of a tree element has changed.
    /// </summary>
    public class TreeSelectionChangedMessage : MessageBase
    {
        private readonly bool _selected;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeSelectionChangedMessage"/> class.
        /// </summary>
        /// <param name="sender">The sender for which the selection has changed.</param>
        /// <param name="selected">Whether the element is selected or not.</param>
        public TreeSelectionChangedMessage(object sender, bool selected)
            : base(sender)
        {
            _selected = selected;
        }

        /// <summary>
        /// Gets a value indicating wheter the element is selected or not.
        /// </summary>
        public bool Selected
        {
            get { return _selected; }
        }
    }
}
