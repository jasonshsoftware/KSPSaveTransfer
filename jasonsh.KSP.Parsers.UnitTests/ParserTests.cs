using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            Assert.AreEqual(name, actual.Name);
            Assert.AreEqual(value, actual.Value);
        }
        [TestMethod]
        public void ParseModel_Literal_LeadingNewLine_ReturnsLiteral()
        {
            var name = "NAME";
            var value = "VALUE";
            var text = "\r\n\r\n" + $"{name} = {value}";

            var actual = Parser.ParseModel<Models.Literal>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(name, actual.Name);
            Assert.AreEqual(value, actual.Value);
        }
        [TestMethod]
        public void ParseModel_Literal_TrainlingNewLine_ReturnsLiteral()
        {
            var name = "NAME";
            var value = "VALUE";
            var text = $"{name} = {value}" + "\r\n\r\n";

            var actual = Parser.ParseModel<Models.Literal>(text + "\r\n\r\n");

            Assert.IsNotNull(actual);
            Assert.AreEqual(name, actual.Name);
            Assert.AreEqual(value, actual.Value);
        }
        [TestMethod]
        public void ParseModel_Literal_LeadingSpaces_ReturnsLiteral()
        {
            var name = "NAME";
            var value = "VALUE";
            var text = "      " + $"{name} = {value}";

            var actual = Parser.ParseModel<Models.Literal>(text);

            Assert.IsNotNull(actual);
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

            Assert.IsNotNull(actual);
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
            Assert.AreEqual(name, actual.Name);
        }
        [TestMethod]
        public void ParseModel_ComplexObject_LeadingNewLine_ReturnsComplexObject()
        {
            var name = "NAME";
            var text = "\r\n\r\n" + $@"{name}
{{
}}";

            var actual = Parser.ParseModel<Models.ComplexObject>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(name, actual.Name);
        }
        [TestMethod]
        public void ParseModel_ComplexObject_TrailingNewLine_ReturnsComplexObject()
        {
            var name = "NAME";
            var text = $@"{name}
{{
}}" + "\r\n\r\n";

            var actual = Parser.ParseModel<Models.ComplexObject>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(name, actual.Name);
        }
        [TestMethod]
        public void ParseModel_ComplexObject_LeadingSpaces_ReturnsComplexObject()
        {
            var name = "NAME";
            var text = "      " + $@"{name}
{{
}}";

            var actual = Parser.ParseModel<Models.ComplexObject>(text);

            Assert.IsNotNull(actual);
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
" + "\t" + $@"{literalName1} = {literalValue1}
" + "\t" + $@"{literalName2} = {literalValue2}
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
        [TestMethod]
        public void ParseModel_ComplexObject_WithLiterals_ToString_ReturnsOriginal()
        {
            var name = "NAME";
            var literalName1 = "LITERAL_NAME_1";
            var literalValue1 = "LITERAL VALUE 1";
            var literalName2 = "LITERAL_NAME_2";
            var literalValue2 = "LITERAL VALUE 2";
            var text = $@"{name}
{{
" + "\t" + $@"{literalName1} = {literalValue1}
" + "\t" + $@"{literalName2} = {literalValue2}
}}
";

            var actual = Parser.ParseModel<Models.ComplexObject>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.ToString());
        }
        [TestMethod]
        public void ParseModel_ComplexObject_WithComplexObjects_ReturnsComplexObject()
        {
            var name = "NAME";
            var complexObjectName1 = "COMPLEX_OBJECT_NAME_1";
            var complexObjectName2 = "COMPLEX_OBJECT_NAME_2";
            var text = $@"{name}
{{
" + "\t" + $@"{complexObjectName1}
" + "\t" + $@"{{
" + "\t" + $@"}}
" + "\t" + $@"{complexObjectName2}
" + "\t" + $@"{{
" + "\t" + $@"}}
}}";

            var actual = Parser.ParseModel<Models.ComplexObject>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(name, actual.Name);

            Assert.IsNotNull(actual.Children);
            Assert.AreEqual(2, actual.Children.Count());

            Assert.IsInstanceOfType(actual.Children.ElementAt(0), typeof(Models.ComplexObject));
            Assert.AreEqual(complexObjectName1, actual.Children.OfType<Models.ComplexObject>().ElementAt(0).Name);

            Assert.IsInstanceOfType(actual.Children.ElementAt(1), typeof(Models.ComplexObject));
            Assert.AreEqual(complexObjectName2, actual.Children.OfType<Models.ComplexObject>().ElementAt(1).Name);
        }
        [TestMethod]
        public void ParseModel_ComplexObject_WithComplexObjects_ToString_ReturnsOriginal()
        {
            var name = "NAME";
            var complexObjectName1 = "COMPLEX_OBJECT_NAME_1";
            var complexObjectName2 = "COMPLEX_OBJECT_NAME_2";
            var text = $@"{name}
{{
" + "\t" + $@"{complexObjectName1}
" + "\t" + $@"{{
" + "\t" + $@"}}
" + "\t" + $@"{complexObjectName2}
" + "\t" + $@"{{
" + "\t" + $@"}}
}}
";

            var actual = Parser.ParseModel<Models.ComplexObject>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.ToString());
        }
        [TestMethod]
        public void ParseModel_ComplexObject_WithComplexObjectsAndLiterals_ReturnsComplexObject()
        {
            var name = "NAME";
            var complexObjectName1 = "COMPLEX_OBJECT_NAME_1";
            var complexObjectName2 = "COMPLEX_OBJECT_NAME_2";
            var literalName1 = "LITERAL_NAME_1";
            var literalValue1 = "LITERAL VALUE 1";
            var literalName2 = "LITERAL_NAME_2";
            var literalValue2 = "LITERAL VALUE 2";
            var text = $@"{name}
{{
" + "\t" + $@"{complexObjectName1}
" + "\t" + $@"{{
" + "\t" + $@"}}
" + "\t" + $@"{literalName1} = {literalValue1}
" + "\t" + $@"{complexObjectName2}
" + "\t" + $@"{{
" + "\t" + $@"}}
" + "\t" + $@"{literalName2} = {literalValue2}
}}";

            var actual = Parser.ParseModel<Models.ComplexObject>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(name, actual.Name);

            Assert.IsNotNull(actual.Children);
            Assert.AreEqual(4, actual.Children.Count());

            Assert.IsInstanceOfType(actual.Children.ElementAt(0), typeof(Models.ComplexObject));
            Assert.AreEqual(complexObjectName1, actual.Children.OfType<Models.ComplexObject>().ElementAt(0).Name);

            Assert.IsInstanceOfType(actual.Children.ElementAt(1), typeof(Models.Literal));
            Assert.AreEqual(literalName1, actual.Children.OfType<Models.Literal>().ElementAt(0).Name);
            Assert.AreEqual(literalValue1, actual.Children.OfType<Models.Literal>().ElementAt(0).Value);

            Assert.IsInstanceOfType(actual.Children.ElementAt(2), typeof(Models.ComplexObject));
            Assert.AreEqual(complexObjectName2, actual.Children.OfType<Models.ComplexObject>().ElementAt(1).Name);

            Assert.IsInstanceOfType(actual.Children.ElementAt(3), typeof(Models.Literal));
            Assert.AreEqual(literalName2, actual.Children.OfType<Models.Literal>().ElementAt(1).Name);
            Assert.AreEqual(literalValue2, actual.Children.OfType<Models.Literal>().ElementAt(1).Value);
        }
        [TestMethod]
        public void ParseModel_ComplexObject_WithComplexObjectsAndLiterals_ToString_ReturnsOriginal()
        {
            var name = "NAME";
            var complexObjectName1 = "COMPLEX_OBJECT_NAME_1";
            var complexObjectName2 = "COMPLEX_OBJECT_NAME_2";
            var literalName1 = "LITERAL_NAME_1";
            var literalValue1 = "LITERAL VALUE 1";
            var literalName2 = "LITERAL_NAME_2";
            var literalValue2 = "LITERAL VALUE 2";
            var text = $@"{name}
{{
" + "\t" + $@"{complexObjectName1}
" + "\t" + $@"{{
" + "\t" + $@"}}
" + "\t" + $@"{literalName1} = {literalValue1}
" + "\t" + $@"{complexObjectName2}
" + "\t" + $@"{{
" + "\t" + $@"}}
" + "\t" + $@"{literalName2} = {literalValue2}
}}
";

            var actual = Parser.ParseModel<Models.ComplexObject>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.ToString());
        }
        [TestMethod]
        public void ParseModel_ComplexObject_WithComplexObjectsAndSubLiterals_ReturnsComplexObject()
        {
            var name = "NAME";
            var complexObjectName1 = "COMPLEX_OBJECT_NAME_1";
            var complexObjectName2 = "COMPLEX_OBJECT_NAME_2";
            var literalName1 = "LITERAL_NAME_1";
            var literalValue1 = "LITERAL VALUE 1";
            var literalName2 = "LITERAL_NAME_2";
            var literalValue2 = "LITERAL VALUE 2";
            var text = $@"{name}
{{
" + "\t" + $@"{complexObjectName1}
" + "\t" + $@"{{
" + "\t" + "\t" + $@"{literalName1} = {literalValue1}
" + "\t" + $@"}}
" + "\t" + $@"{complexObjectName2}
" + "\t" + $@"{{
" + "\t" + "\t" + $@"{literalName2} = {literalValue2}
" + "\t" + $@"}}
}}";

            var actual = Parser.ParseModel<Models.ComplexObject>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(name, actual.Name);

            Assert.IsNotNull(actual.Children);
            Assert.AreEqual(2, actual.Children.Count());

            Assert.IsInstanceOfType(actual.Children.ElementAt(0), typeof(Models.ComplexObject));
            Assert.AreEqual(complexObjectName1, actual.Children.OfType<Models.ComplexObject>().ElementAt(0).Name);

            Assert.AreEqual(1, actual.Children.OfType<Models.ComplexObject>().ElementAt(0).Children.Count());
            Assert.IsInstanceOfType(actual.Children.OfType<Models.ComplexObject>().ElementAt(0).Children.ElementAt(0), typeof(Models.Literal));
            Assert.AreEqual(literalName1, actual.Children.OfType<Models.ComplexObject>().ElementAt(0).Children.OfType<Models.Literal>().ElementAt(0).Name);
            Assert.AreEqual(literalValue1, actual.Children.OfType<Models.ComplexObject>().ElementAt(0).Children.OfType<Models.Literal>().ElementAt(0).Value);

            Assert.IsInstanceOfType(actual.Children.ElementAt(1), typeof(Models.ComplexObject));
            Assert.AreEqual(complexObjectName2, actual.Children.OfType<Models.ComplexObject>().ElementAt(1).Name);

            Assert.AreEqual(1, actual.Children.OfType<Models.ComplexObject>().ElementAt(1).Children.Count());
            Assert.IsInstanceOfType(actual.Children.OfType<Models.ComplexObject>().ElementAt(1).Children.ElementAt(0), typeof(Models.Literal));
            Assert.AreEqual(literalName2, actual.Children.OfType<Models.ComplexObject>().ElementAt(1).Children.OfType<Models.Literal>().ElementAt(0).Name);
            Assert.AreEqual(literalValue2, actual.Children.OfType<Models.ComplexObject>().ElementAt(1).Children.OfType<Models.Literal>().ElementAt(0).Value);
        }
        [TestMethod]
        public void ParseModel_ComplexObject_WithComplexObjectsAndSubLiterals_ToString_ReturnsOriginal()
        {
            var name = "NAME";
            var complexObjectName1 = "COMPLEX_OBJECT_NAME_1";
            var complexObjectName2 = "COMPLEX_OBJECT_NAME_2";
            var literalName1 = "LITERAL_NAME_1";
            var literalValue1 = "LITERAL VALUE 1";
            var literalName2 = "LITERAL_NAME_2";
            var literalValue2 = "LITERAL VALUE 2";
            var text = $@"{name}
{{
" + "\t" + $@"{complexObjectName1}
" + "\t" + $@"{{
" + "\t" + "\t" + $@"{literalName1} = {literalValue1}
" + "\t" + $@"}}
" + "\t" + $@"{complexObjectName2}
" + "\t" + $@"{{
" + "\t" + "\t" + $@"{literalName2} = {literalValue2}
" + "\t" + $@"}}
}}
";

            var actual = Parser.ParseModel<Models.ComplexObject>(text);

            Assert.IsNotNull(actual);
            Assert.AreEqual(text, actual.ToString());
        }
        #endregion

        #region Smoke Tests
        [TestMethod]
        public async Task SmokeTest()
        {
            var prefix = $"{this.GetType().FullName}.";

            var testFiles = Assembly.GetExecutingAssembly().GetManifestResourceNames()
                .Where(p => p.StartsWith(prefix))
                .Select(p => new { FullName = p, Name = p.Substring(prefix.Length) })
                .ToList();

            Assert.AreNotEqual(0, testFiles.Count, $"There are no smoke test files for {this.GetType().Name}");

            foreach (var testFile in testFiles)
            {
                var content = "";
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(testFile.FullName))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        content = await reader.ReadToEndAsync();
                    }
                }

                Assert.IsFalse(String.IsNullOrEmpty(content), $"File empty: {testFile.Name}");

                var actual = Parser.ParseModel(content);

                Assert.AreEqual(content, actual.ToString(), $"Serialized content does not match for {testFile.Name}");
            }
        }
        #endregion
    }
}
