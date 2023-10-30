namespace VUDK.Features.Main.SceneManagement
{
    using UnityEngine.SceneManagement;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Serializable;
    using VUDK.Config;

    public class SceneSwitcher : MonoBehaviour
    {
        [field: SerializeField]
        protected TimeDelay WaitLoadingSceneDelay { get; private set; }

        private int _sceneToWaitLoad;

        protected virtual void Update() => WaitLoadingSceneDelay.Process();

        protected virtual void OnEnable() => WaitLoadingSceneDelay.OnCompleted += ChangeToWaitScene;

        protected virtual void OnDisable() => WaitLoadingSceneDelay.OnCompleted -= ChangeToWaitScene;

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
            WaitLoadingSceneDelay.Start();
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.SceneEvents.OnBeforeChangeScene);
            _sceneToWaitLoad = sceneIndex;
        }

        private void ChangeToWaitScene()
        {
            ChangeScene(_sceneToWaitLoad);
        }
    }
}