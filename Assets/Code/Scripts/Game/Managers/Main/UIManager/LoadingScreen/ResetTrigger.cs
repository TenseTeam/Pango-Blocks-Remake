namespace ProjectPBR.Managers.Main.UIManager.LoadingScreen
{
    using UnityEngine;
    using VUDK.Extensions.CustomAttributes;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.GameConfig.Constants;

    public class ResetTrigger : MonoBehaviour
    {
        /// <summary>
        /// Triggers the gameover loading screen is covered.
        /// </summary>
        [CalledByAnimationEvent]
        public void LoadingScreenCovered()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.UIEvents.OnGameoverLoadingScreenCovered);
        }
    }
}
