namespace ProjectPBR.UI.Menu
{
    using UnityEngine;
    using ProjectPBR.Data.SaveDatas.Enums;

    [RequireComponent(typeof(SpriteRenderer))]
    public class UIDifficultyButton : MonoBehaviour
    {
        [SerializeField, Header("Sprites")]
        private Sprite _selectedSprite;
        [SerializeField]
        private Sprite _deselectedSprite;
        [SerializeField, Header("Difficulty")]
        private GameDifficulty _difficulty;


    }
}
