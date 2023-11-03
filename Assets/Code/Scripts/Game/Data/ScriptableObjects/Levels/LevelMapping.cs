namespace ProjectPBR.Data.ScriptableObjects.Levels
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Level Mapping")]
    public class LevelMapping : ScriptableObject
    {
        [Min(1)]
        public int LevelsPerStage;
        public LevelMap[] Levels;
    }
}
