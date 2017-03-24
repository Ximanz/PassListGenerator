using System.Collections.Generic;

namespace PassListGenerator.CharacterVariants
{
    public interface ICharacterVariation
    {
        List<char> GenerateCharacterVariations(char c);
    }
}