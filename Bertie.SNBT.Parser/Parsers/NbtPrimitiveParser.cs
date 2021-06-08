using Bertie.SNBT.Parser.Convert;
using Bertie.SNBT.Parser.NBT;
using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Bertie.SNBT.Parser.Parsers {
    public class NbtPrimitiveParser : IStringParser<NbtPrimitive> {
        private static readonly char[] allowedUnquoted = new char[] {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '+', '-', '_', '.'
        };
        private static readonly char[] numberTypes = new char[] { 'b', 's', 'l', 'f', 'd' };

        /// <summary>
        /// Parses primitive stringified nbt. No whitespace around the value allowed.
        /// See also <see cref="NbtPrimitive"/> for the available types.
        /// </summary>
        /// <param name="nbt">The stringified nbt to parse.</param>
        /// <param name="pos">The starting position.</param>
        /// <returns>Returns the parsed primitive</returns>
        public NbtPrimitive Parse(string nbt, ref int pos) {
            char? quote = null;
            bool escape = false;
            bool quoteEnd = false;
            if (nbt[pos] == '"') quote = '"';
            else if (nbt[pos] == '\'') quote = '\'';

            var sb = new StringBuilder();

            if (quote != null) pos++;

            for (; pos < nbt.Length; pos++) {
                char c = nbt[pos];
                if (quote != null) {
                    if (!escape) {
                        if (c == quote) {
                            pos++;
                            quoteEnd = true;
                            break;
                        } else if (c == '\\') escape = true;
                        else sb.Append(c);
                    } else {
                        escape = false;
                        sb.Append(c);
                    }
                } else {
                    if (!allowedUnquoted.Contains(c)) break;
                    sb.Append(c);
                }
            }

            if (pos == nbt.Length && quote != null && !quoteEnd) throw new ArgumentException($"Quoted string is not terminated: {nbt}.");

            var raw = sb.ToString();
            if (quote != null) {
                return new NbtPrimitive<string>(raw);
            } else if (bool.TryParse(raw, out bool boolValue)) {
                return new NbtPrimitive<bool>(boolValue);
            } else if (char.ToLowerInvariant(raw[^1]) == 'b' && sbyte.TryParse(raw[..^1], NumberStyles.AllowLeadingSign, NumberFormatInfo.InvariantInfo, out var byteValue)) {
                return new NbtPrimitive<sbyte>(byteValue);
            } else if (char.ToLowerInvariant(raw[^1]) == 's' && short.TryParse(raw[..^1], NumberStyles.AllowLeadingSign, NumberFormatInfo.InvariantInfo, out var shortValue)) {
                return new NbtPrimitive<short>(shortValue);
            } else if (char.ToLowerInvariant(raw[^1]) == 'l' && long.TryParse(raw[..^1], NumberStyles.AllowLeadingSign, NumberFormatInfo.InvariantInfo, out var longValue)) {
                return new NbtPrimitive<long>(longValue);
            } else if (char.ToLowerInvariant(raw[^1]) == 'd' && double.TryParse(raw[..^1], NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out var doubleValue)) {
                return new NbtPrimitive<double>(doubleValue);
            } else if (char.ToLowerInvariant(raw[^1]) == 'f' && float.TryParse(raw[..^1], NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out var floatValue)) {
                return new NbtPrimitive<float>(floatValue);
            } else if (int.TryParse(raw, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out var intValue)) {
                return new NbtPrimitive<int>(intValue);
            } else {
                return new NbtPrimitive<string>(raw);
            }
        }
    }
}
