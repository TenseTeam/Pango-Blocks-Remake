namespace ProjectPBR.Managers.Static.Profiles
{
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.GameConfig.Constants;

    public static class ProfileSelector
    {
        public static ProfileData SelectedProfile;

        /// <summary>
        /// Selects a player profile by name.
        /// </summary>
        /// <param name="profileName">The name of the profile to select.</param>
        public static void SelectProfile(string profileName)
        {
            SelectProfile(ProfilesManager.GetProfile(profileName));
        }

        /// <summary>
        /// Selects a player profile.
        /// </summary>
        /// <param name="profile">The profile to select.</param>
        public static void SelectProfile(ProfileData profile)
        {
            SelectedProfile = profile;
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnSelectedProfile, profile);
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnProfileAlteration, profile);
        }

        /// <summary>
        /// Selects the next available player profile.
        /// </summary>
        public static void SelectNextProfile()
        {
            if (SelectedProfile == null) return;
            TrySelectNextFirstDifferent();
        }

        /// <summary>
        /// Changes the name and difficulty of the currently selected player profile.
        /// </summary>
        /// <param name="newName">The new name for the profile.</param>
        /// <param name="newDifficulty">The new difficulty level for the profile.</param>
        public static void ChangeSelectedProfileValues(string newName, GameDifficulty newDifficulty)
        {
            ProfilesManager.ChangeProfileNameAndDifficulty(SelectedProfile.ProfileIndex, newName, newDifficulty);
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnModifiedProfile, SelectedProfile);
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnProfileAlteration, SelectedProfile);
        }

        /// <summary>
        /// Tries to select the next first different player profile.
        /// </summary>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool TrySelectNextFirstDifferent()
        {
            ProfileData profile = ProfilesManager.GetNextFirstDifferent(SelectedProfile.ProfileIndex);
            if (profile == null)
                return false;

            SelectProfile(profile);
            return true;
        }

        /// <summary>
        /// Tries to select a player profile or the first one if not found.
        /// </summary>
        /// <param name="profileIndex">The index of the profile to select.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool TrySelectProfileOrFirst(int profileIndex)
        {
            ProfileData profile = ProfilesManager.GetProfileOrFirst(profileIndex);
            if (profile == null)
                return false;

            SelectProfile(profile);
            return true;
        }

        /// <summary>
        /// Tries to select a specific player profile.
        /// </summary>
        /// <param name="profileIndex">The index of the profile to select.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool TrySelectProfile(int profileIndex)
        {
            ProfileData profile = ProfilesManager.GetProfile(profileIndex);

            if (profile == null)
                return false;

            SelectProfile(profile);
            return true;
        }

        /// <summary>
        /// Tries to select the first player profile.
        /// </summary>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool TrySelectFirstProfile()
        {
            ProfileData firstProfile = ProfilesManager.GetFirstProfile();

            if (firstProfile == null)
                return false;

            SelectProfile(firstProfile);
            return true;
        }

        /// <summary>
        /// Selects or creates the first player profile if none is selected.
        /// </summary>
        public static void SelectOrCreateFirstProfile()
        {
            if (!TrySelectFirstProfile())
                ProfilesManager.CreateRandomAndSelect();
        }

        /// <summary>
        /// Selects or creates the first player profile if none is selected.
        /// </summary>
        public static void SelectOrCreateIfNoProfileSelected()
        {
            if (SelectedProfile == null)
                SelectOrCreateFirstProfile();
        }

        /// <summary>
        /// Deletes the currently selected player profile and deselects it.
        /// </summary>
        public static void DeleteAndDeselect()
        {
            ProfilesManager.DeleteProfile(SelectedProfile.ProfileIndex);
            SelectedProfile = null;
        }
    }
}