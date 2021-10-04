using System.Collections.Generic;
using Quoridor.Model;

namespace Quoridor.View
{
    public interface IView
    {
        void UnhighlightAllCells();
        void HighlightCells(IEnumerable<CellCoordinates> cellCoordinatesArray);
        void MovePawnToCell(PawnType pawnType, CellCoordinates cellCoordinates);

        void ShowVictory(PawnType pawnType);
    }
}
