using System.Collections.Generic;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;

namespace Quoridor.View
{
    public class ViewCommunication : IView
    {
        public void ShowAvailableMoves(IEnumerable<Coordinates> cells)
        {
            throw new System.NotImplementedException();
        }
        public void ShowAvailableWalls(IEnumerable<Coordinates> walls)
        {
            throw new System.NotImplementedException();
        }

        public void MovePlayerToCell(PlayerType playerType, Coordinates cell)
        {
            throw new System.NotImplementedException();
        }
        public void PlaceWall(Player player, Coordinates wall)
        {
            throw new System.NotImplementedException();
        }
        
        public void EndGame(PlayerType winner)
        {
            throw new System.NotImplementedException();
        }
    }
}