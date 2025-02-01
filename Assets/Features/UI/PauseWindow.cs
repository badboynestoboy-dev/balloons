namespace Balloons.Features.UI
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Представление окна паузы
    /// </summary>
    public sealed class PauseWindow : BaseWindow
    {
        /// <summary>
        /// Событие о клике на кнопку завершения игры
        /// </summary>
        public event Action OnFinishClick = delegate { };

        /// <summary>
        /// Событие о клике на кнопку продолжения игры
        /// </summary>
        public event Action OnContinueClick = delegate { };

        [SerializeField]
        private Button _continueButton = default;
        [SerializeField]
        private Button _finishButton = default;

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(NotifyOfContinueClick);
            _finishButton.onClick.AddListener(NotifyOfFinishClick);
        }

        private void NotifyOfContinueClick() => OnContinueClick();

        private void NotifyOfFinishClick() => OnFinishClick();

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(NotifyOfContinueClick);
            _finishButton.onClick.RemoveListener(NotifyOfFinishClick);
        }
    }
}