using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jasonsh.KSP.Parsers.Save
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Parse_Literal_ReturnsLiteral()
        {
            var name = "NAME";
            var value = "VALUE";
            var text = $"{name} = {value}";

            var actual = Parser.Parse<Models.Save.Literal>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.Original);
            Assert.AreEqual(name, actual.Name);
            Assert.AreEqual(value, actual.Value);
        }
        [TestMethod]
        public void Parse_Literal_EndingNewLine_ReturnsLiteral()
        {
            var name = "NAME";
            var value = "VALUE";
            var text = $"{name} = {value}";

            var actual = Parser.Parse<Models.Save.Literal>(text + "\r\n");

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.Original);
            Assert.AreEqual(name, actual.Name);
            Assert.AreEqual(value, actual.Value);
        }
        [TestMethod]
        public void Parse_Literal_LeadingSpaces_ReturnsLiteral()
        {
            var name = "NAME";
            var value = "VALUE";
            var text = $"      {name} = {value}";

            var actual = Parser.Parse<Models.Save.Literal>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.Original);
            Assert.AreEqual(name, actual.Name);
            Assert.AreEqual(value, actual.Value);
        }
        [TestMethod]
        public void Parse_Literal_LeadingTabs_ReturnsLiteral()
        {
            var name = "NAME";
            var value = "VALUE";
            var text = $"\t\t\t{name} = {value}";

            var actual = Parser.Parse<Models.Save.Literal>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.Original);
            Assert.AreEqual(name, actual.Name);
            Assert.AreEqual(value, actual.Value);
        }
    }
}
