namespace ProjectPBR.Managers
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.InputSystem.MobileInputs;

    public class GameManager : GameManagerBase
    {
        [field: SerializeField, Header(nameof(MobileInputsManager))]
        public MobileInputsManager MobileInputsManager { get; private set; }

        [field: SerializeField, Header(nameof(GameGridManager))]
        public GameGridManager GameGridManager { get; private set; }

        [field: SerializeField, Header(nameof(BlocksManager))]
        public BlocksManager BlocksManager { get; private set; }

        [field: SerializeField, Header(nameof(PathManager))]
        public PathManager PathManager { get; private set; }

        [field: SerializeField, Header(nameof(GameoverManager))]
        public GameoverManager GameoverManager { get; private set; }
    }
}
