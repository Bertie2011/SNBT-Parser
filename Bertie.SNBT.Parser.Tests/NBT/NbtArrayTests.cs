using Bertie.SNBT.Parser.NBT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Bertie.SNBT.Parser.Tests.NBT {
    public class NbtArrayTests {
        [Fact]
        public void CountWorks() {
            var array = new NbtArray<NbtPrimitive<int>>();
            Assert.Equal(0, array.Count);
            array.Add(new NbtPrimitive<int>(3));
            Assert.Equal(1, array.Count);
        }

        [Fact]
        public void ContainsItemReturnsTrueForSameItem() {

            var array = new NbtArray<NbtPrimitive<int>>();
            var item = new NbtPrimitive<int>(3);
            array.Add(item);
            Assert.True(array.ContainsItem(item));
        }

        [Fact]
        public void ContainsItemReturnsFalseForEqualItem() {
            var array = new NbtArray<NbtPrimitive<int>>();
            var item = new NbtPrimitive<int>(3);
            array.Add(item);
            Assert.False(array.ContainsItem(new NbtPrimitive<int>(3)));
        }

        [Fact]
        public void IndexOfItemReturnsForSameItem() {
            var array = new NbtArray<NbtPrimitive<int>>();
            var item = new NbtPrimitive<int>(3);
            array.Add(item);
            Assert.Equal(0, array.IndexOfItem(item));
        }

        [Fact]
        public void IndexOfItemFailsForEqualItem() {
            var array = new NbtArray<NbtPrimitive<int>>();
            var item = new NbtPrimitive<int>(3);
            array.Add(item);
            Assert.Equal(-1, array.IndexOfItem(new NbtPrimitive<int>(3)));
        }

        [Fact]
        public void ItemIsReturnsTrueForMatchingClass() {
            var array = new NbtArray<NbtPrimitive<int>>();
            array.Add(new NbtPrimitive<int>(3));
            Assert.True(array.ItemIs<NbtPrimitive<int>>(0));
            Assert.True(array.ItemIs<NbtPrimitive>(0));
            Assert.True(array.ItemIs<NbtTag>(0));
        }
        [Fact]
        public void ItemIsReturnsFalseForNonMatchingClass() {
            var array = new NbtArray<NbtPrimitive<int>>();
            array.Add(new NbtPrimitive<int>(3));
            Assert.False(array.ItemIs<NbtPrimitive<string>>(0));
            Assert.False(array.ItemIs<NbtArray>(0));
        }

        [Fact]
        public void ItemAsReturnsForMatchingClass() {
            var array = new NbtArray<NbtPrimitive<int>>();
            array.Add(new NbtPrimitive<int>(3));
            array.ItemAs<NbtPrimitive<int>>(0);
            array.ItemAs<NbtPrimitive>(0);
            array.ItemAs<NbtTag>(0);
        }
        [Fact]
        public void ItemAsFailsForNonMatchingClass() {
            var array = new NbtArray<NbtPrimitive<int>>();
            array.Add(new NbtPrimitive<int>(3));
            Assert.Throws<InvalidCastException>(() => array.ItemAs<NbtPrimitive<string>>(0));
            Assert.Throws<InvalidCastException>(() => array.ItemAs<NbtArray>(0));
        }
        [Fact]
        public void TryItemAsReturnsTrueForMatchingClass() {
            var array = new NbtArray<NbtPrimitive<int>>();
            var item = new NbtPrimitive<int>(3);
            array.Add(item);
            Assert.True(array.TryItemAs<NbtPrimitive<int>>(0, out var v1));
            Assert.Equal(item, v1);
            Assert.True(array.TryItemAs<NbtPrimitive>(0, out var v2));
            Assert.Equal(item, v2);
            Assert.True(array.TryItemAs<NbtTag>(0, out var v3));
            Assert.Equal(item, v3);
        }
        [Fact]
        public void TryItemAsReturnsFalseForNonMatchingClass() {
            var array = new NbtArray<NbtPrimitive<int>>();
            array.Add(new NbtPrimitive<int>(3));
            Assert.False(array.TryItemAs<NbtPrimitive<string>>(0, out _));
            Assert.False(array.TryItemAs<NbtArray>(0, out _));
        }

        [Fact]
        public void ItemsAsReturnsForMatchingValueType() {
            var array = new NbtArray<NbtPrimitive<int>>();
            array.Add(new NbtPrimitive<int>(3));
            array.Add(new NbtPrimitive<int>(7));
            array.Add(new NbtPrimitive<int>(8));
            Assert.Equal(3, array.ItemsAs<NbtPrimitive>().Count());
        }
        [Fact]
        public void ItemsAsFailsForNonMatchingValueType() {
            var array = new NbtArray<NbtPrimitive<int>>();
            array.Add(new NbtPrimitive<int>(3));
            array.Add(new NbtPrimitive<int>(7));
            array.Add(new NbtPrimitive<int>(8));
            Assert.Throws<InvalidCastException>(() => array.ItemsAs<NbtCompound>());
        }

        [Fact]
        public void ValueAsReturnsForMatchingValueType() {
            var array = new NbtArray<NbtPrimitive<int>>();
            array.Add(new NbtPrimitive<int>(3));
            array.ValueAs<sbyte>(0);
            array.ValueAs<bool>(0);
            array.ValueAs<float>(0);
        }
        [Fact]
        public void ValueAsFailsForNonMatchingValueType() {
            var array = new NbtArray<NbtPrimitive<int>>();
            array.Add(new NbtPrimitive<int>(3));
            Assert.Throws<InvalidCastException>(() => array.ValueAs<string>(0));
        }

        [Fact]
        public void TryValueAsReturnsTrueForMatchingValueType() {
            var array = new NbtArray<NbtPrimitive<int>>();
            var item = new NbtPrimitive<int>(3);
            array.Add(item);
            Assert.True(array.TryValueAs<long>(0, out var v1));
            Assert.Equal(3, v1);
            Assert.True(array.TryValueAs<double>(0, out var v2));
            Assert.Equal(3.0, v2);
            Assert.True(array.TryValueAs<bool>(0, out var v3));
            Assert.True(v3);
        }
        [Fact]
        public void TryValueAsReturnsFalseForNonMatchingValueType() {
            var array = new NbtArray<NbtPrimitive<int>>();
            array.Add(new NbtPrimitive<int>(3));
            Assert.False(array.TryValueAs<string>(0, out _));
        }

        [Fact]
        public void ValuesAsReturnsForMatchingValueType() {
            var array = new NbtArray<NbtPrimitive<int>>();
            array.Add(new NbtPrimitive<int>(3));
            array.Add(new NbtPrimitive<int>(7));
            array.Add(new NbtPrimitive<int>(8));
            Assert.Equal(3, array.ValuesAs<long>().Count());
        }
        [Fact]
        public void ValuesAsFailsForNonMatchingValueType() {
            var array = new NbtArray<NbtPrimitive<int>>();
            array.Add(new NbtPrimitive<int>(3));
            array.Add(new NbtPrimitive<int>(7));
            array.Add(new NbtPrimitive<int>(8));
            Assert.Throws<InvalidCastException>(() => array.ValuesAs<string>());
        }
    }
}
