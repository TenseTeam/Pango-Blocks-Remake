namespace ProjectPBR.Data.ScriptableObjects.Levels
{
    using UnityEngine;

    [System.Serializable]
    public struct LevelMap
    {
        [Min(0)]
        public int EasyBuildIndex;
        [Min(0)]
        public int HardBuildIndex;
    }
}
