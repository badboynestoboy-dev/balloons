namespace Balloons.Features.Saves
{
    using System;
    using System.IO;
    using UnityEngine;

    /// <summary>
    /// Сохранение в json
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    public class JsonSave<T> : ISaveLoad<T>
    {
        public T Load(string path)
        {
            try
            {
                string loadedData = File.ReadAllText(path);
                return JsonUtility.FromJson<T>(Encrypting.CryptoXOR(loadedData));
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                return default;
            }
        }

        public void Save(T data, string path)
        {
            try
            {
                string saveData = JsonUtility.ToJson(data);
                File.WriteAllText(path, Encrypting.CryptoXOR(saveData));
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
            }
        }

        public void Clear(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
            }
        }

        protected void LogError(string error) => Debug.LogError($"{nameof(JsonSave<T>)} {error}");
    }
}