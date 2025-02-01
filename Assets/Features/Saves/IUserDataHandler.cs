namespace Balloons.Features.Saves
{
    /// <summary>
    /// Интерфейс для обработки данных пользователя
    /// </summary>
    public interface IUserDataHandler
    {
        public UserData UserData { get; }

        public void AddResult(int result);

        public void ClearData();

        public void LoadData();

        public void SaveData();
    }
}