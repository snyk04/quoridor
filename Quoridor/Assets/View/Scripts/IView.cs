using System.Collections.Generic;
using Quoridor.Model.Cells;
using Quoridor.Model.Players;

namespace Quoridor.View
{
    public interface IView
    {
        void UnhighlightAllCells();
        void HighlightCells(IEnumerable<CellCoordinates> cellCoordinatesArray);
        void MovePlayerToCell(PlayerType playerType, CellCoordinates cellCoordinates);

        void ShowVictory(PlayerType playerType);
    }
}
