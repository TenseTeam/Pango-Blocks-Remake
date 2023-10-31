#if UNITY_EDITOR || DEBUG
namespace ProjectPBR.Debug
{
    using UnityEngine;
    using ProjectPBR.SaveSystem;
    using ProjectPBR.Data.SaveDatas.Enums;

    public class SaveTester : MonoBehaviour
    {
        public string ProfileToCreate;
        public string ProfileToSelect;
        public LevelDifficulty DifficultyToChange;

        [ContextMenu("CreateProfile")]
        public void Create()
        {
            ProfilesManager.CreateProfile(ProfileToCreate);
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

        [ContextMenu("Change Selected Profile Difficulty")]
        public void ChangeDifficulty()
        {
            ProfileSelector.ChangeSelectedProfileDifficulty(DifficultyToChange);
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
