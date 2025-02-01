namespace Balloons.Features.Gameplay
{
    using System;
    using Balloons.Features.Input;

    /// <summary>
    /// Счетчик тапов по объектам
    /// </summary>
    public sealed class SuccessfulTapsCounter
    {
        public event Action OnScoreChanged = delegate { };

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

        public void ObserveTaps() => _tapsDetector.OnObjectTapDetected += HandleTap;

        public void ResetScore() => Score = 0;

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