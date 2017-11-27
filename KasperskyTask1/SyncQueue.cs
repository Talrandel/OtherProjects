using System;
using System.Collections.Generic;
using System.Threading;

namespace KasperskyTask1
{
    // Исходя из задания, предполагается generic очередь, но в конце упоминается, что использовать следует просто Queue. Я делаю ставку на использование generic Queue<T>, дабы избежать проблем boxing/unboxing. Полагаю, что подразумевалось использование просто очереди, а не готовых обёрток для многопоточной работы с ней

    /// <summary>
    /// Синхронизированная/потокобезопасная обобщенная очередь
    /// </summary>
    /// <typeparam name="T">Тип данных для очереди, без ограничений</typeparam>
    internal class SyncQueue<T>
    {
        /// <summary>
        /// Обобщенная очередь 
        /// </summary>
        private readonly Queue<T> _queue;

        /// <summary>
        /// Добавление данных в очередь
        /// </summary>
        /// <param name="data"> Данные для добавления в очередь</param>
        public void Push(T data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            lock (_queue)
            {
                _queue.Enqueue(data);
                if (_queue.Count == 1)
                    Monitor.PulseAll(_queue);
            }
        }

        /// <summary>
        /// Извлечение данных из очереди.
        /// </summary>
        /// <returns> Верхний (извлеченный) элемент очереди </returns>
        public T Pop()
        {
            lock (_queue)
            {
                // Очередь пуста - блокируем поток, ждем сигнала об освобождении
                while (_queue.Count == 0)
                {
                    Monitor.Wait(_queue);
                }

                T data = _queue.Dequeue();
                Monitor.PulseAll(_queue);
                return data;
            }
        }

        /// <summary>
        /// Конструктор очереди по умолчанию
        /// </summary>
        public SyncQueue()
        {
            _queue = new Queue<T>();
        }
    }
}