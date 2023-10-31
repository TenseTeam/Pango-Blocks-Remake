namespace ProjectPBR.Data.ScriptableObjects.Levels
{
    using UnityEngine;

    [System.Serializable]
    public struct LevelIndex
    {
        [Min(0)]
        public int LevelNumber; // Level Number is the SaveIndex
        [Min(0)]
        public int LevelBuildIndex;
    }

    [CreateAssetMenu(menuName = "Level Indexes")]
    public class LevelBuildIndexes : ScriptableObject
    {
        public LevelIndex[] EasyLevels;
        public LevelIndex[] HardLevels;
    }
}
