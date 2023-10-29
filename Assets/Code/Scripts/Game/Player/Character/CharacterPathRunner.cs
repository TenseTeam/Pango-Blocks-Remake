﻿namespace ProjectPBR.Player.Character
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Config.Constants;
    using ProjectPBR.Level.PathSystem;
    using ProjectPBR.Managers.GameManagers;

    public class CharacterPathRunner : MonoBehaviour, ICastGameManager<GameManager>
    {
        [SerializeField, Header("Movement")]
        private float _speed = 1f;

        private bool _isRunningPath = false;
        private int _currentNodeIndex = 0;

        private Path _pathData;
        private Vector3 _startPosition;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;

        private void Awake()
        {
            _startPosition = transform.position;
        }

        private void Start()
        {
            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnCharacterSendPosition, transform.position);
        }

        private void Update() => RunPath();

        public void ResetPosition()
        {
            transform.position = _startPosition;
        }

        public void StartPath()
        {
            _pathData = GameManager.PathManager.GetPath();
            _isRunningPath = true;
        }

        private void RunPath()
        {
            if (!_isRunningPath) return;

            if (_currentNodeIndex >= _pathData.Nodes.Count)
            {
                ReachDestination();
                return;
            }

            float distance = Vector3.Distance(transform.position, _pathData.Nodes[_currentNodeIndex].Position);
            float t = Time.deltaTime * _speed / distance;
            transform.position = Vector3.Lerp(transform.position, _pathData.Nodes[_currentNodeIndex].Position, t);
            if (distance < 0.01f)
            {
                MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnCharacterChangedTile, _pathData.Nodes[_currentNodeIndex].BlockType);
                transform.position = _pathData.Nodes[_currentNodeIndex].Position;
                _currentNodeIndex++;
            }
        }

        private void ReachDestination()
        {
            MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnCharacterReachedDestination, _pathData);
            _isRunningPath = false;
            _currentNodeIndex = 0;
        }
    }
}
