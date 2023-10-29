namespace ProjectPBR.Managers.GameManagers
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.InputSystem.MobileInputs;
    using ProjectPBR.Managers.GameManagers.BlocksManagement;

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

        [field: SerializeField, Header("Levels Manager")]
        public StageSaver LevelsManager { get; private set; }
    }
}
