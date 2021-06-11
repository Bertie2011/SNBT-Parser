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

            //Check ahead if semicolon is found and pick correct array type.
            if (pos + 1 < nbt.Length && nbt[pos + 1] == ';') {
                var type = nbt[pos];
                pos += 2;
                if (type == 'B') return CreateArray<NbtPrimitive<sbyte>>(nbt, ref pos);
                else if (type == 'I') return CreateArray<NbtPrimitive<int>>(nbt, ref pos);
                else if (type == 'L') return CreateArray<NbtPrimitive<long>>(nbt, ref pos);
                else throw new ArgumentException($"Array of unknown type at {pos}: {nbt}");
            } else {
                return CreateArray<NbtTag>(nbt, ref pos);
            }
        }

        /// <summary>
        /// Creates array from nbt, where pos is at the start of the first element (possibly prefixed by whitespace).
        /// </summary>
        /// <typeparam name="T">The type of array to create</typeparam>
        /// <param name="nbt">The nbt to parse</param>
        /// <param name="pos">The pos located after the first item.</param>
        /// <returns>Returns a filled array.</returns>
        private NbtArray CreateArray<T>(string nbt, ref int pos) where T : NbtTag {
            var result = new NbtArray<T>();
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
