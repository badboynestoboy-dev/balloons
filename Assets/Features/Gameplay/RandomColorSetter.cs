namespace Balloons.Features.Gameplay
{
    using UnityEngine;

    /// <summary>
    /// Рандомизация цвета объекта при его включении
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class RandomColorSetter : MonoBehaviour
    {
        private SpriteRenderer _renderer = default;

        [SerializeField]
        private Color[] _colors =
        {
            Color.blue,
            Color.cyan,
            Color.green,
            Color.magenta,
            Color.red,
            Color.yellow,
            Color.white
        };

        private void Awake() => _renderer = GetComponent<SpriteRenderer>();

        private void OnEnable() => _renderer.color = _colors[Random.Range(0, _colors.Length)];
    }
}