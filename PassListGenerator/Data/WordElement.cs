using PassListGenerator.CharacterModifier;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassListGenerator.Data
{
    public class WordElement : IEnumerable
    {
        private string _root;
        public string Root => _root;

        private List<ICharacterModifier> _characterModifiers;
        private List<List<char>> characterVariations;

        public List<char> GetCharacterVariants(int i)
        {
            return characterVariations[i];
        }
        
        public WordElement(string root)
        {
            _root = root;
            characterVariations = new List<List<char>>();
            _characterModifiers = new List<ICharacterModifier>();
        }

        public void AddCharacterModifier(ICharacterModifier modifier)
        {
            _characterModifiers.Add(modifier);
        }

        public int Count => WordVariationsCount();

        public string GetVariantAtIndex(int index)
        {
            if (index >= WordVariationsCount()) throw new System.IndexOutOfRangeException();

            var result = new StringBuilder(_root);
            var lowerIndex = 0;

            for (var characterIndex = 0; characterIndex < _root.Length; characterIndex++)
            {
                for (var variantIndex = 0; variantIndex < characterVariations[characterIndex].Count; variantIndex ++)
                {
                    var lowerBound = GetLowerBound(characterIndex, variantIndex);
                    if (index >= (lowerIndex + lowerBound)
                        && index <= (lowerIndex + GetUpperBound(characterIndex, variantIndex)))
                    {
                        result[characterIndex] = characterVariations[characterIndex][variantIndex];
                        lowerIndex = lowerIndex + lowerBound;

                        if (lowerIndex == index) return result.ToString();
                        break;
                    }
                }
            }

            if (lowerIndex != index) throw new KeyNotFoundException($"Cannot find word variant at index {index} for root {_root}.");

            return result.ToString();
        }

        internal int GetIncrementCount(int characterIndex)
        {
            if (characterIndex >= _root.Length) throw new System.IndexOutOfRangeException();
            if (characterIndex == (_root.Length - 1)) return 1;

            return characterVariations.GetRange(characterIndex + 1, characterVariations.Count - (characterIndex + 1)).Select(v => v.Count).Aggregate((a, b) => a * b);
        }

        internal int GetLowerBound(int characterIndex, int variantIndex)
        {
            return variantIndex * GetIncrementCount(characterIndex);
        }

        internal int GetUpperBound(int characterIndex, int variantIndex)
        {
            return GetLowerBound(characterIndex, variantIndex + 1) - 1;
        }

        public WordEnum GetEnumerator()
        {
            return new WordEnum(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public void GenerateCharacterVariations()
        {
            characterVariations.Clear();

            for (var i = 0; i < _root.Length; i++)
            {
                characterVariations.Insert(i, new List<char>() { _root[i] });
                _characterModifiers.ForEach(mod => characterVariations[i].AddRange(mod.GenerateCharacterVariations(_root[i])));
            }
        }

        internal int WordVariationsCount()
        {
            var counts = new List<int>();
            foreach (var character in _root)
            {
                var count = 1;
                foreach (var modifier in _characterModifiers)
                {
                    count += modifier.CharacterVariationCount(character);
                }
                counts.Add(count);
            }

            return counts.Aggregate((a, b) => a * b);
        }
    }

    public class WordEnum : IEnumerator
    {
        WordElement _wordElement;

        List<int> characterIndex;

        public int WordLength => _wordElement.Root.Length;

        public WordEnum(WordElement wordElement)
        {
            _wordElement = wordElement;

            characterIndex = new List<int>(WordLength);
            characterIndex.AddRange(Enumerable.Repeat(0, WordLength - 1));
            characterIndex.Add(-1);
        }

        public string Current
        {
            get
            {
                var result = new StringBuilder(_wordElement.Root);
                for (var i = 0; i < WordLength; i++)
                {
                    result[i] = _wordElement.GetVariantAtIndex(i)[characterIndex[i]];
                }

                return result.ToString();
            }
        }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            return NextVariation();
        }

        private bool NextVariation()
        {
            // work backwords through the characters and try to find an index to increment
            // if all are at max position then return false
            for (var i = characterIndex.Count - 1; i >= 0; i--)
            {
                var maxIndex = _wordElement.GetVariantAtIndex(i).Count() - 1;
                if (characterIndex[i] == maxIndex) continue;

                characterIndex[i]++;
                for (var j = i + 1; j < characterIndex.Count; j++)
                {
                    characterIndex[j] = 0;
                }

                return true;
            }

            return false;
        }

        public void Reset()
        {
            characterIndex.ForEach(i => i = 0);
            characterIndex[WordLength - 1] = -1;
        }
    }

}