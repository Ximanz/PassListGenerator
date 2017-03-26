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
            Assert.IsTrue(results.Contains("test"));
            Assert.IsTrue(results.Contains("tesT"));
            Assert.IsTrue(results.Contains("teSt"));
            Assert.IsTrue(results.Contains("teST"));
            Assert.IsTrue(results.Contains("tEst"));
            Assert.IsTrue(results.Contains("tEsT"));
            Assert.IsTrue(results.Contains("tESt"));
            Assert.IsTrue(results.Contains("tEST"));
            Assert.IsTrue(results.Contains("Test"));
            Assert.IsTrue(results.Contains("TesT"));
            Assert.IsTrue(results.Contains("TeSt"));
            Assert.IsTrue(results.Contains("TeST"));
            Assert.IsTrue(results.Contains("TEst"));
            Assert.IsTrue(results.Contains("TEsT"));
            Assert.IsTrue(results.Contains("TESt"));
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