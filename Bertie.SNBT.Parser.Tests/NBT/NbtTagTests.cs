using Bertie.SNBT.Parser.NBT;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Bertie.SNBT.Parser.Tests.NBT {
    public class NbtTagTests {
        [Fact]
        public void IsReturnsTrueForEqualTypes() {
            NbtTag tag = new NbtPrimitive<int>(3);
            Assert.True(tag.Is<NbtPrimitive<int>>());
        }

        [Fact]
        public void IsReturnsTrueForParentType() {
            NbtTag tag = new NbtPrimitive<int>(3);
            Assert.True(tag.Is<NbtPrimitive>());
        }

        [Fact]
        public void IsReturnsFalseForNonEqualTypes() {
            NbtTag tag = new NbtPrimitive<int>(3);
            Assert.False(tag.Is<NbtCompound>());
        }

        [Fact]
        public void AsReturnsForEqualTypes() {
            NbtTag tag = new NbtPrimitive<int>(3);
            var r = tag.As<NbtPrimitive<int>>();
        }

        [Fact]
        public void AsReturnsForParentTypes() {
            NbtTag tag = new NbtPrimitive<int>(3);
            var r = tag.As<NbtPrimitive>();
        }

        [Fact]
        public void AsThrowsForNonEqualTypes() {
            NbtTag tag = new NbtPrimitive<int>(3);
            Assert.Throws<InvalidCastException>(() => tag.As<NbtCompound>());
        }

        [Fact]
        public void TryAsReturnsTrueForEqualTypes() {
            NbtTag tag = new NbtPrimitive<int>(3);
            Assert.True(tag.TryAs<NbtPrimitive<int>>(out var r));
            Assert.Equal(3, r.ValueAs<int>());
        }

        [Fact]
        public void TryAsReturnsTrueForParentType() {
            NbtTag tag = new NbtPrimitive<int>(3);
            Assert.True(tag.TryAs<NbtPrimitive>(out var r));
            Assert.Equal(3, r.ValueAs<int>());
        }

        [Fact]
        public void TryAsReturnsFalseForNonEqualTypes() {
            NbtTag tag = new NbtPrimitive<int>(3);
            Assert.False(tag.TryAs<NbtCompound>(out var r));
        }
    }
}
