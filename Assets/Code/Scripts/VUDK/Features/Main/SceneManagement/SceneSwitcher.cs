namespace VUDK.Features.Main.SceneManagement
{
    using UnityEngine.SceneManagement;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Serializable;

    public class SceneSwitcher : MonoBehaviour
    {
        [field: SerializeField]
        public TimeDelay WaitDelay { get; private set; }

        private int _sceneToWaitLoad;

        protected virtual void Update() => WaitDelay.Process();

        protected virtual void OnEnable() => WaitDelay.OnCompleted += ChangeToWaitScene;

        protected virtual void OnDisable() => WaitDelay.OnCompleted -= ChangeToWaitScene;

        /// <summary>
        /// Switches to a scene.
        /// </summary>
        /// <param name="sceneToLoad">Build index of the scene to load.</param>
        public virtual void ChangeScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }

        /// <summary>
        /// Waits for seconds and then switches to a scene.
        /// </summary>
        /// <param name="sceneToLoad">Scene build index of the scene to load.</param>
        public virtual void WaitChangeScene(int sceneIndex)
        {
            WaitDelay.Reset();
            WaitDelay.Start();
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.SceneEvents.OnBeforeChangeScene);
            _sceneToWaitLoad = sceneIndex;
        }

        private void ChangeToWaitScene()
        {
            ChangeScene(_sceneToWaitLoad);
        }
    }
}