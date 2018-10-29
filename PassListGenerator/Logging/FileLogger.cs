using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassListGenerator.Logging
{
    class FileLogger : LoggerBase
    {
        private System.IO.StreamWriter outputFile;

        public FileLogger()
        {
            outputFile = new System.IO.StreamWriter($"output-{DateTime.Now.ToFileTime()}.txt");
        }

        protected override void BulkLog(List<string> buffer)
        {
            foreach (var line in buffer)
            {
                outputFile.WriteLine(line);
            }
        }

        public override void Dispose()
        {
            Console.WriteLine($"Results written to {outputFile}");

            outputFile.Close();
            base.Dispose();
        }
    }
}
