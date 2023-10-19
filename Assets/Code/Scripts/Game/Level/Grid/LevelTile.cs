namespace ProjectPBR.Level.Grid
{
    using UnityEngine;
    using VUDK.Generic.Structures.Grid;    
    using ProjectPBR.Level.Blocks;

    public class LevelTile : GridTileBase/*, ICastGameManager<GameManager>*/
    {
        public BlockBase InsertedBlock { get; private set; }
        //public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        public bool IsOccupied => InsertedBlock/*Physics2D.OverlapBox(transform.position, transform.localScale.Sum(-.2f), 0f, GameManager.GameGridManager.BlocksLayerMask | MainManager.Ins.GameConfig.PlayerLayerMask)*/;

        public void InsertBlock(PlaceableBlock block)
        {
            block.transform.parent = null;
            block.transform.position = transform.position;
        }

        private void OnTriggerStay2D(Collider2D collision) // TO DO: Optimize this
        {
            if (IsOccupied) return;

            if (collision.TryGetComponent(out BlockBase block))
                InsertedBlock = block;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out BlockBase block))
                InsertedBlock = null;
        }

#if DEBUG
        private void OnDrawGizmos()
        {
            Gizmos.color = IsOccupied ? Color.red : Color.green;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
#endif
    }
}
