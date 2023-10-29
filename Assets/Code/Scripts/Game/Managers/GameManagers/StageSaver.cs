namespace ProjectPBR.Managers.GameManagers
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using VUDK.Generic.Managers.Main;
    using VUDK.SaveSystem;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Config.Constants;

    public class StageSaver : MonoBehaviour
    {
        private LevelsData _levelsData;

        private void Awake()
        {
            if(!SaveManager.TryLoad(out _levelsData))
                _levelsData = new LevelsData();
        }

        [ContextMenu("Visualize")]
        public void Visualize()
        {
            foreach (var val in _levelsData.ClearedLevels)
            {
                Debug.Log(val);
            }
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameWonPhase, SaveLevel);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginGameWonPhase, SaveLevel);
        }

        private void SaveLevel()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;

            if (!_levelsData.ClearedLevels.ContainsKey(currentScene))
                _levelsData.ClearedLevels.Add(currentScene, true);
            //else
            //{
            //    _levelsData.ClearedLevels[currentScene] = true; // No need to set it again to true
            //}
            SaveManager.Save(_levelsData);
        }
    }
}
