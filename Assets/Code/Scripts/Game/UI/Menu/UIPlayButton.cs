namespace ProjectPBR.UI.Menu
{
    using VUDK.UI.Buttons;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Managers.Static.Profiles;


    public class UIPlayButton : UIButton
    {
        protected override void Press()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.UIEvents.OnPlayButtonPressed);
            if (ProfileSelector.SelectedProfile == null)
            {
                OnButtonPressedFail?.Invoke();
                return;
            }

            OnButtonPressedSuccess?.Invoke();
        }
    }
}
