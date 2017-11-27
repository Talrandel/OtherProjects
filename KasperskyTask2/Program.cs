using System;

namespace KasperskyTask2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int length;
            do
            {
                Console.WriteLine("Введи количество элементов в коллекции:");
            } while (!int.TryParse(Console.ReadLine(), out length));

            var numbers = new NumbersCollection(length);
            while (true)
            {
                int x;
                do
                {
                    Console.WriteLine("Введи число для сравнения с парами чисел коллекции:");
                } while (!int.TryParse(Console.ReadLine(), out x));

                numbers.FindPairsAndPrint(x);

                Console.WriteLine("Повторить сравнение с другим числом? N/n для выхода, любая клавиша для повтора");
                var readLine = Console.ReadLine();
                if (readLine != null && readLine.ToLower() == "n")
                    break;
            }
        }
    }
}