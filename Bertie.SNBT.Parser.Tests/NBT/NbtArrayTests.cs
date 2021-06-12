using Bertie.SNBT.Parser.NBT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Bertie.SNBT.Parser.Tests.NBT {
    public class NbtArrayTests {
        [Fact]
        public void CanAddSameTypeItems() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<int>(3));
            array.Add(new NbtPrimitive<int>(7));
        }

        [Fact]
        public void AddFailsForSecondBadItem() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<int>(3));
            Assert.Throws<InvalidOperationException>(() => array.Add(new NbtPrimitive<double>(7.0)));
        }

        [Fact]
        public void AddFailsForBadItemInTypedArray() {
            var array = new NbtArray(NbtArray.ArrayType.Byte);
            Assert.Throws<InvalidOperationException>(() => array.Add(new NbtPrimitive<int>(7)));
        }

        [Fact]
        public void CanInsertSameTypeItems() {
            var array = new NbtArray();
            array.Insert(0, new NbtPrimitive<int>(3));
            array.Insert(0, new NbtPrimitive<int>(7));
        }

        [Fact]
        public void InsertFailsForSecondBadItem() {
            var array = new NbtArray();
            array.Insert(0, new NbtPrimitive<int>(3));
            Assert.Throws<InvalidOperationException>(() => array.Insert(0, new NbtPrimitive<double>(7.0)));
        }

        [Fact]
        public void InsertFailsForBadItemInTypedArray() {
            var array = new NbtArray(NbtArray.ArrayType.Byte);
            Assert.Throws<InvalidOperationException>(() => array.Insert(0, new NbtPrimitive<int>(7)));
        }

        [Fact]
        public void ContainsItemReturnsTrueForSame() {
            var array = new NbtArray();
            var item = new NbtPrimitive<short>(155);
            array.Add(item);
            Assert.True(array.ContainsItem(item));
        }

        [Fact]
        public void ContainsItemReturnsFalseForEqual() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<short>(155));
            Assert.False(array.ContainsItem(new NbtPrimitive<short>(155)));
        }

        [Fact]
        public void ContainsItemReturnsFalseForDifferentValue() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<short>(155));
            Assert.False(array.ContainsItem(new NbtPrimitive<short>(12)));
        }

        [Fact]
        public void ContainsItemReturnsFalseForDifferentType() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<short>(155));
            Assert.False(array.ContainsItem(new NbtPrimitive<string>("test")));
        }

        [Fact]
        public void IndexOfItemReturnsForSame() {
            var array = new NbtArray();
            var item = new NbtPrimitive<short>(155);
            array.Add(new NbtPrimitive<short>(123));
            array.Add(item);
            Assert.Equal(1, array.IndexOfItem(item));
        }

        [Fact]
        public void IndexOfItemFailsForEqual() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<short>(123));
            array.Add(new NbtPrimitive<short>(155));
            Assert.Equal(-1, array.IndexOfItem(new NbtPrimitive<short>(155)));
        }

        [Fact]
        public void IndexOfItemFailsForDifferentValue() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<short>(123));
            array.Add(new NbtPrimitive<short>(155));
            Assert.Equal(-1, array.IndexOfItem(new NbtPrimitive<short>(12)));
        }

        [Fact]
        public void IndexOfItemFailsForDifferentType() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<short>(123));
            array.Add(new NbtPrimitive<short>(155));
            Assert.Equal(-1, array.IndexOfItem(new NbtPrimitive<string>("test")));
        }

        [Fact]
        public void ContainsValueReturnsTrueForSame() {
            var array = new NbtArray();
            var item = new NbtPrimitive<short>(155);
            array.Add(item);
            Assert.True(array.ContainsValue<short>(155));
        }

        [Fact]
        public void ContainsValueReturnsFalseForDifferentValue() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<short>(155));
            Assert.False(array.ContainsValue(12));
        }

        [Fact]
        public void ContainsValueReturnsFalseForDifferentType() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<short>(155));
            Assert.False(array.ContainsValue("test"));
        }

        [Fact]
        public void IndexOfValueReturnsForSame() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<short>(123));
            array.Add(new NbtPrimitive<short>(155));
            array.Add(new NbtPrimitive<short>(155));
            Assert.Equal(1, array.IndexOfValue(155));
        }

        [Fact]
        public void IndexOfValueFailsForDifferentValue() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<short>(123));
            array.Add(new NbtPrimitive<short>(155));
            Assert.Equal(-1, array.IndexOfValue(12));
        }

        [Fact]
        public void IndexOfValueFailsForDifferentType() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<short>(123));
            array.Add(new NbtPrimitive<short>(155));
            Assert.Equal(-1, array.IndexOfValue("test"));
        }

        [Fact]
        public void CountWorks() {
            var array = new NbtArray();
            Assert.Equal(0, array.Count);
            array.Add(new NbtPrimitive<int>(3));
            Assert.Equal(1, array.Count);
        }

        [Fact]
        public void ContainsItemReturnsTrueForSameItem() {

            var array = new NbtArray();
            var item = new NbtPrimitive<int>(3);
            array.Add(item);
            Assert.True(array.ContainsItem(item));
        }

        [Fact]
        public void ContainsItemReturnsFalseForEqualItem() {
            var array = new NbtArray();
            var item = new NbtPrimitive<int>(3);
            array.Add(item);
            Assert.False(array.ContainsItem(new NbtPrimitive<int>(3)));
        }

        [Fact]
        public void IndexOfItemReturnsForSameItem() {
            var array = new NbtArray();
            var item = new NbtPrimitive<int>(3);
            array.Add(item);
            Assert.Equal(0, array.IndexOfItem(item));
        }

        [Fact]
        public void IndexOfItemFailsForEqualItem() {
            var array = new NbtArray();
            var item = new NbtPrimitive<int>(3);
            array.Add(item);
            Assert.Equal(-1, array.IndexOfItem(new NbtPrimitive<int>(3)));
        }

        [Fact]
        public void ItemsAreReturnsTrueForMatchingClass() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<int>(3));
            Assert.True(array.ItemsAre<NbtPrimitive<int>>());
            Assert.True(array.ItemsAre<NbtPrimitive>());
            Assert.True(array.ItemsAre<NbtTag>());
        }
        [Fact]
        public void ItemsAreReturnsFalseForNonMatchingClass() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<int>(3));
            Assert.False(array.ItemsAre<NbtPrimitive<string>>());
            Assert.False(array.ItemsAre<NbtArray>());
        }
        [Fact]
        public void ItemsAsReturnsForMatchingValueType() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<int>(3));
            array.Add(new NbtPrimitive<int>(7));
            array.Add(new NbtPrimitive<int>(8));
            Assert.Equal(3, array.ItemsAs<NbtPrimitive>().Count());
        }
        [Fact]
        public void ItemsAsFailsForNonMatchingValueType() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<int>(3));
            array.Add(new NbtPrimitive<int>(7));
            array.Add(new NbtPrimitive<int>(8));
            Assert.Throws<InvalidCastException>(() => array.ItemsAs<NbtCompound>());
        }
        [Fact]
        public void TryItemsAsReturnsTrueForMatchingClass() {
            var array = new NbtArray();
            var item = new NbtPrimitive<int>(3);
            array.Add(item);
            Assert.True(array.TryItemsAs<NbtPrimitive<int>>(out var v1));
            Assert.Equal(item, v1[0]);
            Assert.True(array.TryItemsAs<NbtPrimitive>(out var v2));
            Assert.Equal(item, v2[0]);
            Assert.True(array.TryItemsAs<NbtTag>(out var v3));
            Assert.Equal(item, v3[0]);
        }
        [Fact]
        public void TryItemsAsReturnsFalseForNonMatchingClass() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<int>(3));
            Assert.False(array.TryItemsAs<NbtPrimitive<string>>(out _));
            Assert.False(array.TryItemsAs<NbtArray>(out _));
        }
        [Fact]
        public void ValuesAsReturnsForMatchingValueType() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<int>(3));
            array.Add(new NbtPrimitive<int>(7));
            array.Add(new NbtPrimitive<int>(8));
            Assert.Equal(3, array.ValuesAs<long>().Count());
        }
        [Fact]
        public void ValuesAsFailsForNonMatchingValueType() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<int>(3));
            array.Add(new NbtPrimitive<int>(7));
            array.Add(new NbtPrimitive<int>(8));
            Assert.Throws<InvalidCastException>(() => array.ValuesAs<string>());
        }
        [Fact]
        public void TryValueAsReturnsTrueForMatchingValueType() {
            var array = new NbtArray();
            var item = new NbtPrimitive<int>(3);
            array.Add(item);
            Assert.True(array.TryValuesAs<long>(out var v1));
            Assert.Equal(3, v1[0]);
            Assert.True(array.TryValuesAs<double>(out var v2));
            Assert.Equal(3.0, v2[0]);
            Assert.True(array.TryValuesAs<bool>(out var v3));
            Assert.True(v3[0]);
        }
        [Fact]
        public void TryValueAsReturnsFalseForNonMatchingValueType() {
            var array = new NbtArray();
            array.Add(new NbtPrimitive<int>(3));
            Assert.False(array.TryValuesAs<string>(out _));
        }
    }
}
