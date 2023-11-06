namespace ProjectPBR.Data.ScriptableObjects.Levels
{
    using UnityEngine;
    using ProjectPBR.Data.ScriptableObjects.Levels.Structs;
    using System.Collections.Generic;

    [CreateAssetMenu(menuName = "Mapping/ScenesMapping")]
    public class ScenesMappingData : ScriptableObject
    {
        [Min(0), Header("Menu")]
        public int MenuBuildIndex;

        [Min(1), Header("Stages")]
        public int LevelsPerStage;
        public List<StageMapData> Stages;

        public int TotalLevelsCount => Stages.Count * LevelsPerStage;
    }
}
