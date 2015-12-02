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
        public static T Parse<T>(string text)
            where T : Models.BaseModel
        {
            return Parse(text) as T;
        }
        public static Models.BaseModel Parse(string text)
        {
            var literalResult = Common.LiteralParser.TryParse(text);
            if (literalResult.WasSuccessful)
                return literalResult.Value;

            return null;
        }
    }
}
