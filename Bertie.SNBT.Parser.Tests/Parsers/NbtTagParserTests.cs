using Bertie.SNBT.Parser.NBT;
using Bertie.SNBT.Parser.Parsers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Bertie.SNBT.Parser.Tests.Parsers {
    public class NbtTagParserTests {
        [Fact]
        public void EmptyStringThrows() {
            var parser = new NbtTagParser();
            Assert.Throws<ArgumentException>(() => parser.Parse("     "));
        }
        [Fact]
        public void ArrayResultsInArray() {
            var parser = new NbtTagParser();
            var result = parser.Parse("[L;34634897L,6230948L,79823L]").As<NbtArray>();
            Assert.Equal(3, result.Count);
            Assert.Equal(34634897L, result.ValueAs<long>(0));
            Assert.Equal(6230948L, result.ValueAs<long>(1));
            Assert.Equal(79823L, result.ValueAs<long>(2));
        }
        [Fact]
        public void PrimitiveResultsInValue() {
            var parser = new NbtTagParser();
            var result = parser.Parse("352.09d").As<NbtPrimitive>();
            Assert.Equal(352.09, result.ValueAs<double>());
        }
        [Fact]
        public void CompoundResultsInCompound() {
            var parser = new NbtTagParser();
            var result = parser.Parse("{key: value}").As<NbtCompound>();
            Assert.Equal(1, result.Count);
            Assert.Equal("value", result.ValueAs<string>("key"));
        }
        [Fact]
        public void ComplexNbtGetsParsedCorrectly() {
            var parser = new NbtTagParser();
            var result = parser.Parse("{compound:{\"name\":hello,value:2.43d, array :[ {value: 34b, value2: 12}, {Count:14s, Time:9999L }, {Motion:.5f}, {B:[B;34b],I:[I;645],L:[L;358L]} ]}, \"@$#()\": true}XXXXX").As<NbtCompound>();
            //TODO: Check complex structure
        }
    }
}
