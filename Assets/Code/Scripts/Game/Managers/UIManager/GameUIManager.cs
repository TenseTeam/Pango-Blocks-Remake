namespace ProjectPBR.Managers.UIManager
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Managers.UIManager.LoadingScreen;

    public class GameUIManager : UIManagerBase
    {
        [field: SerializeField, Header("Loading Screen")]
        public LoadingScreenManager LoadingScreenManager { get; private set; }
    }
}
