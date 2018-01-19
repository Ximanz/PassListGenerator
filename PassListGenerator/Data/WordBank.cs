using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassListGenerator.Data
{
    public class WordBank : IEnumerable
    {
        private List<WordVariants> _elements;
        public List<WordVariants> Elements => _elements;

        private int _maxElements;
        public int MaxElements
        {
            get { return _maxElements; }
            set { _maxElements = value; }
        }

        public WordBank()
        {
            _elements = new List<WordVariants>();
            _maxElements = 1;
        }

        public void AddWord(WordVariants word)
        {
            _elements.Add(word);
        }

        public WordVariants GetRootVariants(string root)
        {
            return _elements.SingleOrDefault(w => w.Root.Equals(root));
        }

        public PhraseEnum GetEnumerator()
        {
            return new PhraseEnum(this, _maxElements);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public int Count => _elements.Count;
    }

    public class PhraseEnum : IEnumerator
    {
        WordBank _wordBank;
        List<string> _phrases;
        List<string> _currentPhrase;

        List<int> variantIndex;
        int phraseIndex;
        int _totalElements;

        public PhraseEnum(WordBank wordBank, int totalElements)
        {
            _wordBank = wordBank;
            _totalElements = totalElements;

            variantIndex = new List<int>(_totalElements);
            variantIndex.AddRange(Enumerable.Repeat(0, _totalElements - 1));
            variantIndex.Add(-1);

            phraseIndex = 0;

            _phrases = PermutatePhrase(_totalElements, new List<int>()).ToList();
            _currentPhrase = _phrases[phraseIndex].Split(',').ToList();
        }

        private IEnumerable<string> PermutatePhrase(int wordsToUse, List<int> skipIndices)
        {
            if (skipIndices.Count == _wordBank.Count) yield return string.Empty;

            for (var index = 0; index < _wordBank.Count; index++)
            {
                if (skipIndices.Contains(index)) continue;
                if (wordsToUse == 1)
                {
                    yield return _wordBank.Elements[index].Root;
                    continue;
                }

                var newSkipIndices = new List<int>(skipIndices);
                newSkipIndices.Add(index);
                foreach (var element in PermutatePhrase(wordsToUse - 1, newSkipIndices))
                {
                    yield return string.Join(",", _wordBank.Elements[index].Root, element);
                }
            }
        }

        public string Current
        {
            get
            {
                var result = string.Empty;
                for (var i = 0; i < _totalElements; i++)
                {
                    result = string.Concat(result, _wordBank.GetRootVariants(_currentPhrase[i]).GetVariantAtIndex(variantIndex[i]));
                }

                return result;
            }
        }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (!NextPermutation())
                return NextPhrase();

            return true;
        }

        private bool NextPermutation()
        {
            // work backwords in the current phrase and try to find an index to increment
            // if all are at max position then return false
            for (var i = variantIndex.Count - 1; i >= 0; i--)
            {
                var maxIndex = _wordBank.GetRootVariants(_currentPhrase[i]).Count - 1;
                if (variantIndex[i] == maxIndex) continue;

                variantIndex[i]++;
                for (var j = i + 1; j < variantIndex.Count; j++)
                {
                    variantIndex[j] = 0;
                }

                return true;
            }

            return false;
        }

        private bool NextPhrase()
        {
            if (phraseIndex == _phrases.Count - 1) return false;

            _currentPhrase = _phrases[++phraseIndex].Split(',').ToList();
            for (var i = 0; i < _totalElements; i++)
            {
                variantIndex[i] = 0;
            }
            return true;
        }

        public void Reset()
        {
            phraseIndex = 0;
            variantIndex.ForEach(i => i = 0);
            variantIndex[_totalElements - 1] = -1;
        }
    }
}
