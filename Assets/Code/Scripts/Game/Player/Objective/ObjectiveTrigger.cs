namespace ProjectPBR.Player.Objective
{
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.Inputs.MobileInputs;
    using VUDK.Features.Main.InputSystem.MobileInputs;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Managers;
    using ProjectPBR.Config.Constants;

    public class ObjectiveTrigger : TouchBehaviour, ICastGameManager<GameManager>
    {
        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;

        private void Awake()
        {
            Init(GameManager.MobileInputsManager);
        }

        protected override void Init(MobileInputsManager inputsManager)
        {
            MobileInputsManager = inputsManager;
        }

        protected override void OnTouchDown2D()
        {
            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnObjeciveTouched);
        }
    }
}