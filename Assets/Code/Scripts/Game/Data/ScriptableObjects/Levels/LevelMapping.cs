namespace ProjectPBR.Data.ScriptableObjects.Levels
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Level Mapping")]
    public class LevelMapping : ScriptableObject
    {
        [Min(0), Header("Menu")]
        public int MenuBuildIndex;

        [Min(1), Header("Levels")]
        public int LevelsPerStage;
        public LevelMap[] Levels;
    }
}
