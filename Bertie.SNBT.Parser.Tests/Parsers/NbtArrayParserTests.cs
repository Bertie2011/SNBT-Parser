using Bertie.SNBT.Parser.NBT;
using Bertie.SNBT.Parser.Parsers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Bertie.SNBT.Parser.Tests.Parsers {
    public class NbtArrayParserTests {
        [Fact]
        public void EmptyStringResultsInException() {
            var parser = new NbtArrayParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("  "));
        }
        [Fact]
        public void OnlyOpenBracketResultsInException() {
            var parser = new NbtArrayParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("["));
        }
        [Fact]
        public void EmptyArrayResultsInNbtTagArray() {
            var parser = new NbtArrayParser();
            var pos = 0;
            var nbt = "[   ]xxx";
            var array = parser.Parse(nbt, ref pos);
            Assert.IsType<NbtArray<NbtTag>>(array);
            Assert.Equal(0, array.Count);
            Assert.Equal(nbt.Length - 3, pos);
        }
        [Fact]
        public void SingleItemArrayResultsInNbtTagArray() {
            var parser = new NbtArrayParser();
            var pos = 0;
            var nbt = "[ asdfasf]xxx";
            var array = parser.Parse(nbt, ref pos);
            Assert.IsType<NbtArray<NbtTag>>(array);
            Assert.Equal(1, array.Count);
            Assert.True(array.TryItemAs(0, out NbtPrimitive item) && item.ValueEquals("asdfasf"));
            Assert.Equal(nbt.Length - 3, pos);
        }
        [Fact]
        public void SingleItemNotEndingArrayResultsInException() {
            var parser = new NbtArrayParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("[ 2.0f"));
        }
        [Fact]
        public void SingleItemBadEndingArrayResultsInException() {
            var parser = new NbtArrayParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("[ 2.0f#"));
        }
        [Fact]
        public void EmptyBArrayResultsInSByteArray() {
            var parser = new NbtArrayParser();
            var pos = 0;
            var nbt = "[B;]xxx";
            var array = parser.Parse(nbt, ref pos);
            Assert.IsType<NbtArray<NbtPrimitive<sbyte>>>(array);
            Assert.Equal(0, array.Count);
            Assert.Equal(nbt.Length - 3, pos);
        }
        [Fact]
        public void EmptyIArrayResultsInIntegerArray() {
            var parser = new NbtArrayParser();
            var pos = 0;
            var nbt = "[I;   ]xxx";
            var array = parser.Parse(nbt, ref pos);
            Assert.IsType<NbtArray<NbtPrimitive<int>>>(array);
            Assert.Equal(0, array.Count);
            Assert.Equal(nbt.Length - 3, pos);
        }
        [Fact]
        public void EmptyLArrayResultsInLongArray() {
            var parser = new NbtArrayParser();
            var pos = 0;
            var nbt = "[  L;]xxx";
            var array = parser.Parse(nbt, ref pos);
            Assert.IsType<NbtArray<NbtPrimitive<long>>>(array);
            Assert.Equal(0, array.Count);
            Assert.Equal(nbt.Length - 3, pos);
        }
        [Fact]
        public void EmptArrayOfBadTypeResultsInException() {
            var parser = new NbtArrayParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("[  X;]"));
        }
        [Fact]
        public void EmptyNotEndingBArrayResultsInException() {
            var parser = new NbtArrayParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("[B;   "));
        }
        [Fact]
        public void SingleItemNotEndingBArrayResultsInException() {
            var parser = new NbtArrayParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("[B;125b"));
        }
        [Fact]
        public void SingleItemBadEndingBArrayResultsInException() {
            var parser = new NbtArrayParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("[B;125b#"));
        }
        [Fact]
        public void BadItemInBArrayResultsInException() {
            var parser = new NbtArrayParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("[B;125"));
        }
        [Fact]
        public void FilledArrayResultsInNbtTagArray() {
            var parser = new NbtArrayParser();
            var pos = 0;
            var nbt = "[125, 25b   ,   99bx  , true  , 4.3d, 512.5f, heythere, \"Hello World!]\"]xxx";
            var array = parser.Parse(nbt, ref pos);
            Assert.IsType<NbtArray<NbtTag>>(array);
            Assert.Equal(8, array.Count);
            Assert.True(array.TryItemAs<NbtPrimitive<int>>(0, out var item0) && item0.ValueEquals(125));
            Assert.True(array.TryItemAs<NbtPrimitive<sbyte>>(1, out var item1) && item1.ValueEquals((sbyte)25));
            Assert.True(array.TryItemAs<NbtPrimitive<string>>(2, out var item2) && item2.ValueEquals("99bx"));
            Assert.True(array.TryItemAs<NbtPrimitive<bool>>(3, out var item3) && item3.ValueEquals(true));
            Assert.True(array.TryItemAs<NbtPrimitive<double>>(4, out var item4) && item4.ValueEquals(4.3));
            Assert.True(array.TryItemAs<NbtPrimitive<float>>(5, out var item5) && item5.ValueEquals(512.5f));
            Assert.True(array.TryItemAs<NbtPrimitive<string>>(6, out var item6) && item6.ValueEquals("heythere"));
            Assert.True(array.TryItemAs<NbtPrimitive<string>>(7, out var item7) && item7.ValueEquals("Hello World!]"));
            Assert.Equal(nbt.Length - 3, pos);
        }
        [Fact]
        public void EmptyItemResultsInException() {
            var parser = new NbtArrayParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("[asdf,,dfsf]"));
        }
        [Fact]
        public void FilledBArrayResultsInSByteArray() {
            var parser = new NbtArrayParser();
            var pos = 0;
            var nbt = "[B; 19b, -42b, 100b, 0b]xxx";
            var array = parser.Parse(nbt, ref pos);
            Assert.IsType<NbtArray<NbtPrimitive<sbyte>>>(array);
            Assert.Equal(4, array.Count);
            Assert.True(array.TryItemAs<NbtPrimitive<sbyte>>(0, out var item0) && item0.ValueEquals(19));
            Assert.True(array.TryItemAs<NbtPrimitive<sbyte>>(1, out var item1) && item1.ValueEquals(-42));
            Assert.True(array.TryItemAs<NbtPrimitive<sbyte>>(2, out var item2) && item2.ValueEquals(100));
            Assert.True(array.TryItemAs<NbtPrimitive<sbyte>>(3, out var item3) && item3.ValueEquals(0));
            Assert.Equal(nbt.Length - 3, pos);
        }
        [Fact]
        public void FilledIArrayResultsInIntArray() {
            var parser = new NbtArrayParser();
            var pos = 0;
            var nbt = "[I; 19, -42, 100, 0]xxx";
            var array = parser.Parse(nbt, ref pos);
            Assert.IsType<NbtArray<NbtPrimitive<int>>>(array);
            Assert.Equal(4, array.Count);
            Assert.True(array.TryItemAs<NbtPrimitive<int>>(0, out var item0) && item0.ValueEquals(19));
            Assert.True(array.TryItemAs<NbtPrimitive<int>>(1, out var item1) && item1.ValueEquals(-42));
            Assert.True(array.TryItemAs<NbtPrimitive<int>>(2, out var item2) && item2.ValueEquals(100));
            Assert.True(array.TryItemAs<NbtPrimitive<int>>(3, out var item3) && item3.ValueEquals(0));
            Assert.Equal(nbt.Length - 3, pos);
        }
        [Fact]
        public void FilledLArrayResultsInLongArray() {
            var parser = new NbtArrayParser();
            var pos = 0;
            var nbt = "[L; 19l, -42l, 100l, 0l]xxx";
            var array = parser.Parse(nbt, ref pos);
            Assert.IsType<NbtArray<NbtPrimitive<long>>>(array);
            Assert.Equal(4, array.Count);
            Assert.True(array.TryItemAs<NbtPrimitive<long>>(0, out var item0) && item0.ValueEquals(19));
            Assert.True(array.TryItemAs<NbtPrimitive<long>>(1, out var item1) && item1.ValueEquals(-42));
            Assert.True(array.TryItemAs<NbtPrimitive<long>>(2, out var item2) && item2.ValueEquals(100));
            Assert.True(array.TryItemAs<NbtPrimitive<long>>(3, out var item3) && item3.ValueEquals(0));
            Assert.Equal(nbt.Length - 3, pos);
        }
    }
}
