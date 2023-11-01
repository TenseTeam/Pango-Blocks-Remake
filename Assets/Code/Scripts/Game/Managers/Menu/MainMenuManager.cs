namespace ProjectPBR.Managers.Menu
{
    using UnityEngine;
    using VUDK.Patterns.Singleton;
    using VUDK.UI.Menu;

    public class MainMenuManager : Singleton<MainMenuManager>
    {
        [field: SerializeField, Header("Profiler")]
        public UIProfiler Profiler { get; private set; }
    }
}
