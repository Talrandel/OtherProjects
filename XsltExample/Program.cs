using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace XsltExample
{
    class Program
    {
        /// <summary>
        /// Путь к файлу xslt
        /// </summary>
        const string XSLT_FILE = "transform.xslt";

        /// <summary>
        /// Объект трансформации
        /// </summary>
        static XslCompiledTransform xslt;

        //static XsltArgumentList xsltArguments;

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
            var fileExtension = args[1];
            if (string.IsNullOrEmpty(fileExtension))
            {
                Console.WriteLine("No output file.");
                return;
            }
            bool condition = false;
            var condition_t = args[2];
            if (!string.IsNullOrEmpty(args[2]))
                bool.TryParse(args[2], out condition);

            // Сам объект трансформации
            xslt = new XslCompiledTransform(true);
            // Загрузка и компиляция Xslt
            using (var sourceFile = new StreamReader(XSLT_FILE))
            {
                using (var reader = XmlReader.Create(sourceFile))
                {
                    xslt.Load(reader);
                }
            }
            // Параметры, передаваемые трансформации
            //xsltArguments = new XsltArgumentList();

            //var files = Directory.GetFiles(startPath, fileExtension, SearchOption.AllDirectories);
            //foreach (var f in files)
            //{
            //    ProcessFile(f);
            //    if (condition)
            //        Process.Start("notepad++", f);
            //}

            var files = Directory.GetFiles(startPath, fileExtension, SearchOption.TopDirectoryOnly);
            foreach (var f in files)
            {
                ProcessFile(f);
                if (condition)
                    Process.Start("notepad++", f);
            }
        }

        static void ProcessFile(string fileName)
        {
            var destFile = string.Empty;
            // Трансформация 
            using (var sourceFile = new StreamReader(fileName))
            {
                using (var xmlInput = XmlReader.Create(sourceFile))
                {
                    destFile = Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName) + "_new" + Path.GetExtension(fileName);
                    using (var xmlOutput = XmlWriter.Create(destFile, xslt.OutputSettings))
                    {
                        //if (xsltArguments == null)
                        xslt.Transform(xmlInput, xmlOutput);
                        //else
                        //    xslt.Transform(xmlInput, xsltArguments, xmlOutput, null);
                        xmlOutput.Flush();
                    }
                }
            }
            var backUp = fileName + ".backup";
            // Замена исходного файла новым + бэкап
            File.Replace(destFile, fileName, backUp);
            // Удалить бэкап
            File.Delete(backUp);
        }
    }
}