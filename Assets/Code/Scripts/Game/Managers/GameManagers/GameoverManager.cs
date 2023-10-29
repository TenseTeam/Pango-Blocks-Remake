namespace ProjectPBR.Managers.GameManagers
{
    using System.Collections;
    using UnityEngine.SceneManagement;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Config.Constants;

    public class GameoverManager : MonoBehaviour
    {
        //[SerializeField, Header("Scenes")]
        //private float _timeToLoadNextScene;
        //[SerializeField]
        //private float _timeToReloadScene;

        //private int _currentBuildIndex;
        //private bool _isLoading;

        //private void Awake()
        //{
        //    _currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        //}

        //private void OnEnable()
        //{
        //    MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameWonPhase, Gamewin);
        //    MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameoverPhase, Gameover);
        //}

        //private void OnDisable()
        //{
        //    MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginGameWonPhase, Gamewin);
        //    MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginGameoverPhase, Gameover);
        //}

        //private void Gameover()
        //{
        //    ReloadScene();
        //}

        //private void Gamewin()
        //{
        //    LoadNextScene();
        //}

        //private void ReloadScene()
        //{
        //    LoadSceneWithDelay(_currentBuildIndex, _timeToReloadScene);
        //}

        //private void LoadNextScene()
        //{
        //    if (_currentBuildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        //    {
        //        LoadSceneWithDelay(0, _currentBuildIndex); // Load the menu if there is not a next level scene
        //        return;
        //    }
        //    LoadSceneWithDelay(_currentBuildIndex + 1, _timeToLoadNextScene);
        //}

        //private void LoadSceneWithDelay(int sceneIndex, float time)
        //{
        //    if(_isLoading) return;
        //    StartCoroutine(WaitLoadNextSceneRoutine(sceneIndex, time));
        //}

        //private IEnumerator WaitLoadNextSceneRoutine(int sceneIndex, float time)
        //{
        //    yield return new WaitForSeconds(time);
        //    SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        //}
    }
}