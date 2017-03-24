using PassListGenerator.CharacterModifier;
using PassListGenerator.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace PassListGenerator
{
    class PassListGenerator
    {
        private readonly Dictionary<string, WordOptions> _inputElements;
        private readonly string _symbolMap;
        private readonly int _minimum;
        private readonly int _maximum;
        private bool _verbose;

        internal class WordOptions
        {
            public bool SymbolVariation = false;
            public bool CaseVariation = false;
        }

        public PassListGenerator(Options options)
        {
            _inputElements = Utility.ReadInputFromFile<Dictionary<string, WordOptions>>(options.InputFile);
            _symbolMap = options.SymbolMap;
            _verbose = options.Verbose;
            _minimum = (options.Minimum == 0 || options.Minimum > _inputElements.Count) ? _inputElements.Count : options.Minimum;
            _maximum = (options.Maximum == 0 || options.Maximum > _inputElements.Count) ? _inputElements.Count : options.Maximum;
            if (_maximum < _minimum) _maximum = _minimum;
        }

        public void GeneratePasswordList()
        {
            var results = new List<string>();
            var wordList = GenerateWordVariations().ToList();

            for (var totalElements = _minimum; totalElements <= _maximum; totalElements++)
            {
                results.AddRange(PhraseHelper.PermutatePhrase(wordList, totalElements));
            }

            System.IO.File.WriteAllLines("output.txt", results.ToArray());
        }

        private IEnumerable<IEnumerable<string>> GenerateWordVariations()
        {
            var expandedWordsCollection = new List<List<string>>();
            var symbolMapProvided = !string.IsNullOrWhiteSpace(_symbolMap);
            ICharacterVariation symbolModifier = null; 
            if (symbolMapProvided) symbolModifier = new CharacterSymbolModifier(_symbolMap);
            var caseModifier = new CharacterCaseModifier();

            foreach (var inputElement in _inputElements)
            {
                var expandedWordList = new List<string>();
                expandedWordList.Add(inputElement.Key);

                var characterModifiers = new List<ICharacterVariation>();
                if (symbolMapProvided && inputElement.Value.SymbolVariation) characterModifiers.Add(symbolModifier);
                if (inputElement.Value.CaseVariation) characterModifiers.Add(caseModifier);

                if (characterModifiers.Count > 0)
                    expandedWordList.AddRange(WordHelper.GenerateWordVariations(inputElement.Key, characterModifiers));

                expandedWordsCollection.Add(expandedWordList);
            }

            return expandedWordsCollection;
        }
    }
}
