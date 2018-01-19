using System.Collections.Generic;

namespace PassListGenerator.CharacterModifier
{
    public interface ICharacterModifier
    {
        List<char> GenerateCharacterVariations(char c);

        int CharacterVariationCount(char c);
    }
}