namespace ProjectPBR.Level.Grid
{
    using UnityEngine;
    using VUDK.Extensions.Vectors;
    using VUDK.Generic.Structures.Grid;

    public class LevelGrid : Grid<LevelTile>
    {
        private void Start()
        {
            GenerateGrid();
        }

#if DEBUG
        private void OnDrawGizmos()
        {
            Vector3 pivotPosition = transform.position + new Vector3(Size.x / 2 -.5f, Size.y / 2, 0);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(pivotPosition, new Vector3(Size.x, Size.y, 0));
        }
#endif
    }
}
