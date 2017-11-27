namespace aFixedThreadPool
{
    /// <summary>
    /// Основной интерфейс для логгеров приложения
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Записать в лог данное сообщение
        /// </summary>
        /// <param name="message">Записываемое сообщение</param>
        void WriteMessage(string message);

        /// <summary>
        /// Записать в лог данное сообщение с параметром
        /// </summary>
        /// <param name="message">Записываемое сообщение</param>
        /// <param name="parameter">Вспомогательный параметр</param>
        void WriteMessage(string message, object parameter);
    }
}