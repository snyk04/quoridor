using Quoridor.Model.Cells;

namespace Quoridor.View.Cells
{
    public sealed class CellStorage : Storage<CellVisual>
    {
        protected override int AmountOfColumns => CellsManager.AmountOfColumns;
    }
}
