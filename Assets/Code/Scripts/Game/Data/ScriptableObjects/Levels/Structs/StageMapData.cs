namespace ProjectPBR.Data.ScriptableObjects.Levels.Structs
{
    using System.Collections.Generic;

    [System.Serializable]
    public struct StageMapData
    {
        public List<LevelMapData> Levels;
        public int CutsceneBuildIndex;
    }
}
