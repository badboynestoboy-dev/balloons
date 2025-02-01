namespace Balloons.Features.UI
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Представление окна главного меню
    /// </summary>
    public sealed class MenuWindow : BaseWindow
    {
        /// <summary>
        /// Событие, сообщающее об изменении имени пользователя
        /// Передает в параметрах результат изменения
        /// </summary>
        public event Action<string> OnUsernameEdit = delegate { };

        /// <summary>
        /// Событие о клике на кнопку запуска игры
        /// </summary>
        public event Action OnPlayClick = delegate { };

        /// <summary>
        /// Событие о клике на кнопку показа результатов
        /// </summary>
        public event Action OnScoresClick = delegate { };

        [SerializeField]
        private InputField _usernameInputField = default;

        [SerializeField]
        private Button _playButton = default;
        [SerializeField]
        private Button _scoresButton = default;

        /// <summary>
        /// Отобразить текущее имя пользователя
        /// </summary>
        /// <param name="username">имя пользователя</param>
        public void SetCurrentUsername(string username)
            => _usernameInputField.SetTextWithoutNotify(username);

        private void OnEnable()
        {
            _usernameInputField.onEndEdit.AddListener(NotifyOfUsernameEdit);
            _playButton.onClick.AddListener(NotifyOfPlayClick);
            _scoresButton.onClick.AddListener(NotifyOfScoresClick);
        }

        private void NotifyOfScoresClick() => OnScoresClick();

        private void NotifyOfPlayClick() => OnPlayClick();

        private void NotifyOfUsernameEdit(string nameInput) => OnUsernameEdit(nameInput);

        private void OnDisable()
        {
            _usernameInputField.onEndEdit.RemoveListener(NotifyOfUsernameEdit);
            _playButton.onClick.RemoveListener(NotifyOfPlayClick);
            _scoresButton.onClick.RemoveListener(NotifyOfScoresClick);
        }
    }
}