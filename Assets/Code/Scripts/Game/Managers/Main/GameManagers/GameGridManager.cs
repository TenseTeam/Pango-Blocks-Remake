namespace ProjectPBR.Managers.Main.GameManagers
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

        public List<PlaceableBlockBase> BlocksOnGrid { get; private set; } = new List<PlaceableBlockBase>();

        /// <summary>
        /// Inserts a block on the grid.
        /// </summary>
        /// <param name="tile"><see cref="LevelTile"/> where to insert the block.</param>
        /// <param name="blockToPlace"><see cref="PlaceableBlockBase"/> to insert in tile.</param>
        public void Insert(LevelTile tile, PlaceableBlockBase blockToPlace)
        {
            AddPlacealbleBlockToGridList(blockToPlace);
            tile.Insert(blockToPlace);
        }

        /// <summary>
        /// Removes a block from the grid list.
        /// </summary>
        /// <param name="block"></param>
        public void RemoveBlock(PlaceableBlockBase block)
        {
            RemovePlaceableBlockFromGridList(block);
        }

        /// <summary>
        /// Gets the closest tile to the provided position.
        /// </summary>
        /// <param name="position">world position.</param>
        /// <returns>Closest tile from the position.</returns>
        public LevelTile GetClosestTile(Vector3 position)
        {
            Vector2Int tilePos = Grid.WorldToGridPosition(position);
            return Grid.GridTiles[tilePos.x, tilePos.y];
        }

        /// <summary>
        /// Adjusts all the blocks position on the grid.
        /// </summary>
        public void AdjustBlocksPositionOnGrid()
        {
            foreach (PlaceableBlockBase block in BlocksOnGrid)
            {
                LevelTile closestTile = GetClosestTile(block.transform.position);
                //There is no need to check with AreTilesFreeForBlock because the block is already on the grid,
                //so thanks to its colliders it won't overlap
                closestTile.Insert(block);
            }
        }

        /// <summary>
        /// Adds a block to the grid list.
        /// </summary>
        /// <param name="block">Block to add.</param>
        private void AddPlacealbleBlockToGridList(PlaceableBlockBase block)
        {
            if (!BlocksOnGrid.Contains(block))
                BlocksOnGrid.Add(block);
        }

        /// <summary>
        /// Removes a block from the grid list.
        /// </summary>
        /// <param name="block">Block to remove.</param>
        private void RemovePlaceableBlockFromGridList(PlaceableBlockBase block)
        {
            BlocksOnGrid.Remove(block);
        }

        /// <summary>
        /// Checks if the tiles are free for the block.
        /// </summary>
        /// <param name="fromTile">Tile from where to check.</param>
        /// <param name="block">Block to check.</param>
        /// <returns>True if the tiles from the given FromTile are free, False if not.</returns>
        public bool AreTilesFreeForBlock(LevelTile fromTile, PlaceableBlockBase block)
        {
            if (block is SinglePlaceableBlock) return !fromTile.IsOccupied;

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

        /// <summary>
        /// Checks if the grid list is empty.
        /// </summary>
        /// <returns>True if it is empty, False if not.</returns>
        public bool IsGridEmpty()
        {
            return BlocksOnGrid.Count == 0;
        }

        /// <summary>
        /// Checks if the grid list is full.
        /// </summary>
        /// <returns>True if it is full, False if not.</returns>
        public bool IsGridFull()
        {
            return BlocksOnGrid.Count == Grid.GridTiles.Length;
        }

        /// <summary>
        /// Checks if the grid list contains the given block.
        /// </summary>
        /// <param name="block">Block to check.</param>
        /// <returns>True if the given block is contained in the grid list, False if not.</returns>
        public bool Contains(PlaceableBlockBase block)
        {
            return BlocksOnGrid.Contains(block);
        }
    }
}
