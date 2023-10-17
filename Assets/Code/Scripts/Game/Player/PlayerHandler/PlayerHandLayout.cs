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
        private Vector2 _layoutblockSize;

        private float _usedLayoutWidth;
        private Collider2D _collider;
        
        [field: SerializeField]
        public LayerMask LayoutMask { get; private set; }

        private void Awake()
        {
            TryGetComponent(out _collider);
        }

        private void OnValidate()
        {
            if (_layoutblockSize.magnitude <= .01f)
                Debug.LogWarning($"Layout blocks size in {transform.name} is zero.");
        }

        /// <summary>
        /// Inserts a block in the layout, aligned in a row.
        /// </summary>
        /// <param name="block">Block to insert.</param>
        public void InsertInRow(PlaceableBlock block)
        {
            _usedLayoutWidth += _spacing;
            block.transform.position = new Vector2(transform.position.x + _usedLayoutWidth, transform.position.y);
            _usedLayoutWidth += block.BlockData.UnitLength;
            ResetBlockInLayout(block);
        }

        /// <summary>
        /// Inserts a block in the layout, not aligned, if it is inside the layout bounds.
        /// </summary>
        /// <param name="block">Block to insert.</param>
        public void InsertInBounds(PlaceableBlock block)
        {
            block.transform.SetLossyScale(_layoutblockSize); // Set the block size to the layout size
            block.transform.position = new Vector2(block.transform.position.x, transform.position.y);
            if (!IsBlockInsideBounds(block))
            {
                block.transform.SetLossyScale(Vector3.one);
                return;
            }
            ResetBlockInLayout(block);
        }

        public PlaceableBlock GetAndRemoveFromHand(PlaceableBlock block)
        {
            RemoveFromLayout(block);
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
            block.transform.SetLossyScale(_layoutblockSize);
            block.SetResetPosition();
        }
    }
}
