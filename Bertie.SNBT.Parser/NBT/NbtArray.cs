using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bertie.SNBT.Parser.NBT {
    public abstract class NbtArray : NbtTag {
        public abstract int Count { get; }

        /// <summary>
        /// Checks if the array contains an nbt tag.
        /// </summary>
        /// <typeparam name="I">The type of tag to check for.</typeparam>
        /// <param name="item">The tag to check for.</param>
        /// <returns>Returns true if array contains the specified tag.</returns>
        public abstract bool ContainsItem<I>(I item) where I : NbtTag;

        /// <summary>
        /// Returns the index of the specified nbt tag.
        /// </summary>
        /// <typeparam name="I">The type of tag to check for.</typeparam>
        /// <param name="item">The tag to check for.</param>
        /// <returns>Returns the index of the specified item or -1 if not found.</returns>
        public abstract int IndexOfItem<I>(I item) where I : NbtTag;
        
        /// <summary>
        /// Checks if the item at the index is <typeparamref name="R"/>.
        /// </summary>
        /// <typeparam name="R">The tag type to check for.</typeparam>
        /// <param name="index">The index of the item to check.</param>
        /// <returns>Returns true if the item is <typeparamref name="R"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if index is out of range.</exception>
        public abstract bool ItemIs<R>(int index) where R : NbtTag;
        /// <summary>
        /// Returns the item as <typeparamref name="R"/>.
        /// </summary>
        /// <typeparam name="R">The type to return the tag as.</typeparam>
        /// <param name="index">The index of the item to return.</param>
        /// <returns>Returns the item at the specified index as <typeparamref name="R"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if index is out of range.</exception>
        /// <exception cref="InvalidCastException">Throws if the item at the specified index is not <typeparamref name="R"/>.</exception>
        public abstract R ItemAs<R>(int index) where R : NbtTag;
        /// <summary>
        /// Checks if the item is <typeparamref name="R"/> and if so, returns it.
        /// </summary>
        /// <typeparam name="R">The type to return the tag as.</typeparam>
        /// <param name="index">The index of the item to return.</param>
        /// <param name="result">If possible, will contain the item as <typeparamref name="R"/></param>
        /// <returns>Returns true if the item could be returned as <typeparamref name="R"/></returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if index is out of range.</exception>
        public abstract bool TryItemAs<R>(int index, out R result) where R : NbtTag;

        public abstract IEnumerable<R> ItemsAs<R>() where R : NbtTag;

        /// <summary>
        /// Checks if the array contains a <see cref="NbtPrimitive"/> with a value that after possible conversion matches the specified value.
        /// </summary>
        /// <typeparam name="V">The type of value to check for.</typeparam>
        /// <param name="value">The value to check for.</param>
        /// <returns>Returns true if the array contains a <see cref="NbtPrimitive"/> of type <typeparamref name="V"/></returns>
        public abstract bool ContainsValue<V>(V value);

        /// <summary>
        /// Returns the index of the first <see cref="NbtPrimitive"/> with a value that after possible conversion matches the specified value.
        /// </summary>
        /// <typeparam name="V">The type of value to check for.</typeparam>
        /// <param name="item">The value to check for.</param>
        /// <returns>Returns the index of the specified value or -1 if not found.</returns>
        public abstract int IndexOfValue<V>(V value);

        /// <summary>
        /// Returns the value of the <see cref="NbtPrimitive"/> at the specified index as <typeparamref name="R"/>.
        /// </summary>
        /// <typeparam name="R">The type to return the value as.</typeparam>
        /// <param name="index">The index of the value to return.</param>
        /// <returns>Returns the value at the specified index as <typeparamref name="R"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if index is out of range.</exception>
        /// <exception cref="InvalidCastException">Throws if the item at the specified index is not an <see cref="NbtPrimitive"/> or the value is not <typeparamref name="R"/>.</exception>
        public R ValueAs<R>(int index) {
            return ItemAs<NbtPrimitive>(index).ValueAs<R>();
        }

        /// <summary>
        /// Checks if the value of the <see cref="NbtPrimitive"/> at the specified index can be returned as <typeparamref name="R"/> and if so, returns it.
        /// </summary>
        /// <typeparam name="R">The type to return the value as.</typeparam>
        /// <param name="index">The index of the item to return.</param>
        /// <param name="result">If possible, will contain the value of the the item as <typeparamref name="R"/></param>
        /// <returns>Returns true if the item is a <see cref="NbtPrimitive"/> and the value could be returned as <typeparamref name="R"/></returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if index is out of range.</exception>
        public bool TryValueAs<R>(int index, out R result) {
            result = default;
            return TryItemAs<NbtPrimitive>(index, out var primitive) && primitive.TryValueAs(out result);
        }
        public abstract IEnumerable<R> ValuesAs<R>();
    }

    public class NbtArray<T> : NbtArray where T : NbtTag {
        private List<T> Values { get; } = new List<T>();

        public override int Count => Values.Count;

        public void Add(T item) {
            Values.Add(item);
        }

        public void Clear() {
            Values.Clear();
        }

        public override bool ContainsItem<I>(I item) {
            return item is T ? Values.Contains(item as T) : false;
        }

        public void CopyTo(T[] array, int arrayIndex) {
            Values.CopyTo(array, arrayIndex);
        }

        public override int IndexOfItem<I>(I item) {
            return item is T ? Values.IndexOf(item as T) : -1;
        }

        public override bool ContainsValue<V>(V value) {
            return Values.Any(v => v.TryAs<NbtPrimitive>(out var primitive) && primitive.ValueEquals(value));
        }

        public override int IndexOfValue<V>(V value) {
            return Values.FindIndex(v => v.TryAs<NbtPrimitive>(out var primitive) && primitive.ValueEquals(value));
        }

        public void Insert(int index, T item) {
            Values.Insert(index, item);
        }

        public bool Remove(T item) {
            return Values.Remove(item);
        }

        public void RemoveAt(int index) {
            Values.RemoveAt(index);
        }

        public override bool ItemIs<R>(int index) {
            return Values[index].Is<R>();
        }

        public override R ItemAs<R>(int index) {
            return Values[index].As<R>();
        }

        public override bool TryItemAs<R>(int index, out R result) {
            return Values[index].TryAs(out result);
        }

        public override IEnumerable<R> ItemsAs<R>() {
            return Values.Select(v => v.As<R>()).ToList();
        }
        public override IEnumerable<R> ValuesAs<R>() {
            return Values.Select(v => {
                var primitive = v.As<NbtPrimitive>();
                return primitive.ValueAs<R>();
            }).ToList();
        }
    }
}
