namespace ProjectPBR.Player.Character
{
    using UnityEngine;
    using System.Collections.Generic;
    using ProjectPBR.Config.Constants;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Managers;
    using ProjectPBR.Level.Grid;

    public class CharacterPathRunner : MonoBehaviour, ICastGameManager<GameManager>
    {
        [SerializeField, Header("Movement")]
        private float _speed = 1f;

        private bool _isRunningPath = false;
        private bool _hasReachedDestination = false;
        private int _currentIndex = 0;
        private List<Node> _nodes;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginObjectivePhase, StartPath);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginObjectivePhase, StartPath);
        }

        private void Update() => RunPath();

        private void StartPath()
        {
            _hasReachedDestination = GameManager.PathManager.GetPathNodes(out _nodes);
            _isRunningPath = true;
        }

        private void RunPath()
        {
            if (!_isRunningPath) return;
            if (_currentIndex >= _nodes.Count)
            {
                MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnCharacterReachedDestination, _hasReachedDestination);
                _isRunningPath = false;
                return;
            }

            float distance = Vector3.Distance(transform.position, _nodes[_currentIndex].Position);
            float t = Time.deltaTime * _speed / distance;
            transform.position = Vector3.Lerp(transform.position, _nodes[_currentIndex].Position, t);
            if (distance < 0.1f)
            {
                MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnCharacterChangedTile, _nodes[_currentIndex].BlockType);
                _currentIndex++;
            }
        }
    }
}
