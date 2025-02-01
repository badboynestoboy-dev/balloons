namespace Balloons.Features.Dependencies
{
    using Saves;
    using UI;
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Класс для запуска игры
    /// </summary>
    public class Bootstrapper : MonoBehaviour
    {
        [Inject]
        protected IUserDataHandler dataHandler = default;
        [Inject]
        protected MainMenuPresenter menuPresenter = default;
        [Inject]
        protected GameplayPresenter gameplayPresenter = default;
        [Inject]
        protected PausePresenter pausePresenter = default;

        protected virtual void Start()
        {
            dataHandler.LoadData();
            menuPresenter.OpenMenu();
            gameplayPresenter.ObserveGameActivity();
            pausePresenter.ObserveGameActivity();
        }
    }
}