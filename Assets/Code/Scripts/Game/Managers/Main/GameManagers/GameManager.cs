namespace ProjectPBR.Managers.Main.GameManagers
{
    using UnityEngine;
    using VUDK.Features.Main.InputSystem.MobileInputs;
    using VUDK.Generic.Managers.Main.Bases;
    using ProjectPBR.Managers.Main.GameManagers.BlocksManagement;
    using ProjectPBR.Managers.Main.GameManagers.Profiles;

    public class GameManager : GameManagerBase
    {
        [field: SerializeField, Header("Mobile Inputs")]
        public MobileInputsManager MobileInputsManager { get; private set; }

        [field: SerializeField, Header("Game Grid Manager")]
        public GameGridManager GameGridManager { get; private set; }

        [field: SerializeField, Header("Blocks Manager")]
        public BlocksManager BlocksManager { get; private set; }

        [field: SerializeField, Header("Path Manager")]
        public PathManager PathManager { get; private set; }

        [field: SerializeField, Header("Profiles Controller")]
        public ProfileLevelsController ProfileLevelsController { get; private set; }
    }
}
