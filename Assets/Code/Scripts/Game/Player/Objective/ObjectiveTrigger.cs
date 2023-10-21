namespace ProjectPBR.Player.Objective
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Config.Constants;

    public class ObjectiveTrigger : MonoBehaviour
    {
        public void Trigger()
        {
            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnObjectiveTouched);
        }
    }
}