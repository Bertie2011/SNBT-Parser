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
            Assert.IsType<NbtArray>(array);
            Assert.Equal(0, array.Count);
            Assert.Equal(nbt.Length - 3, pos);
        }
        [Fact]
        public void SingleItemArrayResultsInNbtTagArray() {
            var parser = new NbtArrayParser();
            var pos = 0;
            var nbt = "[ asdfasf]xxx";
            var array = parser.Parse(nbt, ref pos);
            Assert.IsType<NbtArray>(array);
            Assert.Equal(1, array.Count);
            Assert.True(array.ItemsAs<NbtPrimitive>()[0].ValueEquals("asdfasf"));
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
            Assert.IsType<NbtArray>(array);
            Assert.Equal(0, array.Count);
            Assert.Equal(nbt.Length - 3, pos);
        }
        [Fact]
        public void EmptyIArrayResultsInIntegerArray() {
            var parser = new NbtArrayParser();
            var pos = 0;
            var nbt = "[I;   ]xxx";
            var array = parser.Parse(nbt, ref pos);
            Assert.IsType<NbtArray>(array);
            Assert.Equal(0, array.Count);
            Assert.Equal(nbt.Length - 3, pos);
        }
        [Fact]
        public void EmptyLArrayResultsInLongArray() {
            var parser = new NbtArrayParser();
            var pos = 0;
            var nbt = "[  L;]xxx";
            var array = parser.Parse(nbt, ref pos);
            Assert.IsType<NbtArray>(array);
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
        public void FilledArrayResultsInNbtTagArray() {
            var parser = new NbtArrayParser();
            var pos = 0;
            var nbt = "[lkajsdf, \"#$%$#@\", heythere, \"Hello World!]\"]xxx";
            var array = parser.Parse(nbt, ref pos);
            Assert.IsType<NbtArray>(array);
            Assert.True(array.TryValuesAs<string>(out var stringArray));
            Assert.Equal(4, array.Count);
            Assert.Equal("lkajsdf", stringArray[0]);
            Assert.Equal("#$%$#@", stringArray[1]);
            Assert.Equal("heythere", stringArray[2]);
            Assert.Equal("Hello World!]", stringArray[3]);
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
            Assert.IsType<NbtArray>(array);
            var byteArray = array.ValuesAs<sbyte>();
            Assert.Equal(4, array.Count);
            Assert.Equal(19, byteArray[0]);
            Assert.Equal(-42, byteArray[1]);
            Assert.Equal(100, byteArray[2]);
            Assert.Equal(0, byteArray[3]);
            Assert.Equal(nbt.Length - 3, pos);
        }
        [Fact]
        public void FilledIArrayResultsInIntArray() {
            var parser = new NbtArrayParser();
            var pos = 0;
            var nbt = "[I; 19, -42, 100, 0]xxx";
            var array = parser.Parse(nbt, ref pos);
            Assert.IsType<NbtArray>(array);
            var intArray = array.ValuesAs<int>();
            Assert.Equal(4, array.Count);
            Assert.Equal(19, intArray[0]);
            Assert.Equal(-42, intArray[1]);
            Assert.Equal(100, intArray[2]);
            Assert.Equal(0, intArray[3]);
            Assert.Equal(nbt.Length - 3, pos);
        }
        [Fact]
        public void FilledLArrayResultsInLongArray() {
            var parser = new NbtArrayParser();
            var pos = 0;
            var nbt = "[L; 19l, -42l, 100l, 0l]xxx";
            var array = parser.Parse(nbt, ref pos);
            Assert.IsType<NbtArray>(array);
            var longArray = array.ValuesAs<long>();
            Assert.Equal(4, array.Count);
            Assert.Equal(19, longArray[0]);
            Assert.Equal(-42, longArray[1]);
            Assert.Equal(100, longArray[2]);
            Assert.Equal(0, longArray[3]);
            Assert.Equal(nbt.Length - 3, pos);
        }
    }
}
