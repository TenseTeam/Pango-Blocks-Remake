namespace ProjectPBR.Managers
{
    using UnityEngine;
    using VUDK.Features.Main.InputSysten.MobileInputs;
    using VUDK.Generic.Managers.Main;

    public class PBRGameManager : GameManager
    {
        [field: SerializeField, Header("Mobile Inputs Manager")]
        public MobileInputsManager MobileInputsManager { get; private set; }

        [field: SerializeField, Header("Grid Blocks Manager")]
        public GridBlocksManager GridBlocksManager { get; private set; }
    }
}
