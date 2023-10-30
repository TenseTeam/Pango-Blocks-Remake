namespace ProjectPBR.Managers.GameManagers
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Serializable;
    using VUDK.SaveSystem;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.SaveSystem;

    public class SaveTester : MonoBehaviour
    {
        //[SerializeField, Header("Default Levels")]
        //private Range<int> _defaultUnlockedLevels;
        //private LevelsData _levelsData;

        //private void Awake()
        //{
        //    RefillLoadedData();
        //}

        [ContextMenu("Visualize")]
        public void Visualize()
        {
            foreach (var val in StageSaver.LevelsData.ClearedLevels)
            {
                Debug.Log(val);
            }
        }

        //private void OnEnable()
        //{
        //    MainManager.Ins.EventManager.AddListener(Constants.Events.OnBeginGameWonPhase, SaveLevel);
        //}

        //private void OnDisable()
        //{
        //    MainManager.Ins.EventManager.RemoveListener(Constants.Events.OnBeginGameWonPhase, SaveLevel);
        //}

        //public override void SaveAllData()
        //{
        //    SaveManager.Save(_levelsData);
        //}

        //public override void RefillLoadedData()
        //{
        //    if (!SaveManager.TryLoad(out _levelsData))
        //    {
        //        Debug.Log("Creating new data");
        //        _levelsData = new LevelsData(_defaultUnlockedLevels);
        //        SaveAllData();
        //    }
        //}

        //private void SaveLevel()
        //{
        //    int currentScene = SceneManager.GetActiveScene().buildIndex;

        //    if(!_levelsData.ClearedLevels.TryAdd(currentScene, LevelStatus.Completed))
        //        _levelsData.ClearedLevels[currentScene] = LevelStatus.Completed;

        //    SaveAllData();
        //}
    }
}
