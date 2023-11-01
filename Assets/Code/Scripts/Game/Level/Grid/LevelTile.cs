namespace ProjectPBR.Level.Grid
{
    using UnityEngine;
    using VUDK.Generic.Structures.Grid;    
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Level.Blocks;
    using ProjectPBR.Player.Objective.Interfaces;
    using ProjectPBR.Managers.Main.GameStateMachine.States.Keys;

    public class LevelTile : GridTileBase
    {
        public BlockBase Block { get; private set; }
        public bool IsOccupiedByObjective { get; private set; }
        public Vector2 LeftVertex => new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f);
        public bool IsOccupiedByBlock => Block;
        public bool IsOccupied => IsOccupiedByBlock || IsOccupiedByObjective;

        public bool IsPlacementPhase => MainManager.Ins.GameStateMachine.IsState(GamePhaseKeys.PlacementPhase);

        public void Insert(PlaceableBlock block)
        {
            block.transform.parent = null;
            block.transform.rotation = Quaternion.identity;
            block.transform.position = transform.position;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!IsPlacementPhase) return;
            if (IsOccupiedByBlock) return;

            if (collision.TryGetComponent(out BlockBase block))
            {
                if (block is ComplexPlaceableBlock) return;
                if (block is PlaceableBlock && (block as PlaceableBlock).IsResettingPosition) return;

                Block = block;
                return;
            }

            if (collision.TryGetComponent(out IGoal _))
                IsOccupiedByObjective = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // Checks the Physics2D Matrix to see with which layer the collision is happening
            if (!IsPlacementPhase) return;
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
