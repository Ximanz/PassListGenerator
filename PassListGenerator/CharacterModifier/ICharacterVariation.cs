using System.Collections.Generic;

namespace PassListGenerator.CharacterModifier
{
    public interface ICharacterVariation
    {
        List<char> GenerateCharacterVariations(char c);

        int CharacterVariationCount(char c);
    }
}