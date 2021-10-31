using System.Collections.Generic;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;

namespace Quoridor.View
{
    public interface IView
    {
        void ShowAvailableMoves(IEnumerable<Coordinates> cells);
        void ShowAvailableWalls(IEnumerable<Coordinates> walls);
        void ShowAvailableJumps(IEnumerable<Coordinates> jumps);
        
        void MovePlayerToCell(Player player, Coordinates cell);
        void JumpPlayerToCell(Player player, Coordinates cell);
        void PlaceWall(Player player, Coordinates wall);

        void EndGame(PlayerColor winner);
    }
}
