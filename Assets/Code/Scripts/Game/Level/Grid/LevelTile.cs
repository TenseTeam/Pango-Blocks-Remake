namespace ProjectPBR.Level.Grid
{
    using UnityEngine;
    using VUDK.Generic.Structures.Grid;    
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Player.Objective.Goal;

    public class LevelTile : GridTileBase
    {
        public BlockBase Block { get; private set; }
        public bool IsOccupiedByObjective { get; private set; }
        public Vector2 LeftVertex => new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f);
        public bool IsOccupiedByBlock => Block;
        public bool IsOccupied => IsOccupiedByBlock || IsOccupiedByObjective;

        public void Insert(PlaceableBlock block)
        {
            block.transform.parent = null;
            block.transform.rotation = Quaternion.identity;
            block.transform.position = transform.position;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (IsOccupiedByBlock) return;

            if (collision.TryGetComponent(out BlockBase block))
            {
                if (block is ComplexPlaceableBlock) return;

                Block = block;
                return;
            }

            if (collision.TryGetComponent(out ObjectiveGoal goal))
                IsOccupiedByObjective = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // No needs to check for static blocks because they cannot be moved
            Block = null;
            IsOccupiedByObjective = false;
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
