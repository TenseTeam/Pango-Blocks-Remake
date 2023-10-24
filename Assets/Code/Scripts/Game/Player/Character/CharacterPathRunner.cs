namespace ProjectPBR.Player.Character
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Managers;
    using ProjectPBR.Config.Constants;
    using ProjectPBR.Level.PathSystem.Data;

    public class CharacterPathRunner : MonoBehaviour, ICastGameManager<GameManager>
    {
        [SerializeField, Header("Movement")]
        private float _speed = 1f;

        private bool _isRunningPath = false;
        private int _currentNodeIndex = 0;

        private PathData _pathData;

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
            _pathData = GameManager.PathManager.GetPath();
            _isRunningPath = true;
        }

        private void RunPath()
        {
            if (!_isRunningPath) return;
            if (_currentNodeIndex >= _pathData.Nodes.Count)
            {
                MainManager.Ins.EventManager.TriggerEvent(Constants.Events.OnCharacterReachedDestination, _pathData);
                _isRunningPath = false;
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
    }
}
