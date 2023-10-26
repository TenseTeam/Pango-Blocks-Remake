namespace VUDK.Generic.Utility
{
    using UnityEngine.SceneManagement;
    using UnityEngine;
    using System.Collections;
    using VUDK.Generic.Managers.Main;

    public class SwitchScene : MonoBehaviour
    {
        [SerializeField]
        private float _waitTime;

        /// <summary>
        /// Switches to a scene.
        /// </summary>
        /// <param name="sceneToLoad">Scene to load in a string format.</param>
        public void ChangeScene(string sceneToLoad)
        {
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.SceneEvents.OnBeforeChangeScene);
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }

        /// <summary>
        /// Switches to a scene.
        /// </summary>
        /// <param name="sceneToLoad">Build index of the scene to load.</param>
        public void ChangeScene(int sceneToLoadBuildIndex)
        {
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.SceneEvents.OnBeforeChangeScene);
            SceneManager.LoadScene(sceneToLoadBuildIndex, LoadSceneMode.Single);
        }

        /// <summary>
        /// Waits for seconds and then switches to a scene.
        /// </summary>
        /// <param name="sceneToLoad">Name of the scene to load.</param>
        public void WaitChangeScene(string sceneToLoad)
        {
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.SceneEvents.OnBeforeChangeScene);
            StartCoroutine(ChangeSceneRoutine(sceneToLoad, _waitTime));
        }

        /// <summary>
        /// Waits for seconds and then switches to a scene.
        /// </summary>
        /// <param name="sceneToLoad">Scene build index of the scene to load.</param>
        public void WaitChangeScene(int sceneIndex)
        {
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.SceneEvents.OnBeforeChangeScene);
            StartCoroutine(ChangeSceneRoutine(sceneIndex, _waitTime));
        }

        private IEnumerator ChangeSceneRoutine(string sceneToLoad, float time)
        {
            yield return new WaitForSeconds(time);
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }

        private IEnumerator ChangeSceneRoutine(int sceneIndex, float time)
        {
            yield return new WaitForSeconds(time);
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}