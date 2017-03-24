using PassListGenerator.CharacterModifier;
using System.Collections.Generic;
using System.Linq;

namespace PassListGenerator.Helpers
{
    public static class WordHelper
    {
        public static IEnumerable<string> GenerateWordVariations(string word, List<ICharacterVariation> characterModifiers)
        {
            var character = word.First();
            var characterVariants = new List<char> { character };
            characterModifiers.ForEach(mod => characterVariants.AddRange(mod.GenerateCharacterVariations(character)));

            foreach (var characterVariant in characterVariants)
            {
                if (word.Length == 1) yield return characterVariant.ToString();
                else
                {
                    foreach (var variant in GenerateWordVariations(word.Substring(1), characterModifiers))
                    {
                        yield return string.Concat(characterVariant, variant);
                    }
                }
            }
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
