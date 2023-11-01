namespace ProjectPBR.Managers.Main.GameStats
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main.Bases;
    using ProjectPBR.Data.ScriptableObjects.Levels;

    public class GameStats : GameStatsBase
    {
        [field: SerializeField, Header("Level Mapping")]
        public LevelMapping MappedLevels { get; private set; }
    }
}
