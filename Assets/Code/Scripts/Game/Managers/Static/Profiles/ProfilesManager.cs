namespace ProjectPBR.Managers.Static.Profiles
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using VUDK.Extensions.Strings;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.SaveSystem;
    using VUDK.Extensions.Colors;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.Data.SaveDatas.Enums;

    public static class ProfilesManager
    {
        private static SortedDictionary<int, ProfileData> s_Profiles = new SortedDictionary<int, ProfileData>();
        private static Color[] s_TagColors;

        public static int Count => s_Profiles.Count;

        static ProfilesManager()
        {
            s_TagColors = new Color[5] { Color.red, Color.blue, Color.green, Color.yellow, Color.magenta };
        }

        /// <summary>
        /// Initializes the profiles manager, loading profiles on runtime.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)] // Called before FirstSceneIsloaded and AfterAssembliesLoaded
        public static void Init()
        {
            LoadProfiles();
        }

        /// <summary>
        /// Loads existing profiles from the save system.
        /// </summary>
        public static void LoadProfiles()
        {
            foreach(string fileName in SaveManager.GetFileNames(GameConstants.ProfileSaving.ProfileExtension))
            {
                if(SaveManager.TryLoad(out ProfileData profile, fileName, GameConstants.ProfileSaving.ProfileExtension))
                {
                    s_Profiles.Add(profile.ProfileIndex, profile);
                }
            }
        }

        /// <summary>
        /// Saves all existing profiles using the save system.
        /// </summary>
        public static void SaveProfiles()
        {
            foreach(var profilePair in s_Profiles)
            {
                SaveManager.Save(profilePair.Value, profilePair.Value.ProfileName, GameConstants.ProfileSaving.ProfileExtension);
            }
        }

        /// <summary>
        /// Creates a new player profile with the specified name and difficulty.
        /// </summary>
        /// <param name="profileName">The name of the new profile.</param>
        /// <param name="difficulty">The difficulty level of the new profile.</param>
        /// <returns>True if the profile is successfully created, false otherwise.</returns>
        public static bool CreateProfile(string profileName, GameDifficulty difficulty = GameDifficulty.Easy)
        {
            if (s_Profiles.Count >= GameConstants.ProfileSaving.MaxProfilesCount) return false;
            if (HasProfile(profileName)) return false;

            int profileIndex = s_Profiles.LastOrDefault().Key + 1;                                                  // Get last profile index and add 1
            ProfileData profile = DataFactory.Create(profileName, GetAvailableColor(), profileIndex, difficulty);   // Create new profile
            SaveProfile(profile);                                                                                   // Save profile
            s_Profiles.Add(profileIndex, profile);                                                                  // Add profile to list
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnCreatedProfile, s_Profiles.Count);     // Trigger event
            return true;
        }

        /// <summary>
        /// Creates a new player profile with the specified name and difficulty, and selects it.
        /// </summary>
        /// <param name="profileName">The name of the new profile.</param>
        /// <param name="difficulty">The difficulty level of the new profile.</param>
        /// <returns>True if the profile is successfully created and s
        public static bool CreateAndSelect(string profileName, GameDifficulty difficulty = GameDifficulty.Easy)
        {
            if(CreateProfile(profileName, difficulty))
            {
                ProfileSelector.SelectProfile(profileName);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates a random player profile and selects it.
        /// </summary>
        public static void CreateRandomAndSelect()
        {
            string profileName = StringExtension.Random(GameConstants.ProfileSaving.MaxProfileNameLength);
            CreateProfile(profileName);
            ProfileSelector.SelectProfile(profileName);
        }

        /// <summary>
        /// Changes the difficulty level of a specific player profile.
        /// </summary>
        /// <param name="profileIndex">The index of the profile to modify.</param>
        /// <param name="difficulty">The new difficulty level for the profile.</param>
        public static void ChangeProfileDifficulty(int profileIndex, GameDifficulty difficulty)
        {
            if (!s_Profiles.ContainsKey(profileIndex)) return;

            ProfileData profile = GetProfile(profileIndex);
            profile.CurrentDifficulty = difficulty;
            SaveProfile(profile);
        }

        /// <summary>
        /// Changes the name of a specific player profile.
        /// </summary>
        /// <param name="profileIndex">The index of the profile to modify.</param>
        /// <param name="profileName">The new name for the profile.</param>
        public static void ChangeProfileName(int profileIndex, string profileName)
        {
            if (!s_Profiles.ContainsKey(profileIndex)) return;

            ProfileData profile = GetProfile(profileIndex);
            profile.ProfileName = profileName;
            SaveProfile(profile);
        }

        /// <summary>
        /// Changes the name and difficulty level of a specific player profile.
        /// </summary>
        /// <param name="profileIndex">The index of the profile to modify.</param>
        /// <param name="profileName">The new name for the profile.</param>
        /// <param name="difficulty">The new difficulty level for the profile.</param>
        public static void ChangeProfileNameAndDifficulty(int profileIndex, string profileName, GameDifficulty difficulty)
        {
            if (!s_Profiles.ContainsKey(profileIndex)) return;

            ProfileData profile = GetProfile(profileIndex);
            profile.ProfileName = profileName;
            profile.CurrentDifficulty = difficulty;
            SaveProfile(profile);
        }

        /// <summary>
        /// Deletes a specific player profile.
        /// </summary>
        /// <param name="profileIndex">The index of the profile to delete.</param>
        /// <returns>True if the profile is successfully deleted, false otherwis
        public static bool DeleteProfile(int profileIndex)
        {
            if (!s_Profiles.ContainsKey(profileIndex)) return false;

            SaveManager.DeleteSave(s_Profiles[profileIndex].Id.ToString(), GameConstants.ProfileSaving.ProfileExtension);
            s_Profiles.Remove(profileIndex);
            return true;
        }

        /// <summary>
        /// Deletes all player profiles.
        /// </summary>
        public static void DeleteAllProfiles()
        {
            foreach (var profilePair in s_Profiles)
                SaveManager.DeleteSave(profilePair.Value.ProfileName, GameConstants.ProfileSaving.ProfileExtension);

            s_Profiles.Clear();
        }

        /// <summary>
        /// Gets the first player profile in the list.
        /// </summary>
        /// <returns>The first player profile.</returns>
        public static ProfileData GetFirstProfile()
        {
            return s_Profiles.FirstOrDefault().Value;
        }

        /// <summary>
        /// Gets a player profile by name.
        /// </summary>
        /// <param name="profileName">The name of the profile to retrieve.</param>
        /// <returns>The player profile with the specified name, or null if not found.</returns>
        public static ProfileData GetProfile(string profileName)
        {
            foreach(var profilePair in s_Profiles)
            {
                if(profilePair.Value.ProfileName == profileName)
                    return profilePair.Value;
            }

            return null;
        }

        /// <summary>
        /// Gets the next player profile with an index greater than the specified index.
        /// </summary>
        /// <param name="profileIndex">The index of the current profile.</param>
        /// <returns>The next player profile with an index greater than the specified index, or the first profile if none found.</returns>
        public static ProfileData GetNextFirstDifferent(int profileIndex)
        {
            if (!s_Profiles.ContainsKey(profileIndex)) return null;

            SortedDictionary<int, ProfileData>.Enumerator enumerator = s_Profiles.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Key > profileIndex)
                    return enumerator.Current.Value;
            }

            return GetFirstProfile();
        }

        /// <summary>
        /// Gets a player profile by index.
        /// </summary>
        /// <param name="profileIndex">The index of the profile to retrieve.</param>
        /// <returns>The player profile with the specified index, or null if not found.</returns>
        public static ProfileData GetProfile(int profileIndex)
        {
            try
            {
                return s_Profiles[profileIndex];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a player profile by index, or the first profile if the specified index is not found.
        /// </summary>
        /// <param name="index">The index of the profile to retrieve.</param>
        /// <returns>The player profile with the specified index, or the first profile if not found.</returns>
        public static ProfileData GetProfileOrFirst(int index)
        {
            ProfileData profile = GetProfile(index);
            profile ??= GetFirstProfile();

            return profile;
        }

        /// <summary>
        /// Checks if a player profile with the specified name exists.
        /// </summary>
        /// <param name="profileName">The name of the profile to check for.</param>
        /// <returns>True if a profile with the specified name exists, false otherwise.</returns>
        public static bool HasProfile(string profileName)
        {
            foreach(var profile in s_Profiles)
            {
                if(profile.Value.ProfileName == profileName)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if a player profile with the specified name exists, excluding a specific profile.
        /// </summary>
        /// <param name="profileName">The name of the profile to check for.</param>
        /// <param name="excludedProfile">The profile to exclude from the check.</param>
        /// <returns>True if a profile with the specified name exists (excluding the specified profile), false otherwise.</returns>
        public static bool HasProfile(string profileName, ProfileData excludedProfile)
        {
            foreach (var profile in s_Profiles)
            {
                if (excludedProfile != profile.Value && profile.Value.ProfileName == profileName)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Saves a player profile using the save system.
        /// </summary>
        /// <param name="profile">The player profile to save.</param>
        public static void SaveProfile(ProfileData profile)
        {
            SaveManager.Save(profile, profile.Id.ToString(), GameConstants.ProfileSaving.ProfileExtension);
        }

        /// <summary>
        /// Checks if a given profile name is valid.
        /// </summary>
        /// <param name="profileName">The profile name to validate.</param>
        /// <returns>True if the profile name is valid, false otherwise.</returns>
        public static bool IsProfileNameValid(string profileName)
        {
            return
                !string.IsNullOrEmpty(profileName)
                && !string.IsNullOrWhiteSpace(profileName)
                && !profileName.Contains(" ");
        }

        /// <summary>
        /// Gets an available color for a new profile, avoiding color duplicates.
        /// </summary>
        /// <returns>An available color for a new profile.</returns>
        public static Color GetAvailableColor()
        {
            foreach (Color color in s_TagColors)
            {
                if (!s_Profiles.Any(profile => ColorExtension.Deserialize(profile.Value.Rgba).Equals(color)))
                    return color;
            }

            return Color.white;
        }

#if DEBUG
        /// <summary>
        /// Prints information about all existing profiles to the debug log.
        /// </summary>
        public static void PrintProfiles()
        {
            foreach (var profilePair in s_Profiles)
                Debug.Log($"{profilePair.Value}");
        }
#endif
    }
}
