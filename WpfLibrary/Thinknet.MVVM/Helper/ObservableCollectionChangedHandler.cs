namespace Thinknet.MVVM.Helper
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics;

    /// <summary>
    /// the collection changed handler processes changes in source collection and updates the collection with elements of type TUpdate
    /// </summary>
    /// <typeparam name="TSource">
    /// element type of the source collection
    /// </typeparam>
    /// <typeparam name="TUpdate">
    /// element type of the collection to update
    /// </typeparam>
    public class ObservableCollectionChangedHandler<TSource, TUpdate>
    {
        /// <summary>
        /// process a change event in the source collection
        /// </summary>
        /// <param name="collection">collection to update</param>
        /// <param name="e">change notification data</param>
        /// <param name="convertFunc">delegate to create new elements of type TUpdate</param>
        public void ProcessChanges(ObservableCollection<TUpdate> collection, NotifyCollectionChangedEventArgs e, Func<TSource, TUpdate> convertFunc)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        ProcessAdd(collection, e, convertFunc);
                    }

                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        ProcessRemove(collection, e);
                    }

                    break;

                case NotifyCollectionChangedAction.Reset:
                    Debug.Assert(false, "Reset not implemented/tested");
                    ProcessReset(collection, e, convertFunc);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        private void ProcessAdd(ObservableCollection<TUpdate> collection, NotifyCollectionChangedEventArgs e, Func<TSource, TUpdate> convertFunc)
        {
            int index = e.NewStartingIndex;
            foreach (TSource item in e.NewItems)
            {
                collection.Insert(index++, convertFunc(item));
            }
        }

        private void ProcessRemove(ObservableCollection<TUpdate> collection, NotifyCollectionChangedEventArgs e)
        {
            foreach (TSource item in e.OldItems)
            {
                collection.RemoveAt(e.OldStartingIndex);
            }
        }

        private void ProcessReset(ObservableCollection<TUpdate> collection, NotifyCollectionChangedEventArgs e, Func<TSource, TUpdate> convertFunc)
        {
            collection.Clear();
            if (e.NewItems != null)
            {
                foreach (TSource item in e.NewItems)
                {
                    collection.Add(convertFunc(item));
                }
            }
        }
    }
}
