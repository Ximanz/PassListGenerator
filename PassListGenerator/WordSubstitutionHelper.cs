using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PassListGenerator.CharacterVariants;
using PassListGenerator.WordVariants;

namespace PassListGenerator
{
    public static class WordSubstitutionHelper
    {
        public static void GenerateWordSubstitutes(this List<string> wordList, List<ICharacterVariation> characterModifiers)
        {
            var expandedList = new List<string>();

            foreach (var word in wordList)
            {
                expandedList.AddRange(WordVariation.GenerateWordVariations(word, characterModifiers));
            }

            wordList.Clear();
            wordList.AddRange(expandedList);
        }
    }
}
