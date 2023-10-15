namespace ProjectPBR.Level.Grid
{
    using UnityEngine;
    using VUDK.Generic.Structures.Grid;    
    using ProjectPBR.Level.Blocks;

    public class LevelTile : GridTileBase
    {
        public PlaceableBlock Block { get; private set; }
        public bool IsOccupied { get; private set; }

        public void InsertBlock(PlaceableBlock block)
        {
            block.transform.parent = null;
            block.transform.position = transform.position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IsOccupied = true; // Because the tile will be occupied whatever if it's a placeable block or not

            if (other.TryGetComponent(out PlaceableBlock block))
                Block = block;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IsOccupied = false;
            if (other.TryGetComponent(out PlaceableBlock block))
                Block = null;
        }
    }
}
