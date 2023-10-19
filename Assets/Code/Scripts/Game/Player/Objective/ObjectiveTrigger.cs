namespace ProjectPBR.Player.Objective
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Config.Constants;

    public class ObjectiveTrigger : MonoBehaviour /*TouchBehaviour, ICastGameManager<GameManager>*/
    {
        //public GameManager GameManager => MainManager.Ins.GameManager as GameManager;

        //private void Awake()
        //{
        //    Init(GameManager.MobileInputsManager);
        //}

        //protected override void Init(MobileInputsManager inputsManager)
        //{
        //    MobileInputsManager = inputsManager;
        //}

        //protected override void OnTouchDown2D()
        //{
        //    MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnObjectiveTouched);
        //}

        public void Trigger()
        {
            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnObjectiveTouched);
        }
    }
}