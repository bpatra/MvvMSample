using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BonenLawyer
{
    public interface ICollectionView<T> : IEnumerable<T>, ICollectionView
    {
        IEnumerable<T> SourceCollectionGeneric { get; }
    }

    public class MyCollectionViewGeneric<T> : ICollectionView<T>
    {
        private readonly ICollectionView _collectionView;

        public MyCollectionViewGeneric(ICollectionView generic)
        {
            _collectionView = generic;
        }

        private class MyEnumerator : IEnumerator<T>
        {
            private readonly IEnumerator _enumerator;
            public MyEnumerator(IEnumerator enumerator)
            {
                _enumerator = enumerator;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
               return _enumerator.MoveNext();
            }

            public void Reset()
            {
               _enumerator.Reset();
            }

            public T Current { get { return (T) _enumerator.Current; } }

            object IEnumerator.Current
            {
                get { return Current; }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator(_collectionView.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collectionView.GetEnumerator();
        }

        public bool Contains(object item)
        {
            return _collectionView.Contains(item);
        }

        public void Refresh()
        {
            _collectionView.Refresh();
        }

        public IDisposable DeferRefresh()
        {
            return _collectionView.DeferRefresh();
        }

        public bool MoveCurrentToFirst()
        {
            return _collectionView.MoveCurrentToFirst();
        }

        public bool MoveCurrentToLast()
        {
            return _collectionView.MoveCurrentToLast();
        }

        public bool MoveCurrentToNext()
        {
            return _collectionView.MoveCurrentToNext();
        }

        public bool MoveCurrentToPrevious()
        {
            return _collectionView.MoveCurrentToPrevious();
        }

        public bool MoveCurrentTo(object item)
        {
            return _collectionView.MoveCurrentTo(item);
        }

        public bool MoveCurrentToPosition(int position)
        {
            return _collectionView.MoveCurrentToPosition(position);
        }

        public CultureInfo Culture
        {
            get { return _collectionView.Culture; }
            set { _collectionView.Culture = value; }
        }

        public IEnumerable SourceCollection
        {
            get { return _collectionView.SourceCollection; }
        }

        public Predicate<object> Filter
        {
            get { return _collectionView.Filter; }
            set { _collectionView.Filter = value; }
        }

        public bool CanFilter
        {
            get { return _collectionView.CanFilter; }
        }

        public SortDescriptionCollection SortDescriptions
        {
            get { return _collectionView.SortDescriptions; }
        }

        public bool CanSort
        {
            get { return _collectionView.CanSort; }
        }

        public bool CanGroup
        {
            get { return _collectionView.CanGroup; }
        }

        public ObservableCollection<GroupDescription> GroupDescriptions
        {
            get { return _collectionView.GroupDescriptions; }
        }

        public ReadOnlyObservableCollection<object> Groups
        {
            get { return _collectionView.Groups; }
        }

        public bool IsEmpty { get { return _collectionView.IsEmpty; } }

        public object CurrentItem { get { return _collectionView.CurrentItem; } }

        public int CurrentPosition { get { return _collectionView.CurrentPosition; } }

        public bool IsCurrentAfterLast { get { return _collectionView.IsCurrentAfterLast; } }

        public bool IsCurrentBeforeFirst { get { return _collectionView.IsCurrentBeforeFirst; } }
        
        public event CurrentChangingEventHandler CurrentChanging
        {
            add
            {
                lock (objectLock)
                {
                    _collectionView.CurrentChanging += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    _collectionView.CurrentChanging -= value;
                }
            }
        }

        object objectLock = new object();
        public event EventHandler CurrentChanged
        {
            add {
                lock (objectLock)
                {
                    _collectionView.CurrentChanged += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    _collectionView.CurrentChanged -= value;
                }
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                lock (objectLock)
                {
                    _collectionView.CollectionChanged += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    _collectionView.CollectionChanged -= value;
                }
            }
        }

        public IEnumerable<T> SourceCollectionGeneric
        {
            get { return _collectionView.Cast<T>(); }
        }
    }
}
