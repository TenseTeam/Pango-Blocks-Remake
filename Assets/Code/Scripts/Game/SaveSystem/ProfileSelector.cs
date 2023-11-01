namespace ProjectPBR.SaveSystem
{
    using UnityEngine;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Managers.Static;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.GameConfig.Constants;

    public static class ProfileSelector
    {
        public static ProfileData SelectedProfile;

        // This method is called after SubsystemRegistration
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void Init()
        {
            TrySelectFirstProfile();
        }

        public static void SelectProfile(string profileName)
        {
            SelectedProfile = ProfilesManager.GetProfile(profileName);
        }

        public static void SelectProfile(ProfileData profile)
        {
            SelectedProfile = profile;
        }

        public static void ChangeSelectedProfileDifficulty(GameDifficulty difficulty)
        {
            ProfilesManager.ChangeProfileDifficulty(SelectedProfile.ProfileName, difficulty);
        }

        public static bool TrySelectFirstProfile()
        {
            ProfileData firstProfile = ProfilesManager.GetFirstProfile();

            if(firstProfile == null)
                return false;

            SelectProfile(firstProfile);
            return true;
        }
    }
}
