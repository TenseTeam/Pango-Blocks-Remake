namespace VUDK.Generic.Managers.Main
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main.BaseManagers;
    using VUDK.Patterns.Pooling;
    using VUDK.Patterns.Singleton;

    /// <summary>
    /// Managers Structure:
    /// MainManager: Serves as the central hub for primary managers.
    /// Extensible managers:
    /// - GameManager: Orchestrates game-specific managers for precise game control; ExecutionOrder(-900).
    /// - UIManager: Manages the game's UIs; ExecutionOrder(-895).
    /// - EventManager: Governs all in-game events, providing centralized event handling; ExecutionOrder(-850).
    /// - GameMachine: Manages the game's state through a versatile state machine; ExecutionOrder(-990).
    /// Not extensible managers:
    /// - AudioManager: Manages all the possible game's audio; ExecutionOrder(-890).
    /// - GameConfig: Manages all the possible game's configurations; ExecutionOrder(-800).
    /// - PoolsManager: Manages all the possible game's pools; ExecutionOrder(-100).
    /// </summary>
    [DefaultExecutionOrder(-999)]
    public sealed class MainManager : Singleton<MainManager>
    {
        [field: SerializeField, Header("Game Manager")]
        public GameManagerBase GameManager { get; private set; }

        [field: SerializeField, Header("UI Manager")]
        public UIManagerBase UIManager { get; private set; }

        [field: SerializeField, Header("Event Manager")]
        public EventManager EventManager { get; private set; }

        [field: SerializeField, Header("Game State Machine")]
        public GameMachineBase GameStateMachine { get; private set; }

        [field: SerializeField, Header("Scene Manager")]
        public SceneManagerBase SceneManager { get; private set; }

        [field: SerializeField, Header("Audio Manager")]
        public AudioManager AudioManager { get; private set; }

        [field: SerializeField, Header("Game Config")]
        public GameConfig GameConfig { get; private set; }

        [field: SerializeField, Header("Pooling")]
        public PoolsManager PoolsManager { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            GameStateMachine.Init();
        }
    }
}
