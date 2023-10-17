namespace ProjectPBR.Level.Grid
{
    using UnityEngine;
    using VUDK.Generic.Structures.Grid;    
    using VUDK.Extensions.Vectors;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Managers;
    using ProjectPBR.Level.Blocks;

    public class LevelTile : GridTileBase, ICastGameManager<GameManager>
    {
        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        public bool IsOccupied => Physics2D.OverlapBox(transform.position, transform.localScale.Sum(-.2f), 0f, GameManager.GridBlocksManager.BlocksLayerMask);

        public void InsertBlock(PlaceableBlock block)
        {
            block.transform.parent = null;
            block.transform.position = transform.position;
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
