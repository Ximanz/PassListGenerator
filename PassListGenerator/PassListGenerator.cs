using System;
using PassListGenerator.CharacterModifier;
using PassListGenerator.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PassListGeneratorTests")]

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

            Console.WriteLine($"Generating password list with a total of {_inputElements.Count()} base words.");
            Console.WriteLine(_maximum == _minimum
                ? $"Results will contain combinations of {_maximum} base words."
                : $"Results will contain combinations of {_minimum} to {_maximum} base words.");

            Console.WriteLine();
        }

        public void GeneratePasswordList()
        {
            var results = new List<string>();
            var wordList = GenerateWordVariations().ToList();

            var permutationCount = 0;
            for (var totalElements = _minimum; totalElements <= _maximum; totalElements++)
                permutationCount += PhraseHelper.CountPermutations(wordList, totalElements);
            Console.WriteLine($"A total of {permutationCount} permutations will be generated.");

            for (var totalElements = _minimum; totalElements <= _maximum; totalElements++)
            {
                Console.WriteLine($"Generating {totalElements} word permutations...");
                results.AddRange(PhraseHelper.PermutatePhrase(wordList, totalElements, new List<int>()));
            }

            Console.WriteLine("Writing results to output.txt");
            System.IO.File.WriteAllLines("output.txt", results.ToArray());
            Console.WriteLine("All done.");
        }

        private List<List<string>> GenerateWordVariations()
        {
            Console.WriteLine("Generating variations for base words...");

            var expandedWordsCollection = new List<List<string>>();
            var symbolMapProvided = !string.IsNullOrWhiteSpace(_symbolMap);
            ICharacterVariation symbolModifier = null; 
            if (symbolMapProvided) symbolModifier = new CharacterSymbolModifier(_symbolMap);
            var caseModifier = new CharacterCaseModifier();

            foreach (var inputElement in _inputElements)
            {
                var expandedWordList = new List<string>();
                expandedWordList.Add(inputElement.Key);

                Console.Write($"Word: '{inputElement.Key}'");

                var characterModifiers = new List<ICharacterVariation>();
                if (symbolMapProvided && inputElement.Value.SymbolVariation)
                {
                    Console.Write(" + symbol substitution");
                    characterModifiers.Add(symbolModifier);
                }
                if (inputElement.Value.CaseVariation)
                {
                    Console.Write(" + case variation");
                    characterModifiers.Add(caseModifier);
                }

                Console.Write(" ... ");

                if (characterModifiers.Count > 0)
                    expandedWordList.AddRange(WordHelper.GenerateWordVariations(inputElement.Key, characterModifiers));

                Console.WriteLine($"{expandedWordList.Count()} variants");
                expandedWordsCollection.Add(expandedWordList);
            }

            Console.WriteLine();
            return expandedWordsCollection;
        }
    }
}
