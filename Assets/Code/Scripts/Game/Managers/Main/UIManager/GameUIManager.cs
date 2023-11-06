namespace ProjectPBR.Managers.Main.UIManager
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main.Bases;
    using ProjectPBR.Managers.UIManager.Game.LoadingScreen;

    public class GameUIManager : UIManagerBase
    {
        [field: SerializeField, Header("Loading Screen")]
        public LoadingScreenManager LoadingScreenManager { get; private set; }
    }
}
