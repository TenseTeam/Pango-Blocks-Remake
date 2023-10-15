namespace ProjectPBR.Player.PlayerHandler
{
    using UnityEngine;
    using ProjectPBR.Level.Blocks;
    using System.Collections.Generic;
    using static Unity.Collections.AllocatorManager;
    using VUDK.Extensions.Transform;

    [RequireComponent(typeof(Collider2D))]
    public class PlayerHandLayout : MonoBehaviour
    {
        [SerializeField, Header("Layout Settings")]
        private float _spacing;
        [SerializeField]
        private Vector2 _blockSizeInHand;

        private float _usedLayoutWidth;
        private Collider2D _collider;

        private void Awake()
        {
            TryGetComponent(out _collider);
        }

        /// <summary>
        /// Inserts a block in the layout, aligned in a row.
        /// </summary>
        /// <param name="block">Block to insert.</param>
        public void InsertInRow(PlaceableBlock block)
        {
            block.transform.position = transform.position;
            block.transform.position += block.BlockData.UnitLength * Vector3.right * (_usedLayoutWidth + _spacing);
            _usedLayoutWidth += block.BlockData.UnitLength + _spacing;
            ResetBlockInLayout(block);
        }

        /// <summary>
        /// Inserts a block in the layout, not aligned, if it is inside the layout bounds.
        /// </summary>
        /// <param name="block">Block to insert.</param>
        public void InsertInBounds(PlaceableBlock block)
        {
            block.transform.position = new Vector2(block.transform.position.x, transform.position.y);
            if (!IsBlockInsideBounds(block))
                return;
            ResetBlockInLayout(block);
        }

        public PlaceableBlock GetAndRemoveFromHand(PlaceableBlock block)
        {
            block.transform.SetParent(null);
            block.transform.SetLossyScale(Vector3.one);
            return block;
        }

        public void RemoveFromLayout(PlaceableBlock block)
        {
            block.transform.SetParent(null);
            block.transform.SetLossyScale(Vector3.one);
        }

        public void ResetRow()
        {
            _usedLayoutWidth = 0;
        }

        private bool IsBlockInsideBounds(PlaceableBlock block)
        {
            foreach (Vector2 point in block.Collider.points)
            {
                Vector2 pos = block.transform.TransformPoint(point);

                if (!_collider.bounds.Contains(pos))
                    return false;
            }

            return true;
        }

        public void ResetBlockInLayout(PlaceableBlock block)
        {
            block.transform.SetParent(transform);
            block.transform.SetLossyScale(_blockSizeInHand);
            block.SetResetPosition();
        }
    }
}
