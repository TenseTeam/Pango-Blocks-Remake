namespace VUDK.Generic.Structures.Grid
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Extensions.Vectors;

    public class Grid<T> : MonoBehaviour where T : GridTileBase
    {
        [field: SerializeField, Header("Tile")]
        public GameObject TilePrefab { get; private set; }
        [field: SerializeField]
        public Vector2Int Size { get; private set; }

        public T[,] GridTiles { get; private set; }

        /// <summary>
        /// Generates a grid of T components.
        /// </summary>
        /// <returns>Grid of T components.</returns>
        public virtual T[,] GenerateGrid()
        {
            T[,] tiles = new T[Size.x, Size.y];

            for (int y = 0; y < Size.y; y++)
            {
                for (int x = 0; x < Size.x; x++)
                {
                    GenerateTile(tiles, new Vector2Int(x, y));
                }
            }

            GridTiles = tiles;
            return GridTiles;
        }

        /// <summary>
        /// Checks if two cells in the grid are vertically or horizontally adjacent.
        /// </summary>
        /// <param name="positionTileA">Position of the Tile A.</param>
        /// <param name="positionTileB">Position of the Tile B.</param>
        /// <returns>True if they are adjacent, False if they are not.</returns>
        public bool CheckTileAdjacency(Vector2Int positionTileA, Vector2Int positionTileB)
        {
            return IsTileVerticallyAdjacent(positionTileA, positionTileB) || IsTileHorizontallyAdjacent(positionTileA, positionTileB);
        }

        public Vector2Int WorldToGridPosition(Vector3 worldPosition)
        {
            Vector3 localPosition = worldPosition - transform.position;
            int x = Mathf.FloorToInt(localPosition.x);
            int y = Mathf.FloorToInt(localPosition.y);

            // Assicurati che la posizione rientri all'interno della griglia
            x = Mathf.Clamp(x, 0, Size.x - 1);
            y = Mathf.Clamp(y, 0, Size.y - 1);

            return new Vector2Int(x, y);
        }

        /// <summary>
        /// Instantiates a TilePrefab GameObject and attempts to assign the T component to a grid cell.
        /// </summary>
        /// <param name="grid">Grid of T components.</param>
        /// <param name="position">Grid cell position.</param>
        /// <returns>T generated tile's component.</returns>
        protected virtual GridTileBase GenerateTile(GridTileBase[,] grid, Vector2Int position)
        {
            GameObject tile = Instantiate(TilePrefab, transform.position, Quaternion.identity, transform);

            if (tile.TryGetComponent(out GridTileBase tileBase))
            {
                tileBase.Init(position);
                grid[position.x, position.y] = tileBase;
                SetTileWorldPosition(tileBase);
            }

            return tileBase;
        }

        protected virtual void SetTileWorldPosition(GridTileBase tile)
        {
            Vector3 gridPos = new Vector3(tile.GridPosition.x, tile.GridPosition.y, 0f);
            tile.transform.position = transform.position + gridPos;
        }

        /// <summary>
        /// Checks if the grid is full.
        /// </summary>
        /// <returns>True if it is full, False if it is not.</returns>
        protected bool IsGridFull()
        {
            foreach (T tile in GridTiles)
            {
                if (tile == null) return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if two cells in the grid are vertically adjacent.
        /// </summary>
        /// <param name="positionTileA">Position of the Tile A.</param>
        /// <param name="positionTileB">Position of the Tile B.</param>
        /// <returns>True if they are adjacent, False if they are not.</returns>
        protected bool IsTileVerticallyAdjacent(Vector2Int positionTileA, Vector2Int positionTileB)
        {
            int p1 = Mathf.Max(positionTileA.x, positionTileB.x);
            int p2 = Mathf.Min(positionTileA.x, positionTileB.x);

            return p1 - p2 == 1 && positionTileB.y - positionTileA.y == 0;
        }

        /// <summary>
        /// Checks if two cells in the grid are horizontally adjacent.
        /// </summary>
        /// <param name="positionTileA">Position of the Tile A.</param>
        /// <param name="positionTileB">Position of the Tile B.</param>
        /// <returns>True if they are adjacent, False if they are not.</returns>
        protected bool IsTileHorizontallyAdjacent(Vector2Int positionTileA, Vector2Int positionTileB)
        {
            int p1 = Mathf.Max(positionTileA.y, positionTileB.y);
            int p2 = Mathf.Min(positionTileA.y, positionTileB.y);

            return p1 - p2 == 1 && positionTileB.x - positionTileA.x == 0;
        }
    }
}