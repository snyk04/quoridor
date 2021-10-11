using System.Collections.Generic;
using Quoridor.Model.Cells;
using Quoridor.Model.Players;

namespace Quoridor.View
{
    public interface IView
    {
        void HighlightCells(IEnumerable<Coordinates> cellCoordinatesArray);
        void MovePlayerToCell(PlayerType playerType, Coordinates cellCoordinates);
        void PlaceWall(Coordinates wallCoordinates, IEnumerable<Coordinates> overlappedWalls);

        void EndGame(PlayerType winner);
    }
}
