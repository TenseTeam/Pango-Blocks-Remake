namespace ProjectPBR.Managers.Static.Profiles
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using VUDK.Generic.Managers.Static;
    using VUDK.Extensions.Strings;
    using VUDK.Generic.Managers.Main;
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

        // This method is called before the first scene is loaded, before AfterAssembliesLoaded
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Init()
        {
            LoadProfiles();
        }

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

        public static void SaveProfiles()
        {
            foreach(var profilePair in s_Profiles)
            {
                SaveManager.Save(profilePair.Value, profilePair.Value.ProfileName, GameConstants.ProfileSaving.ProfileExtension);
            }
        }

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

        public static bool CreateAndSelect(string profileName, GameDifficulty difficulty = GameDifficulty.Easy)
        {
            if(CreateProfile(profileName, difficulty))
            {
                ProfileSelector.SelectProfile(profileName);
                return true;
            }
            return false;
        }

        public static void CreateRandomAndSelect()
        {
            string profileName = StringExtension.Random(GameConstants.ProfileSaving.MaxProfileNameLength);
            CreateProfile(profileName);
            ProfileSelector.SelectProfile(profileName);
        }

        public static void ChangeProfileDifficulty(int profileIndex, GameDifficulty difficulty)
        {
            if (!s_Profiles.ContainsKey(profileIndex)) return;

            ProfileData profile = GetProfile(profileIndex);
            profile.CurrentDifficulty = difficulty;
            SaveProfile(profile);
        }

        public static void ChangeProfileName(int profileIndex, string profileName)
        {
            if (!s_Profiles.ContainsKey(profileIndex)) return;

            ProfileData profile = GetProfile(profileIndex);
            profile.ProfileName = profileName;
            SaveProfile(profile);
        }

        public static void ChangeProfileNameAndDifficulty(int profileIndex, string profileName, GameDifficulty difficulty)
        {
            if (!s_Profiles.ContainsKey(profileIndex)) return;

            ProfileData profile = GetProfile(profileIndex);
            profile.ProfileName = profileName;
            profile.CurrentDifficulty = difficulty;
            SaveProfile(profile);
        }

        public static bool DeleteProfile(int profileIndex)
        {
            if (!s_Profiles.ContainsKey(profileIndex)) return false;

            SaveManager.DeleteSave(s_Profiles[profileIndex].Id.ToString(), GameConstants.ProfileSaving.ProfileExtension);
            s_Profiles.Remove(profileIndex);
            return true;
        }

        public static void DeleteAllProfiles()
        {
            foreach (var profilePair in s_Profiles)
                SaveManager.DeleteSave(profilePair.Value.ProfileName, GameConstants.ProfileSaving.ProfileExtension);

            s_Profiles.Clear();
        }

        public static ProfileData GetFirstProfile()
        {
            return s_Profiles.FirstOrDefault().Value;
        }

        public static ProfileData GetProfile(string profileName)
        {
            foreach(var profilePair in s_Profiles)
            {
                if(profilePair.Value.ProfileName == profileName)
                    return profilePair.Value;
            }

            return null;
        }

        public static ProfileData GetNextFirstDifferent(int profileIndex)
        {
            if (s_Profiles.Count == 0) return null;
            if (!s_Profiles.ContainsKey(profileIndex)) return null;

            SortedDictionary<int, ProfileData>.Enumerator enumerator = s_Profiles.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Key > profileIndex)
                    return enumerator.Current.Value;
            }

            return GetFirstProfile();
        }


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

        public static ProfileData GetProfileOrFirst(int index)
        {
            ProfileData profile = GetProfile(index);
            if (profile == null)
                profile = GetFirstProfile();

            return profile;
        }

        public static bool HasProfile(string profileName)
        {
            foreach(var profile in s_Profiles)
            {
                if(profile.Value.ProfileName == profileName)
                    return true;
            }

            return false;
        }

        public static bool HasMultipleProfiles(string profileName)
        {
            int n = 0;

            foreach(var profile in s_Profiles)
            {
                if(profile.Value.ProfileName == profileName)
                    n++;

                if(n > 1)
                    return true;
            }

            return false;
        }

        public static void SaveProfile(ProfileData profile)
        {
            SaveManager.Save(profile, profile.Id.ToString(), GameConstants.ProfileSaving.ProfileExtension);
        }

        public static bool IsProfileNameValid(string profileName)
        {
            return
                !string.IsNullOrEmpty(profileName)
                && !string.IsNullOrWhiteSpace(profileName)
                && !profileName.Contains(" ");
        }

        public static Color GetAvailableColor()
        {
            foreach (Color color in s_TagColors)
            {
                if (!s_Profiles.Any(profile => profile.Value.Color.Equals(color)))
                    return color;
            }

            return Color.white;
        }
#if DEBUG
        public static void PrintProfiles()
        {
            foreach (var profilePair in s_Profiles)
                Debug.Log($"{profilePair.Value}");
        }
#endif
    }
}
