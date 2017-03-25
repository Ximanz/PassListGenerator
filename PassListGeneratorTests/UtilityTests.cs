using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PassListGenerator;

namespace PassListGeneratorTests
{
    [TestClass()]
    public class UtilityTests
    {
        [TestMethod()]
        public void ReadInputFromFileTestCharacterMap()
        {
            var results = Utility.ReadInputFromFile<Dictionary<char, char[]>>("CharacterMap.json");

            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
            Assert.IsTrue(results.ContainsKey('a'));
            Assert.AreEqual(2, results['a'].Length);
            Assert.IsTrue(results.ContainsKey('e'));
            Assert.AreEqual(1, results['e'].Length);
        }

        [TestMethod()]
        public void ReadInputFromFileTestWordList()
        {
            var results = Utility.ReadInputFromFile<Dictionary<string, PassListGenerator.PassListGenerator.WordOptions>>("WordList.json");

            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
            Assert.IsTrue(results.ContainsKey("Correct"));
            Assert.IsTrue(results.ContainsKey("Staple"));
            Assert.IsTrue(results["Staple"].SymbolVariation);
            Assert.IsFalse(results["Staple"].CaseVariation);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadInputFromFileTestInvalidFileName()
        {
            Utility.ReadInputFromFile<Dictionary<char, char[]>>("INVALID.json");
        }
    }
}