using Quoridor.Model.Cells;

namespace Quoridor.View.Walls
{
    public class WallStorage : Storage<WallVisual>
    {
        protected override int AmountOfColumns => CellsManager.WallsAmountOfColumns;
    }
}
