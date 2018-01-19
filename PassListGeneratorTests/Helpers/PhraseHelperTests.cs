using Microsoft.VisualStudio.TestTools.UnitTesting;
using PassListGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassListGeneratorTests.Helpers
{
    [TestClass()]
    public class PhraseHelperTests
    {
        public static class Data
        {
            public static List<string> Group1 = new List<string>() { "One", "ONE", "one" };
            public static List<string> Group2 = new List<string>() { "Two", "TWO", "two" };
            public static List<string> Group3 = new List<string>() { "Three", "THREE", "three" };
            public static List<string> Group4 = new List<string>() { "Four", "FOUR", "four", "fOuR" };
        }

        [TestMethod()]
        public void PermutatePhraseTestFourGroupsSingleWord()
        {
            var wordCollection = new List<List<string>>() { Data.Group1, Data.Group2, Data.Group3, Data.Group4 };

            var phraseHelper = new PhraseHelper(wordCollection);
            var results = phraseHelper.PermutatePhrase(1, new List<int>());

            Assert.IsNotNull(results);
            Assert.AreEqual(13, results.Count());
        }

        [TestMethod()]
        public void PermutatePhraseTestFourGroupsTwoWords()
        {
            var wordCollection = new List<List<string>>() { Data.Group1, Data.Group2, Data.Group3, Data.Group4 };

            var phraseHelper = new PhraseHelper(wordCollection);
            var results = phraseHelper.PermutatePhrase(2, new List<int>());

            Assert.IsNotNull(results);
            Assert.AreEqual(126, results.Count());
        }

        [TestMethod()]
        public void PermutatePhraseTestFourGroupsThreeWords()
        {
            var wordCollection = new List<List<string>>() { Data.Group1, Data.Group2, Data.Group3, Data.Group4 };

            var phraseHelper = new PhraseHelper(wordCollection);
            var results = phraseHelper.PermutatePhrase(3, new List<int>());

            Assert.IsNotNull(results);
            Assert.AreEqual(810, results.Count());
        }

        [TestMethod()]
        public void PermutatePhraseTestOneGroupSingleWord()
        {
            var wordCollection = new List<List<string>>() { Data.Group1 };

            var phraseHelper = new PhraseHelper(wordCollection);
            var results = phraseHelper.PermutatePhrase(1, new List<int>());

            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count());
        }

        [TestMethod()]
        public void PermutatePhraseTestOneGroupTwoWords()
        {
            var wordCollection = new List<List<string>>() { Data.Group1 };

            var phraseHelper = new PhraseHelper(wordCollection);
            var results = phraseHelper.PermutatePhrase(2, new List<int>());

            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count());
        }

        [TestMethod()]
        public void PermutatePhraseTestTwoGroupsThreeWords()
        {
            var wordCollection = new List<List<string>>() { Data.Group1 };

            var phraseHelper = new PhraseHelper(wordCollection);
            var results = phraseHelper.PermutatePhrase(3, new List<int>());

            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count());
        }

        [TestMethod()]
        public void CountPermutationsTest()
        {
            var wordCollection = new List<List<string>>() { Data.Group1, Data.Group2, Data.Group3, Data.Group4 };

            var phraseHelper = new PhraseHelper(wordCollection);
            var result = phraseHelper.CountPermutations(3);

            Assert.IsNotNull(result);
            Assert.AreEqual(810, result);
        }
    }
}