namespace ProjectPBR.Level.Player.PlayerHandler
{
    using System.Collections.Generic;
    using UnityEngine;
    using ProjectPBR.ScriptableObjects;
    using ProjectPBR.Factories;
    using ProjectPBR.Player.PlayerHandler.Blocks;

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
            foreach(BlockData blockData in _playerBlocks)
            {
                PlaceableBlock block = BlocksFactory.Create(blockData) as PlaceableBlock;
                block.Init(blockData);
                _playerHandLayout.Insert(block);
            }
        }
    }
}
