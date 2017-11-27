using System;
using System.Threading;

namespace KasperskyTask1
{
    internal class Program
    {
        // Здесь представлен тестовый вариант работы с потокобезопасной очередью, что я описал в соседнем классе

        private static void Main(string[] args)
        {
            var q = new SyncQueue<int>();

            var t1 = new Thread(() => 
            {
                var rand = new Random();
                for (var i = 0; i < 25; i++)
                {
                    q.Push(i);
                    Console.WriteLine("Добавили {0} в очередь.", i);
                    Thread.Sleep(rand.Next(200, 601));
                }
            });

            var t2 = new Thread(() =>
            {
                var rand = new Random();
                for (var i = 0; i < 25; i++)
                {
                    var item = q.Pop();
                    Console.WriteLine("Извлекли {0} из очереди.", item);
                    Thread.Sleep(rand.Next(100, 301));
                }
            });

            t1.Start();
            Thread.Sleep(1500);
            t2.Start();
            t2.Join();

            Console.WriteLine("Нажми Enter для выхода.");
            Console.ReadLine();
        }
    }
}