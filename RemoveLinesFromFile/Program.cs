using System;
using System.IO;
using System.Linq;

namespace RemoveLinesFromFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("No arguments, restart with arguments: input file + whitespace + output file + whitespace + line to remove");
                return;
            }
            var inputFile = args[0];
            if (string.IsNullOrEmpty(inputFile) || !File.Exists(inputFile))
            {
                Console.WriteLine("No input file.");
                return;
            }
            var outputFile = args[1];
            if (string.IsNullOrEmpty(outputFile))
            {
                Console.WriteLine("No output file.");
                return;
            }
            var removeLine = args[2];
            if (string.IsNullOrEmpty(removeLine))
            {
                Console.WriteLine("No remove line.");
                return;
            }
            var lines = File.ReadAllLines(inputFile);
            var res = lines.Where(l => !l.Contains(removeLine)).ToList();
            File.WriteAllLines(outputFile, res);
        }
    }
}