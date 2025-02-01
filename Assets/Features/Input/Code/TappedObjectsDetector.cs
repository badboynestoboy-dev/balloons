namespace Balloons.Features.Input
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// Обработка тапов пользователя с реализацией обнаружения объектов типа ITapHandler
    /// </summary>
    public class TappedObjectsDetector : AbstractInputHandler
    {
        /// <summary>
        /// Событие об обнаружении объекта типа ITapHandler
        /// </summary>
        public event Action<ITapHandler> OnObjectTapDetected = delegate { };

        protected const string TAP_REFERENCE = "Tap";

        protected readonly Camera cam = default;

        protected RaycastHit2D hit = default;

        public TappedObjectsDetector(InputActionAsset inputActions, Camera camera) :
            base(inputActions, TAP_REFERENCE) => cam = camera;

        protected override void HandleActionPerformance(InputAction.CallbackContext context)
        {
            hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(GetTouchPosition()));

            if (hit.collider && hit.collider.TryGetComponent(out ITapHandler tapHandler))
            {
                OnObjectTapDetected(tapHandler);
            }
        }

        protected virtual Vector3 GetTouchPosition() => Touchscreen.current.primaryTouch.position.ReadValue();
    }
}