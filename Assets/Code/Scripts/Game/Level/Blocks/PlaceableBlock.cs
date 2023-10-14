namespace ProjectPBR.Level.Blocks
{
    using UnityEngine;

    public class PlaceableBlock : BlockBase
    {
        public Vector2 ResetPosition { get; private set; }

        public void SetResetPosition(Vector2 resetPosition)
        {
            ResetPosition = resetPosition;
        }
    }
}
