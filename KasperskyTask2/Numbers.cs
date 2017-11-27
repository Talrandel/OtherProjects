using System;
using System.Collections.Generic;

namespace KasperskyTask2
{
    /// <summary>
    /// Обёртка над коллекцией чисел, представляющая методы ее автоматической инициализации и поиска пар чисел, сумма которых равна переданному числу
    /// </summary>
    internal class NumbersCollection
    {
        /// <summary>
        /// Количество элементов коллекции по умолчанию
        /// </summary>
        private const int _listLength = 50;

        /// <summary>
        /// Нижняя граница для генерации чисел для коллекции
        /// </summary>
        private readonly int _lowerBound = 1;

        /// <summary>
        /// Верхняя граница для генерации чисел для коллекции
        /// </summary>
        private readonly int _upperBound = 101;

        /// <summary>
        /// Количество элементов коллекции
        /// </summary>
        public int ListLength { get; private set; }

        /// <summary>
        /// Коллекция чисел
        /// </summary>
        private List<int> _collection;

        /// <summary>
        /// Поиск пар значений коллекции, сумма которых равна заданному числу, и печать результатов
        /// </summary>
        /// <param name="x">Число для сравнения сумм пар чисел коллекции</param>
        public void FindPairsAndPrint(int x)
        {
            // Выведем всю коллекцию чисел
            Print();
            // Цикл до n-1 элемента
            for (var i = 0; i < ListLength - 1; i++)
            {
                var first = _collection[i];
                // Цикл от следующего за текущим числа до конца коллекции (от i до n)
                for (var j = i + 1; j < ListLength; j++)
                {
                    var second = _collection[j];
                    if (first + second != x) continue;
                    Console.WriteLine("{0} + {1} = {2}", first, second, x);
                    // Если коллекция заполняется уникальными значениями, то логично прервать дальнейший перебор, т.к. найденная пара уникальна
                    break;
                }
            }
            Console.WriteLine("\n");
        }

        /// <summary>
        /// Печать коллекции чисел
        /// </summary>
        public void Print()
        {
            Console.WriteLine("\nКоллекция чисел:\n");
            for (var i = 0; i < ListLength; i++)
                Console.Write("{0}\t", _collection[i]);

            Console.WriteLine("\n\n");
        }

        /// <summary>
        /// Заполнение коллекции чисел случайными значениями
        /// </summary>
        private void InitNumList()
        {
            var rand = new Random();
            for (var i = 0; i < ListLength; i++)
            {
                var tempNumber = 0;
                // Сгенерировать число, если такого нет в списке - добавить, иначе повторить генерацию
                while (_collection.Contains(tempNumber = rand.Next(_lowerBound, _upperBound)))
                { }
                _collection.Add(tempNumber);
            }
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public NumbersCollection()
            : this(_listLength)
        { }

        /// <summary>
        /// Конструктор с заданной длинной коллекции чисел
        /// </summary>
        /// <param name="length"></param>
        public NumbersCollection(int length)
        {
            ListLength = length;
            _collection = new List<int>(length);
            InitNumList();
        }
    }
}