namespace ProjectPBR.Player.PlayerHandler
{
    using System.Collections.Generic;
    using UnityEngine;
    using ProjectPBR.Data.ScriptableObjects.Blocks;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.Level.Blocks;

    public class PlayerHand : MonoBehaviour
    {
        [SerializeField, Header("Blocks")]
        private List<BlockDataBase> _playerBlocks;

        [field: SerializeField]
        public PlayerHandLayout Layout { get; private set; }

        private void Start()
        {
            GenerateBlocks();
        }

        /// <summary>
        /// Generates the player hand blocks.
        /// </summary>
        private void GenerateBlocks()
        {
            foreach (BlockDataBase blockData in _playerBlocks)
            {
                PlaceableBlockBase block = GameFactory.Create(blockData, false) as PlaceableBlockBase;
                block.transform.name = blockData.name.Replace("so_", "");
                Layout.SetBlockPositionInLayoutRow(block);
            }
        }
    }
}
