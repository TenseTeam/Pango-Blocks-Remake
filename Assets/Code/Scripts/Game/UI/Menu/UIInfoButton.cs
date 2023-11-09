namespace ProjectPBR.UI.Menu
{
    using VUDK.UI.Buttons;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.GameConfig.Constants;

    public class UIInfoButton : UIButton
    {
        /// <inheritdoc/>
        protected override void Press()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.UIEvents.OnInfoButtonPressed);
            OnButtonPressedSuccess?.Invoke();
        }
    }
}
