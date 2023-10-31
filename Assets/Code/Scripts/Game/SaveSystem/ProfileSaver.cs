namespace ProjectPBR.SaveSystem
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Data.SaveDatas;

    public static class ProfileSaver
    {
        private static ProfileData s_SelectedProfile; 

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            SceneManager.sceneLoaded += BindEvents;
            SceneManager.sceneUnloaded += UnbindEvents;
        }

        private static void BindEvents(Scene arg0, LoadSceneMode arg1)
        {
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginGameWonPhase, SaveLevel);
        }

        private static void UnbindEvents(Scene arg0)
        {
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnBeginGameWonPhase, SaveLevel);
        }

        public static void SelectProfile(string profileName)
        {
            s_SelectedProfile = ProfilesManager.GetProfile(profileName);
        }

        private static void SaveLevel()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;

            if (!s_SelectedProfile.ClearedLevels.TryAdd(currentScene, LevelStatus.Completed))
                s_SelectedProfile.ClearedLevels[currentScene] = LevelStatus.Completed;

            ProfilesManager.SaveProfile(s_SelectedProfile);
        }
    }
}
