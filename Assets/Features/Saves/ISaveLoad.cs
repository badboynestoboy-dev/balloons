namespace Balloons.Features.Saves
{
    /// <summary>
    /// Обобщенный интерфейс для сохранения данных
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    public interface ISaveLoad<T>
    {
        public void Save(T data, string path);

        public T Load(string path);

        public void Clear(string path);
    }
}
