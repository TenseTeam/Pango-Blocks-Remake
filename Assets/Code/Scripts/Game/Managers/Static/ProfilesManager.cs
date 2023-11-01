namespace ProjectPBR.Managers.Static
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using VUDK.Generic.Managers.Static;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Patterns.Factories;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.SaveSystem;
    using VUDK.Extensions.Strings;

    public static class ProfilesManager
    {
        private static Dictionary<string, ProfileData> s_Profiles = new Dictionary<string, ProfileData>();

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
                    s_Profiles.Add(fileName, profile);
                }
            }
        }

        public static void SaveProfiles()
        {
            foreach(var profilePair in s_Profiles)
            {
                SaveManager.Save(profilePair.Value, profilePair.Key, GameConstants.ProfileSaving.ProfileExtension);
            }
        }

        public static bool CreateProfile(string profileName, GameDifficulty difficulty = GameDifficulty.Easy)
        {
            if (s_Profiles.Count >= GameConstants.ProfileSaving.MaxProfilesNumber) return false;
            if (s_Profiles.ContainsKey(profileName)) return false;

            ProfileData profile = DataFactory.Create(profileName, difficulty);
            SaveManager.Save(profile, profileName, GameConstants.ProfileSaving.ProfileExtension);
            s_Profiles.Add(profileName, profile);
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
            ProfilesManager.CreateProfile(profileName);
            ProfileSelector.SelectProfile(profileName);
        }

        public static void ChangeProfileDifficulty(string profileName, GameDifficulty difficulty)
        {
            if (!s_Profiles.ContainsKey(profileName)) return;

            ProfileData profile = GetProfile(profileName);
            profile.CurrentDifficulty = difficulty;
            SaveProfile(profile);
        }

        public static bool DeleteProfile(string profileName)
        {
            if (!s_Profiles.ContainsKey(profileName)) return false;

            SaveManager.DeleteSave(profileName, GameConstants.ProfileSaving.ProfileExtension);
            s_Profiles.Remove(profileName);
            return true;
        }

        public static void DeleteAllProfiles()
        {
            foreach (var profilePair in s_Profiles)
                SaveManager.DeleteSave(profilePair.Key, GameConstants.ProfileSaving.ProfileExtension);

            s_Profiles.Clear();
        }

        public static ProfileData GetFirstProfile()
        {
            if (s_Profiles.Count == 0) return null;
            return s_Profiles.ElementAt(0).Value;
        }

        public static ProfileData GetProfile(string profileName)
        {
            if (!s_Profiles.ContainsKey(profileName)) return null;
            return s_Profiles[profileName];
        }

        public static bool HasProfile(string profileName)
        {
            return s_Profiles.ContainsKey(profileName);
        }

        public static void SaveProfile(ProfileData profile)
        {
            SaveManager.Save(profile, profile.ProfileName, GameConstants.ProfileSaving.ProfileExtension);
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
