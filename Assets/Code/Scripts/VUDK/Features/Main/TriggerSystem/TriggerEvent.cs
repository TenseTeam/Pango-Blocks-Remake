namespace VUDK.Features.Main.TriggerSystem
{
    using UnityEngine;
    using UnityEngine.Events;
    using VUDK.Extensions.Gizmos;

    [RequireComponent(typeof(Collider))]
    public class TriggerEvent : MonoBehaviour
    {
        [SerializeField, Header("Events")]
        protected UnityEvent OnEnter;
        [SerializeField]
        protected UnityEvent OnExit;

        protected virtual void OnTriggerEnter(Collider other)
        {
            OnEnter?.Invoke();
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            OnExit?.Invoke();
        }

        protected virtual void OnTriggerStay(Collider other)
        {
        }
    }
}