using Bertie.SNBT.Parser.NBT;
using Bertie.SNBT.Parser.Parsers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Bertie.SNBT.Parser.Tests.Parsers {
    public class NbtCompoundParserTests {
        [Fact]
        public void EmptyStringThrowsException() {
            var parser = new NbtCompoundParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("  "));
        }

        [Fact]
        public void BadStartCharThrowsException() {
            var parser = new NbtCompoundParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("  [ ]"));
        }

        [Fact]
        public void NoKeyThrowsException() {
            var parser = new NbtCompoundParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("  {"));
        }

        [Fact]
        public void NoColumnThrowsException() {
            var parser = new NbtCompoundParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("  {key"));
        }

        [Fact]
        public void BadColumnThrowsException() {
            var parser = new NbtCompoundParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("  {key#"));
        }

        [Fact]
        public void NoValueThrowsException() {
            var parser = new NbtCompoundParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("  {key:"));
        }

        [Fact]
        public void NoSeparatorThrowsException() {
            var parser = new NbtCompoundParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("  {key:value"));
        }

        [Fact]
        public void BadSeparatorThrowsException() {
            var parser = new NbtCompoundParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("  {key:value#"));
        }

        [Fact]
        public void EmptyCompoundResultsInCompound() {
            var parser = new NbtCompoundParser();
            var nbt = "   {}xxx";
            var pos = 0;
            var result = parser.Parse(nbt, ref pos).As<NbtCompound>();
            Assert.Equal(0, result.Count);
            Assert.Equal(nbt.Length - 3, pos);
        }

        [Fact]
        public void SimpleKeyCompoundResultsInCompound() {
            var parser = new NbtCompoundParser();
            var nbt = "   {test:20.0d, key : true , smth: \"  abc  \"}xxx";
            var pos = 0;
            var result = parser.Parse(nbt, ref pos).As<NbtCompound>();
            Assert.Equal(3, result.Count);
            Assert.Equal(20.0, result.ValueAs<double>("test"));
            Assert.True(result.ValueAs<bool>("key"));
            Assert.Equal("  abc  ", result.ValueAs<string>("smth"));
            Assert.Equal(nbt.Length - 3, pos);
        }

        [Fact]
        public void ComplexKeyCompoundResultsInCompound() {
            var parser = new NbtCompoundParser();
            var key = "!@#$%^&*()=';:<[}]{>";
            var nbt = $"   {{   \"{key}\" : 151215L}}xxx";
            var pos = 0;
            var result = parser.Parse(nbt, ref pos).As<NbtCompound>();
            Assert.Equal(1, result.Count);
            Assert.Equal(151215, result.ValueAs<long>(key));
            Assert.Equal(nbt.Length - 3, pos);
        }

        [Fact]
        public void ArrayValueCompoundResultsInCompound() {
            var parser = new NbtCompoundParser();
            var nbt = "   {test: [ 20.0d, 542.5d , 897.234d]}xxx";
            var pos = 0;
            var result = parser.Parse(nbt, ref pos).As<NbtCompound>().ItemAs<NbtArray>("test").ValuesAs<double>();
            Assert.Equal(3, result.Count);
            Assert.Equal(20.0, result[0]);
            Assert.Equal(542.5, result[1]);
            Assert.Equal(897.234, result[2]);
            Assert.Equal(nbt.Length - 3, pos);
        }
    }
}
