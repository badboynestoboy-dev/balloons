namespace Balloons.Features.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Представление для отображения результата игры
    /// </summary>
    public sealed class ScoreView : MonoBehaviour
    {
        [SerializeField]
        private Text _score = default;
        [SerializeField]
        private Text _date = default;

        /// <summary>
        /// Обновить данные
        /// </summary>
        /// <param name="score">Кол-во очков</param>
        /// <param name="date">Дата</param>
        public void UpdateView(string score, string date)
        {
            _score.text = score;
            _date.text = date;
        }
    }
}