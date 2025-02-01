namespace Balloons.Features.UI
{
    using System.Collections.Generic;
    using System.Globalization;
    using Balloons.Features.Saves;

    /// <summary>
    /// Класс для обновления представления окна результатов
    /// </summary>
    public class ScoresPresenter
    {
        private readonly IUserDataHandler _dataHandler = default;

        private readonly ScoresWindow _scoresWindow = default;

        private readonly CultureInfo _formatProvider = CultureInfo.InvariantCulture;

        private List<ScoreData> _scoresList = new List<ScoreData>();

        public ScoresPresenter (IUserDataHandler userDataHandler, ScoresWindow scoresWindow)
        {
            _dataHandler = userDataHandler;
            _scoresWindow = scoresWindow;
            _scoresWindow.OnBackClick += HandleBackClick;
            _scoresWindow.Close();
        }

        /// <summary>
        /// Открыть окно результатов
        /// </summary>
        public void OpenScoresWindow()
        {
            UpdateScores();
            _scoresWindow.gameObject.SetActive(true);
        }

        private void HandleBackClick() => _scoresWindow.Close();

        private void UpdateScores()
        {
            _scoresList = _dataHandler.UserData.Scores;
            _scoresList.Sort();
            _scoresList.ForEach(x => _scoresWindow.AddScore(x.Score.ToString(), x.TryGetDate(_formatProvider).ToString()));
        }

        ~ScoresPresenter() => _scoresWindow.OnBackClick -= HandleBackClick;
    }
}