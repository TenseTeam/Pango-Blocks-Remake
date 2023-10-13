namespace VUDK.Patterns.Singleton
{
    using UnityEngine;

    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance;
        public static T Ins => Instance;

        protected virtual void Awake()
        {
            if (!Instance)
            {
                if (!TryGetComponent(out Instance))
                    Instance = gameObject.AddComponent<T>();
            }
            else
                Destroy(gameObject);
        }
    }
}