namespace ProjectPBR.Level.Grid
{
    using UnityEngine;
    using VUDK.Generic.Structures.Grid;    
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Level.Blocks.ComplexBlock;

    public class LevelTile : GridTileBase
    {
        public BlockBase InsertedBlock { get; private set; }
        public Vector2 LeftVertex => new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f);
        public bool IsOccupied => InsertedBlock;

        public void InsertBlock(PlaceableBlock block)
        {
            block.transform.parent = null;
            block.transform.position = transform.position;
        }

        private void OnTriggerStay2D(Collider2D collision) // TO DO: Optimize this
        {
            if (IsOccupied) return;

            if (collision.TryGetComponent(out BlockBase block))
            {
                if(block is SinglePlaceableBlock || block is StaticBlock)
                    InsertedBlock = block;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // No needs to check for static blocks because they cannot be moved
            if (collision.TryGetComponent(out SinglePlaceableBlock block)) 
                InsertedBlock = null;
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
