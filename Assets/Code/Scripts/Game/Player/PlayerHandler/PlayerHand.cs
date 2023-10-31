namespace ProjectPBR.Player.PlayerHandler
{
    using UnityEngine;
    using System.Collections.Generic;
    using ProjectPBR.Data.ScriptableObjects.Blocks;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.Level.Blocks;

    public class PlayerHand : MonoBehaviour
    {
        [SerializeField, Header("Blocks")]
        private List<BlockData> _playerBlocks;

        [field: SerializeField]
        public PlayerHandLayout Layout { get; private set; }

        private void Start()
        {
            GenerateBlocks();
        }

        private void GenerateBlocks()
        {
            foreach (BlockData blockData in _playerBlocks)
            {
                PlaceableBlock block = GameFactory.Create(blockData, false) as PlaceableBlock;
                block.transform.name = blockData.name.Replace("so_", "");
                Layout.SetBlockPositionInLayoutRow(block);
            }
        }
    }
}
