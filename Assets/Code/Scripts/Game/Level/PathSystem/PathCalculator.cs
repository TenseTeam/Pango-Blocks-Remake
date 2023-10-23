namespace ProjectPBR.Level.PathSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Level.Grid;
    using ProjectPBR.Level.PathSystem.Data;

    public static class PathCalculator
    {
        /// <summary>
        /// Calculates the path from tile A to tile B in a <see cref="LevelGrid"/>.
        /// </summary>
        /// <param name="grid">The grid to calculate the path.</param>
        /// <param name="tileA">The start tile.</param>
        /// <param name="tileB">The end tile.</param>
        /// <returns><see cref="PathData"/></returns>
        public static PathData CalculatePath(LevelGrid grid, LevelTile tileA, LevelTile tileB)
        {
            PathData pathData = new PathData();
            pathData.Nodes = new List<Node>();

            LevelTile[,] tiles = grid.GridTiles;

            for (Vector2Int currTilePos = tileA.GridPosition; currTilePos.x <= tileB.GridPosition.x; currTilePos.x++)
            {
                LevelTile currTile = tiles[currTilePos.x, currTilePos.y];

                if (currTile == tileB)
                    return pathData;

                LevelTile nextTile = tiles[currTilePos.x + 1, currTilePos.y];
                LevelTile nextAboveTile;
                LevelTile nextUnderTile;

                Node node = new Node(nextTile.LeftVertex, BlockType.Flat);

                if (currTilePos.y + 1 < grid.Size.y)
                    nextAboveTile = tiles[currTilePos.x + 1, currTilePos.y + 1];
                else
                    nextAboveTile = null;

                if (currTilePos.y - 1 >= 0)
                    nextUnderTile = tiles[currTilePos.x + 1, currTilePos.y - 1];
                else
                    nextUnderTile = null;

                pathData.Nodes.Add(node);

                if (nextTile.IsOccupied)
                {
                    if (!nextTile.Block.IsClimbable || (nextAboveTile && nextAboveTile.IsOccupied))
                    {
                        pathData.IsGoingToCollide = true;
                        return pathData;
                    }

                    currTilePos.y++;
                    node.BlockType = BlockType.Climbable;
                }
                else if (nextUnderTile) // That means there is the terrain below
                {
                    if (!nextUnderTile.IsOccupied || nextUnderTile.Block.IsClimbable)
                    {
                        pathData.IsGoingToFall = true;
                        return pathData;
                    }

                    if (nextUnderTile.Block.IsSlideable) // If it is not flat
                    {
                        currTilePos.y--;
                        node.BlockType = BlockType.Slideable;
                    }
                }
            }

            return pathData;
        }
    }
}
