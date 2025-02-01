namespace Balloons.Features.Dependencies
{
    using FloatingObjects;
    using Gameplay;
    using Input;
    using Saves;
    using UI;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Zenject;

    /// <summary>
    /// Установщик зависисмостей
    /// </summary>
    public class MainInstaller : MonoInstaller
    {
        [SerializeField]
        protected Bootstrapper bootstrapper = default;

        [SerializeField]
        protected InputActionAsset inputMap = default;

        [SerializeField]
        protected Camera cam = default;

        public override void InstallBindings()
        {
            BindData();
            BindWindows();
            BindPresenters();
            BindGameplayModules();
            Container.Bind<Bootstrapper>().FromInstance(bootstrapper).AsSingle();
        }

        protected virtual void BindData()
        {
            Container.Bind<ISaveLoad<UserData>>().To<JsonSave<UserData>>().AsSingle();
            Container.Bind<IUserDataHandler>().To<UserDataHandler>().AsSingle();
        }

        protected virtual void BindWindows()
        {
            Container.BindMemoryPool<ScoreView, MonoMemoryPool<ScoreView>>().ExpandByOneAtATime().FromComponentInNewPrefabResource("ScoreView").AsCached();
            Container.Bind<MenuWindow>().FromComponentInNewPrefabResource("Windows/MenuWindow").AsSingle();
            Container.Bind<GameplayWindow>().FromComponentInNewPrefabResource("Windows/GameplayWindow").AsSingle();
            Container.Bind<PauseWindow>().FromComponentInNewPrefabResource("Windows/PauseWindow").AsSingle();
            Container.Bind<ScoresWindow>().FromComponentInNewPrefabResource("Windows/ScoresWindow").AsSingle();
        }

        protected virtual void BindPresenters()
        {
            Container.Bind<IGameActivity>().To<GameActivity>().AsSingle();
            Container.Bind<ScoresPresenter>().AsSingle();
            Container.Bind<MainMenuPresenter>().AsSingle();
            Container.Bind<GameplayPresenter>().AsSingle();
            Container.Bind<PausePresenter>().AsSingle();
        }

        protected virtual void BindGameplayModules()
        {
            Container.Bind<InputActionAsset>().FromInstance(inputMap).AsSingle();
            Container.Bind<Camera>().FromInstance(cam).AsSingle();
            Container.BindMemoryPool<AbstractFloatingObject, MonoMemoryPool<AbstractFloatingObject>>().ExpandByOneAtATime().FromComponentInNewPrefabResource("Balloon").AsCached();
            Container.Bind<IFloatingObjectsSpawner>().To<FloatingObjectsSpawner>().FromComponentInNewPrefabResource("FloatingObjectsSpawner").AsSingle();
            Container.Bind<TappedObjectsDetector>().AsSingle();
            Container.Bind<SuccessfulTapsCounter>().AsSingle();
            Container.Bind<ScoreDataSetter>().AsSingle();
        }
    }
}