namespace Balloons.Features.Gameplay
{
    using Saves;

    /// <summary>
    /// Запись результатов игры
    /// </summary>
    public sealed class ScoreDataSetter
    {
        private readonly IUserDataHandler _dataHandler = default;

        public ScoreDataSetter(IUserDataHandler dataHandler) => _dataHandler = dataHandler;

        /// <summary>
        /// Сохранить результат
        /// </summary>
        /// <param name="score">Кол-во очков</param>
        public void SaveResult(int score)
        {
            if (score > 0)
            {
                _dataHandler.AddResult(score);
            }
        }
    }
}