using System;

namespace aFixedThreadPool
{
    public class LogConsole : ILog
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine(string.Format("{0:MM / dd / yy H: mm:ss}\t{1}", DateTime.Now.ToString(), message));
        }

        public void WriteMessage(string message, object parameter)
        {
            if (parameter is ConsoleColor)
                Console.ForegroundColor = (ConsoleColor)parameter;
            WriteMessage(message);
        }
    }
}