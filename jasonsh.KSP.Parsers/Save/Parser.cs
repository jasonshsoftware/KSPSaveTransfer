using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jasonsh.KSP.Parsers.Save
{
    public static class Parser
    {
        public static T Parse<T>(string text)
            where T : Models.Save.SaveBaseModel
        {
            return Parse(text) as T;
        }
        public static Models.Save.SaveBaseModel Parse(string text)
        {
            var literalResult = Common.LiteralParser.TryParse(text);
            if (literalResult.WasSuccessful)
                return literalResult.Value;

            return null;
        }
    }
}
