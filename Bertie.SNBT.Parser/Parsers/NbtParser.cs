using Bertie.SNBT.Parser.NBT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bertie.SNBT.Parser.Parsers {
    public class NbtParser : StringParser<NbtTag> {
        /// <summary>
        /// Parses any nbt and returns the appropriate tag.
        /// </summary>
        /// <param name="nbt">The stringified nbt.</param>
        /// <param name="pos">The starting position.</param>
        /// <returns>Returns the parsed tag.</returns>
        public override NbtTag Parse(string nbt, ref int pos) {
            SkipWhitespace(nbt, ref pos);
            var result = new NbtPrimitiveParser().Parse(nbt, ref pos);
            SkipWhitespace(nbt, ref pos);
            return result;
        }
    }
}
