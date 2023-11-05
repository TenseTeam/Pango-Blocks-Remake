namespace ProjectPBR.Data.ScriptableObjects.Levels.Structs
{
    using UnityEngine;

    [System.Serializable]
    public struct LevelMapData
    {
        [Min(0)]
        public int EasyBuildIndex;
        [Min(0)]
        public int HardBuildIndex;

        public LevelMapData(int index)
        {
            EasyBuildIndex = index;
            HardBuildIndex = index;
        }
    }
}
