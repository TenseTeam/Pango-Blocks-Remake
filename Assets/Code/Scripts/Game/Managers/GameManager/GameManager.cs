namespace ProjectPBR.Managers
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.InputSysten.MobileInputs;

    public class GameManager : GameManagerBase
    {
        [field: SerializeField, Header("Mobile Inputs Manager")]
        public MobileInputsManager MobileInputsManager { get; private set; }

        [field: SerializeField, Header("Grid Blocks Manager")]
        public GameGridManager GridBlocksManager { get; private set; }

        [field: SerializeField, Header("Blocks Controller")]
        public BlocksController BlocksController { get; private set; }
    }
}
