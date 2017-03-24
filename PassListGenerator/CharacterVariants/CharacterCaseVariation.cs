using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassListGenerator.CharacterVariants
{
    public class CharacterCaseVariation : ICharacterVariation
    {
        public List<char> GenerateCharacterVariations(char c)
        {
            var results = new List<char> {c};

            if (char.IsLetter(c))
            {
                results.Add(char.IsLower(c) ? char.ToUpper(c) : char.ToLower(c));
            }

            return results;
        }
    }
}
