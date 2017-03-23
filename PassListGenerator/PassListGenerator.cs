using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                for (var element = 1; element <= totalElements; element++)
                {
                    
                }
            }
        }

        private int CalculateTotalPermutations()
        {
            var result = 0;

            return result;
        }

        private List<string> GeneratePhrasePermutations(int length)
        {
            var results = new List<string>();

            results = PermutatePhrase(_inputElements, 1, length);

            return results;
        }

        private List<string> PermutatePhrase(List<string> source, int size, int length)
        {
            var results = new List<string>();



            return results;
        }
    }
}
