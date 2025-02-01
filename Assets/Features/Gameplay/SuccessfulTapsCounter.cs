namespace Balloons.Features.Gameplay
{
    using System;
    using Balloons.Features.Input;

    /// <summary>
    /// Счетчик кол-ва очков
    /// </summary>
    public sealed class SuccessfulTapsCounter
    {
        /// <summary>
        /// Событие об изменении кол-ва очков
        /// </summary>
        public event Action OnScoreChanged = delegate { };

        /// <summary>
        /// Кол-во очков
        /// </summary>
        public int Score
        {
            get => score;
            private set
            {
                score = value;
                OnScoreChanged();
            }
        }

        private readonly TappedObjectsDetector _tapsDetector = default;

        private ITapHandler lastRegisteredObject = default;
        private int score = 0;

        public SuccessfulTapsCounter(TappedObjectsDetector tapsDetector)
        {
            _tapsDetector = tapsDetector;
            ResetScore();
        }

        /// <summary>
        /// Начать отслеживание кол-ва очков
        /// </summary>
        public void ObserveTaps() => _tapsDetector.OnObjectTapDetected += HandleTap;

        /// <summary>
        /// Сбросить кол-во очков
        /// </summary>
        public void ResetScore() => Score = 0;

        /// <summary>
        /// Остановить отслеживание кол-ва очков
        /// </summary>
        public void StopObservingTaps() => _tapsDetector.OnObjectTapDetected -= HandleTap;

        private void HandleTap(ITapHandler tapHandler)
        {
            if (tapHandler != lastRegisteredObject)
            {
                lastRegisteredObject = tapHandler;
                lastRegisteredObject.HandleTap();
                Score++;
            }
        }

        ~SuccessfulTapsCounter() => StopObservingTaps();
    }
}