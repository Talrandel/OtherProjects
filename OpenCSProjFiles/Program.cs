using System;
using System.IO;
using System.Diagnostics;

namespace OpenCSProjFiles
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("No arguments, restart with arguments: start path, extension");
                return;
            }
            var startPath = args[0];
            if (string.IsNullOrEmpty(startPath) || !Directory.Exists(startPath))
            {
                Console.WriteLine("No directory or it doesn't exists.");
                return;
            }
            var extension = args[1];
            if (string.IsNullOrEmpty(extension))
            {
                Console.WriteLine("No file extension.");
                return;
            }
            var fileNames = Directory.GetFiles(startPath, extension, SearchOption.AllDirectories);
            foreach (var f in fileNames)
            {
                Process.Start("notepad++", f);
            }
        }
    }
}