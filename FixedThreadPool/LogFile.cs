using System;
using System.IO;
using System.Reflection;

namespace aFixedThreadPool
{
    class LogFile : ILog
    {
        private static object _writeLockObject = new object();
        public string FileName { get; set; }
        public void WriteMessage(string message)
        {
            lock (_writeLockObject)
            {
                using (StreamWriter sw = new StreamWriter(FileName, true))
                {
                    sw.WriteLine(string.Format("{0:MM / dd / yy H: mm:ss}\t{1}", DateTime.Now.ToString(), message));
                }
            }
        }

        public void WriteMessage(string message, object parameter)
        {
            WriteMessage(message);
        }

        public LogFile()
        {
            FileName = Assembly.GetExecutingAssembly().GetName().Name + ".txt";
            if (File.Exists(FileName))
                File.Delete(FileName);
        }
    }
}