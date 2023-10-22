namespace ProjectPBR.Managers
{
    using System.Collections;
    using UnityEngine.SceneManagement;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Config.Constants;

    public class GameoverManager : MonoBehaviour
    {
        [SerializeField, Header("Scenes")]
        private int _sceneToLoadOnWin;
        [SerializeField]
        private float _timeToLoadNextScene;
        [SerializeField]
        private float _timeToReloadScene;

        private bool _isLoading;

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameWonPhase, Gamewin);
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameoverPhase, Gameover);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginGameWonPhase, Gamewin);
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginGameoverPhase, Gameover);
        }

        private void Gameover()
        {
            ReloadScene();
        }

        private void Gamewin()
        {
            LoadNextScene();
        }

        private void ReloadScene()
        {
            LoadSceneWithDelay(SceneManager.GetActiveScene().buildIndex, _timeToReloadScene);
        }

        private void LoadNextScene()
        {
            LoadSceneWithDelay(_sceneToLoadOnWin, _timeToLoadNextScene);
        }

        private void LoadSceneWithDelay(int sceneIndex, float time)
        {
            if(_isLoading) return;
            StartCoroutine(WaitLoadNextSceneRoutine(sceneIndex, time));
        }

        private IEnumerator WaitLoadNextSceneRoutine(int sceneIndex, float time)
        {
            yield return new WaitForSeconds(time);
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}