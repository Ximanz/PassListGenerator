using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassListGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();

            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
            {
                var inputElements = new List<string>();

                if (string.IsNullOrWhiteSpace(options.InputFile))
                {
                    inputElements = ReadInputFromConsole();
                }
                else
                {
                    inputElements = ReadInputFromFile(options.InputFile);
                }

                if (!inputElements.Any())
                {
                    Console.WriteLine("No valid input elements provided.");
                    return;
                }


                if (options.Verbose)
                {
                    Console.WriteLine(options.InputFile);
                    Console.WriteLine(options.MaximumLength);
                }
                else
                    Console.WriteLine("working ...");
            }
        }

        private static List<string> ReadInputFromFile(string fileName)
        {
            var results = new List<string>();

            if (System.IO.File.Exists(fileName))
            {
                results = System.IO.File.ReadAllLines(fileName).ToList();
                if (results.Any(line => line.Any(char.IsWhiteSpace)))
                {
                    Console.WriteLine("Some lines containing whitespaces in the input file will be ignored.");
                    results.RemoveAll(line => line.Any(char.IsWhiteSpace));
                }
            }
            else
            {
                Console.WriteLine($"Can't find {fileName}.");
            }

            return results;
        }

        private static List<string> ReadInputFromConsole()
        {
            var results = new List<string>();
            string line;

            Console.WriteLine("Enter the string elements to use for generating the password list, each element must not contain any whitespaces. Supplying an empty string at any time will begin processing.");
            do
            {
                Console.Write("Input: ");
                line = Console.ReadLine();
                if (line == null) continue;

                if (line.Any(char.IsWhiteSpace))
                {
                    Console.WriteLine("Input must not contain any whitespaces, please enter as separate elements.");
                }
                else
                {
                    results.Add(line);
                }
            } while (line != null);

            return results;
        }
    }
}
