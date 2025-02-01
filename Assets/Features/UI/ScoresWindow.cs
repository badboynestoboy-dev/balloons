namespace Balloons.Features.UI
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    /// <summary>
    /// Представление окна со списком результатов игры
    /// </summary>
    public sealed class ScoresWindow : BaseWindow
    {
        /// <summary>
        /// Событие о клике на кнопку назад
        /// </summary>
        public event Action OnBackClick = delegate { };

        [SerializeField]
        private Button _backButton = default;

        [SerializeField]
        private RectTransform _scoresParent = default;

        private MonoMemoryPool<ScoreView> _pool = default;

        private List<ScoreView> _scoreViews = new List<ScoreView>();

        private ScoreView _pooledView = default;

        [Inject]
        public void Construct(MonoMemoryPool<ScoreView> pool) => _pool = pool;

        /// <summary>
        /// Добавить результат
        /// </summary>
        /// <param name="score">Кол-во очков</param>
        /// <param name="date">Дата</param>
        public void AddScore(string score, string date)
        {
            _pooledView = _pool.Spawn();
            _pooledView.transform.SetParent(_scoresParent);
            _pooledView.UpdateView(score, date);

            if (!_scoreViews.Contains(_pooledView))
            {
                _scoreViews.Add(_pooledView);
            }
        }

        private void OnEnable() => _backButton.onClick.AddListener(NotifyOfBackClick);

        private void NotifyOfBackClick() => OnBackClick();

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(NotifyOfBackClick);
            _scoreViews.ForEach(x => _pool.Despawn(x));
        }
    }
}