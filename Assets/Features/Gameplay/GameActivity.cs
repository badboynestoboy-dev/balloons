namespace Balloons.Features.Gameplay
{
    using System;
    using FloatingObjects;
    using UnityEngine;

    /// <summary>
    /// Реализация класса, контролирующего статус игры
    /// </summary>
    public sealed class GameActivity : IGameActivity
    {
        public event Action OnGameStart = delegate { };

        public event Action OnGameFinish = delegate { };

        public event Action OnPause = delegate { };

        private readonly IFloatingObjectsSpawner _spawner = default;

        private readonly SuccessfulTapsCounter _tapsCounter = default;

        private readonly ScoreDataSetter _dataSetter = default;

        public GameActivity(IFloatingObjectsSpawner spawner, SuccessfulTapsCounter tapsCounter, ScoreDataSetter dataSetter)
        {
            _spawner = spawner;
            _tapsCounter = tapsCounter;
            _dataSetter = dataSetter;
        }

        public void StartGame()
        {
            _tapsCounter.ResetScore();
            OnGameStart();
            ContinueGame();
        }

        public void ContinueGame()
        {
            SetTimeScale(1);
            _tapsCounter.ObserveTaps();
            _spawner.StartSpawn();
        }

        public void FinishGame()
        {
            SetTimeScale(1);
            _tapsCounter.StopObservingTaps();
            _spawner.StopSpawn();
            _dataSetter.SaveResult(_tapsCounter.Score);
            OnGameFinish();
        }

        public void PauseGame()
        {
            SetTimeScale(0);
            _tapsCounter.StopObservingTaps();
            OnPause();
        }

        private void SetTimeScale(int scaleValue) => Time.timeScale = scaleValue;
    }
}