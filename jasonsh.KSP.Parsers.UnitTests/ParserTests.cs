using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jasonsh.KSP.Parsers
{
    [TestClass]
    public class ParserTests
    {
        #region Literal
        [TestMethod]
        public void ParseModel_Literal_ReturnsLiteral()
        {
            var name = "NAME";
            var value = "VALUE";
            var text = $"{name} = {value}";

            var actual = Parser.ParseModel<Models.Literal>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.Original);
            Assert.AreEqual(name, actual.Name);
            Assert.AreEqual(value, actual.Value);
        }
        [TestMethod]
        public void ParseModel_Literal_LeadingNewLine_ReturnsLiteral()
        {
            var name = "NAME";
            var value = "VALUE";
            var text = $"{name} = {value}";

            var actual = Parser.ParseModel<Models.Literal>("\r\n\r\n" + text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.Original);
            Assert.AreEqual(name, actual.Name);
            Assert.AreEqual(value, actual.Value);
        }
        [TestMethod]
        public void ParseModel_Literal_TrainlingNewLine_ReturnsLiteral()
        {
            var name = "NAME";
            var value = "VALUE";
            var text = $"{name} = {value}";

            var actual = Parser.ParseModel<Models.Literal>(text + "\r\n\r\n");

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.Original);
            Assert.AreEqual(name, actual.Name);
            Assert.AreEqual(value, actual.Value);
        }
        [TestMethod]
        public void ParseModel_Literal_LeadingSpaces_ReturnsLiteral()
        {
            var name = "NAME";
            var value = "VALUE";
            var text = $"      {name} = {value}";

            var actual = Parser.ParseModel<Models.Literal>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.Original);
            Assert.AreEqual(name, actual.Name);
            Assert.AreEqual(value, actual.Value);
        }
        [TestMethod]
        public void ParseModel_Literal_LeadingTabs_ReturnsLiteral()
        {
            var name = "NAME";
            var value = "VALUE";
            var text = $"\t\t\t{name} = {value}";

            var actual = Parser.ParseModel<Models.Literal>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.Original);
            Assert.AreEqual(name, actual.Name);
            Assert.AreEqual(value, actual.Value);
        }
        [TestMethod]
        public void ParseModel_Literal_ToString_ReturnsOriginal()
        {
            var name = "NAME";
            var value = "VALUE";
            var text = $"{name} = {value}";

            var actual = Parser.ParseModel<Models.Literal>(text);

            Assert.AreEqual(text, actual.ToString());
        }
        #endregion

        #region ComplexObject
        [TestMethod]
        public void ParseModel_ComplexObject_ReturnsComplexObject()
        {
            var name = "NAME";
            var text = $@"{name}
{{
}}";

            var actual = Parser.ParseModel<Models.ComplexObject>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.Original);
            Assert.AreEqual(name, actual.Name);
        }
        [TestMethod]
        public void ParseModel_ComplexObject_LeadingNewLine_ReturnsComplexObject()
        {
            var name = "NAME";
            var text = $@"{name}
{{
}}";

            var actual = Parser.ParseModel<Models.ComplexObject>("\r\n\r\n" + text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.Original);
            Assert.AreEqual(name, actual.Name);
        }
        [TestMethod]
        public void ParseModel_ComplexObject_TrailingNewLine_ReturnsComplexObject()
        {
            var name = "NAME";
            var text = $@"{name}
{{
}}";

            var actual = Parser.ParseModel<Models.ComplexObject>(text + "\r\n\r\n");

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.Original);
            Assert.AreEqual(name, actual.Name);
        }
        [TestMethod]
        public void ParseModel_ComplexObject_LeadingSpaces_ReturnsComplexObject()
        {
            var name = "NAME";
            var text = $@"             {name}
{{
}}";

            var actual = Parser.ParseModel<Models.ComplexObject>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.Original);
            Assert.AreEqual(name, actual.Name);
        }
        [TestMethod]
        public void ParseModel_ComplexObject_LeadingTabs_ReturnsComplexObject()
        {
            var name = "NAME";
            var text = "\t\t\t" + $@"{name}
{{
}}";

            var actual = Parser.ParseModel<Models.ComplexObject>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.Original);
            Assert.AreEqual(name, actual.Name);
        }
        [TestMethod]
        public void ParseModel_ComplexObject_WithLiterals_ReturnsComplexObject()
        {
            var name = "NAME";
            var literalName1 = "LITERAL_NAME_1";
            var literalValue1 = "LITERAL VALUE 1";
            var literalName2 = "LITERAL_NAME_2";
            var literalValue2 = "LITERAL VALUE 2";
            var text = $@"{name}
{{
    {literalName1} = {literalValue1}
    {literalName2} = {literalValue2}
}}";

            var actual = Parser.ParseModel<Models.ComplexObject>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(name, actual.Name);

            Assert.IsNotNull(actual.Children);
            Assert.AreEqual(2, actual.Children.Count());

            Assert.IsInstanceOfType(actual.Children.ElementAt(0), typeof(Models.Literal));
            Assert.AreEqual(literalName1, actual.Children.OfType<Models.Literal>().ElementAt(0).Name);
            Assert.AreEqual(literalValue1, actual.Children.OfType<Models.Literal>().ElementAt(0).Value);

            Assert.IsInstanceOfType(actual.Children.ElementAt(1), typeof(Models.Literal));
            Assert.AreEqual(literalName2, actual.Children.OfType<Models.Literal>().ElementAt(1).Name);
            Assert.AreEqual(literalValue2, actual.Children.OfType<Models.Literal>().ElementAt(1).Value);
        }
        #endregion
    }
}
