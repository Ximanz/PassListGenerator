I created this because I encrypted a file a long time ago and I forgot my password, but I know that it's probably some permutation of a few phrases/words that I regularly use in passwords, with some characters swapped for numbers, case changes, etc..  So I needed a way to generate a list of all possible permutations of those phrases so I could crack the encryption by brute force.  Hopefully this can be useful to someone else with a similar problem.

# Parameters
Run this on the command line to get a list of input parameters, but here they are:

**-i, --input**         Required. Path to the input file containing word elements to use for generating pass phrases.

**-m, --minimum**       (Default: 1) Minimum number of input elements to use. 0 = use all elements

**-x, --maximum**       (Default: 0) Maximum number of input elements to use. 0 = use all elements

**-s, --symbol-map**    Specify a mapping file to substitute characters for alternate symbols. Using this option could result in a dramatic increase in the number of variations generated.

**-v**                  Print details during execution.

# How it works
This tool takes a list of words/phrases, and generates all possible variations of that word based on options you choose, then it generates all possible permutations of passphrases using combinations of those root words.
You can specify the minimum and maximum number of words to use in a passphrase, e.g. given 6 input words, you can ask the tool to generate all permutations using at least 2, but no more than 4 words at a time.

# Inputs
Basically this tool takes a JSON file as input, there's an example included in the repo but here's what it looks like:
```
{
	"Correct": { "SymbolVariation": true, "CaseVariation":  true},
	"Horse": { "SymbolVariation": false, "CaseVariation":  false},
	"Battery": { "SymbolVariation": true, "CaseVariation":  false},
	"Staple": { "SymbolVariation": false, "CaseVariation":  true}
}
```
So each JSON Name is a word that will be used to generate the password combinations, and the Value is an object with two booleans:
* SymbolVariation specifies whether to generate variants of the word by replacing certain characters with other alternatives.
* CaseVariation specifies whether to generate variants of the word with combinations upper and lower casing.

If CaseVariation is true then a second JSON file must also be specified as an argument, an example of this is also included, which looks like:
```
{
	"a": ["4", "@"],
	"b": ["8"],
	"e": ["3"]
}
```
This tells the tool that whenever the character "a" appears in a word, generate variations of that word with "a" replaced by "4", and "@", etc.

# Example usage
An example usage would be to run:
```
PassListGenerator.exe -i "WordList.json" -s "CharacterMap.json" -m 2 -x 0 -v
```
This uses the sample input files provided, and generates all passphrase permutations with a combination of at 2, 3, and 4 words.  It will go up to 4 because the -x parameter is set to 0, but you can specifiy a maximum to go up to.

# Output
The results will be saved in a .txt file with a timestamp of the current time in the filename.

