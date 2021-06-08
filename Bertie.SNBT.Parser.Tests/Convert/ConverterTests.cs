using Bertie.SNBT.Parser.Convert;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Bertie.SNBT.Parser.Tests.Convert {
    public class ConverterTests {

        [Fact]
        public void DefaultSByteToBool() {
            Assert.True(Converter.Default.TryConvert<bool>((sbyte)1, out var r1));
            Assert.True(r1);
            Assert.True(Converter.Default.TryConvert<bool>((sbyte)0, out var r2));
            Assert.False(r2);
            Assert.True(Converter.Default.TryConvert<bool>((sbyte)-1, out var r3));
            Assert.True(r3);
        }

        [Fact]
        public void DefaultShortToBool() {
            Assert.True(Converter.Default.TryConvert<bool>((short)1, out var r1));
            Assert.True(r1);
            Assert.True(Converter.Default.TryConvert<bool>((short)0, out var r2));
            Assert.False(r2);
            Assert.True(Converter.Default.TryConvert<bool>((short)-1, out var r3));
            Assert.True(r3);
        }

        [Fact]
        public void DefaultIntToBool() {
            Assert.True(Converter.Default.TryConvert<bool>(1, out var r1));
            Assert.True(r1);
            Assert.True(Converter.Default.TryConvert<bool>(0, out var r2));
            Assert.False(r2);
            Assert.True(Converter.Default.TryConvert<bool>(-1, out var r3));
            Assert.True(r3);
        }

        [Fact]
        public void DefaultLongToBool() {
            Assert.True(Converter.Default.TryConvert<bool>(1L, out var r1));
            Assert.True(r1);
            Assert.True(Converter.Default.TryConvert<bool>(0L, out var r2));
            Assert.False(r2);
            Assert.True(Converter.Default.TryConvert<bool>(-1L, out var r3));
            Assert.True(r3);
        }

        [Fact]
        public void DefaultBoolToSByte() {
            Assert.True(Converter.Default.TryConvert<sbyte>(true, out var r));
            Assert.Equal((sbyte)1, r);
        }

        [Fact]
        public void DefaultShortToSByte() {
            Assert.True(Converter.Default.TryConvert<sbyte>((short)124, out var r));
            Assert.Equal((sbyte)124, r);
        }

        [Fact]
        public void DefaultIntToSByte() {
            Assert.True(Converter.Default.TryConvert<sbyte>(124, out var r));
            Assert.Equal((sbyte)124, r);
        }

        [Fact]
        public void DefaultLongToSByte() {
            Assert.True(Converter.Default.TryConvert<sbyte>(124L, out var r));
            Assert.Equal((sbyte)124, r);
        }

        [Fact]
        public void DefaultBoolToShort() {
            Assert.True(Converter.Default.TryConvert<short>(true, out var r));
            Assert.Equal((short)1, r);
        }

        [Fact]
        public void DefaultSByteToShort() {
            Assert.True(Converter.Default.TryConvert<short>((sbyte)124, out var r));
            Assert.Equal((short)124, r);
        }

        [Fact]
        public void DefaultIntToShort() {
            Assert.True(Converter.Default.TryConvert<short>(124, out var r));
            Assert.Equal((short)124, r);
        }

        [Fact]
        public void DefaultLongToShort() {
            Assert.True(Converter.Default.TryConvert<short>(124L, out var r));
            Assert.Equal((short)124, r);
        }

        [Fact]
        public void DefaultBoolToInt() {
            Assert.True(Converter.Default.TryConvert<int>(true, out var r));
            Assert.Equal(1, r);
        }

        [Fact]
        public void DefaultSByteToInt() {
            Assert.True(Converter.Default.TryConvert<int>((sbyte)124, out var r));
            Assert.Equal(124, r);
        }

        [Fact]
        public void DefaultShortToInt() {
            Assert.True(Converter.Default.TryConvert<int>((short)124, out var r));
            Assert.Equal(124, r);
        }

        [Fact]
        public void DefaultLongToInt() {
            Assert.True(Converter.Default.TryConvert<int>(124L, out var r));
            Assert.Equal(124, r);
        }

        [Fact]
        public void DefaultBoolToLong() {
            Assert.True(Converter.Default.TryConvert<long>(true, out var r));
            Assert.Equal(1L, r);
        }

        [Fact]
        public void DefaultSByteToLong() {
            Assert.True(Converter.Default.TryConvert<long>((sbyte)124, out var r));
            Assert.Equal(124L, r);
        }

        [Fact]
        public void DefaultShortToLong() {
            Assert.True(Converter.Default.TryConvert<long>((short)124, out var r));
            Assert.Equal(124L, r);
        }

        [Fact]
        public void DefaultIntToLong() {
            Assert.True(Converter.Default.TryConvert<long>(124, out var r));
            Assert.Equal(124L, r);
        }

        [Fact]
        public void DefaultBoolToFloat() {
            Assert.True(Converter.Default.TryConvert<float>(true, out var r));
            Assert.Equal(1.0F, r);
        }

        [Fact]
        public void DefaultSByteToFloat() {
            Assert.True(Converter.Default.TryConvert<float>((sbyte)124, out var r));
            Assert.Equal(124.0F, r);
        }

        [Fact]
        public void DefaultShortToFloat() {
            Assert.True(Converter.Default.TryConvert<float>((short)124, out var r));
            Assert.Equal(124.0F, r);
        }

        [Fact]
        public void DefaultIntToFloat() {
            Assert.True(Converter.Default.TryConvert<float>(124, out var r));
            Assert.Equal(124.0F, r);
        }

        [Fact]
        public void DefaultLongToFloat() {
            Assert.True(Converter.Default.TryConvert<float>(124L, out var r));
            Assert.Equal(124.0F, r);
        }

        [Fact]
        public void DefaultDoubleToFloat() {
            Assert.True(Converter.Default.TryConvert<float>(124.4, out var r));
            Assert.Equal(124.4F, r);
        }

        [Fact]
        public void DefaultBoolToDouble() {
            Assert.True(Converter.Default.TryConvert<double>(true, out var r));
            Assert.Equal(1.0, r);
        }

        [Fact]
        public void DefaultSByteToDouble() {
            Assert.True(Converter.Default.TryConvert<double>((sbyte)124, out var r));
            Assert.Equal(124.0, r);
        }

        [Fact]
        public void DefaultShortToDouble() {
            Assert.True(Converter.Default.TryConvert<double>((short)124, out var r));
            Assert.Equal(124.0, r);
        }

        [Fact]
        public void DefaultIntToDouble() {
            Assert.True(Converter.Default.TryConvert<double>(124, out var r));
            Assert.Equal(124.0, r);
        }

        [Fact]
        public void DefaultLongToDouble() {
            Assert.True(Converter.Default.TryConvert<double>(124L, out var r));
            Assert.Equal(124.0, r);
        }

        [Fact]
        public void DefaultFloatToDouble() {
            Assert.True(Converter.Default.TryConvert<double>(124.4F, out var r));
            Assert.Equal(124.4, Math.Round(r, 1));
        }
    }
}
