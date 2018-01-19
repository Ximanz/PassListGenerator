using PassListGenerator.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PassListGenerator.Helpers
{
    public class PhraseHelper
    {
        private WordBank _wordBank;

        public PhraseHelper(WordBank wordBank)
        {
            _wordBank = wordBank;
        }

        public IEnumerable<string> PermutatePhrase(int wordsToUse, List<int> skipIndices)
        {
            if (skipIndices.Count == _wordBank.Count) yield return string.Empty;
            
            for (var index = 0; index < _wordBank.Count; index++)
            {
                if (skipIndices.Contains(index)) continue;
                if (wordsToUse == 1)
                {
                    foreach (string word in _wordBank.Elements[index])
                    {
                        yield return word;
                    }
                    continue;
                }

                var newSkipIndices = new List<int>(skipIndices);
                newSkipIndices.Add(index);
                foreach (var element in PermutatePhrase(wordsToUse - 1, newSkipIndices))
                {
                    foreach (var word in _wordBank.Elements[index])
                    {
                        yield return string.Concat(word, element);
                    }
                }
            }
        }

        public int CountPermutations(int wordsToUse)
        {
            var groupCounts = from collection in _wordBank.Elements select collection.Count();

            var permutations = Count(groupCounts, wordsToUse);

            return permutations;
        }

        private int Count(IEnumerable<int> groupCounts, int remaining)
        {
            var enumerable = groupCounts as IList<int> ?? groupCounts.ToList();

            if (!enumerable.Any()) return 1;
            if (remaining == 1) return enumerable.Sum();

            var sum = 0;

            for (int index = 0; index < enumerable.Count(); index++)
            {
                var reducedList = new List<int>(enumerable);
                reducedList.RemoveAt(index);
                sum += enumerable[index] * Count(reducedList, remaining - 1);
            }

            return sum;
        }
    }
}
