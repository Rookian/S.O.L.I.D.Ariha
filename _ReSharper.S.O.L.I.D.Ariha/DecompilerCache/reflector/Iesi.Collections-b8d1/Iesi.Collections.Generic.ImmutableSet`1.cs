namespace Iesi.Collections.Generic
{
    using Iesi.Collections;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// <p>Implements an immutable (read-only) <c>Set</c> wrapper.</p>
    /// <p>Although this is advertised as immutable, it really isn't.  Anyone with access to the
    /// <c>basisSet</c> can still change the data-set.  So <c>GetHashCode()</c> is not implemented
    /// for this <c>Set</c>, as is the case for all <c>Set</c> implementations in this library.
    /// This design decision was based on the efficiency of not having to <c>Clone()</c> the 
    /// <c>basisSet</c> every time you wrap a mutable <c>Set</c>.</p>
    /// </summary>
    [Serializable]
    public sealed class ImmutableSet<T> : Set<T>
    {
        private const string ERROR_MESSAGE = "Object is immutable.";
        private ISet<T> mBasisSet;

        /// <summary>
        /// Constructs an immutable (read-only) <c>Set</c> wrapper.
        /// </summary>
        /// <param name="basisSet">The <c>Set</c> that is wrapped.</param>
        public ImmutableSet(ISet<T> basisSet)
        {
            this.mBasisSet = basisSet;
        }

        internal ISet<T> BasisSet
        {
            get { return this.mBasisSet; }
        }

        /// <summary>
        /// The number of elements contained in this collection.
        /// </summary>
        public override sealed int Count
        {
            get { return this.mBasisSet.Count; }
        }

        /// <summary>
        /// Returns <see langword="true" /> if this set contains no elements.
        /// </summary>
        public override sealed bool IsEmpty
        {
            get { return this.mBasisSet.IsEmpty; }
        }

        /// <summary>
        /// Indicates that the given instance is read-only
        /// </summary>
        public override sealed bool IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Returns an object that can be used to synchronize use of the <c>Set</c> across threads.
        /// </summary>
        public override sealed bool IsSynchronized
        {
            get { return ((ICollection) this.mBasisSet).IsSynchronized; }
        }

        /// <summary>
        /// Returns an object that can be used to synchronize the <c>Set</c> between threads.
        /// </summary>
        public override sealed object SyncRoot
        {
            get { return ((ICollection) this.mBasisSet).SyncRoot; }
        }

        /// <summary>
        /// Adds the specified element to this set if it is not already present.
        /// </summary>
        /// <param name="o">The object to add to the set.</param>
        /// <returns>nothing</returns>
        /// <exception cref="T:System.NotSupportedException"> is always thrown</exception>
        public override sealed bool Add(T o)
        {
            throw new NotSupportedException("Object is immutable.");
        }

        /// <summary>
        /// Adds all the elements in the specified collection to the set if they are not already present.
        /// </summary>
        /// <param name="c">A collection of objects to add to the set.</param>
        /// <returns>nothing</returns>
        /// <exception cref="T:System.NotSupportedException"> is always thrown</exception>
        public override sealed bool AddAll(ICollection<T> c)
        {
            throw new NotSupportedException("Object is immutable.");
        }

        /// <summary>
        /// Removes all objects from the set.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException"> is always thrown</exception>
        public override sealed void Clear()
        {
            throw new NotSupportedException("Object is immutable.");
        }

        /// <summary>
        /// Returns a clone of the <c>Set</c> instance.  
        /// </summary>
        /// <returns>A clone of this object.</returns>
        public override sealed object Clone()
        {
            return new ImmutableSet<T>(this.mBasisSet);
        }

        /// <summary>
        /// Returns <see langword="true" /> if this set contains the specified element.
        /// </summary>
        /// <param name="o">The element to look for.</param>
        /// <returns><see langword="true" /> if this set contains the specified element, <see langword="false" /> otherwise.</returns>
        public override sealed bool Contains(T o)
        {
            return this.mBasisSet.Contains(o);
        }

        /// <summary>
        /// Returns <see langword="true" /> if the set contains all the elements in the specified collection.
        /// </summary>
        /// <param name="c">A collection of objects.</param>
        /// <returns><see langword="true" /> if the set contains all the elements in the specified collection, <see langword="false" /> otherwise.</returns>
        public override sealed bool ContainsAll(ICollection<T> c)
        {
            return this.mBasisSet.ContainsAll(c);
        }

        /// <summary>
        /// Copies the elements in the <c>Set</c> to an array of T. The type of array needs
        /// to be compatible with the objects in the <c>Set</c>, obviously.
        /// </summary>
        /// <param name="array">An array that will be the target of the copy operation.</param>
        /// <param name="index">The zero-based index where copying will start.</param>
        public override sealed void CopyTo(T[] array, int index)
        {
            this.mBasisSet.CopyTo(array, index);
        }

        /// <summary>
        /// Performs an "exclusive-or" of the two sets, keeping only the elements that
        /// are in one of the sets, but not in both.  The original sets are not modified
        /// during this operation.  The result set is a <c>Clone()</c> of this set containing
        /// the elements from the exclusive-or operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <returns>A set containing the result of <c>a ^ b</c>.</returns>
        public override sealed ISet<T> ExclusiveOr(ISet<T> a)
        {
            return new ImmutableSet<T>(this.GetUltimateBasisSet().ExclusiveOr(a));
        }

        /// <summary>
        /// Gets an enumerator for the elements in the <c>Set</c>.
        /// </summary>
        /// <returns>An <c>IEnumerator</c> over the elements in the <c>Set</c>.</returns>
        public override sealed IEnumerator<T> GetEnumerator()
        {
            return this.mBasisSet.GetEnumerator();
        }

        private ISet<T> GetUltimateBasisSet()
        {
            ISet<T> mBasisSet = this.mBasisSet;
            while (mBasisSet is ImmutableSet<T>)
            {
                mBasisSet = ((ImmutableSet<T>) mBasisSet).mBasisSet;
            }
            return mBasisSet;
        }

        /// <summary>
        /// Performs an "intersection" of the two sets, where only the elements
        /// that are present in both sets remain.  That is, the element is included if it exists in
        /// both sets.  The <c>Intersect()</c> operation does not modify the input sets.  It returns
        /// a <c>Clone()</c> of this set with the appropriate elements removed.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <returns>The intersection of this set with <c>a</c>.</returns>
        public override sealed ISet<T> Intersect(ISet<T> a)
        {
            return new ImmutableSet<T>(this.GetUltimateBasisSet().Intersect(a));
        }

        /// <summary>
        /// Performs a "minus" of set <c>b</c> from set <c>a</c>.  This returns a set of all
        /// the elements in set <c>a</c>, removing the elements that are also in set <c>b</c>.
        /// The original sets are not modified during this operation.  The result set is a <c>Clone()</c>
        /// of this <c>Set</c> containing the elements from the operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <returns>A set containing the elements from this set with the elements in <c>a</c> removed.</returns>
        public override sealed ISet<T> Minus(ISet<T> a)
        {
            return new ImmutableSet<T>(this.GetUltimateBasisSet().Minus(a));
        }

        /// <summary>
        /// Performs CopyTo when called trhough non-generic ISet (ICollection) interface
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        protected override void NonGenericCopyTo(Array array, int index)
        {
            ((ICollection) this.BasisSet).CopyTo(array, index);
        }

        /// <summary>
        /// Performs ExclusiveOr when called trhough non-generic ISet interface
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        protected override sealed ISet NonGenericExclusiveOr(ISet a)
        {
            return new ImmutableSet(((ISet) this.GetUltimateBasisSet()).ExclusiveOr(a));
        }

        /// <summary>
        /// Performs Intersect when called trhough non-generic ISet interface
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        protected override sealed ISet NonGenericIntersect(ISet a)
        {
            return new ImmutableSet(((ISet) this.GetUltimateBasisSet()).Intersect(a));
        }

        /// <summary>
        /// Performs Minus when called trhough non-generic ISet interface
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        protected override sealed ISet NonGenericMinus(ISet a)
        {
            return new ImmutableSet(((ISet) this.GetUltimateBasisSet()).Minus(a));
        }

        /// <summary>
        /// Performs Union when called trhough non-generic ISet interface
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        protected override sealed ISet NonGenericUnion(ISet a)
        {
            return new ImmutableSet(((ISet) this.GetUltimateBasisSet()).Union(a));
        }

        /// <summary>
        /// Removes the specified element from the set.
        /// </summary>
        /// <param name="o">The element to be removed.</param>
        /// <returns>nothing</returns>
        /// <exception cref="T:System.NotSupportedException"> is always thrown</exception>
        public override sealed bool Remove(T o)
        {
            throw new NotSupportedException("Object is immutable.");
        }

        /// <summary>
        /// Remove all the specified elements from this set, if they exist in this set.
        /// </summary>
        /// <param name="c">A collection of elements to remove.</param>
        /// <returns>nothing</returns>
        /// <exception cref="T:System.NotSupportedException"> is always thrown</exception>
        public override sealed bool RemoveAll(ICollection<T> c)
        {
            throw new NotSupportedException("Object is immutable.");
        }

        /// <summary>
        /// Retains only the elements in this set that are contained in the specified collection.
        /// </summary>
        /// <param name="c">Collection that defines the set of elements to be retained.</param>
        /// <returns>nothing</returns>
        /// <exception cref="T:System.NotSupportedException"> is always thrown</exception>
        public override sealed bool RetainAll(ICollection<T> c)
        {
            throw new NotSupportedException("Object is immutable.");
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
        public override sealed ISet<T> Union(ISet<T> a)
        {
            return new ImmutableSet<T>(this.GetUltimateBasisSet().Union(a));
        }
    }
}