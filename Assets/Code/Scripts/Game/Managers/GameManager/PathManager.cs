namespace ProjectPBR.Managers
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Level.Grid;
    using ProjectPBR.Level.PathSystem;
    using ProjectPBR.Player.Character;
    using ProjectPBR.Player.Objective.Goal;
    using ProjectPBR.Config.Constants;
    using System;

    public class PathManager : MonoBehaviour, ICastGameManager<GameManager>
    {
        private Vector2Int _fromTile;
        private Vector2Int _toTile;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        private LevelGrid _grid => GameManager.GameGridManager.Grid;

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<Vector3>(Constants.Events.OnCharacterSendPosition, SetFromTile);
            MainManager.Ins.EventManager.AddListener<Vector3>(Constants.Events.OnObjectiveGoalSendPosition, SetToTile);
        }

        /// <summary>
        /// Gets the path from Tile to Tile.
        /// </summary>
        /// <returns><see cref="Path"/></returns>
        public Path GetPath()
        {
            return PathCalculator.CalculatePath(_grid, _grid.GridTiles[_fromTile.x, _fromTile.y], _grid.GridTiles[_toTile.x, _toTile.y]);
        }

        private void SetFromTile(Vector3 worldPosition)
        {
            _fromTile = _grid.WorldToGridPosition(worldPosition);
        }

        private void SetToTile(Vector3 worldPosition)
        {
            _toTile = _grid.WorldToGridPosition(worldPosition);
        }
    }
}
