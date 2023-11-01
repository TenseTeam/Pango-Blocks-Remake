namespace ProjectPBR.SaveSystem
{
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Managers.Static;
    using VUDK.Extensions.Strings;

    public static class ProfileSelector
    {
        public static ProfileData SelectedProfile;

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

        public static void CreateAndSelect()
        {
            string profileName = StringExtension.Random(GameConstants.ProfileSaving.MaxProfileNameLength);
            ProfilesManager.CreateProfile(profileName);
            SelectProfile(profileName);
        }
    }
}
