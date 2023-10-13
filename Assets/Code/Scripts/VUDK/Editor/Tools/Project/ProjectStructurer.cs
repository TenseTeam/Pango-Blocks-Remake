namespace VUDK.Editor.Tools.Project
{
    using System.IO;
    using UnityEditor;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.ObjectPool;

    public class ProjectStructurer : EditorWindow
    {
        private const string MainFolder = "Assets";

        private string[] _mainFolders = new string[]
        {
                "Art",
                "Audio",
                "Code",
                "Docs",
                "Level",
                "Resources",
        };

        private string[] _artFolders = new string[]
        {
                "Fonts",
                "Materials",
                "Models",
                "Sprites",
                "Textures"
        };

        private string[] _audioFolders = new string[]
        {
                "Music",
                "Mixers",
                "SFX"
        };

        private string[] _codeFolders = new string[]
        {
                "Scripts",
                "Shaders",
        };

        private string[] _levelFolders = new string[]
        {
                "Data",
                "Prefabs",
                "Scenes"
        };

        private string[] _resourceFolders = new string[]
        {
                "Settings",
                "Volumes",
                "Third Party"
        };

        [MenuItem("Tools/VUDK/Project/Project Structurer")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ProjectStructurer), false, "Project Structurer");
        }

        private void OnGUI()
        {
            GUILayout.Label("Project Structurer", EditorStyles.boldLabel);
            if (GUILayout.Button("Create Folders"))
                CreateFolders();

            if (GUILayout.Button("Create Managers"))
                CreateMainManager();
        }

        private void CreateFolders()
        {
            CreateFoldersInParentFolder(MainFolder, _mainFolders);
            CreateFoldersInParentFolder(MainFolder + "\\" + _mainFolders[0], _artFolders);
            CreateFoldersInParentFolder(MainFolder + "\\" + _mainFolders[1], _audioFolders);
            CreateFoldersInParentFolder(MainFolder + "\\" + _mainFolders[2], _codeFolders);
            CreateFoldersInParentFolder(MainFolder + "\\" + _mainFolders[4], _levelFolders);
            CreateFoldersInParentFolder(MainFolder + "\\" + _mainFolders[5], _resourceFolders);
        }

        private void CreateFoldersInParentFolder(string parentFolder, string[] folders)
        {
            foreach (string folderName in folders)
            {
                string folderPath = Path.Combine(parentFolder, folderName);

                if (!Directory.Exists(folderPath))
                {
                    AssetDatabase.CreateFolder(parentFolder, folderName);
#if DEBUG
                    Debug.Log("Directory created: " + folderPath);
#endif
                }
            }
        }

        private void CreateMainManager()
        {
            GameObject mainManagerObj = new GameObject(nameof(MainManager));
            MainManager mainManager = mainManagerObj.AddComponent<MainManager>();

            GameObject gameManagerObj = new GameObject(nameof(GameManager));
            GameManager gameManager = gameManagerObj.AddComponent<GameManager>();
            gameManagerObj.transform.parent = mainManagerObj.transform;

            GameObject poolsManagerObj = new GameObject(nameof(PoolsManager));
            PoolsManager poolsManager = poolsManagerObj.AddComponent<PoolsManager>();
            poolsManagerObj.transform.parent = gameManagerObj.transform;

            GameObject eventManagerObj = new GameObject(nameof(EventManager));
            EventManager eventManager = eventManagerObj.AddComponent<EventManager>();
            eventManagerObj.transform.parent = mainManagerObj.transform;

            GameObject gameConfigObj = new GameObject(nameof(GameConfig));
            GameConfig gameConfig = gameConfigObj.AddComponent<GameConfig>();
            gameConfigObj.transform.parent = mainManagerObj.transform;

            GameObject gameStateMachineObj = new GameObject(nameof(GameMachine));
            GameMachine gameStateMachine = gameStateMachineObj.AddComponent<GameMachine>();
            gameStateMachineObj.transform.parent = mainManagerObj.transform;

            //AssignReferences(mainManager, gameManager, poolsManager, eventManager, gameConfig, gameStateMachine);
        }

        //[System.Obsolete]
        //private void AssignReferences(MainManager main, GameManager gameManager, PoolsManager pools, EventManager eventManager, GameConfig config, GameStateMachine machine)
        //{
        //    main.AssignReferences(gameManager, pools, eventManager, config, machine);
        //}
    }
}