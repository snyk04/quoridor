using System.Collections.Generic;
using Quoridor.Model;

namespace Quoridor.View
{
    public interface IView
    {
        void HighlightCells(IEnumerable<CellCoordinates> cellCoordinatesArray);
        void MovePawnToCell(PawnType pawnType, CellCoordinates cellCoordinates);
    }
}
