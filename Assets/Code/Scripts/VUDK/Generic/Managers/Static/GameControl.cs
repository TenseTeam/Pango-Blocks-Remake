namespace VUDK.Generic.Managers.Static
{
    using UnityEngine;
    using VUDK.Config;
    using VUDK.Generic.Managers.Main;

    public static class GameControl
    {
        /// <summary>
        /// Is the game has been started?
        /// </summary>
        public static bool HasBeenStarted { get; private set; }

        /// <summary>
        /// Is this the first launch of the game?
        /// </summary>
        public static bool IsFirstLaunch { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Init()
        {
            FirstLaunch();
            GameOpen();
        }

        private static void GameOpen()
        {
            HasBeenStarted = true;
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.GameEvents.OnGameOpened);
        }

        private static void FirstLaunch()
        {
            IsFirstLaunch = PlayerPrefs.GetInt(Constants.Prefs.FirstLaunch, 1) == 1; // 1 = true

            if(IsFirstLaunch)
            {
                PlayerPrefs.SetInt(Constants.Prefs.FirstLaunch, 0); // 0 = false
                PlayerPrefs.Save();
                MainManager.Ins.EventManager.TriggerEvent(EventKeys.GameEvents.OnFirstLaunch);
            }
        }
    }
}
