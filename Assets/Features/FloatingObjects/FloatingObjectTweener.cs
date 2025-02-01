namespace Balloons.Features.FloatingObjects
{
    using Balloons.Features.Input;
    using DG.Tweening;
    using UnityEngine;

    /// <summary>
    /// Реализация парящего объекта, анимированного с помощью DOTween
    /// </summary>
    public sealed class FloatingObjectTweener : AbstractFloatingObject, ITapHandler
    {
        private Tween _tween = default;

        public override void StartFloating(Vector3 from, Vector3 to)
        {
            transform.position = from;
            _tween = transform.DOMove(to, GetRandomDuration()).SetEase(Ease.Linear).OnComplete(() => Disappear());
        }

        public void HandleTap()
        {
            _tween.Pause();
            _tween.Kill();
            _tween = transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => Disappear());
        }

        private void OnEnable() => transform.localScale = Vector3.one;

        private void OnDestroy() => _tween.Kill();
    }
}