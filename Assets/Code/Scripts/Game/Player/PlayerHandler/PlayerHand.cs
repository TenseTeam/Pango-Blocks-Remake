namespace ProjectPBR.Player.PlayerHandler
{
    using System.Collections.Generic;
    using UnityEngine;
    using ProjectPBR.ScriptableObjects;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.Level.Blocks;

    public class PlayerHand : MonoBehaviour
    {
        [SerializeField, Header("Blocks")]
        private List<BlockData> _playerBlocks;
        [SerializeField]
        private PlayerHandLayout _playerHandLayout;

        private void Start()
        {
            GenerateBlocks();
        }

        private void GenerateBlocks()
        {
            foreach (BlockData blockData in _playerBlocks)
                _playerHandLayout.InsertInRow(BlocksFactory.Create(blockData) as PlaceableBlock);
        }
    }
}
