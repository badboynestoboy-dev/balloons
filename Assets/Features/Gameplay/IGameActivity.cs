
namespace Balloons.Features.Gameplay
{
    using System;

    /// <summary>
    /// Интерфейс для контроля статуса игры
    /// </summary>
    public interface IGameActivity
    {
        public event Action OnGameStart;
        public event Action OnGameFinish;
        public event Action OnPause;

        public void StartGame();

        public void PauseGame();

        public void ContinueGame();

        public void FinishGame();
    }
}