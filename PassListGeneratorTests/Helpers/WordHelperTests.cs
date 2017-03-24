using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PassListGenerator.CharacterModifier;
using PassListGenerator.Helpers;

namespace PassListGeneratorTests.Helpers
{
    [TestClass()]
    public class WordHelperTests
    {
        [TestMethod()]
        public void GenerateWordVariationsTestCaseOnly()
        {
            var modifiers = new List<ICharacterVariation>() { new CharacterCaseModifier() };
            var results = WordHelper.GenerateWordVariations("test", modifiers).ToList();

            Assert.IsNotNull(results);
            Assert.AreEqual(16, results.Count());
            Assert.IsTrue(results.Contains("TeSt"));
            Assert.IsTrue(results.Contains("TEST"));
        }

        [TestMethod()]
        public void GenerateWordVariationsTestSymbolOnly()
        {
            var modifiers = new List<ICharacterVariation>() { new CharacterSymbolModifier("CharacterMap.json") };
            var results = WordHelper.GenerateWordVariations("sample", modifiers).ToList();

            Assert.IsNotNull(results);
            Assert.AreEqual(6, results.Count());
            Assert.IsTrue(results.Contains("sample"));
            Assert.IsTrue(results.Contains("s4mple"));
            Assert.IsTrue(results.Contains("s@mple"));
            Assert.IsTrue(results.Contains("sampl3"));
            Assert.IsTrue(results.Contains("s4mpl3"));
            Assert.IsTrue(results.Contains("s@mpl3"));
        }

        [TestMethod()]
        public void GenerateWordVariationsTestCombined()
        {
            var modifiers = new List<ICharacterVariation>() { new CharacterCaseModifier(), new CharacterSymbolModifier("CharacterMap.json") };
            var results = WordHelper.GenerateWordVariations("sample", modifiers).ToList();

            Assert.IsNotNull(results);
            Assert.AreEqual(192, results.Count());
            Assert.IsTrue(results.Contains("sample"));
            Assert.IsTrue(results.Contains("SAMPLE"));
            Assert.IsTrue(results.Contains("S@mplE"));
            Assert.IsTrue(results.Contains("s4mPL3"));
        }

        [TestMethod()]
        public void WordVariationsCountTest()
        {
            var modifiers = new List<ICharacterVariation>() { new CharacterCaseModifier(), new CharacterSymbolModifier("CharacterMap.json") };
            var results = WordHelper.WordVariationsCount("sample", modifiers);

            Assert.IsNotNull(results);
            Assert.AreEqual(192, results);

            results = WordHelper.WordVariationsCount("test", modifiers);

            Assert.IsNotNull(results);
            Assert.AreEqual(24, results);
        }
    }
}