namespace ProjectPBR.Managers.Static.Profiles
{
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.GameConfig.Constants;

    public static class ProfileSelector
    {
        public static ProfileData SelectedProfile;

        //// This method is called after SubsystemRegistration
        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        //public static void Init()
        //{
        //    TrySelectFirstProfile();
        //}

        public static void SelectProfile(string profileName)
        {
            SelectProfile(ProfilesManager.GetProfile(profileName));
        }

        public static void SelectProfile(ProfileData profile)
        {
            SelectedProfile = profile;
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnSelectedProfile, profile);
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnProfileAlteration, profile);
        }

        public static void SelectNextProfile()
        {
            if (SelectedProfile == null) return;
            TrySelectNextFirstDifferent();
        }

        public static void ChangeSelectedProfileValues(string newName, GameDifficulty newDifficulty)
        {
            ProfilesManager.ChangeProfileNameAndDifficulty(SelectedProfile.ProfileIndex, newName, newDifficulty);
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnModifiedProfile, SelectedProfile);
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnProfileAlteration, SelectedProfile);
        }

        public static bool TrySelectNextFirstDifferent()
        {
            ProfileData profile = ProfilesManager.GetNextFirstDifferent(SelectedProfile.ProfileIndex);
            if (profile == null)
                return false;

            SelectProfile(profile);
            return true;
        }

        public static bool TrySelectProfileOrFirst(int profileIndex)
        {
            ProfileData profile = ProfilesManager.GetProfileOrFirst(profileIndex);
            if (profile == null)
                return false;

            SelectProfile(profile);
            return true;
        }

        public static bool TrySelectProfile(int profileIndex)
        {
            ProfileData profile = ProfilesManager.GetProfile(profileIndex);

            if (profile == null)
                return false;

            SelectProfile(profile);
            return true;
        }

        public static bool TrySelectFirstProfile()
        {
            ProfileData firstProfile = ProfilesManager.GetFirstProfile();

            if (firstProfile == null)
                return false;

            SelectProfile(firstProfile);
            return true;
        }

        public static void SelectOrCreateFirstProfile()
        {
            if (!TrySelectFirstProfile())
                ProfilesManager.CreateRandomAndSelect();
        }

        public static void SelectOrCreateIfNoProfileSelected()
        {
            if (SelectedProfile == null)
                SelectOrCreateFirstProfile();
        }

        public static void DeleteAndDeselect()
        {
            ProfilesManager.DeleteProfile(SelectedProfile.ProfileIndex);
            SelectedProfile = null;
        }
    }
}