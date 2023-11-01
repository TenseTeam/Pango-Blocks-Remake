namespace ProjectPBR.Player.Objective
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.GameConfig.Constants;

    public class ObjectiveTrigger : MonoBehaviour
    {
        public virtual void Trigger()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnObjectiveTriggered);
        }
    }
}