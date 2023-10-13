namespace VUDK.Generic.Managers.Main
{
    using UnityEngine;
    using VUDK.Patterns.ObjectPool;
    using static VUDK.Config.Constants.Constants;

    [DefaultExecutionOrder(-900)]
    public class GameManager : MonoBehaviour
    {
        [field: SerializeField, Header("Pooling")]
        public PoolsManager PoolsManager { get; private set; }
        
//#if DEBUG && UNITY_EDITOR
//        [System.Obsolete("This method should only be used for specific purposes. Do not use casually.", true)]
//        public void AssignReferences(PoolsManager pools)
//        {
//            PoolsManager = pools;
//        }
//#endif
    }
}