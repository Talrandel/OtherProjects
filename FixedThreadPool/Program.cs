using System;
using System.Threading;

namespace aFixedThreadPool
{
    class Program
    {
        static void Main(params string[] args)
        {
            // Задаем режим работы лога
            int logMode = 0;
            do
            {
                Console.WriteLine("Задайте режим работы лога: 1 - консольный, 2 - файловый");
            } while (!int.TryParse(Console.ReadLine(), out logMode));

            ILog logger = null;
            switch (logMode)
            {
                case 1:
                    logger = new LogConsole();
                    break;
                case 2:
                    logger = new LogFile();
                    break;
                default:
                    break;
            }

            // Задаем количество потоков для одновременного выполнения
            int threadsCount = 0;
            do
            {
                Console.WriteLine("Задайте количество одновременно выполняемых задач ( > 0): ");
            } while (!int.TryParse(Console.ReadLine(), out threadsCount) && threadsCount > 0);

            // Создаем экземпляр пула задач с заданным количеством одновременно выполняемых задач (потоков) и логгером
            FixedThreadPool pool = new FixedThreadPool(threadsCount, logger, logger);

            // Задаем количество задач, которые нужно выполнить
            int tasksCount = 0;
            do
            {
                Console.WriteLine("Задайте количество задач для выполнения ( > 0): ");
            } while (!int.TryParse(Console.ReadLine(), out tasksCount) && tasksCount > 0);

            // Массив выполняемых задач
            Task[] tasks = new Task[tasksCount];

            // Генератор ПсевдоСлучайных Чисел
            Random rand = new Random();

            // Создаем задачи
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task( () => 
                {
                    logger.WriteMessage("\tВ задаче сгенерировалось ПСЧ: " + rand.Next());
                    Thread.Sleep(1000);
                });
            }

            // Отправляем задачи на выполнение
            for (int i = 0; i < tasks.Length; i++)
            {
                switch (rand.Next(1, 9))
                {
                    case 1:
                        pool.Execute(tasks[i], TaskPriority.Low);
                        break;
                    case 2:
                    case 3:
                        pool.Execute(tasks[i], TaskPriority.Normal);
                        break;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        pool.Execute(tasks[i], TaskPriority.High);
                        break;
                    default:
                        break;
                }
            }
        }
    }   
}