namespace ProjectPBR.Managers
{
    using UnityEngine;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Level.Grid;
    using ProjectPBR.Player.PlayerHandler;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Config.Constants;

    public class BlocksManager : MonoBehaviour, ICastGameManager<GameManager>
    {
        [field: SerializeField, Header("Blocks Dragger")]
        public BlockDragger Dragger { get; private set; }

        [field: SerializeField, Header("Player Hand")]
        public PlayerHand PlayerHand { get; private set; }

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        private GameGridManager _grid => GameManager.GameGridManager;

        [SerializeField, Min(0f)]
        private float _resetBlockTime;

        private void OnValidate()
        {
            if(!PlayerHand)
                PlayerHand = FindObjectOfType<PlayerHand>();
        }

        /// <summary>
        /// Reset the block in the player hand layout by starting its lerp.
        /// </summary>
        /// <param name="block"><see cref="PlaceableBlock"/> to reset.</param>
        public void LerpResetBlockInHand(PlaceableBlock block)
        {
            block.DisableGravity();
            block.SetIsInvalid(false);
            _grid.RemoveBlockFromGrid(block);
            //MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnBlockReset, block, _resetBlockTime);
            PlayerHand.Layout.LerpPutItBackInHand(block, _resetBlockTime);
        }

        public void PlaceBlockOnGrid(PlaceableBlock block, LevelTile tile)
        {
            _grid.PlaceBlockOnGrid(tile, block);
        }
    }
}
