using System.Collections.Generic;

namespace PassListGenerator.CharacterModifier
{
    public class CharacterCaseModifier : ICharacterVariation
    {
        public List<char> GenerateCharacterVariations(char c)
        {
            var results = new List<char>();

            if (char.IsLetter(c))
            {
                results.Add(char.IsLower(c) ? char.ToUpper(c) : char.ToLower(c));
            }

            return results;
        }

        public int CharacterVariationCount(char c)
        {
            return char.IsLetter(c) ? 1 : 0;
        }
    }
}
