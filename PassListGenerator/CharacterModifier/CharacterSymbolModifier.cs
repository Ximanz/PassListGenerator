using System.Collections.Generic;

namespace PassListGenerator.CharacterModifier
{
    public class CharacterSymbolModifier : ICharacterVariation
    {
        private readonly Dictionary<char, char[]> _characterMap = new Dictionary<char, char[]>();

        public CharacterSymbolModifier(string mapFile)
        {
            _characterMap = Utility.ReadInputFromFile<Dictionary<char, char[]>>(mapFile);
        }

        public List<char> GenerateCharacterVariations(char c)
        {
            var results = new List<char>();

            if (_characterMap.ContainsKey(char.ToLower(c))) results.AddRange(_characterMap[char.ToLower(c)]);
            if (_characterMap.ContainsKey(char.ToUpper(c))) results.AddRange(_characterMap[char.ToUpper(c)]);

            return results;
        }

        public int CharacterVariationCount(char c)
        {
            return _characterMap.ContainsKey(c) ? _characterMap[c].Length : 0;
        }
    }
}
