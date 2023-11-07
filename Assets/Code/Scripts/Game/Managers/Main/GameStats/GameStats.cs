namespace ProjectPBR.Managers.Main.GameStats
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using VUDK.Generic.Managers.Main.Bases;
    using ProjectPBR.Data.ScriptableObjects.Levels;

    public class GameStats : GameStatsBase
    {
        [field: SerializeField, Header("Scenes Mapping")]
        public ScenesMappingData ScenesMapping { get; private set; }

        [field: SerializeField, Header("Sorting Layers")]
        public int CharacterLayer { get; private set; }
        [field: SerializeField]
        public int PlacingBlockLayer { get; private set; }
        [field: SerializeField]
        public int PlacedBlockLayer { get; private set; }
    }
}
