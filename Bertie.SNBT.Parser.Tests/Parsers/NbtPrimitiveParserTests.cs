using Bertie.SNBT.Parser.NBT;
using Bertie.SNBT.Parser.Parsers;
using System;
using Xunit;

namespace Bertie.SNBT.Parser.Tests.Parsers {
    public class NbtPrimitiveParserTests {
        [Fact]
        public void EmptyStringThrowsError() {
            int pos = 0;
            string nbt = @"    ";
            Assert.Throws<ArgumentException>(() => new NbtPrimitiveParser().Parse(nbt, ref pos));
        }

        [Fact]
        public void QuotedReturnsString() {
            int pos = 0;
            string nbt = @"""4""";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("4", result.ValueAs<string>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void SingleQuotedReturnsString() {
            int pos = 0;
            string nbt = @"'abcdef'";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("abcdef", result.ValueAs<string>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void QuotedEscapeReturnsCorrectString() {
            int pos = 0;
            string nbt = @"'abc\def'";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("abcdef", result.ValueAs<string>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void QuotedEscapedQuoteDoesNotTerminate() {
            int pos = 0;
            string nbt = @"""abc\""def""";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("abc\"def", result.ValueAs<string>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void SingleQuotedEscapedQuoteDoesNotTerminate() {
            int pos = 0;
            string nbt = @"'abc\'def'";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("abc'def", result.ValueAs<string>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void QuotedEscapedEndQuoteThrowsError() {
            int pos = 0;
            string nbt = @"'abcdef\'";
            Assert.Throws<ArgumentException>(() => new NbtPrimitiveParser().Parse(nbt, ref pos));
        }

        [Fact]
        public void QuotedDoubleEscapeQuoteEndsString() {
            int pos = 0;
            string nbt = @"'abcdef\\'";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("abcdef\\", result.ValueAs<string>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void TerminatorIsNextAfterQuoted() {
            int pos = 0;
            string nbt = @"'abcdef'next";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("abcdef", result.ValueAs<string>());
            Assert.Equal(nbt.Length - 4, pos);
            Assert.Equal('n', nbt[pos]);
        }

        [Fact]
        public void TerminatorIsNextAfterUnquoted() {
            int pos = 0;
            string nbt = @"abcdef,";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("abcdef", result.ValueAs<string>());
            Assert.Equal(nbt.Length - 1, pos);
            Assert.Equal(',', nbt[pos]);
        }

        [Fact]
        public void SpaceAllowedInQuoted() {
            int pos = 0;
            string nbt = @"'abc def'";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("abc def", result.ValueAs<string>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void SpaceEndsStringInUnquoted() {
            int pos = 0;
            string nbt = @"abc def";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("abc", result.ValueAs<string>());
            Assert.Equal(nbt.Length - 4, pos);
            Assert.Equal(' ', nbt[pos]);
        }

        [Fact]
        public void OpenBracketAllowedInQuoted() {
            int pos = 0;
            string nbt = @"'abc{def'";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("abc{def", result.ValueAs<string>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void OpenBracketEndsStringInUnquoted() {
            int pos = 0;
            string nbt = @"abc{def";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("abc", result.ValueAs<string>());
            Assert.Equal(nbt.Length - 4, pos);
            Assert.Equal('{', nbt[pos]);
        }

        [Fact]
        public void UnquotedResultsInString() {
            int pos = 0;
            string nbt = @"adfasdf235343ALKJIO";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("adfasdf235343ALKJIO", result.ValueAs<string>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void IntegerResultsInInteger() {
            int pos = 0;
            string nbt = @"5432345";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<int>>(result);
            Assert.Equal(5432345, result.ValueAs<int>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void TooBigIntegerResultsInString() {
            int pos = 0;
            string nbt = @"9999999999";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("9999999999", result.ValueAs<string>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void LongResultsInLong() {
            int pos = 0;
            string nbt = @"9999999999l";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<long>>(result);
            Assert.Equal(9999999999L, result.ValueAs<long>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void ByteResultsInByte() {
            int pos = 0;
            string nbt = @"-24b";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<sbyte>>(result);
            Assert.Equal(-24, result.ValueAs<sbyte>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void ShortResultsInShort() {
            int pos = 0;
            string nbt = @"543s";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<short>>(result);
            Assert.Equal(543, result.ValueAs<short>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void FloatResultsInFloat() {
            int pos = 0;
            string nbt = @"25.54f";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<float>>(result);
            Assert.Equal(25.54F, result.ValueAs<float>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void DoubleResultsInDouble() {
            int pos = 0;
            string nbt = @"25.54d";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<double>>(result);
            Assert.Equal(25.54, result.ValueAs<double>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void DoubleWithoutDotResultsInDouble() {
            int pos = 0;
            string nbt = @"6432d";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<double>>(result);
            Assert.Equal(6432, result.ValueAs<double>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void DoubleStartingWithDotResultsInDouble() {
            int pos = 0;
            string nbt = @".6432d";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<double>>(result);
            Assert.Equal(0.6432, result.ValueAs<double>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void DoubleEndingWithDotResultsInDouble() {
            int pos = 0;
            string nbt = @"235.d";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<double>>(result);
            Assert.Equal(235, result.ValueAs<double>());
            Assert.Equal(nbt.Length, pos);
        }


        [Fact]
        public void DoubleWithOnlyDotResultsInString() {
            int pos = 0;
            string nbt = @".d";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal(".d", result.ValueAs<string>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void ShortStartingWithDotResultsInString() {
            int pos = 0;
            string nbt = @".6543s";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal(".6543s", result.ValueAs<string>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void ShortEndingWithDotResultsInString() {
            int pos = 0;
            string nbt = @"2385.s";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("2385.s", result.ValueAs<string>());
            Assert.Equal(nbt.Length, pos);
        }


        [Fact]
        public void ShortWithOnlyDotResultsInString() {
            int pos = 0;
            string nbt = @".s";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal(".s", result.ValueAs<string>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void CapitalShortResultsInShort() {
            int pos = 0;
            string nbt = @"344S";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<short>>(result);
            Assert.Equal(344, result.ValueAs<short>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void ShortSuffixedResultsInString() {
            int pos = 0;
            string nbt = @"344sx";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("344sx", result.ValueAs<string>());
            Assert.Equal(nbt.Length, pos);
        }

        [Fact]
        public void TooBigWholeNumberResulstInString() {
            int pos = 0;
            string nbt = @"99999s";
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            Assert.IsType<NbtPrimitive<string>>(result);
            Assert.Equal("99999s", result.ValueAs<string>());
            Assert.Equal(nbt.Length, pos);
        }
    }
}
