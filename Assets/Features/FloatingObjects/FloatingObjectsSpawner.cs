namespace Balloons.Features.FloatingObjects
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;
    using Random = UnityEngine.Random;

    /// <summary>
    /// Реализация спавна парящих объектов
    /// </summary>
    public sealed class FloatingObjectsSpawner : MonoBehaviour, IFloatingObjectsSpawner
    {
        private MonoMemoryPool<AbstractFloatingObject> _pool = default;

        private Coroutine _spawnDelay = default;

        private Vector2 _xBorders = new Vector2(-2, 2);
        private Vector2 _yBorders = new Vector2(-7, 7);

        private Vector3 _from = Vector3.zero;
        private Vector3 _to = Vector3.zero;

        private float _minSpawnRate = 0.1f;
        private float _maxSpawnRate = 1f;
        private float _spawnInterval = 1f;

        private readonly List<AbstractFloatingObject> _floatingObjects = new List<AbstractFloatingObject>();

        private AbstractFloatingObject _pooledObject = default;

        [Inject]
        public void Construct(MonoMemoryPool<AbstractFloatingObject> pool) => _pool = pool;

        public void StartSpawn() => SpawnObject();

        public void StopSpawn()
        {
            if (_spawnDelay != null)
            {
                StopCoroutine(_spawnDelay);
                _spawnDelay = null;
            }
        }

        public void SetBorders(Vector3 xBorders, Vector3 yBorders)
        {
            _xBorders = xBorders;
            _yBorders = yBorders;
        }

        public void SetSpawnRate(float minRate, float maxRate)
        {
            _minSpawnRate = minRate;
            _maxSpawnRate = maxRate;
        }

        private void SpawnObject()
        {
            _pooledObject = _pool.Spawn();

            if (!_floatingObjects.Contains(_pooledObject))
            {
                _floatingObjects.Add(_pooledObject);
                _pooledObject.OnDisappear += HandleDisappearance;
            }

            RandomizeTargetPoints();
            _pooledObject.StartFloating(_from, _to);

            AwaitNextSpawn();
        }

        private void HandleDisappearance(AbstractFloatingObject floatingObject) => _pool.Despawn(floatingObject);

        private void AwaitNextSpawn()
        {
            _spawnInterval = Random.Range(_minSpawnRate, _maxSpawnRate);

            StopSpawn();
            _spawnDelay = StartCoroutine(DelayedSpawn());
        }

        private IEnumerator DelayedSpawn()
        {
            yield return new WaitForSeconds(_spawnInterval);
            SpawnObject();
        }

        private void RandomizeTargetPoints()
        {
            _from = new Vector3(Random.Range(_xBorders.x, _xBorders.y), _yBorders.x, 0);
            _to = new Vector3(Random.Range(_xBorders.x, _xBorders.y), _yBorders.y, 0);
        }

        private void OnDisable()
        {
            StopSpawn();
            _floatingObjects.ForEach(x => x.OnDisappear -= HandleDisappearance);
        }
    }
}