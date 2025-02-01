namespace Balloons.Features.Saves
{
    using System;
    using System.IO;
    using UnityEngine;

    /// <summary>
    /// Обработчик данных игрока
    /// </summary>
    public sealed class UserDataHandler : IUserDataHandler
    {
        public UserData UserData { get; private set; } = default;

        private const string FILE_NAME = "userdata.dat";

        private readonly ISaveLoad<UserData> _saver = default;

        private readonly string _path = string.Empty;

        public UserDataHandler(ISaveLoad<UserData> saver)
        {
            _saver = saver;
            _path = Path.Combine(Application.persistentDataPath, FILE_NAME);
        }

        public void LoadData() => UserData = _saver.Load(_path) ?? new UserData();

        public void AddResult(int result)
        {
            UserData.Scores.Add(new ScoreData(result, DateTime.Now));
            SaveData();
        }

        public void SaveData() => _saver.Save(UserData, _path);

        public void ClearData() => _saver.Clear(_path);
    }
}