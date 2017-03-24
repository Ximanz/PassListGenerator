using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using PassListGenerator.CharacterVariants;

namespace PassListGenerator
{
    class PassListGenerator
    {
        private List<string> _inputElements;
        private int _minimum;
        private int _maximum;
        private bool _substitute;
        private bool _changeCase;
        private bool _verbose;

        public PassListGenerator(List<string> inputElements, Options options)
        {
            _inputElements = inputElements;
            _substitute = options.Substitute;
            _changeCase = options.ChangeCase;
            _verbose = options.Verbose;
            _minimum = (options.Minimum == 0 || options.Minimum > inputElements.Count) ? inputElements.Count : options.Minimum;
            _maximum = (options.Maximum == 0 || options.Maximum > inputElements.Count) ? inputElements.Count : options.Maximum;
            if (_maximum < _minimum) _maximum = _minimum;
        }

        public void GeneratePasswordList()
        {
            var results = new List<string>();

            for (var totalElements = _minimum; totalElements <= _maximum; totalElements++)
            {
                results.AddRange(GeneratePhrasePermutations(totalElements));
            }

            System.IO.File.WriteAllLines("output.txt", results.ToArray());
        }

        private int CalculateTotalPermutations()
        {
            var result = 0;

            return result;
        }

        private List<string> GeneratePhrasePermutations(int wordsToUse)
        {
            return PermutatePhrase(GenerateWordVariations(), wordsToUse).ToList();
        }

        private IEnumerable<string> PermutatePhrase(IEnumerable<string> list, int wordsToUse)
        {
            var enumerable = list as IList<string> ?? list.ToList();

            for (var index = 0; index < enumerable.Count(); index++)
            {
                if (wordsToUse == 1) yield return enumerable[index];

                var reducedList = new List<string>(enumerable);
                reducedList.RemoveAt(index);
                foreach (var element in PermutatePhrase(reducedList, wordsToUse - 1))
                {
                    yield return string.Concat(enumerable[index], element);
                }
            }
        }

        private List<string> GenerateWordVariations()
        {
            var expandedWordList = new List<string>(_inputElements);
            if (!_substitute && !_changeCase) return expandedWordList;

            var characterModifiers = new List<ICharacterVariation>();

            if (_substitute) characterModifiers.Add(new CharacterSymbolVariation());
            if (_changeCase) characterModifiers.Add(new CharacterCaseVariation());

            expandedWordList.GenerateWordSubstitutes(characterModifiers);

            return expandedWordList;
        }
    }
}
