namespace ProjectPBR.Managers
{
    using System;
    using UnityEngine;
    using ProjectPBR.Level.Grid;
    using ProjectPBR.Level.Blocks;

    public class GameGridManager : MonoBehaviour
    {
        [field: SerializeField, Header("Level Grid")]
        public LevelGrid Grid { get; private set; }

        [field: SerializeField, Header("Layer Masks")]
        public LayerMask BlocksLayerMask { get; private set; }

        public void PlaceBlockInGrid(LevelTile tile, PlaceableBlock blockToPlace)
        {
            blockToPlace.EnableCollider();
            tile.InsertBlock(blockToPlace);
        }

        public bool AreTilesFreeForBlock(LevelTile fromTile, PlaceableBlock block)
        {
            LevelTile[,] tiles = Grid.GridTiles;

            for(int i = 0; i < block.BlockData.UnitLength; i++)
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
    }
}
