namespace ProjectPBR.Data.ScriptableObjects.Levels
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Level Mapping")]
    public class LevelMapping : ScriptableObject
    {
        // The index of the level of the array is the save index
        // did this way to semplify the mapping
        public LevelMap[] Levels;
        [Min(1)]
        public int LevelsPerStage;
    }
}
