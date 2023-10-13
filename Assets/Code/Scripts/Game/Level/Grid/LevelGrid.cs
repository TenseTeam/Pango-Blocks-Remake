namespace ProjectPBR.Level.Grid
{
    using VUDK.Generic.Structures.Grid;

    public class LevelGrid : Grid<LevelTile>
    {
        private void Start()
        {
            GenerateGrid();
        }
    }
}
