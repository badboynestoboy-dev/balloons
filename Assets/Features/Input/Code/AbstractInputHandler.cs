namespace Balloons.Features.Input
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// Абстракция для обработки действий пользователя
    /// </summary>
    public abstract class AbstractInputHandler
    {
        protected readonly InputActionAsset inputMap = default;
        protected readonly InputAction targetAction = default;

        public AbstractInputHandler(InputActionAsset inputActions, string actionReference)
        {
            inputMap = inputActions;
            inputMap.Enable();
            targetAction = inputMap.FindAction(actionReference);

            if (targetAction != null)
            {
                targetAction.Enable();
                targetAction.performed += HandleActionPerformance;
            }
            else
            {
                Debug.LogError($"Action {actionReference} not found");
            }
        }

        /// <summary>
        /// Обработка действия
        /// </summary>
        /// <param name="context">Контекст действия</param>
        protected abstract void HandleActionPerformance(InputAction.CallbackContext context);

        ~AbstractInputHandler()
        {
            if (targetAction != null)
            {
                targetAction.performed -= HandleActionPerformance;
                targetAction.Disable();
            }
        }
    }
}