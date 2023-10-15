namespace ProjectPBR.Level.Grid
{
    using UnityEngine;
    using VUDK.Generic.Structures.Grid;    
    using ProjectPBR.Level.Blocks;

    public class LevelTile : GridTileBase
    {
        public bool IsOccupied { get; private set; }

        public void InsertBlock(PlaceableBlock block)
        {
            block.transform.parent = null;
            block.transform.position = transform.position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IsOccupied = true; // Because the tile will be occupied whatever if it's a block or not
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IsOccupied = false;
        }
    }
}
