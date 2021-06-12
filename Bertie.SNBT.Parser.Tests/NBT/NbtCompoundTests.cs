using Bertie.SNBT.Parser.NBT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Bertie.SNBT.Parser.Tests.NBT {
    public class NbtCompoundTests {
        [Fact]
        public void KeyPropertyReturnsAllKeys() {
            var compound = new NbtCompound();
            compound.Add("abc", new NbtPrimitive<int>(1));
            compound.Add("xyz", new NbtPrimitive<int>(1));
            compound.Add("rst", new NbtPrimitive<int>(1));
            Assert.Equal(3, compound.Keys.Count);
            Assert.True(compound.Keys.Contains("abc"));
            Assert.True(compound.Keys.Contains("xyz"));
            Assert.True(compound.Keys.Contains("rst"));
        }

        [Fact]
        public void CountReturnsCount() {
            var compound = new NbtCompound();
            Assert.Equal(0, compound.Keys.Count);
            compound.Add("abc", new NbtPrimitive<int>(1));
            compound.Add("xyz", new NbtPrimitive<int>(1));
            compound.Add("rst", new NbtPrimitive<int>(1));
            Assert.Equal(3, compound.Keys.Count);
        }

        [Fact]
        public void ClearEmptiesCompound() {
            var compound = new NbtCompound();
            compound.Add("abc", new NbtPrimitive<int>(1));
            compound.Add("xyz", new NbtPrimitive<int>(1));
            compound.Add("rst", new NbtPrimitive<int>(1));
            Assert.Equal(3, compound.Keys.Count);
            compound.Clear();
            Assert.Equal(0, compound.Keys.Count);
        }

        [Fact]
        public void ContainsKeyReturnsTrueIfPresent() {
            var compound = new NbtCompound();
            compound.Add("abc", new NbtPrimitive<int>(1));
            Assert.True(compound.ContainsKey("abc"));
        }

        [Fact]
        public void ContainsKeyReturnsFalseIfNotPresent() {
            var compound = new NbtCompound();
            compound.Add("abc", new NbtPrimitive<int>(1));
            Assert.False(compound.ContainsKey("xyz"));
        }

        [Fact]
        public void RemoveReturnsTrueIfPresent() {
            var compound = new NbtCompound();
            compound.Add("abc", new NbtPrimitive<int>(1));
            Assert.True(compound.Remove("abc"));
            Assert.False(compound.ContainsKey("abc"));
        }

        [Fact]
        public void RemoveReturnsFalseIfNotPresent() {
            var compound = new NbtCompound();
            compound.Add("abc", new NbtPrimitive<int>(1));
            Assert.False(compound.Remove("xyz"));
            Assert.True(compound.ContainsKey("abc"));
        }

        [Fact]
        public void ItemIsReturnsTrueIfClassMatches() {
            var compound = new NbtCompound();
            compound.Add("test", new NbtPrimitive<double>(34.642));
            Assert.True(compound.ItemIs<NbtTag>("test"));
            Assert.True(compound.ItemIs<NbtPrimitive>("test"));
            Assert.True(compound.ItemIs<NbtPrimitive<double>>("test"));
        }

        [Fact]
        public void ItemIsReturnsFalseIfClassDoesNotMatch() {
            var compound = new NbtCompound();
            compound.Add("test", new NbtPrimitive<double>(34.642));
            Assert.False(compound.ItemIs<NbtCompound>("test"));
            Assert.False(compound.ItemIs<NbtArray>("test"));
            Assert.False(compound.ItemIs<NbtPrimitive<string>>("test"));
        }

        [Fact]
        public void ItemIsFailsIfKeyDoesNotExist() {
            var compound = new NbtCompound();
            compound.Add("test", new NbtPrimitive<double>(34.642));
            Assert.Throws<KeyNotFoundException>(() => compound.ItemIs<NbtTag>("xyz"));
        }

        [Fact]
        public void ItemAsReturnsIfClassMatches() {
            var compound = new NbtCompound();
            compound.Add("test", new NbtPrimitive<bool>(true));
            Assert.True(compound.ItemAs<NbtPrimitive>("test").ValueAs<bool>());
        }

        [Fact]
        public void ItemAsFailsIfClassDoesNotMatch() {
            var compound = new NbtCompound();
            compound.Add("test", new NbtPrimitive<bool>(true));
            Assert.Throws<InvalidCastException>(() => compound.ItemAs<NbtCompound>("test"));
        }

        [Fact]
        public void ItemAsFailsIfKeyDoesNotExist() {
            var compound = new NbtCompound();
            compound.Add("test", new NbtPrimitive<bool>(true));
            Assert.Throws<KeyNotFoundException>(() => compound.ItemAs<NbtCompound>("xyz"));
        }

        [Fact]
        public void TryItemAsReturnsTrueIfFoundAndConvertible() {
            var compound = new NbtCompound();
            compound.Add("xyz", new NbtPrimitive<string>("abc"));
            Assert.True(compound.TryItemAs<NbtPrimitive>("xyz", out var result));
            Assert.Equal("abc", result.ValueAs<string>());
        }

        [Fact]
        public void TryItemAsReturnsFalseIfNotFound() {
            var compound = new NbtCompound();
            compound.Add("xyz", new NbtPrimitive<string>("abc"));
            Assert.False(compound.TryItemAs<NbtPrimitive>("XXX", out _));
        }

        [Fact]
        public void TryItemAsFailsIfNotFoundAndRequired() {
            var compound = new NbtCompound();
            compound.Add("xyz", new NbtPrimitive<string>("abc"));
            Assert.Throws<KeyNotFoundException>(() => compound.TryItemAs<NbtPrimitive>("XXX", out _, true));
        }

        [Fact]
        public void TryItemAsReturnsFalseIfNotConvertible() {
            var compound = new NbtCompound();
            compound.Add("xyz", new NbtPrimitive<string>("abc"));
            Assert.False(compound.TryItemAs<NbtArray>("xyz", out _));
        }

        [Fact]
        public void TryItemAsFailsIfNotConvertibleAndRequired() {
            var compound = new NbtCompound();
            compound.Add("xyz", new NbtPrimitive<string>("abc"));
            Assert.Throws<InvalidCastException>(() => compound.TryItemAs<NbtCompound>("xyz", out _, false, true));
        }

        [Fact]
        public void ValueAsReturnsIfValueTypeMatches() {
            var compound = new NbtCompound();
            compound.Add("test", new NbtPrimitive<bool>(true));
            Assert.True(compound.ValueAs<bool>("test"));
        }

        [Fact]
        public void ValueAsFailsIfValueTypeDoesNotMatch() {
            var compound = new NbtCompound();
            compound.Add("test", new NbtPrimitive<bool>(true));
            Assert.Throws<InvalidCastException>(() => compound.ValueAs<string>("test"));
        }

        [Fact]
        public void ValueAsFailsIfKeyDoesNotExist() {
            var compound = new NbtCompound();
            compound.Add("test", new NbtPrimitive<bool>(true));
            Assert.Throws<KeyNotFoundException>(() => compound.ValueAs<bool>("xyz"));
        }

        [Fact]
        public void TryValueAsReturnsTrueIfFoundAndConvertible() {
            var compound = new NbtCompound();
            compound.Add("xyz", new NbtPrimitive<string>("abc"));
            Assert.True(compound.TryValueAs<string>("xyz", out var result));
            Assert.Equal("abc", result);
        }

        [Fact]
        public void TryValueAsReturnsFalseIfNotFound() {
            var compound = new NbtCompound();
            compound.Add("xyz", new NbtPrimitive<string>("abc"));
            Assert.False(compound.TryValueAs<string>("XXX", out _));
        }

        [Fact]
        public void TryValueAsFailsIfNotFoundAndRequired() {
            var compound = new NbtCompound();
            compound.Add("xyz", new NbtPrimitive<string>("abc"));
            Assert.Throws<KeyNotFoundException>(() => compound.TryValueAs<string>("XXX", out _, true));
        }

        [Fact]
        public void TryValueAsReturnsFalseIfNotConvertible() {
            var compound = new NbtCompound();
            compound.Add("xyz", new NbtPrimitive<string>("abc"));
            Assert.False(compound.TryValueAs<bool>("xyz", out _));
        }

        [Fact]
        public void TryValueAsFailsIfNotConvertibleAndRequired() {
            var compound = new NbtCompound();
            compound.Add("xyz", new NbtPrimitive<string>("abc"));
            Assert.Throws<InvalidCastException>(() => compound.TryValueAs<bool>("xyz", out _, false, true));
        }

        [Fact]
        public void ItemsAsCanBeIterated() {
            var compound = new NbtCompound();
            compound.Add("xyz", new NbtPrimitive<double>(234.324d));
            compound.Add("test", new NbtPrimitive<double>(987.563d));
            compound.Add("abc", new NbtPrimitive<double>(656.098d));
            Assert.Equal(3, compound.ItemsAs<NbtPrimitive>().Count());
        }

        [Fact]
        public void ItemsAsFailsForBadType() {
            var compound = new NbtCompound();
            compound.Add("xyz", new NbtPrimitive<double>(234.324d));
            compound.Add("test", new NbtCompound());
            compound.Add("abc", new NbtPrimitive<double>(656.098d));
            Assert.Throws<InvalidCastException>(() => compound.ItemsAs<NbtPrimitive>().Count());
        }

        [Fact]
        public void ValuesAsCanBeIterated() {
            var compound = new NbtCompound();
            compound.Add("xyz", new NbtPrimitive<double>(234.324d));
            compound.Add("test", new NbtPrimitive<double>(987.563d));
            compound.Add("abc", new NbtPrimitive<double>(656.098d));
            Assert.Equal(3, compound.ValuesAs<double>().Count());
        }

        [Fact]
        public void ValuesAsFailsForBadType() {
            var compound = new NbtCompound();
            compound.Add("xyz", new NbtPrimitive<double>(234.324d));
            compound.Add("test", new NbtPrimitive<string>("abc"));
            compound.Add("abc", new NbtPrimitive<double>(656.098d));
            Assert.Throws<InvalidCastException>(() => compound.ValuesAs<double>().Count());
        }
    }
}
