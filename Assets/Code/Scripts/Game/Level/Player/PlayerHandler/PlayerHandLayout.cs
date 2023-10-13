namespace ProjectPBR.Level.Player.PlayerHandler
{
    using ProjectPBR.Player.PlayerHandler.Blocks;
    using UnityEngine;
    using VUDK.Extensions.Transform;

    public class PlayerHandLayout : MonoBehaviour
    {
        [SerializeField, Header("Box Settings")]
        private float _spacing;
        private float _currentWidth;

        public void Insert(PlaceableBlock block)
        {
            SetBlock(block);
        }

        private void SetBlock(PlaceableBlock block)
        {
            block.transform.parent = null;
            block.transform.position = transform.position;
            block.transform.position += block.BlockData.UnitLength * Vector3.right * (_currentWidth + _spacing);

            _currentWidth += block.BlockData.UnitLength + _spacing;
            block.transform.SetParent(transform, true);
            block.SetOriginalPosition(block.transform.position);
        }
    }
}
