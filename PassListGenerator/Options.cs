using CommandLine;

namespace PassListGenerator
{
    class Options
    {
        [Option('i', "input", Required = false, HelpText = "Input file to read. If none is provided then input elements must be entered manually.")]
        public string InputFile { get; set; }

        [Option('m', "minimum", DefaultValue = 1, HelpText = "Minimum number of input elements to use. 0 = use all elements")]
        public int Minimum { get; set; }

        [Option('x', "maximum", DefaultValue = 0, HelpText = "Maximum number of input elements to use. 0 = use all elements")]
        public int Maximum { get; set; }

        [Option('s', "substitute", DefaultValue = false, HelpText = "Should include character substitution variants? Setting this option will generate more results with letters substituted for numerals (e.g. e = 3, a = 4, etc..)")]
        public bool Substitute { get; set; }

        [Option('c', "change-case", DefaultValue = false, HelpText = "Should include case changed variants? Setting this option will generate more results with different combination of upper and lower cases.")]
        public bool ChangeCase { get; set; }

        [Option('v', null, HelpText = "Print details during execution.")]
        public bool Verbose { get; set; }

    }
}
