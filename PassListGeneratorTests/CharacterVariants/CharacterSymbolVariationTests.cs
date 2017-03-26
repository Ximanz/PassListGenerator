using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PassListGenerator.CharacterModifier;

namespace PassListGeneratorTests.CharacterVariants
{
    [TestClass()]
    public class CharacterSymbolVariationTests
    {
        [TestMethod()]
        public void GenerateCharacterVariationsTestSingleVariance()
        {
            var modifier = new CharacterSymbolModifier("CharacterMap.json");
            var results = modifier.GenerateCharacterVariations('e');

            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual('3', results.FirstOrDefault());

            results = modifier.GenerateCharacterVariations('E');

            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod()]
        public void GenerateCharacterVariationsTestMultipleVariance()
        {
            var modifier = new CharacterSymbolModifier("CharacterMap.json");
            var results = modifier.GenerateCharacterVariations('a');

            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
            Assert.IsTrue(results.Contains('4'));
            Assert.IsTrue(results.Contains('@'));
        }

        [TestMethod()]
        public void GenerateCharacterVariationsTestNoVariance()
        {
            var modifier = new CharacterSymbolModifier("CharacterMap.json");
            var results = modifier.GenerateCharacterVariations('k');

            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod()]
        public void CharacterVariationCountTest()
        {
            var modifier = new CharacterSymbolModifier("CharacterMap.json");
            var results = modifier.CharacterVariationCount('a');

            Assert.IsNotNull(results);
            Assert.AreEqual(2, results);
        }
    }
}