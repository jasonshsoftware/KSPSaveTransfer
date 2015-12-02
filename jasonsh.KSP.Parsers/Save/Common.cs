using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jasonsh.KSP.Parsers.Save
{
    internal static class Common
    {
        public static readonly Parser<string> Equals = Parse.String(" = ").Text();

        public static Parser<Models.Save.Literal> LiteralParser =
            from leadingWhitespace in Parse.WhiteSpace.Many().Text()
            from name in Parse.AnyChar.Except(Common.Equals).Many().Text()
            from eq in Common.Equals
            from value in Parse.AnyChar.Except(Parse.LineEnd).Except(Parse.LineTerminator).Many().Text()
            select new Models.Save.Literal(name, value, $"{leadingWhitespace}{name}{eq}{value}");

    }
}
