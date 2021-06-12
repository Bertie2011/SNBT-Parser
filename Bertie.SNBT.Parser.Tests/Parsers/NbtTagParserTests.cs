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
            var result = parser.Parse("[L;34634897L,6230948L,79823L]").As<NbtArray>().ValuesAs<long>();
            Assert.Equal(3, result.Count);
            Assert.Equal(34634897L, result[0]);
            Assert.Equal(6230948L, result[1]);
            Assert.Equal(79823L, result[2]);
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
            var nbt = "{compound:{\"name\":hello,value:2.43d, array :[ {value: 34b, value2: 12}, {Count:14s, Time:9999L }, {Motion:.5f}, {B:[B;34b],I:[I;645],L:[L;358L]} ]}, \"@$#()\": true}XXXXX";
            var pos = 0;
            var result = parser.Parse(nbt, ref pos).As<NbtCompound>();
            Assert.Equal("hello", result.ItemAs<NbtCompound>("compound").ValueAs<string>("name"));
            Assert.Equal(2.43, result.ItemAs<NbtCompound>("compound").ValueAs<double>("value"));
            var array = result.ItemAs<NbtCompound>("compound").ItemAs<NbtArray>("array").ItemsAs<NbtCompound>();
            Assert.Equal(34, array[0].ValueAs<sbyte>("value"));
            Assert.Equal(12, array[0].ValueAs<int>("value2"));
            Assert.Equal(14, array[1].ValueAs<short>("Count"));
            Assert.Equal(9999, array[1].ValueAs<long>("Time"));
            Assert.Equal(0.5, array[2].ValueAs<float>("Motion"));
            Assert.Equal(34, array[3].ItemAs<NbtArray>("B").ValuesAs<sbyte>()[0]);
            Assert.Equal(645, array[3].ItemAs<NbtArray>("I").ValuesAs<int>()[0]);
            Assert.Equal(358, array[3].ItemAs<NbtArray>("L").ValuesAs<long>()[0]);
            Assert.True(result.ValueAs<bool>("@$#()"));
            Assert.Equal(nbt.Length - 5, pos);
        }
    }
}
