namespace Thinknet.Core
{
    using System;

    /// <summary>
    /// Additional information for generic events.
    /// </summary>
    /// <typeparam name="TEventData">
    /// Type of the event data.
    /// </typeparam>
    public class GenericEventArgs<TEventData> : EventArgs
    {
        private readonly TEventData _eventData;
        
        /// <summary>
        /// Initializes a new instance of the GenericEventArgs class.
        /// </summary>
        /// <param name="eventData">
        /// Information describing the event.
        /// </param>
        public GenericEventArgs(TEventData eventData)
        {
            _eventData = eventData;
        }
        
        /// <summary>
        /// Gets information describing the event.
        /// </summary>
        /// <value>
        /// Information describing the event.
        /// </value>
        public TEventData EventData
        {
            get { return _eventData; }
        }
    }
}