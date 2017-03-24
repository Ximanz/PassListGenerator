using Newtonsoft.Json;
using System;

namespace PassListGenerator
{
    public static class Utility
    {
        public static T ReadInputFromFile<T>(string fileName)
        {
            var results = default(T);

            if (System.IO.File.Exists(fileName))
            {
                var json = System.IO.File.ReadAllText(fileName);
                results = JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                Console.WriteLine($"Can't find {fileName}.");
                throw new ArgumentException("Invalid filename");
            }

            return results;
        }
    }
}
