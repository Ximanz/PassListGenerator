using System.Collections.Generic;
using System.Linq;

namespace PassListGenerator.Helpers
{
    public static class PhraseHelper
    {
        public static IEnumerable<string> PermutatePhrase(IEnumerable<IEnumerable<string>> wordCollection, int wordsToUse)
        {
            var enumerable = wordCollection as IList<IEnumerable<string>> ?? wordCollection.ToList();

            if (!enumerable.Any()) yield return string.Empty;

            for (var index = 0; index < enumerable.Count(); index++)
            {
                if (wordsToUse == 1)
                {
                    foreach (var word in enumerable[index])
                    {
                        yield return word;
                    }
                }

                var reducedList = new List<IEnumerable<string>>(enumerable);
                reducedList.RemoveAt(index);
                foreach (var element in PermutatePhrase(reducedList, wordsToUse - 1))
                {
                    foreach (var word in enumerable[index])
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

            return permutations.Sum();
        }

        private static IEnumerable<int> Count(IEnumerable<int> groupCounts, int remaining)
        {
            var enumerable = groupCounts as IList<int> ?? groupCounts.ToList();

            if (!enumerable.Any()) yield return 1;
            if (remaining == 1) yield return enumerable.Sum();

            for (int index = 0; index < enumerable.Count(); index++)
            {
                var reducedList = new List<int>(enumerable);
                reducedList.RemoveAt(index);
                foreach (var sum in Count(reducedList, remaining - 1))
                {
                    yield return enumerable[index]*sum;
                }
            }
        }
    }
}
