namespace ProjectPBR.Player.PlayerHandler
{
    using UnityEngine;
    using ProjectPBR.Level.Blocks;

    [RequireComponent(typeof(Collider2D))]
    public class PlayerHandLayout : MonoBehaviour
    {
        [SerializeField, Header("Box Settings")]
        private float _spacing;
        private float _currentWidth;

        private Collider2D _collider;

        private void Awake()
        {
            TryGetComponent(out _collider);
        }

        public void Insert(PlaceableBlock block)
        {
            SetBlock(block);
        }

        public void ReplaceBlockInHand(PlaceableBlock block)
        {
            block.transform.position = new Vector2(block.transform.position.x, transform.position.y);

            if(!IsBlockInsideBounds(block))
                return;

            block.SetResetPosition(block.transform.position);
        }

        private void SetBlock(PlaceableBlock block)
        {
            //block.transform.parent = null;
            block.transform.position = transform.position;
            block.transform.position += block.BlockData.UnitLength * Vector3.right * (_currentWidth + _spacing);

            _currentWidth += block.BlockData.UnitLength + _spacing;
            //block.transform.SetParent(transform, true);
            block.SetResetPosition(block.transform.position);
        }

        private bool IsBlockInsideBounds(PlaceableBlock block)
        {
            Debug.Log(_collider.bounds);

            foreach (Vector2 point in block.Collider.points)
            {
                Vector2 pos = block.transform.TransformPoint(point);

                if (!_collider.bounds.Contains(pos))
                {
                    Debug.Log(pos);
                    return false;
                }
            }

            return true;
        }
    }
}
