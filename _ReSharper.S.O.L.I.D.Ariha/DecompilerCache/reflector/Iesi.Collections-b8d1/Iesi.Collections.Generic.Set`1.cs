namespace Iesi.Collections.Generic
{
    using Iesi.Collections;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary><p>A collection that contains no duplicate elements.  This class models the mathematical
    /// <c>Set</c> abstraction, and is the base class for all other <c>Set</c> implementations.  
    /// The order of elements in a set is dependant on (a)the data-structure implementation, and 
    /// (b)the implementation of the various <c>Set</c> methods, and thus is not guaranteed.</p>
    /// 
    /// <p>None of the <c>Set</c> implementations in this library are guranteed to be thread-safe
    /// in any way unless wrapped in a <c>SynchronizedSet</c>.</p>
    /// 
    /// <p>The following table summarizes the binary operators that are supported by the <c>Set</c> class.</p>
    /// <list type="table">
    /// <listheader>
    /// <term>Operation</term>
    /// <term>Description</term>
    /// <term>Method</term>
    /// <term>Operator</term>
    /// </listheader>
    /// <item>
    /// <term>Union (OR)</term>
    /// <term>Element included in result if it exists in either <c>A</c> OR <c>B</c>.</term>
    /// <term><c>Union()</c></term>
    /// <term><c>|</c></term>
    /// </item>
    /// <item>
    /// <term>Intersection (AND)</term>
    /// <term>Element included in result if it exists in both <c>A</c> AND <c>B</c>.</term>
    /// <term><c>InterSect()</c></term>
    /// <term><c>&amp;</c></term>
    /// </item>
    /// <item>
    /// <term>Exclusive Or (XOR)</term>
    /// <term>Element included in result if it exists in one, but not both, of <c>A</c> and <c>B</c>.</term>
    /// <term><c>ExclusiveOr()</c></term>
    /// <term><c>^</c></term>
    /// </item>
    /// <item>
    /// <term>Minus (n/a)</term>
    /// <term>Take all the elements in <c>A</c>.  Now, if any of them exist in <c>B</c>, remove
    /// them.  Note that unlike the other operators, <c>A - B</c> is not the same as <c>B - A</c>.</term>
    /// <term><c>Minus()</c></term>
    /// <term><c>-</c></term>
    /// </item>
    /// </list>
    /// </summary>
    [Serializable]
    public abstract class Set<T> : ISet<T>, ICollection<T>, IEnumerable<T>, ISet, ICollection, IEnumerable, ICloneable
    {
        protected Set()
        {
        }

        #region ISet Members

        bool ISet.Add(object o)
        {
            return this.Add((T) o);
        }

        bool ISet.AddAll(ICollection c)
        {
            bool flag = false;
            foreach (T local in c)
            {
                flag |= this.Add(local);
            }
            return flag;
        }

        bool ISet.Contains(object o)
        {
            return ((o is T) && this.Contains((T) o));
        }

        bool ISet.ContainsAll(ICollection c)
        {
            ICollection<T> is2 = new List<T>(c.Count);
            foreach (object obj2 in c)
            {
                if (obj2 is T)
                {
                    is2.Add((T) obj2);
                }
                else
                {
                    return false;
                }
            }
            return this.ContainsAll(is2);
        }

        ISet ISet.ExclusiveOr(ISet a)
        {
            return this.NonGenericExclusiveOr(a);
        }

        ISet ISet.Intersect(ISet a)
        {
            return this.NonGenericIntersect(a);
        }

        ISet ISet.Minus(ISet a)
        {
            return this.NonGenericMinus(a);
        }

        bool ISet.Remove(object o)
        {
            return ((o is T) && this.Remove((T) o));
        }

        bool ISet.RemoveAll(ICollection c)
        {
            ICollection<T> is2 = new List<T>(c.Count);
            foreach (object obj2 in c)
            {
                if (obj2 is T)
                {
                    is2.Add((T) obj2);
                }
            }
            return this.RemoveAll(is2);
        }

        bool ISet.RetainAll(ICollection c)
        {
            ICollection<T> is2 = new List<T>(c.Count);
            foreach (object obj2 in c)
            {
                if (obj2 is T)
                {
                    is2.Add((T) obj2);
                }
            }
            return this.RetainAll(is2);
        }

        ISet ISet.Union(ISet a)
        {
            return this.NonGenericUnion(a);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.NonGenericCopyTo(array, index);
        }

        /// <summary>
        /// Returns <see langword="true" /> if the <c>Set</c> is synchronized across threads.  Note that
        /// enumeration is inherently not thread-safe.  Use the <c>SyncRoot</c> to lock the
        /// object during enumeration.
        /// </summary>
        public abstract bool IsSynchronized { get; }

        /// <summary>
        /// An object that can be used to synchronize this collection to make it thread-safe.
        /// When implementing this, if your object uses a base object, like an <c>IDictionary</c>,
        /// or anything that has a <c>SyncRoot</c>, return that object instead of "<c>this</c>".
        /// </summary>
        public abstract object SyncRoot { get; }

        #endregion

        #region ISet<T> Members

        /// <summary>
        /// Adds the specified element to this set if it is not already present.
        /// </summary>
        /// <param name="o">The object to add to the set.</param>
        /// <returns><see langword="true" /> is the object was added, <see langword="false" /> if it was already present.</returns>
        public abstract bool Add(T o);

        /// <summary>
        /// Adds all the elements in the specified collection to the set if they are not already present.
        /// </summary>
        /// <param name="c">A collection of objects to add to the set.</param>
        /// <returns><see langword="true" /> is the set changed as a result of this operation, <see langword="false" /> if not.</returns>
        public abstract bool AddAll(ICollection<T> c);

        /// <summary>
        /// Removes all objects from the set.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Returns a clone of the <c>Set</c> instance.  This will work for derived <c>Set</c>
        /// classes if the derived class implements a constructor that takes no arguments.
        /// </summary>
        /// <returns>A clone of this object.</returns>
        public virtual object Clone()
        {
            Set<T> set = (Set<T>) Activator.CreateInstance(base.GetType());
            set.AddAll(this);
            return set;
        }

        /// <summary>
        /// Returns <see langword="true" /> if this set contains the specified element.
        /// </summary>
        /// <param name="o">The element to look for.</param>
        /// <returns><see langword="true" /> if this set contains the specified element, <see langword="false" /> otherwise.</returns>
        public abstract bool Contains(T o);

        /// <summary>
        /// Returns <see langword="true" /> if the set contains all the elements in the specified collection.
        /// </summary>
        /// <param name="c">A collection of objects.</param>
        /// <returns><see langword="true" /> if the set contains all the elements in the specified collection, <see langword="false" /> otherwise.</returns>
        public abstract bool ContainsAll(ICollection<T> c);

        /// <summary>
        /// Copies the elements in the <c>Set</c> to an array.  The type of array needs
        /// to be compatible with the objects in the <c>Set</c>, obviously.
        /// </summary>
        /// <param name="array">An array that will be the target of the copy operation.</param>
        /// <param name="index">The zero-based index where copying will start.</param>
        public abstract void CopyTo(T[] array, int index);

        /// <summary>
        /// Performs an "exclusive-or" of the two sets, keeping only the elements that
        /// are in one of the sets, but not in both.  The original sets are not modified
        /// during this operation.  The result set is a <c>Clone()</c> of this set containing
        /// the elements from the exclusive-or operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <returns>A set containing the result of <c>a ^ b</c>.</returns>
        public virtual ISet<T> ExclusiveOr(ISet<T> a)
        {
            ISet<T> set = (ISet<T>) this.Clone();
            foreach (T local in a)
            {
                if (set.Contains(local))
                {
                    set.Remove(local);
                }
                else
                {
                    set.Add(local);
                }
            }
            return set;
        }

        /// <summary>
        /// Gets an enumerator for the elements in the <c>Set</c>.
        /// </summary>
        /// <returns>An <c>IEnumerator</c> over the elements in the <c>Set</c>.</returns>
        public abstract IEnumerator<T> GetEnumerator();

        /// <summary>
        /// Performs an "intersection" of the two sets, where only the elements
        /// that are present in both sets remain.  That is, the element is included if it exists in
        /// both sets.  The <c>Intersect()</c> operation does not modify the input sets.  It returns
        /// a <c>Clone()</c> of this set with the appropriate elements removed.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <returns>The intersection of this set with <c>a</c>.</returns>
        public virtual ISet<T> Intersect(ISet<T> a)
        {
            ISet<T> set = (ISet<T>) this.Clone();
            if (a != null)
            {
                set.RetainAll(a);
                return set;
            }
            set.Clear();
            return set;
        }

        /// <summary>
        /// Performs a "minus" of set <c>b</c> from set <c>a</c>.  This returns a set of all
        /// the elements in set <c>a</c>, removing the elements that are also in set <c>b</c>.
        /// The original sets are not modified during this operation.  The result set is a <c>Clone()</c>
        /// of this <c>Set</c> containing the elements from the operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <returns>A set containing the elements from this set with the elements in <c>a</c> removed.</returns>
        public virtual ISet<T> Minus(ISet<T> a)
        {
            ISet<T> set = (ISet<T>) this.Clone();
            if (a != null)
            {
                set.RemoveAll(a);
            }
            return set;
        }

        /// <summary>
        /// Removes the specified element from the set.
        /// </summary>
        /// <param name="o">The element to be removed.</param>
        /// <returns><see langword="true" /> if the set contained the specified element, <see langword="false" /> otherwise.</returns>
        public abstract bool Remove(T o);

        /// <summary>
        /// Remove all the specified elements from this set, if they exist in this set.
        /// </summary>
        /// <param name="c">A collection of elements to remove.</param>
        /// <returns><see langword="true" /> if the set was modified as a result of this operation.</returns>
        public abstract bool RemoveAll(ICollection<T> c);

        /// <summary>
        /// Retains only the elements in this set that are contained in the specified collection.
        /// </summary>
        /// <param name="c">Collection that defines the set of elements to be retained.</param>
        /// <returns><see langword="true" /> if this set changed as a result of this operation.</returns>
        public abstract bool RetainAll(ICollection<T> c);

        void ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Performs a "union" of the two sets, where all the elements
        /// in both sets are present.  That is, the element is included if it is in either <c>a</c> or <c>b</c>.
        /// Neither this set nor the input set are modified during the operation.  The return value
        /// is a <c>Clone()</c> of this set with the extra elements added in.
        /// </summary>
        /// <param name="a">A collection of elements.</param>
        /// <returns>A new <c>Set</c> containing the union of this <c>Set</c> with the specified collection.
        /// Neither of the input objects is modified by the union.</returns>
        public virtual ISet<T> Union(ISet<T> a)
        {
            ISet<T> set = (ISet<T>) this.Clone();
            if (a != null)
            {
                set.AddAll(a);
            }
            return set;
        }

        /// <summary>
        /// The number of elements currently contained in this collection.
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// Returns <see langword="true" /> if this set contains no elements.
        /// </summary>
        public abstract bool IsEmpty { get; }

        /// <summary>
        /// Indicates whether the given instance is read-only or not
        /// </summary>
        /// <value>
        /// <see langword="true" /> if the ISet is read-only; otherwise, <see langword="false" />. 
        /// In the default implementation of Set, this property always returns false. 
        /// </value>
        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        /// <summary>
        /// Performs an "exclusive-or" of the two sets, keeping only the elements that
        /// are in one of the sets, but not in both.  The original sets are not modified
        /// during this operation.  The result set is a <c>Clone()</c> of one of the sets
        /// (<c>a</c> if it is not <see langword="null" />) containing
        /// the elements from the exclusive-or operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>A set containing the result of <c>a ^ b</c>.  <see langword="null" /> if both sets are <see langword="null" />.</returns>
        public static ISet<T> ExclusiveOr(ISet<T> a, ISet<T> b)
        {
            if ((a == null) && (b == null))
            {
                return null;
            }
            if (a == null)
            {
                return (ISet<T>) b.Clone();
            }
            if (b == null)
            {
                return (ISet<T>) a.Clone();
            }
            return a.ExclusiveOr(b);
        }

        /// <summary>
        /// Performs an "intersection" of the two sets, where only the elements
        /// that are present in both sets remain.  That is, the element is included only if it exists in
        /// both <c>a</c> and <c>b</c>.  Neither input object is modified by the operation.
        /// The result object is a <c>Clone()</c> of one of the input objects (<c>a</c> if it is not <see langword="null" />) containing the
        /// elements from the intersect operation. 
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>The intersection of the two input sets.  <see langword="null" /> if both sets are <see langword="null" />.</returns>
        public static ISet<T> Intersect(ISet<T> a, ISet<T> b)
        {
            if ((a == null) && (b == null))
            {
                return null;
            }
            if (a == null)
            {
                return b.Intersect(a);
            }
            return a.Intersect(b);
        }

        /// <summary>
        /// Performs a "minus" of set <c>b</c> from set <c>a</c>.  This returns a set of all
        /// the elements in set <c>a</c>, removing the elements that are also in set <c>b</c>.
        /// The original sets are not modified during this operation.  The result set is a <c>Clone()</c>
        /// of set <c>a</c> containing the elements from the operation. 
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>A set containing <c>A - B</c> elements.  <see langword="null" /> if <c>a</c> is <see langword="null" />.</returns>
        public static ISet<T> Minus(ISet<T> a, ISet<T> b)
        {
            if (a == null)
            {
                return null;
            }
            return a.Minus(b);
        }

        /// <summary>
        /// Performs CopyTo when called trhough non-generic ISet (ICollection) interface
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        protected abstract void NonGenericCopyTo(Array array, int index);

        /// <summary>
        /// Performs ExclusiveOr when called trhough non-generic ISet interface
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        protected virtual ISet NonGenericExclusiveOr(ISet a)
        {
            ISet set = (ISet) this.Clone();
            foreach (object obj2 in a)
            {
                if (set.Contains(obj2))
                {
                    set.Remove(obj2);
                }
                else
                {
                    set.Add(obj2);
                }
            }
            return set;
        }

        /// <summary>
        /// Performs Intersect when called trhough non-generic ISet interface
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        protected virtual ISet NonGenericIntersect(ISet a)
        {
            ISet set = (ISet) this.Clone();
            if (a != null)
            {
                set.RetainAll(a);
                return set;
            }
            set.Clear();
            return set;
        }

        /// <summary>
        /// Performs Minus when called trhough non-generic ISet interface
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        protected virtual ISet NonGenericMinus(ISet a)
        {
            ISet set = (ISet) this.Clone();
            if (a != null)
            {
                set.RemoveAll(a);
            }
            return set;
        }

        /// <summary>
        /// Performs Union when called trhough non-generic ISet interface
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        protected virtual ISet NonGenericUnion(ISet a)
        {
            ISet set = (ISet) this.Clone();
            if (a != null)
            {
                set.AddAll(a);
            }
            return set;
        }

        /// <summary>
        /// Performs an "intersection" of the two sets, where only the elements
        /// that are present in both sets remain.  That is, the element is included only if it exists in
        /// both <c>a</c> and <c>b</c>.  Neither input object is modified by the operation.
        /// The result object is a <c>Clone()</c> of one of the input objects (<c>a</c> if it is not <see langword="null" />) containing the
        /// elements from the intersect operation. 
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>The intersection of the two input sets.  <see langword="null" /> if both sets are <see langword="null" />.</returns>
        public static Set<T> operator &(Set<T> a, Set<T> b)
        {
            return (Set<T>) Set<T>.Intersect(a, b);
        }

        /// <summary>
        /// Performs a "union" of two sets, where all the elements
        /// in both are present.  That is, the element is included if it is in either <c>a</c> or <c>b</c>.
        /// The return value is a <c>Clone()</c> of one of the sets (<c>a</c> if it is not <see langword="null" />) with elements of the other set
        /// added in.  Neither of the input sets is modified by the operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>A set containing the union of the input sets.  <see langword="null" /> if both sets are <see langword="null" />.</returns>
        public static Set<T> operator |(Set<T> a, Set<T> b)
        {
            return (Set<T>) Set<T>.Union(a, b);
        }

        /// <summary>
        /// Performs an "exclusive-or" of the two sets, keeping only the elements that
        /// are in one of the sets, but not in both.  The original sets are not modified
        /// during this operation.  The result set is a <c>Clone()</c> of one of the sets
        /// (<c>a</c> if it is not <see langword="null" />) containing
        /// the elements from the exclusive-or operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>A set containing the result of <c>a ^ b</c>.  <see langword="null" /> if both sets are <see langword="null" />.</returns>
        public static Set<T> operator ^(Set<T> a, Set<T> b)
        {
            return (Set<T>) Set<T>.ExclusiveOr(a, b);
        }

        /// <summary>
        /// Performs a "minus" of set <c>b</c> from set <c>a</c>.  This returns a set of all
        /// the elements in set <c>a</c>, removing the elements that are also in set <c>b</c>.
        /// The original sets are not modified during this operation.  The result set is a <c>Clone()</c>
        /// of set <c>a</c> containing the elements from the operation. 
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>A set containing <c>A - B</c> elements.  <see langword="null" /> if <c>a</c> is <see langword="null" />.</returns>
        public static Set<T> operator -(Set<T> a, Set<T> b)
        {
            return (Set<T>) Set<T>.Minus(a, b);
        }

        /// <summary>
        /// Performs a "union" of two sets, where all the elements
        /// in both are present.  That is, the element is included if it is in either <c>a</c> or <c>b</c>.
        /// The return value is a <c>Clone()</c> of one of the sets (<c>a</c> if it is not <see langword="null" />) with elements of the other set
        /// added in.  Neither of the input sets is modified by the operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>A set containing the union of the input sets.  <see langword="null" /> if both sets are <see langword="null" />.</returns>
        public static ISet<T> Union(ISet<T> a, ISet<T> b)
        {
            if ((a == null) && (b == null))
            {
                return null;
            }
            if (a == null)
            {
                return (ISet<T>) b.Clone();
            }
            if (b == null)
            {
                return (ISet<T>) a.Clone();
            }
            return a.Union(b);
        }
    }
}