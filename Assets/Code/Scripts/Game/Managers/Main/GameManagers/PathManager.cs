﻿namespace ProjectPBR.Managers.Main.GameManagers
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Level.Grid;
    using ProjectPBR.Level.PathSystem;
    using ProjectPBR.GameConfig.Constants;

    public class PathManager : MonoBehaviour, ICastGameManager<GameManager>
    {
        private Vector2Int _fromTile;
        private Vector2Int _toTile;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        private LevelGrid _grid => GameManager.GameGridManager.Grid;

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<Vector3>(GameConstants.Events.OnCharacterSendPosition, SetFromTile);
            MainManager.Ins.EventManager.AddListener<Vector3>(GameConstants.Events.OnObjectiveGoalSendPosition, SetToTile);
        }

        public void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<Vector3>(GameConstants.Events.OnCharacterSendPosition, SetFromTile);
            MainManager.Ins.EventManager.RemoveListener<Vector3>(GameConstants.Events.OnObjectiveGoalSendPosition, SetToTile);
        }

        /// <summary>
        /// Gets the path from Tile to Tile.
        /// </summary>
        /// <returns><see cref="Path"/></returns>
        public Path GetPath()
        {
            return PathCalculator.CalculatePath(_grid, _grid.GridTiles[_fromTile.x, _fromTile.y], _grid.GridTiles[_toTile.x, _toTile.y]);
        }

        /// <summary>
        /// Sets the starting point for path calculation based on the provided world position.
        /// </summary>
        /// <param name="worldPosition">The world position used to determine the starting grid tile.</param>
        private void SetFromTile(Vector3 worldPosition)
        {
            _fromTile = _grid.WorldToGridPosition(worldPosition);
        }

        /// <summary>
        /// Sets the ending point for path calculation based on the provided world position.
        /// </summary>
        /// <param name="worldPosition">The world position used to determine the ending grid tile.</param>
        private void SetToTile(Vector3 worldPosition)
        {
            _toTile = _grid.WorldToGridPosition(worldPosition);
        }
    }
}
