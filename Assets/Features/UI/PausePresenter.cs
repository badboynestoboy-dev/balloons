namespace Balloons.Features.UI
{
    using Gameplay;

    /// <summary>
    /// Класс для обновления представления окна паузы
    /// </summary>
    public sealed class PausePresenter
    {
        private readonly IGameActivity _gameActivity = default;

        private readonly PauseWindow _pauseWindow = default;

        public PausePresenter(IGameActivity gameActivity,PauseWindow pauseWindow)
        {
            _gameActivity = gameActivity;
            _pauseWindow = pauseWindow;
            _pauseWindow.Close();
        }

        /// <summary>
        /// Отслеживать игровую активность
        /// </summary>
        public void ObserveGameActivity()
        {
            _gameActivity.OnPause += HandlePause;
            _pauseWindow.OnContinueClick += HandleContinueClick;
            _pauseWindow.OnFinishClick += HandleFinishClick;
        }

        private void HandlePause() => _pauseWindow.gameObject.SetActive(true);

        private void HandleContinueClick()
        {
            _pauseWindow.Close();
            _gameActivity.ContinueGame();
        }

        private void HandleFinishClick()
        {
            _pauseWindow.Close();
            _gameActivity.FinishGame();
        }

        ~PausePresenter()
        {
            _gameActivity.OnPause -= HandlePause;
            _pauseWindow.OnContinueClick -= HandleContinueClick;
            _pauseWindow.OnFinishClick -= HandleFinishClick;
        }
    }
}