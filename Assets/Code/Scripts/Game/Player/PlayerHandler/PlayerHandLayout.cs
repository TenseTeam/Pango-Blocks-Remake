namespace ProjectPBR.Player.PlayerHandler
{
    using UnityEngine;
    using VUDK.Extensions.Transform;
    using ProjectPBR.Level.Blocks;

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

        /// <summary>
        /// Gets a <see cref="PlaceableBlockBase"/> from the layout and removes it from the layout.
        /// </summary>
        /// <param name="block"><see cref="PlaceableBlockBase"/> to get.</param>
        /// <returns>Getted <see cref="PlaceableBlockBase"/>.</returns>
        public PlaceableBlockBase GetAndRemoveFromHand(PlaceableBlockBase block)
        {
            RemoveFromLayout(block);
            return block;
        }

        /// <summary>
        /// Removes a <see cref="PlaceableBlockBase"/> from the layout.
        /// </summary>
        /// <param name="block"><see cref="PlaceableBlockBase"/> to remove.</param>
        public void RemoveFromLayout(PlaceableBlockBase block)
        {
            block.transform.SetLossyScale(Vector3.one);
        }

        /// <summary>
        /// Resets the used width for spacing.
        /// </summary>
        public void ResetRow()
        {
            _usedLayoutWidth = 0;
        }

        /// <summary>
        /// Starts a block lerp resetting position to the hand.
        /// </summary>
        /// <param name="block">Block to reset in hand.</param>
        /// <param name="resetTime">Reset time duration.</param>
        public void StartLerpPositionToHand(PlaceableBlockBase block, float resetTime)
        {
            block.transform.SetLossyScale(_layoutblockSize);
            block.StartLerpResettingPosition(resetTime);
        }

        /// <summary>
        /// Checks if a given <see cref="PlaceableBlockBase"/> is inside this collider bounds.
        /// </summary>
        /// <param name="block"><see cref="PlaceableBlockBase"/> to check.</param>
        /// <returns>True if it is inside, False if not.</returns>
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

        /// <summary>
        /// Checks if a collider is inside this collider bounds.
        /// </summary>
        /// <param name="coll">Collider to check.</param>
        /// <returns>True if it is inside, False if not.</returns>
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
