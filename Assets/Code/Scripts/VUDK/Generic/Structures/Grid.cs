namespace VUDK.Generic.Structures
{
    using UnityEngine;
    using UnityEngine.UI;

    public class Grid<T> : MonoBehaviour
    {
        public T[,] GridTiles { get; set; }

        [field: SerializeField]
        public GridLayoutGroup GridLayout { get; private set; }

        [field: SerializeField, Header("Tile")]
        public GameObject TilePrefab { get; private set; }

        public Vector2Int Size { get; private set; }

        /// <summary>
        /// Initializes grid.
        /// </summary>
        /// <param name="size">Size of the grid.</param>
        public virtual void Init(Vector2Int size)
        {
            Size = size;
        }

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

        /// <summary>
        /// Instantiates a TilePrefab GameObject and attempts to assign the T component to a grid cell.
        /// </summary>
        /// <param name="grid">Grid of T components.</param>
        /// <param name="position">Grid cell position.</param>
        protected virtual T GenerateTile(T[,] grid, Vector2Int position)
        {
            if (Instantiate(TilePrefab, GridLayout.transform.position, Quaternion.identity, GridLayout.transform).TryGetComponent(out T component))
            {
                grid[position.x, position.y] = component;
            }

            return component;
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