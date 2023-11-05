#if UNITY_EDITOR || DEBUG
namespace ProjectPBR.Debug
{
    using UnityEngine;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Managers.Static.Profiles;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Managers.Static;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Patterns.Factories;

    public class SaveTester : MonoBehaviour
    {
        [Header("Profile")]
        public string ProfileToCreate;
        public string ProfileToSelect;
        public GameDifficulty DifficultyToChange;

        [Header("Stage")]
        public int StageToSelect;
        public int LevelToComplete;

        [ContextMenu("CreateProfile")]
        public void Create()
        {
            ProfilesManager.CreateProfile(ProfileToCreate);
        }

        [ContextMenu("SelectProfile")]
        public void Select()
        {
            ProfileSelector.SelectProfile(ProfileToSelect);
        }

        [ContextMenu("Change Difficulty")]
        public void ChangeDifficulty()
        {
            ProfileSelector.SelectedProfile.CurrentDifficulty = DifficultyToChange;
            ProfilesManager.SaveProfile(ProfileSelector.SelectedProfile);
        }

        [ContextMenu("Change Stage")]
        public void ChangeStage()
        {
            LevelMapper.SetCurrentStageIndex(StageToSelect);
        }

        [ContextMenu("Check Cutscene Unlocked")]
        public void CheckCutscene()
        {
            Debug.Log(LevelOperation.IsCutsceneUnlocked(StageToSelect));
        }

        [ContextMenu("Complete Level")]
        public void CompleteLevel()
        {
            LevelKey levelKey = LevelMapper.GetLevelKeyByLevelIndex(LevelToComplete);
            LevelOperation.SetLevelStatus(levelKey, LevelStatus.Completed);
            ProfilesManager.SaveProfile(ProfileSelector.SelectedProfile);
        }

        //public void CompleteAllLevel()
        //{
        //    LevelKey levelKey = DataFactory.Create(StageToSelect, 0, ProfileSelector.SelectedProfile.CurrentDifficulty);

        //    for(int i = 0; i < LevelMapper.ScenesMapping.LevelsPerStage
        //}

        [ContextMenu("Complete Level By Scene Build Index")]
        public void CompleteLevelByBuildIndex()
        {
            LevelKey levelKey = LevelMapper.GetLevelKeyByBuildIndex(MainManager.Ins.SceneManager.CurrentSceneIndex);
            LevelOperation.SetLevelStatus(levelKey, LevelStatus.Completed);
            ProfilesManager.SaveProfile(ProfileSelector.SelectedProfile);
        }

        [ContextMenu("Print Profiles")]
        public void Print()
        {
            ProfilesManager.PrintProfiles();
        }

        [ContextMenu("Delete All Profiles")]
        public void Delete()
        {
            ProfilesManager.DeleteAllProfiles();
        }

        [ContextMenu("Print Selected Profile")]
        public void PrintSelected()
        {
            Debug.Log(ProfileSelector.SelectedProfile);
        }

        [ContextMenu("Print Difficulty")]
        public void PrintDifficulty()
        {
            Debug.Log(ProfileSelector.SelectedProfile.CurrentDifficulty);
        }
    }
}
#endif
