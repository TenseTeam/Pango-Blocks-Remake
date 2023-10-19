namespace ProjectPBR.Managers
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.InputSystem.MobileInputs;

    public class GameManager : GameManagerBase
    {
        [field: SerializeField, Header("Mobile Inputs Manager")]
        public MobileInputsManager MobileInputsManager { get; private set; }

        [field: SerializeField, Header("Grid Blocks Manager")]
        public GameGridManager GameGridManager { get; private set; }

        [field: SerializeField, Header("Path Manager")]
        public PathManager PathManager { get; private set; }

        [field: SerializeField, Header("Gameover Manager")]
        public GameoverManager GameoverManager { get; private set; }

        [field: SerializeField, Header("Blocks Controller")]
        public BlocksController BlocksController { get; private set; }
    }
}
