namespace ProjectPBR.Managers
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Level.Grid;
    using ProjectPBR.Level.Blocks;

    [System.Serializable]
    public class Node
    {
        public BlockType BlockType;
        public Vector3 Position { get; private set; }

        public Node(Vector3 position, BlockType blockType)
        {
            Position = position;
            BlockType = blockType;
        }
    }

    public class PathManager : MonoBehaviour, ICastGameManager<GameManager>
    {
        [SerializeField, Header("Level Path")]
        private Vector2Int _fromTile;
        [SerializeField]
        private Vector2Int _toTile;

        private List<Node> _pathNodes = new List<Node>();

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        private LevelGrid _grid => GameManager.GameGridManager.Grid;

        /// <summary>
        /// Gets the path nodes from the start tile to the end tile.
        /// </summary>
        /// <param name="nodes">List of nodes positions to get.</param>
        /// <returns>True if the end tile is reachable, False if not.</returns>
        public bool GetPathNodes(out List<Node> nodes)
        {
            bool isPathValid = CalculatePath();
            nodes = _pathNodes;
            return isPathValid;
        }

        /// <summary>
        /// Calculates the path from the start tile to the end tile.
        /// </summary>
        /// <returns>True if the end tile is reachable, False if not.</returns>
        private bool CalculatePath()
        {
            _pathNodes.Clear();
            List<Node> path = _pathNodes;
            LevelTile[,] tiles = _grid.GridTiles;
            LevelTile fromTile = tiles[_fromTile.x, _fromTile.y];
            LevelTile toTile = tiles[_toTile.x, _toTile.y];

            for (Vector2Int currTilePos = fromTile.GridPosition; currTilePos.x <= toTile.GridPosition.x; currTilePos.x++)
            {
                LevelTile currTile = tiles[currTilePos.x, currTilePos.y];

                if (currTile == toTile)
                    return true;

                LevelTile nextTile = tiles[currTilePos.x + 1, currTilePos.y];
                LevelTile nextAboveTile;
                LevelTile nextUnderTile;

                Node node = new Node(nextTile.LeftVertex, BlockType.Flat);

                if (currTilePos.y + 1 < _grid.Size.y)
                    nextAboveTile = tiles[currTilePos.x + 1, currTilePos.y + 1];
                else
                    nextAboveTile = null;

                if(currTilePos.y - 1 >= 0)
                    nextUnderTile = tiles[currTilePos.x + 1, currTilePos.y - 1];
                else
                    nextUnderTile = null;

                path.Add(node);

                if (nextTile.IsOccupied)
                {
                    if (nextTile.InsertedBlock.IsSlideable) return false;
                    if (nextTile.InsertedBlock.IsFlat) return false;
                    if (!nextTile.InsertedBlock.IsClimbable) return false;
                    if (!nextAboveTile) return false;
                    if (nextAboveTile.IsOccupied) return false;

                    currTilePos.y++;
                    node.BlockType = BlockType.Climbable;
                }
                else if (nextUnderTile) // That means there is the terrain below
                {
                    if (!nextUnderTile.IsOccupied) return false;
                    if (nextUnderTile.InsertedBlock.IsClimbable) return false;

                    if (nextUnderTile.InsertedBlock.IsSlideable)
                    {
                        currTilePos.y--;
                        node.BlockType = BlockType.Slideable;
                    }
                }
            }

            return false;
        }
    }
}
