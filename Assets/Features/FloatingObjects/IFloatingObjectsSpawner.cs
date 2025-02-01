namespace Balloons.Features.FloatingObjects
{
    using UnityEngine;

    /// <summary>
    /// Интерфейс для спавнера парящих объектов
    /// </summary>
    public interface IFloatingObjectsSpawner
    {
        public void StartSpawn();

        public void StopSpawn();

        public void SetBorders(Vector3 xBorders, Vector3 yBorders);

        public void SetSpawnRate(float minRate, float maxRate);

        public void PauseAllObjects();

        public void LaunchAllObjects();
    }
}