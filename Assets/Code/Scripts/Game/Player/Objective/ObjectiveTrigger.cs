namespace ProjectPBR.Player.Objective
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Config.Constants;
    using ProjectPBR.Level.Blocks;

    public class ObjectiveTrigger : MonoBehaviour
    {
        public virtual void Trigger()
        {
            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnObjectiveTriggered);
        }
    }
}