namespace VUDK.Generic.Managers.Main
{
    using UnityEngine;
    using VUDK.Config;
    using VUDK.Patterns.StateMachine;

    [DefaultExecutionOrder(-990)]
    public abstract class GameMachineBase : StateMachine
    {
        public override void Init()
        {
#if DEBUG
            Debug.Log("GameStateMachine initialized.");
#endif
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.GameEvents.OnGameMachineStart);
        }
    }
}
