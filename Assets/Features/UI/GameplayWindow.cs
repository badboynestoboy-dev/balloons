namespace Balloons.Features.UI
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Представление окна игры
    /// </summary>
    public sealed class GameplayWindow : BaseWindow
    {
        /// <summary>
        /// Событие о клике на паузу
        /// </summary>
        public event Action OnPauseClick = delegate { };

        [SerializeField]
        private Text _currentScore = default;

        [SerializeField]
        private Button _pauseButton = default;

        /// <summary>
        /// Обновить отображение текущего кол-ва очков
        /// </summary>
        /// <param name="score">Кол-во очков</param>
        public void UpdateScore(string score) => _currentScore.text = score;

        private void OnEnable() => _pauseButton.onClick.AddListener(NotifyOfPauseClick);

        private void NotifyOfPauseClick() => OnPauseClick();

        private void OnDisable() => _pauseButton.onClick.RemoveListener(NotifyOfPauseClick);
    }
}