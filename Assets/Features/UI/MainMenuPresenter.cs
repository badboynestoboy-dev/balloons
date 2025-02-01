namespace Balloons.Features.UI
{
    using Saves;
    using Gameplay;

    /// <summary>
    /// Класс для обновления представления окна меню
    /// </summary>
    public sealed class MainMenuPresenter
    {
        private const string USERNAME_DUMMY = "Enter name...";

        private readonly IUserDataHandler _userDataHandler = default;

        private readonly IGameActivity _gameActivity = default;

        private readonly ScoresPresenter _scoresPresenter = default;

        private readonly MenuWindow _menuWindow = default;

        public MainMenuPresenter(IUserDataHandler userDataHandler, IGameActivity gameActivity, ScoresPresenter scoresPresenter, MenuWindow menuWindow)
        {
            _userDataHandler = userDataHandler;
            _gameActivity = gameActivity;
            _scoresPresenter = scoresPresenter;
            _menuWindow = menuWindow;
            _gameActivity.OnGameFinish += OpenMenu;
            _menuWindow.OnPlayClick += HandlePlayClick;
            _menuWindow.OnScoresClick += HandleScoresClick;
            _menuWindow.OnUsernameEdit += HandleUsernameEdit;
        }

        private void HandleScoresClick() => _scoresPresenter.OpenScoresWindow();

        /// <summary>
        /// Открыть окно меню
        /// </summary>
        public void OpenMenu()
        {
            _menuWindow.gameObject.SetActive(true);
            SetUsername(_userDataHandler.UserData.UserName);
        }

        private void SetUsername(string username) => _menuWindow.SetCurrentUsername(string.IsNullOrWhiteSpace(username) ? USERNAME_DUMMY : username);

        private void HandleUsernameEdit(string username)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                _userDataHandler.UserData.UserName = username;
                _userDataHandler.SaveData();
            }
        }

        private void HandlePlayClick()
        {
            _gameActivity.StartGame();
            _menuWindow.Close();
        }

        ~MainMenuPresenter()
        {
            _gameActivity.OnGameFinish -= OpenMenu;
            _menuWindow.OnPlayClick -= HandlePlayClick;
            _menuWindow.OnScoresClick -= HandleScoresClick;
            _menuWindow.OnUsernameEdit -= HandleUsernameEdit;
        }
    }
}