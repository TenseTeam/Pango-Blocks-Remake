namespace ProjectPBR.Player.PlayerHandler
{
    using UnityEngine;
    using System.Collections.Generic;
    using ProjectPBR.ScriptableObjects;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.Level.Blocks;

    public class PlayerHand : MonoBehaviour
    {
        [SerializeField, Header("Blocks")]
        private List<BlockData> _playerBlocks;
        [SerializeField]
        private PlayerHandLayout _playerHandLayout;

        //private List<PlaceableBlock> _currentPlayerBlocks;

        private void Start()
        {
            GenerateBlocks();
        }

        //public PlaceableBlock GetBlockFromCurrentHand(PlaceableBlock block)
        //{
        //    _currentPlayerBlocks.Remove(block);
        //    return block;
        //}

        private void GenerateBlocks()
        {
            foreach (BlockData blockData in _playerBlocks)
            {
                PlaceableBlock block = BlocksFactory.Create(blockData) as PlaceableBlock;
                _playerHandLayout.InsertInRow(block);
                //_currentPlayerBlocks.Add(block);
            }
        }
    }
}
