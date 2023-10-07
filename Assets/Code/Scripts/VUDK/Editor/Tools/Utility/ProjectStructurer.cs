namespace VUDK.Editor.Tools.Utility
{
    using System.IO;
    using UnityEditor;
    using UnityEngine;

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

        [MenuItem("Tools/VUDK/Create Project Folders")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ProjectStructurer), false, "Project Structurer");
        }

        private void OnGUI()
        {
            GUILayout.Label("Project Structurer", EditorStyles.boldLabel);

            if (GUILayout.Button("Create Folders"))
                CreateFolders();
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
    }
}