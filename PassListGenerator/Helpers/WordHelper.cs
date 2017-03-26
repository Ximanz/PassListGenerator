using PassListGenerator.CharacterModifier;
using System.Collections.Generic;
using System.Linq;

namespace PassListGenerator.Helpers
{
    public static class WordHelper
    {
        public static IEnumerable<string> GenerateWordVariations(string word, List<ICharacterVariation> characterModifiers)
        {
            var characterVariantGroups = new List<char>[word.Length];
            var characterVariantCounts = new int[word.Length];

            for (var i = 0; i < word.Length; i++)
            {
                characterVariantGroups[i] = new List<char>() { word[i] };
                characterModifiers.ForEach(mod => characterVariantGroups[i].AddRange(mod.GenerateCharacterVariations(word[i])));
                characterVariantCounts[i] = characterVariantGroups[i].Count;
            }

            var wordVariants = new string[WordVariationsCount(word, characterModifiers)];

            for (var charIndex = 0; charIndex < word.Length; charIndex++)
            {
                var mainIndex = 0;
                var repCount = charIndex == 0 ? 1 : characterVariantCounts.Take(charIndex).Aggregate((a, b) => a * b);
                var multiplier = charIndex == word.Length - 1 ? 1 : characterVariantCounts.Skip(charIndex + 1).Aggregate((a,b) => a * b);
                for (int reps = 0; reps < repCount; reps++)
                {
                    foreach (var character in characterVariantGroups[charIndex])
                    {
                        for (int i = 0; i < multiplier; i++)
                        {
                            wordVariants[mainIndex] = string.Concat(wordVariants[mainIndex], character);
                            mainIndex++;
                        }
                    }
                }
            }

            return wordVariants;
        }

        public static int WordVariationsCount(string word, List<ICharacterVariation> characterModifiers)
        {
            var counts = new List<int>();
            foreach (var character in word)
            {
                var count = 1;
                foreach (var modifier in characterModifiers)
                {
                    count += modifier.CharacterVariationCount(character);
                }
                counts.Add(count);
            }

            return counts.Aggregate((a, b) => a * b);
        }
    }
}
