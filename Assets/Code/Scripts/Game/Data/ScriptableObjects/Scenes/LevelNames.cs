namespace ProjectPBR.Data.ScriptableObjects.Scenes
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public struct LevelScene
    {
        public int Index;
        public Scene Scene;
    }

    public class LevelNames : ScriptableObject
    {
        public LevelScene[] Levels;
    }
}
