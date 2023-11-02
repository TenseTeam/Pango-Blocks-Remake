#if UNITY_EDITOR || DEBUG
namespace ProjectPBR.Debug
{
    using UnityEngine;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Managers.Static.Profiles;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Managers.Static;
    using UnityEngine.SceneManagement;

    public class SaveTester : MonoBehaviour
    {
        public string ProfileToCreate;
        public string ProfileToSelect;
        public int LevelToComplete;
        public GameDifficulty DifficultyToChange;

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

        [ContextMenu("Complete Level")]
        public void CompleteLevel()
        {
            LevelKey levelKey = LevelMapper.GetLevelKeyByLevelIndex(LevelToComplete);
            ProfileLevelOperation.SetLevelStatus(levelKey, LevelStatus.Completed);
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
