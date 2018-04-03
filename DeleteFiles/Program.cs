using System;
using System.IO;

namespace DeleteFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("No arguments, restart with arguments: start path + whitespace + file extension to process + whitespace + condition to open files in notepad++");
                return;
            }
            var startPath = args[0];
            if (string.IsNullOrEmpty(startPath) || !Directory.Exists(startPath))
            {
                Console.WriteLine("No such directory: " + startPath);
                return;
            }
            foreach (var file in Directory.GetFiles(args[0], "*.resx", SearchOption.AllDirectories))
            {
                if (!file.Contains("ja.resx") && !file.Contains("es.resx") &&
                    !file.Contains("de.resx")) continue;
                Console.WriteLine(file);
                File.Delete(file);
            }
            
            Console.WriteLine("Ready!");
            Console.ReadLine();
        }
    }
}