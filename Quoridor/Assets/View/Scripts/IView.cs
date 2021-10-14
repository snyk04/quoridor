using System.Collections.Generic;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;

namespace Quoridor.View
{
    public interface IView
    {
        void ShowAvailableMoves(IEnumerable<Coordinates> cells);
        void ShowAvailableWalls(IEnumerable<Coordinates> walls);
        void MovePlayerToCell(PlayerType playerType, Coordinates cell);
        void PlaceWall(Player player, Coordinates wall);

        void EndGame(PlayerType winner);
    }
}
