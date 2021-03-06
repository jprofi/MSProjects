﻿namespace Thinknet.Core
{
    using System;
    using System.ComponentModel;
    using System.Threading;

    /// <summary>
    /// <para>Provides raising events in a thread-safe way.</para>
    /// </summary>
    public static class EventHelper
    {
        /// <summary>
        /// Raises a event in a atomic thread-safe way.
        /// </summary>
        /// <typeparam name="TEventArgs">
        /// The type of the event data generated by the event.
        /// </typeparam>
        /// <param name="eventHandler">
        /// A reference to the event handler.
        /// </param>
        /// <param name="sender">
        /// The source of the event
        /// </param>
        /// <param name="eventArgs">
        /// An EventArgs that contains the event data.
        /// </param>
        /// <example>
        /// <code>
        /// public event EventHandler&lt;DataEventArgs&gt; DataChanged;
        /// ... 
        /// public void SimulateDataChanged(string data)
        /// {
        ///     EventHelper.Raise(ref DataChanged, this, new DataEventArgs(data));
        /// }
        /// </code>
        /// </example>
        public static void Raise<TEventArgs>(ref EventHandler<TEventArgs> eventHandler, object sender, TEventArgs eventArgs)
#if NET40
            where TEventArgs : EventArgs
#endif
        {
            // Copy the event handler reference into temporary field in atomic thread-safe way.
            EventHandler<TEventArgs> sink = Volatile.Read(ref eventHandler);
            if (sink != null)
            {
                sink(sender, eventArgs);
            }
        }

        /// <summary>
        /// Raises a event in a atomic thread-safe way.
        /// </summary>
        /// <typeparam name="TEventData">
        /// The type of the event data generated by the event.
        /// </typeparam>
        /// <param name="eventHandler">
        /// A reference to the event handler.
        /// </param>
        /// <param name="sender">
        /// The source of the event
        /// </param>
        /// <param name="eventData">
        /// An data that contains the event data.
        /// </param>
        /// <example>
        /// <code>
        /// public event EventHandler&lt;GenericEventArgs&lt;DataEventArgs&gt;&gt; DataChanged;
        /// ... 
        /// public void SimulateDataChanged(string data)
        /// {
        ///     EventHelper.Raise(ref DataChanged, this, data);
        /// }
        /// </code>
        /// </example>
        public static void Raise<TEventData>(ref EventHandler<GenericEventArgs<TEventData>> eventHandler, object sender, TEventData eventData)
        {
            // Copy the event handler reference into temporary field in atomic thread-safe way.
            EventHandler<GenericEventArgs<TEventData>> sink = Volatile.Read(ref eventHandler);
            if (sink != null)
            {
                sink(sender, new GenericEventArgs<TEventData>(eventData));
            }
        }

        /// <summary>
        /// Raises a event in a atomic thread-safe way.
        /// </summary>
        /// <param name="eventHandler">
        /// A reference to the event handler.
        /// </param>
        /// <param name="sender">
        /// The source of the event
        /// </param>
        /// <param name="eventArgs">
        /// Events args type which must be derived from
        /// <see cref="System.EventArgs"/> .
        /// </param>
        /// <example>
        /// <code>
        /// public event EventHandler&lt;DataEventArgs&gt; DataChanged;
        /// ... 
        /// public void SimulateDataChanged(string data)
        /// {
        ///     EventHelper.Raise(ref DataChanged, this, new DataEventArgs(data));
        /// }
        /// </code>
        /// </example>
        public static void Raise(ref EventHandler eventHandler, object sender, EventArgs eventArgs)
        {
            // Copy the event handler reference into temporary field in atomic thread-safe way.
            EventHandler sink = Volatile.Read(ref eventHandler);
            if (sink != null)
            {
                sink(sender, eventArgs);
            }
        }

        /// <summary>
        /// Raises a event in a atomic thread-safe way.
        /// </summary>
        /// <param name="eventHandler">
        /// A reference to the event handler.
        /// </param>
        /// <param name="sender">
        /// The source of the event
        /// </param>
        /// <param name="eventArgs">
        /// Events args type which must be derived from
        /// <see cref="System.EventArgs"/> .
        /// </param>
        /// <example>
        /// <code>
        /// public event EventHandler&lt;DataEventArgs&gt; DataChanged;
        /// ... 
        /// public void SimulateDataChanged(string data)
        /// {
        ///     EventHelper.Raise(ref DataChanged, this, new DataEventArgs(data));
        /// }
        /// </code>
        /// </example>
        public static void Raise(ref PropertyChangedEventHandler eventHandler, object sender, PropertyChangedEventArgs eventArgs)
        {
            // Copy the event handler reference into temporary field in atomic thread-safe way.
            PropertyChangedEventHandler sink = Volatile.Read(ref eventHandler);
            if (sink != null)
            {
                sink(sender, eventArgs);
            }
        }

        /// <summary>
        /// Raises a event in a atomic thread-safe way.
        /// </summary>
        /// <typeparam name="TSender">
        /// The type of the sender.
        /// </typeparam>
        /// <typeparam name="TEventArgs">
        /// The type of the event data generated by the event.
        /// </typeparam>
        /// <param name="eventHandler">
        /// A reference to the event handler.
        /// </param>
        /// <param name="sender">
        /// The source of the event
        /// </param>
        /// <param name="eventArgs">
        /// An EventArgs that contains the event data.
        /// </param>
        /// <example>
        /// <code>
        /// public event TypedEventHandler&lt;View, DataEventArgs&gt; DataChanged;
        /// ... 
        /// public void SimulateDataChanged(string data)
        /// {
        ///     EventHelper.Raise(ref DataChanged, this, new DataEventArgs(data));
        /// }
        /// </code>
        /// </example>
        public static void Raise<TSender, TEventArgs>(ref TypedEventHandler<TSender, TEventArgs> eventHandler, TSender sender, TEventArgs eventArgs)
        {
            // Copy the event handler reference into temporary field  in atomic thread-safe way.
            TypedEventHandler<TSender, TEventArgs> sink = Volatile.Read(ref eventHandler);
            if (sink != null)
            {
                sink(sender, eventArgs);
            }
        }

        /// <summary>
        /// Raises a event in a atomic thread-safe way.
        /// </summary>
        /// <typeparam name="TSender">
        /// The type of the sender.
        /// </typeparam>
        /// <param name="eventHandler">
        /// A reference to the event handler.
        /// </param>
        /// <param name="sender">
        /// The source of the event
        /// </param>
        /// <param name="eventArgs">
        /// An EventArgs that contains the event data.
        /// </param>
        /// <example>
        /// <code>
        /// public event TypedEventHandler&lt;View&gt; DataChanged;
        /// ... 
        /// public void SimulateDataChanged(string data)
        /// {
        ///     EventHelper.Raise(ref DataChanged, this, new DataEventArgs(data));
        /// }
        /// </code>
        /// </example>
        public static void Raise<TSender>(ref TypedEventHandler<TSender> eventHandler, TSender sender, EventArgs eventArgs)
        {
            // Copy the event handler reference into temporary field  in atomic thread-safe way.
            TypedEventHandler<TSender> sink = Volatile.Read(ref eventHandler);
            if (sink != null)
            {
                sink(sender, eventArgs);
            }
        }

        /// <summary>
        /// Clears all subscribers from the event handler.
        /// </summary>
        /// <param name="eventHandler">
        /// The event handler.
        /// </param>
        public static void Clear(ref EventHandler eventHandler)
        {
            Volatile.Write(ref eventHandler, null);
        }

        /// <summary>
        /// Clears all subscribers from the event handler.
        /// </summary>
        /// <param name="eventHandler">
        /// The event handler.
        /// </param>
        public static void Clear(ref PropertyChangedEventHandler eventHandler)
        {
            Volatile.Write(ref eventHandler, null);
        }

        /// <summary>
        /// Clears all subscribers from the event handler.
        /// </summary>
        /// <typeparam name="TEventArgs">
        /// The type of the event args.
        /// </typeparam>
        /// <param name="eventHandler">
        /// The event handler.
        /// </param>
        public static void Clear<TEventArgs>(ref EventHandler<TEventArgs> eventHandler)
#if NET40
 where TEventArgs : EventArgs
#endif
        {
            Volatile.Write(ref eventHandler, null);
        }

        /// <summary>
        /// Clears all subscribers from the event handler.
        /// </summary>
        /// <typeparam name="TSender">
        /// The type of the sender.
        /// </typeparam>
        /// <param name="eventHandler">
        /// The event handler.
        /// </param>
        public static void Clear<TSender>(ref TypedEventHandler<TSender> eventHandler)
        {
            Volatile.Write(ref eventHandler, null);
        }

        /// <summary>
        /// Clears all subscribers from the event handler.
        /// </summary>
        /// <typeparam name="TSender">
        /// The type of the sender.
        /// </typeparam>
        /// <typeparam name="TEventArgs">
        /// The type of the event args.
        /// </typeparam>
        /// <param name="eventHandler">
        /// The event handler.
        /// </param>
        public static void Clear<TSender, TEventArgs>(ref TypedEventHandler<TSender, TEventArgs> eventHandler)
        {
            Volatile.Write(ref eventHandler, null);
        }
    }
}
