namespace ProjectPBR.Player.PlayerHandler
{
    using UnityEngine;
    using ProjectPBR.Level.Blocks;
    using VUDK.Extensions.Transform;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.GameConfig.Constants;
    using VUDK.Generic.Serializable;

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
        /// Set the <see cref="PlaceableBlockBase"/> position, aligned in a row.
        /// </summary>
        /// <param name="block">Block to insert.</param>
        public void SetBlockPositionInLayoutRow(PlaceableBlockBase block)
        {
            block.transform.SetLossyScale(_layoutblockSize); // Set the block size to the layout size
            _usedLayoutWidth += _spacing;
            block.transform.position = new Vector2(transform.position.x + _usedLayoutWidth, transform.position.y);
            if(block is ComplexPlaceableBlock)
                _usedLayoutWidth += (block as ComplexPlaceableBlock).ComposedBlocks.Count;
            else
                _usedLayoutWidth++;
            block.SetResetPosition();
        }

        /// <summary>
        /// Set the reset position of a <see cref="PlaceableBlockBase"/> in the layout position, not aligned, if it is inside the layout bounds.
        /// </summary>
        /// <param name="block">Block to insert.</param>
        public void SetResetPositionInLayoutBounds(PlaceableBlockBase block)
        {
            block.transform.SetLossyScale(_layoutblockSize); // Set the block size to the layout size
            block.transform.position = new Vector2(block.transform.position.x, transform.position.y); // Momentarily align the block to the layout
            if (!IsBlockInsideBounds(block))
            {
                block.transform.SetLossyScale(Vector3.one);
                return;
            }
            block.SetResetPosition(); // If the block is inside the layout, set the reset position
        }

        public PlaceableBlockBase GetAndRemoveFromHand(PlaceableBlockBase block)
        {
            RemoveFromLayout(block);
            return block;
        }

        public void RemoveFromLayout(PlaceableBlockBase block)
        {
            block.transform.SetLossyScale(Vector3.one);
        }

        public void ResetRow()
        {
            _usedLayoutWidth = 0;
        }

        public void LerpPositionToHand(PlaceableBlockBase block, float resetTime)
        {
            block.transform.SetLossyScale(_layoutblockSize);
            block.StartLerpResettingPosition(resetTime);
        }

        private bool IsBlockInsideBounds(PlaceableBlockBase block)
        {
            if (block is SinglePlaceableBlock)
            {
                return IsColliderInsideBounds((block as SinglePlaceableBlock).Collider);
            }

            foreach (SinglePlaceableBlock singleBlock in (block as ComplexPlaceableBlock).ComposedBlocks)
            {
                if (!IsColliderInsideBounds(singleBlock.Collider))
                    return false;
            }

            return true;
        }

        private bool IsColliderInsideBounds(PolygonCollider2D coll)
        {
            foreach (Vector2 point in coll.points)
            {
                Vector2 pos = coll.transform.TransformPoint(point);

                if (!_collider.bounds.Contains(pos))
                    return false;
            }

            return true;
        }
    }
}
