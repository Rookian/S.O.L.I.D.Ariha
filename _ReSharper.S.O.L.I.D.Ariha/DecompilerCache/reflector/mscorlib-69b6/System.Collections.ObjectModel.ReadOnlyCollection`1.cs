namespace System.Collections.ObjectModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading;

    /// <summary>Provides the base class for a generic read-only collection.</summary>
    [Serializable, DebuggerTypeProxy(typeof (Mscorlib_CollectionDebugView<>)), ComVisible(false),
     DebuggerDisplay("Count = {Count}")]
    public class ReadOnlyCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
    {
        [NonSerialized] private object _syncRoot;
        private IList<T> list;

        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"></see> class that is a read-only wrapper around the specified list.</summary>
        /// <param name="list">The list to wrap.</param>
        /// <exception cref="T:System.ArgumentNullException">list is null.</exception>
        public ReadOnlyCollection(IList<T> list)
        {
            if (list == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
            }
            this.list = list;
        }

        /// <summary>Gets the element at the specified index.</summary>
        /// <returns>The element at the specified index.</returns>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero.-or-index is equal to or greater than <see cref="P:System.Collections.ObjectModel.ReadOnlyCollection`1.Count"></see>. </exception>
        public T this[int index]
        {
            get { return this.list[index]; }
        }

        /// <summary>Returns the <see cref="T:System.Collections.Generic.IList`1"></see> that the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"></see> wraps.</summary>
        /// <returns>The <see cref="T:System.Collections.Generic.IList`1"></see> that the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"></see> wraps.</returns>
        protected IList<T> Items
        {
            get { return this.list; }
        }

        #region IList Members

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            }
            if (array.Rank != 1)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
            }
            if (array.GetLowerBound(0) != 0)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
            }
            if (index < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex,
                                                             ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            }
            if ((array.Length - index) < this.Count)
            {
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
            }
            T[] localArray = array as T[];
            if (localArray != null)
            {
                this.list.CopyTo(localArray, index);
            }
            else
            {
                Type elementType = array.GetType().GetElementType();
                Type c = typeof (T);
                if (!elementType.IsAssignableFrom(c) && !c.IsAssignableFrom(elementType))
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
                }
                object[] objArray = array as object[];
                if (objArray == null)
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
                }
                int count = this.list.Count;
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        objArray[index++] = this.list[i];
                    }
                }
                catch (ArrayTypeMismatchException)
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
                }
            }
        }

        int IList.Add(object value)
        {
            ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            return -1;
        }

        void IList.Clear()
        {
            ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
        }

        bool IList.Contains(object value)
        {
            return (ReadOnlyCollection<T>.IsCompatibleObject(value) && this.Contains((T) value));
        }

        int IList.IndexOf(object value)
        {
            if (ReadOnlyCollection<T>.IsCompatibleObject(value))
            {
                return this.IndexOf((T) value);
            }
            return -1;
        }

        void IList.Insert(int index, object value)
        {
            ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
        }

        void IList.Remove(object value)
        {
            ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
        }

        void IList.RemoveAt(int index)
        {
            ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get
            {
                if (this._syncRoot == null)
                {
                    ICollection list = this.list as ICollection;
                    if (list != null)
                    {
                        this._syncRoot = list.SyncRoot;
                    }
                    else
                    {
                        Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
                    }
                }
                return this._syncRoot;
            }
        }

        bool IList.IsFixedSize
        {
            get { return true; }
        }

        bool IList.IsReadOnly
        {
            get { return true; }
        }

        object IList.this[int index]
        {
            get { return this.list[index]; }
            set { ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection); }
        }

        #endregion

        #region IList<T> Members

        /// <summary>Determines whether an element is in the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"></see>.</summary>
        /// <returns>true if value is found in the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"></see>; otherwise, false.</returns>
        /// <param name="value">The object to locate in the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"></see>. The value can be null for reference types.</param>
        public bool Contains(T value)
        {
            return this.list.Contains(value);
        }

        /// <summary>Copies the entire <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"></see> to a compatible one-dimensional <see cref="T:System.Array"></see>, starting at the specified index of the target array.</summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        /// <exception cref="T:System.ArgumentException">index is equal to or greater than the length of array.-or-The number of elements in the source <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"></see> is greater than the available space from index to the end of the destination array.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero.</exception>
        /// <exception cref="T:System.ArgumentNullException">array is null.</exception>
        public void CopyTo(T[] array, int index)
        {
            this.list.CopyTo(array, index);
        }

        /// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"></see>.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1"></see> for the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"></see>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        /// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"></see>.</summary>
        /// <returns>The zero-based index of the first occurrence of item within the entire <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"></see>, if found; otherwise, -1.</returns>
        /// <param name="value">The object to locate in the <see cref="T:System.Collections.Generic.List`1"></see>. The value can be null for reference types.</param>
        public int IndexOf(T value)
        {
            return this.list.IndexOf(value);
        }

        void ICollection<T>.Add(T value)
        {
            ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
        }

        void ICollection<T>.Clear()
        {
            ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
        }

        bool ICollection<T>.Remove(T value)
        {
            ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
            return false;
        }

        void IList<T>.Insert(int index, T value)
        {
            ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
        }

        void IList<T>.RemoveAt(int index)
        {
            ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        /// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"></see> instance.</summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"></see> instance.</returns>
        public int Count
        {
            get { return this.list.Count; }
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return true; }
        }

        T IList<T>.this[int index]
        {
            get { return this.list[index]; }
            set { ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection); }
        }

        #endregion

        private static bool IsCompatibleObject(object value)
        {
            if (!(value is T) && ((value != null) || typeof (T).IsValueType))
            {
                return false;
            }
            return true;
        }

        private static void VerifyValueType(object value)
        {
            if (!ReadOnlyCollection<T>.IsCompatibleObject(value))
            {
                ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof (T));
            }
        }
    }
}