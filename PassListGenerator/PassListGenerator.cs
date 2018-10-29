using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using PassListGenerator.Data;
using PassListGenerator.CharacterModifier;
using PassListGenerator.Logging;

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
            AddWordElements(wordBank);

            var permutationCount = new Dictionary<int, long>();
            for (var totalElements = _minimum; totalElements <= _maximum; totalElements++)
                permutationCount.Add(totalElements, wordBank.CountPermutations(totalElements));
            Console.WriteLine($"A total of {permutationCount.Values.Sum()} permutations will be generated.");

            using (var logger = new FileLogger())
            {
                for (var totalElements = _minimum; totalElements <= _maximum; totalElements++)
                {
                    Console.WriteLine($"Generating {permutationCount[totalElements]} {totalElements} word permutations...");
                    wordBank.MaxElements = totalElements;
                    foreach (var phrase in wordBank)
                    {
                        logger.Log(phrase);
                    }
                }
            }
        }

        private void AddWordElements(WordBank wordBank)
        {
            Console.WriteLine("Adding base words...");

            var symbolMapProvided = !string.IsNullOrWhiteSpace(_symbolMap);
            ICharacterModifier symbolModifier = null;
            if (symbolMapProvided) symbolModifier = new CharacterSymbolModifier(_symbolMap);
            var caseModifier = new CharacterCaseModifier();

            foreach (var inputElement in _inputElements)
            {
                WordElement wordElement = new WordElement(inputElement.Key);

                Console.Write($"Word: '{inputElement.Key}'");

                var characterModifiers = new List<ICharacterModifier>();
                if (symbolMapProvided && inputElement.Value.SymbolVariation)
                {
                    Console.Write(" + symbol substitution");
                    wordElement.AddCharacterModifier(symbolModifier);
                }
                if (inputElement.Value.CaseVariation)
                {
                    Console.Write(" + case variation");
                    wordElement.AddCharacterModifier(caseModifier);
                }

                Console.Write(" ... ");
                wordElement.GenerateCharacterVariations();
                Console.WriteLine($"{wordElement.Count} variants");
                wordBank.AddWord(wordElement);
            }

            Console.WriteLine();
        }
    }
}
