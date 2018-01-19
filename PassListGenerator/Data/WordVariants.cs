using PassListGenerator.CharacterModifier;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PassListGenerator.Data
{
    public class WordVariants : IEnumerable<string>
    {
        private string _root;
        public string Root => _root;
        private List<string> variants;

        public WordVariants(string root)
        {
            _root = root;
            variants = new List<string>() { root };
        }

        public void AddVariant(string variant)
        {
            variants.Add(variant);
        }

        public int Count => variants.Count;

        public string GetVariantAtIndex(int index)
        {
            return variants[index]??string.Empty;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)variants).GetEnumerator();
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return ((IEnumerable<string>)variants).GetEnumerator();
        }

        internal int GenerateWordVariations(List<ICharacterModifier> characterModifiers)
        {
            var characterVariantGroups = new List<char>[_root.Length];
            var characterVariantCounts = new int[_root.Length];

            for (var i = 0; i < _root.Length; i++)
            {
                characterVariantGroups[i] = new List<char>() { _root[i] };
                characterModifiers.ForEach(mod => characterVariantGroups[i].AddRange(mod.GenerateCharacterVariations(_root[i])));
                characterVariantCounts[i] = characterVariantGroups[i].Count;
            }

            var wordVariants = new string[WordVariationsCount(characterModifiers)];

            for (var charIndex = 0; charIndex < _root.Length; charIndex++)
            {
                var mainIndex = 0;
                var repCount = charIndex == 0 ? 1 : characterVariantCounts.Take(charIndex).Aggregate((a, b) => a * b);
                var multiplier = charIndex == _root.Length - 1 ? 1 : characterVariantCounts.Skip(charIndex + 1).Aggregate((a, b) => a * b);
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

            variants.AddRange(wordVariants);

            return variants.Count;
        }

        internal int WordVariationsCount(List<ICharacterModifier> characterModifiers)
        {
            var counts = new List<int>();
            foreach (var character in _root)
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