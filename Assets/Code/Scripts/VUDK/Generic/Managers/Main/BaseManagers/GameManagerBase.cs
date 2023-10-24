namespace VUDK.Generic.Managers.Main
{
    using UnityEngine;

    [DefaultExecutionOrder(-900)]
    public abstract class GameManagerBase : MonoBehaviour
    {
    }

    //public abstract class GameManagerBase<T> : GameManagerBase where T : GameManagerBase
    //{
    //    public T GameManager => MainManager.Ins.GameManager as T;
    //}
}