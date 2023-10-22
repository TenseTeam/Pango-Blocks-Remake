namespace ProjectPBR.Managers
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using ProjectPBR.Level.Grid;
    using ProjectPBR.Level.Blocks;

    public class GameGridManager : MonoBehaviour
    {
        [field: SerializeField, Header("Level Grid")]
        public LevelGrid Grid { get; private set; }

        [field: SerializeField, Header("Layer Masks")]
        public LayerMask BlocksLayerMask { get; private set; }
        [field: SerializeField]
        public LayerMask GridLayerMask { get; private set; }

        public List<PlaceableBlock> BlocksOnGrid { get; private set; } = new List<PlaceableBlock>();

        public void PlaceBlockOnGrid(LevelTile tile, PlaceableBlock blockToPlace)
        {
            blockToPlace.EnableCollider();
            AddBlockToGrid(blockToPlace);
            tile.InsertBlock(blockToPlace);
        }

        public void RemoveBlockFromGrid(PlaceableBlock block)
        {
            BlocksOnGrid.Remove(block);
        }

        //public void AdjustBlocksPositionOnGrid() TO DO: Complex code needed for this
        //{
        //    foreach (LevelTile tile in Grid.GridTiles)
        //    {
        //        if (tile.IsOccupied)
        //            tile.Block.transform.position = tile.transform.position;
        //    }
        //}

        private void AddBlockToGrid(PlaceableBlock block)
        {
            if (!BlocksOnGrid.Contains(block))
                BlocksOnGrid.Add(block);
        }

        public bool AreTilesFreeForBlock(LevelTile fromTile, PlaceableBlock block)
        {
            if (block is SinglePlaceableBlock) return !fromTile.IsOccupied;

            Debug.Log("Is complex block");
            LevelTile[,] tiles = Grid.GridTiles;
            ComplexPlaceableBlock complexPlaceableBlock = block as ComplexPlaceableBlock;

            for (int i = 0; i < complexPlaceableBlock.Data.ComposedBlocks.Count; i++)
            {
                try
                {
                    if (tiles[fromTile.GridPosition.x + i, fromTile.GridPosition.y].IsOccupied) // Checks if the tiles are occupied
                        return false;
                }
                catch (IndexOutOfRangeException)
                {
                    return false; // Tile is out of range
                }
            }

            return true;
        }

        public bool IsGridEmpty()
        {
            return BlocksOnGrid.Count == 0;
        }

        public bool IsGridFull()
        {
            return BlocksOnGrid.Count == Grid.GridTiles.Length;
        }

        public bool Contains(PlaceableBlock block)
        {
            return BlocksOnGrid.Contains(block);
        }
    }
}
