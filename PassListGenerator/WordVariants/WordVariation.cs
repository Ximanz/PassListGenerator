using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PassListGenerator.CharacterVariants;

namespace PassListGenerator.WordVariants
{
    public static class WordVariation
    {
        public static IEnumerable<string> GenerateWordVariations(string word, List<ICharacterVariation> characterModifiers)
        {
            var character = word.First();
            var characterVariants = new List<char>();
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
    }
}
