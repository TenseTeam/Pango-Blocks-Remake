namespace ProjectPBR.Level.Blocks
{
    using UnityEngine;

    public class PlaceableBlock : BlockBase
    {
        private Vector2 _resetPosition;

        public void SetResetPosition()
        {
            _resetPosition = transform.position;
        }

        public void ResetPosition()
        {
            transform.position = _resetPosition;
        }

        public void EnableCollider()
        {
            Collider.enabled = true;
        }

        public void DisableCollider()
        {
            Collider.enabled = false;
        }
    }
}
