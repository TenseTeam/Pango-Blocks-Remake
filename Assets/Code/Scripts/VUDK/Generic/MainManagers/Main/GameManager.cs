namespace VUDK.Generic.Managers.Main
{
    using UnityEngine;
    using VUDK.Patterns.ObjectPool;

    [DefaultExecutionOrder(-900)]
    public class GameManager : MonoBehaviour
    {
        [field: SerializeField, Header("Pooling")]
        public PoolsManager PoolsManager { get; private set; }
    }
}