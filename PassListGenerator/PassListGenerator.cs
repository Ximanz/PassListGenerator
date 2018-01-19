using System;
using PassListGenerator.CharacterModifier;
using PassListGenerator.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using PassListGenerator.Data;

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
            var wordBank = new WordBank();
            GenerateWordVariations(wordBank);

            var phraseHelper = new PhraseHelper(wordBank);
            var permutationCount = new Dictionary<int, int>();
            for (var totalElements = _minimum; totalElements <= _maximum; totalElements++)
                permutationCount.Add(totalElements, phraseHelper.CountPermutations(totalElements));
            Console.WriteLine($"A total of {permutationCount.Values.Sum()} permutations will be generated.");

            var outputFile = $"output-{DateTime.Now.ToFileTime()}.txt";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(outputFile))
            {
                for (var totalElements = _minimum; totalElements <= _maximum; totalElements++)
                {
                    Console.WriteLine($"Generating {permutationCount[totalElements]} {totalElements} word permutations...");
                    //results.AddRange(phraseHelper.PermutatePhrase(totalElements, new List<int>()));
                    wordBank.MaxElements = totalElements;
                    foreach (var phrase in wordBank)
                    {
                        file.WriteLine(phrase);

                    }
                }
                Console.WriteLine($"Results written to {outputFile}");
            }

            Console.WriteLine("All done.");
        }

        private void GenerateWordVariations(WordBank wordBank)
        {
            Console.WriteLine("Generating variations for base words...");

            var symbolMapProvided = !string.IsNullOrWhiteSpace(_symbolMap);
            ICharacterModifier symbolModifier = null; 
            if (symbolMapProvided) symbolModifier = new CharacterSymbolModifier(_symbolMap);
            var caseModifier = new CharacterCaseModifier();

            foreach (var inputElement in _inputElements)
            {
                WordVariants wordVariants = new WordVariants(inputElement.Key);

                Console.Write($"Word: '{inputElement.Key}'");

                var characterModifiers = new List<ICharacterModifier>();
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
                {
                    wordVariants.GenerateWordVariations(characterModifiers);
                }

                Console.WriteLine($"{wordVariants.Count} variants");
                wordBank.AddWord(wordVariants);
            }

            Console.WriteLine();
        }
    }
}
