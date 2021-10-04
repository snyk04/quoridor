using System.Collections.Generic;
using Quoridor.Model;
using Quoridor.Model.Players;

namespace Quoridor.View
{
    public interface IView
    {
        void UnhighlightAllCells();
        void HighlightCells(IEnumerable<CellCoordinates> cellCoordinatesArray);
        void MovePawnToCell(PlayerType playerType, CellCoordinates cellCoordinates);

        void ShowVictory(PlayerType playerType);
    }
}
