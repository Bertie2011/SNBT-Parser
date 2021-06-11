using Bertie.SNBT.Parser.NBT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bertie.SNBT.Parser.Parsers {
    public class NbtArrayParser : StringParser<NbtArray> {
        private NbtTagParser NbtTagParser { get; }

        public NbtArrayParser(NbtTagParser nbtTagParser = null) {
            NbtTagParser = nbtTagParser ?? new NbtTagParser();
        }

        public override NbtArray Parse(string nbt, ref int pos) {
            //Before array
            SkipWhitespace(nbt, ref pos);
            if (pos == nbt.Length || nbt[pos] != '[') throw new ArgumentException($"Expected start of array '[' at {pos}: {nbt}");
            pos++;

            //First character of array.
            SkipWhitespace(nbt, ref pos);
            if (pos == nbt.Length) throw new ArgumentException("Array never ends.");

            if (nbt[pos] == ']') {
                pos++;
                return new NbtArray<NbtTag>();
            }

            //Parse first value, check if separator is ; and create the correct type of array.
            var nbtValue = NbtTagParser.Parse(nbt, ref pos);
            if (pos == nbt.Length) throw new ArgumentException("Array never ends.");
            var separator = nbt[pos];
            pos++;
            if (separator == ';') {
                nbtValue.TryAs<NbtPrimitive<string>>(out var nbtStringValue);
                var stringValue = nbtStringValue?.ValueAs<string>();
                if (stringValue == "B") return CreateArray<NbtPrimitive<sbyte>>(nbt, ref pos);
                else if (stringValue == "I") return CreateArray<NbtPrimitive<int>>(nbt, ref pos);
                else if (stringValue == "L") return CreateArray<NbtPrimitive<long>>(nbt, ref pos);
                else throw new ArgumentException($"Array of unknown type at {pos}: {nbt}");
            } else if (separator == ',') {
                return CreateArray<NbtTag>(nbt, ref pos, nbtValue);
            } else if (separator == ']') {
                var result = new NbtArray<NbtTag>();
                result.Add(nbtValue);
                return result;
            } else {
                throw new ArgumentException($"Array contains unknown separator at {pos}: {nbt}");
            }
        }

        /// <summary>
        /// Creates array from nbt, where pos is at the start of the second element (possibly prefixed by whitespace).
        /// </summary>
        /// <typeparam name="T">The type of array to create</typeparam>
        /// <param name="nbt">The nbt to parse</param>
        /// <param name="pos">The pos located after the first item.</param>
        /// <param name="first">The first item to add to the array if necessary.</param>
        /// <returns>Returns a filled array.</returns>
        private NbtArray CreateArray<T>(string nbt, ref int pos, T first = null) where T : NbtTag {
            var result = new NbtArray<T>();
            if (first != null) result.Add(first);
            SkipWhitespace(nbt, ref pos);
            if (pos >= nbt.Length) throw new ArgumentException("Array never ends.");
            if (nbt[pos] == ']') {
                pos++;
                return result;
            }
            while (true) {
                //Parse element and skip whitespace after.
                var value = NbtTagParser.Parse(nbt, ref pos);
                if (value.TryAs<T>(out var typedValue)) {
                    result.Add(typedValue);
                } else {
                    throw new ArgumentException($"Value does not fit in array type {typeof(T).Name} at {pos}: {nbt}");
                }
                SkipWhitespace(nbt, ref pos);
                if (pos == nbt.Length) throw new ArgumentException("Array never ends.");

                //Process separator character
                if (nbt[pos] == ',') {
                    pos++;
                } else if (nbt[pos] == ']') {
                    pos++;
                    break;
                } else throw new ArgumentException($"Invalid character inside array at {pos}: {nbt}");

                //Skip whitespace after separator.
                SkipWhitespace(nbt, ref pos);
                if (pos == nbt.Length) throw new ArgumentException("Array never ends.");
            }
            return result;
        }
    }
}
