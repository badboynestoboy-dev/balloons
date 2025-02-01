namespace Balloons.Features.UI
{
    using UnityEngine;

    /// <summary>
    /// Базовый класс окна интерфейса
    /// </summary>
    public class BaseWindow : MonoBehaviour
    {
        public virtual void Close() => gameObject.SetActive(false);
    }
}