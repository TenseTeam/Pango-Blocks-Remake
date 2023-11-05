namespace ProjectPBR.Managers.Static.Profiles
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.GameConfig.Constants;

    public class UIProfileSelectController : MonoBehaviour
    {
        private void Start()
        {
            if (ProfileSelector.SelectedProfile == null)
            {
                ProfileSelector.TrySelectFirstProfile();
                return;
            }

            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnProfileAlteration, ProfileSelector.SelectedProfile);
        }
    }
}
