using System.Text.RegularExpressions;

namespace Bertie.SNBT.Parser.Parsers {
    public interface IStringParser<T> {
        T Parse(string str, ref int pos);
        T Parse(string str) {
            int pos = 0;
            return Parse(str, ref pos);
        }
        string RemoveWhiteSpace(string str) {
            return Regex.Replace(str, @"\s+", "");
        }
    }
}
