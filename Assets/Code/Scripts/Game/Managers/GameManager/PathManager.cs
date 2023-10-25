namespace ProjectPBR.Managers
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Level.Grid;
    using ProjectPBR.Level.PathSystem;

    public class PathManager : MonoBehaviour, ICastGameManager<GameManager>
    {
        [SerializeField, Header("Level Path")]
        private Vector2Int _fromTile;
        [SerializeField]
        private Vector2Int _toTile;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        private LevelGrid _grid => GameManager.GameGridManager.Grid;

        /// <summary>
        /// Gets the path from Tile to Tile.
        /// </summary>
        /// <returns><see cref="Path"/></returns>
        public Path GetPath()
        {
            return PathCalculator.CalculatePath(_grid, _grid.GridTiles[_fromTile.x, _fromTile.y], _grid.GridTiles[_toTile.x, _toTile.y]);
        }
    }
}
