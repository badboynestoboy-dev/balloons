namespace Balloons.Features.FloatingObjects
{
    using System;
    using UnityEngine;
    using Random = UnityEngine.Random;

    /// <summary>
    /// Абстракция для парящих объектов с рандомным временем полета
    /// </summary>
    public abstract class AbstractFloatingObject : MonoBehaviour
    {
        /// <summary>
        /// Событие об исчезновении объекта из поля зрения
        /// </summary>
        public event Action<AbstractFloatingObject> OnDisappear = delegate { };

        [SerializeField, Min(1)]
        protected Vector2 durationRange = new Vector2(5f, 25f);

        /// <summary>
        /// Запустить полет
        /// </summary>
        /// <param name="from">Стартовая точка</param>
        /// <param name="to">Конечная точка</param>
        public abstract void StartFloating(Vector3 from, Vector3 to);

        protected void Disappear() => OnDisappear(this);

        protected virtual float GetRandomDuration() => Random.Range(durationRange.x, durationRange.y);
    }
}