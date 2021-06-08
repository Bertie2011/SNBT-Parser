using Bertie.SNBT.Parser.NBT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bertie.SNBT.Parser.Parsers {
    public class NbtParser : IStringParser<NbtTag> {
        /// <summary>
        /// Parses any nbt and returns the appropriate tag.
        /// </summary>
        /// <param name="nbt">The stringified nbt.</param>
        /// <param name="pos">The starting position.</param>
        /// <returns>Returns the parsed tag.</returns>
        public NbtTag Parse(string nbt, ref int pos) {
            for (; pos < nbt.Length; pos++) {
                if (char.IsWhiteSpace(nbt[pos])) continue;
                else break;
            }
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            for (; pos < nbt.Length; pos++) {
                if (char.IsWhiteSpace(nbt[pos])) continue;
                else break;
            }
            return result;
        }
    }
}
