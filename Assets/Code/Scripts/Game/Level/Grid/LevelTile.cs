namespace ProjectPBR.Level.Grid
{
    using UnityEngine;
    using VUDK.Generic.Structures.Grid;    
    using ProjectPBR.Level.Blocks;

    public class LevelTile : GridTileBase
    {
        public BlockBase Block { get; private set; }
        public Vector2 LeftVertex => new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f);
        public bool IsOccupied => Block;

        private Collider2D _coll;

        private void Awake()
        {
            TryGetComponent(out _coll);
        }

        public void InsertBlock(PlaceableBlock block)
        {
            block.transform.parent = null;
            block.transform.position = transform.position;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (IsOccupied) return;

            if (collision.TryGetComponent(out BlockBase block))
            {
                if (block is SinglePlaceableBlock || block is StaticBlock)
                    Block = block;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // No needs to check for static blocks because they cannot be moved
            Block = null;
        }

#if DEBUG
        private void OnDrawGizmos()
        {
            Gizmos.color = IsOccupied ? new Color(1f, 0, 0, 0.35f) : new Color(0, 1f, 0, 0.35f);
            Gizmos.DrawCube(transform.position, transform.localScale);
            Gizmos.DrawWireSphere(LeftVertex, 0.1f);
        }
#endif
    }
}
