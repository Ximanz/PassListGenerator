using CommandLine;

namespace PassListGenerator
{
    class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input file containing word elements to use for generating pass phrases.")]
        public string InputFile { get; set; }

        [Option('m', "minimum", DefaultValue = 1, HelpText = "Minimum number of input elements to use. 0 = use all elements")]
        public int Minimum { get; set; }

        [Option('x', "maximum", DefaultValue = 0, HelpText = "Maximum number of input elements to use. 0 = use all elements")]
        public int Maximum { get; set; }

        [Option('s', "symbol-map", Required = false, HelpText = "Specify a mapping file to substitute characters for alternate symbols. Using this option could result in a dramatic increase in the number of variations generated.")]
        public string SymbolMap { get; set; }

        [Option('v', null, HelpText = "Print details during execution.")]
        public bool Verbose { get; set; }

    }
}
