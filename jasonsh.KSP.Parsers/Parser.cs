using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jasonsh.KSP.Parsers
{
    public static class Parser
    {
        #region Common
        internal static Parser<string> Text(this Parser<IEnumerable<string>> strings) { return strings.Select(p => p.Aggregate("", (l, r) => $"{l}{r}")); }
        internal static readonly Parser<string> EqualsParser = Parse.String(" = ").Text();
        internal static readonly Parser<char> OpenBrace = Parse.Char('{');
        internal static readonly Parser<char> CloseBrace = Parse.Char('}');
        #endregion

        #region Literal
        internal static readonly Parser<Models.BaseModel> LiteralParser =
            from _1 in Parse.LineEnd.Many()
            from _2 in Parse.WhiteSpace.Many()
            from name in Parse.AnyChar.Except(EqualsParser).Except(Parse.WhiteSpace).Many().Text()
            from eq in EqualsParser
            from value in Parse.AnyChar.Except(Parse.LineEnd).Many().Text()
            select new Models.Literal(name, value);
        #endregion

        #region ComplexObject
        internal static readonly Parser<Models.BaseModel> ComplexObjectParser =
            from _1 in Parse.LineEnd.Many()
            from _2 in Parse.WhiteSpace.Many()
            from name in Parse.AnyChar.Except(Parse.LineEnd).Many().Text()
            from _3 in Parse.LineEnd.AtLeastOnce()
            from _4 in Parse.WhiteSpace.Many()
            from openBrace in OpenBrace
            from _5 in Parse.LineEnd.AtLeastOnce()
            from children in LiteralParser.Or(ComplexObjectParser).Many()
            from _6 in Parse.LineEnd.Many()
            from _7 in Parse.WhiteSpace.Many()
            from closeBrace in CloseBrace
            from _8 in Parse.AnyChar.Except(Parse.LineEnd).Many()
            select new Models.ComplexObject(name, children);
        #endregion

        public static T ParseModel<T>(string text)
            where T : Models.BaseModel
        {
            return ParseModel(text) as T;
        }
        public static Models.BaseModel ParseModel(string text)
        {
            var complexObjectResult = ComplexObjectParser.TryParse(text);
            if (complexObjectResult.WasSuccessful)
                return complexObjectResult.Value;

            var literalResult = LiteralParser.TryParse(text);
            if (literalResult.WasSuccessful)
                return literalResult.Value;

            return null;
        }
    }
}
