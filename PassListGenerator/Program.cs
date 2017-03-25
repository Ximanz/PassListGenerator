using System;

namespace PassListGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();

            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
            {
                var generator = new PassListGenerator(options);
                generator.GeneratePasswordList();
            }

        }
    }
}