using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassListGenerator.CharacterVariants
{
    public class CharacterSymbolVariation : ICharacterVariation
    {
        public List<char> GenerateCharacterVariations(char c)
        {
            var results = new List<char> {c};
            switch (char.ToLower(c))
            {
                case 'a':
                    results.Add('4');
                    break;
                case 'b':
                    results.Add('8');
                    break;
                case 'e':
                    results.Add('3');
                    break;
                case 'i':
                case 'l':
                    results.Add('1');
                    break;
                case 'o':
                    results.Add('0');
                    break;
                case 's':
                    results.Add('5');
                    break;
                case 't':
                    results.Add('7');
                    break;
                case 'z':
                    results.Add('2');
                    break;
            }

            return results;
        }
    }
}
