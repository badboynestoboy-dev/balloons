namespace Balloons.Features.UI
{
    using Balloons.Features.Gameplay;

    /// <summary>
    /// Класс для обновления представления окна игры
    /// </summary>
    public class GameplayPresenter
    {
        private const string SCORE_FORMAT = "Score: {0}";

        private readonly IGameActivity _gameActivity = default;

        private readonly SuccessfulTapsCounter _tapsCounter = default;

        private readonly GameplayWindow _gameWindow = default;

        public GameplayPresenter(IGameActivity gameActivity, SuccessfulTapsCounter tapsCounter, GameplayWindow gameWindow)
        {
            _gameActivity = gameActivity;
            _tapsCounter = tapsCounter;
            _gameWindow = gameWindow;
            _gameWindow.Close();
        }

        /// <summary>
        /// Начать отслеживание данных игровой активности
        /// </summary>
        public void ObserveGameActivity()
        {
            _gameActivity.OnGameStart += HandleGameStart;
            _gameActivity.OnGameFinish += HandleGameFinish;
            _gameWindow.OnPauseClick += HandlePauseClick;
            _tapsCounter.OnScoreChanged += HandleScoreChanges;
        }

        private void HandleGameStart() => _gameWindow.gameObject.SetActive(true);

        private void HandleGameFinish() => _gameWindow.Close();

        private void HandleScoreChanges() => _gameWindow.UpdateScore(string.Format(SCORE_FORMAT,_tapsCounter.Score));

        private void HandlePauseClick() => _gameActivity.PauseGame();

        ~GameplayPresenter()
        {
            _gameActivity.OnGameStart -= HandleGameStart;
            _gameActivity.OnGameFinish -= HandleGameFinish;
            _gameWindow.OnPauseClick -= HandlePauseClick;
            _tapsCounter.OnScoreChanged -= HandleScoreChanges;
        }
    }
}