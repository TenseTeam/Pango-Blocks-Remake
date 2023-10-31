namespace ProjectPBR.SaveSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.Main.SaveSystem;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.GameConfig.Constants;
    using VUDK.Generic.Managers.Static;
    using System.Linq;

    public static class ProfilesManager
    {
        private static Dictionary<string, ProfileData> s_Profiles = new Dictionary<string, ProfileData>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
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

        public static bool CreateProfile(string profileName)
        {
            if (s_Profiles.Count >= GameConstants.ProfileSaving.MaxProfilesNumber) return false;
            if (s_Profiles.ContainsKey(profileName)) return false;

            ProfileData profile = new ProfileData(profileName);
            SaveManager.Save(profile, profileName, GameConstants.ProfileSaving.ProfileExtension);
            s_Profiles.Add(profileName, profile);
            return true;
        }

        public static void DeleteProfile(string profileName)
        {
            if (!s_Profiles.ContainsKey(profileName)) return;

            SaveManager.DeleteSave(profileName, GameConstants.ProfileSaving.ProfileExtension);
            s_Profiles.Remove(profileName);
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
