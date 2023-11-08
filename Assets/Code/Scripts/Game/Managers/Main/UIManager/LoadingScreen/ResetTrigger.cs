namespace ProjectPBR.Managers.Main.UIManager.LoadingScreen
{
    using UnityEngine;
    using VUDK.Extensions.CustomAttributes;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.GameConfig.Constants;

    public class ResetTrigger : MonoBehaviour
    {
        [CalledByAnimationEvent]
        public void LoadingScreenCovered()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.UIEvents.OnGameoverLoadingScreenCovered);
        }
    }
}
