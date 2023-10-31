#if UNITY_EDITOR || DEBUG
namespace ProjectPBR.Debug
{
    using UnityEngine;
    using ProjectPBR.SaveSystem;

    public class SaveTester : MonoBehaviour
    {
        public string ProfileToCreate;
        public string ProfileToSelect;

        [ContextMenu("CreateProfile")]
        public void Create()
        {
            ProfilesManager.CreateProfile(ProfileToCreate);
        }

        [ContextMenu("PrintProfiles")]
        public void Print()
        {
            ProfilesManager.PrintProfiles();
        }

        [ContextMenu("DeleteAllProfiles")]
        public void Delete()
        {
            ProfilesManager.DeleteAllProfiles();
        }

        [ContextMenu("SelectProfile")]
        public void Select()
        {
            ProfileSaver.SelectProfile(ProfileToSelect);
        }
    }
}
#endif
