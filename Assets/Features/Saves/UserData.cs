namespace Balloons.Features.Saves
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Данные игрока
    /// </summary>
    [Serializable]
    public class UserData
    {
        public string UserName = string.Empty;

        public List<ScoreData> Scores = new List<ScoreData>();
    }
}