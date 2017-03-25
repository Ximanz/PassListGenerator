using System;
using System.Collections.Generic;
using System.Linq;

namespace PassListGenerator.Helpers
{
    public static class PhraseHelper
    {
        public static IEnumerable<string> PermutatePhrase(List<List<string>> wordCollection, int wordsToUse, List<int> skipIndices)
        {
            if (skipIndices.Count == wordCollection.Count) yield return string.Empty;
            
            for (var index = 0; index < wordCollection.Count(); index++)
            {
                if (skipIndices.Contains(index)) continue;
                if (wordsToUse == 1)
                {
                    foreach (var word in wordCollection[index])
                    {
                        yield return word;
                    }
                    continue;
                }

                var newSkipIndices = new List<int>(skipIndices);
                newSkipIndices.Add(index);
                foreach (var element in PermutatePhrase(wordCollection, wordsToUse - 1, newSkipIndices))
                {
                    foreach (var word in wordCollection[index])
                    {
                        yield return string.Concat(word, element);
                    }
                }
            }
        }

        public static int CountPermutations(IEnumerable<IEnumerable<string>> wordCollection, int wordsToUse)
        {
            var groupCounts = from collection in wordCollection select collection.Count();

            var permutations = Count(groupCounts, wordsToUse);

            return permutations;
        }

        private static int Count(IEnumerable<int> groupCounts, int remaining)
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
