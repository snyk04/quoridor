using Quoridor.OldModel.Cells;

namespace Quoridor.View.Cells
{
    public class CellStorage : Storage<CellVisual>
    {
        protected override int AmountOfColumns => CellsManager.AmountOfColumns;
    }
}
