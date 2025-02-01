namespace Balloons.Features.Saves
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Структура данных для сохранения результатов
    /// </summary>
    [Serializable]
    public struct ScoreData : IComparable<ScoreData>
    {
        public int Score;
        public string Date;

        public ScoreData(int score, DateTime date)
        {
            Score = score;
            Date = date.Ticks.ToString();
        }

        public DateTime TryGetDate(CultureInfo formatProvider)
        {
            long.TryParse(Date, NumberStyles.Integer, formatProvider, out long resultLong);
            return new DateTime(resultLong);
        }

        public int CompareTo(ScoreData other) => other.Score - Score;
    }
}