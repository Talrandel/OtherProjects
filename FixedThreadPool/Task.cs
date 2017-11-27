using System;
using System.Threading;

namespace aFixedThreadPool
{
    public class Task
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр задачи для выполнения в <see cref="FixedThreadPool1"/>
        /// делегатом тела задачи.
        /// </summary>
        /// <param name="aTaskBody">
        /// Делегат тела задачи.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Делегат тела задачи не задан.
        /// </exception>
        public Task(Action aTaskBody)
        {
            if (aTaskBody == null)
                throw new ArgumentNullException("aTaskBody", "Делегат тела задачи не задан.");

            TaskBody = aTaskBody;
        }

        #endregion

        #region Public prope        

        /// <summary>
        /// Получает/устанавливает делегат тела задачи.
        /// </summary>
        public Action TaskBody { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// Событие, сообщающее о завершении выполнения задачи.
        /// </summary>
        public event EventHandler Finished;

        #endregion

        #region Public methods

        /// <summary>
        /// Начинает выполнение задачи.
        /// </summary>
        public void Execute()
        {
            Thread lTaskThread =
                new Thread(
                    () =>
                    {
                        // Выполнить задачу.
                        TaskBody();
                        // Уведомить об её окончании.
                        Finished?.Invoke(this, EventArgs.Empty);
                    })
                { Name = "Task thread." };
            lTaskThread.Start();
        }

        #endregion
    }
}
