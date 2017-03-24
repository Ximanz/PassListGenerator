using Microsoft.VisualStudio.TestTools.UnitTesting;
using PassListGenerator.CharacterModifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassListGeneratorTests.CharacterVariants
{
    [TestClass()]
    public class CharacterCaseVariationTests
    {
        [TestMethod()]
        public void GenerateCharacterVariationsTestLowerCaseLetter()
        {
            var modifier = new CharacterCaseModifier();
            var results = modifier.GenerateCharacterVariations('a');

            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual('A', results.FirstOrDefault());
        }

        [TestMethod()]
        public void GenerateCharacterVariationsTestUpperCaseLetter()
        {
            var modifier = new CharacterCaseModifier();
            var results = modifier.GenerateCharacterVariations('T');

            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual('t', results.FirstOrDefault());
        }

        [TestMethod()]
        public void GenerateCharacterVariationsTestNumber()
        {
            var modifier = new CharacterCaseModifier();
            var results = modifier.GenerateCharacterVariations('4');

            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod()]
        public void GenerateCharacterVariationsTestSymbol()
        {
            var modifier = new CharacterCaseModifier();
            var results = modifier.GenerateCharacterVariations('$');

            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod()]
        public void CharacterVariationCountTestLetter()
        {
            var modifier = new CharacterCaseModifier();
            var results = modifier.CharacterVariationCount('g');

            Assert.IsNotNull(results);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void CharacterVariationCountTestNonLetter()
        {
            var modifier = new CharacterCaseModifier();
            var results = modifier.CharacterVariationCount('7');

            Assert.IsNotNull(results);
            Assert.AreEqual(0, results);
        }
    }
}