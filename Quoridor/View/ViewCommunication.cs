using System;
using System.Collections.Generic;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;

namespace Quoridor.View
{
    public class ViewCommunication : IView
    {
        public void ShowAvailableMoves(IEnumerable<Coordinates> cells)
        {
            // TODO : send to controller
        }
        public void ShowAvailableWalls(IEnumerable<Coordinates> walls)
        {
            // TODO : send to controller
        }

        public void MovePlayerToCell(PlayerType playerType, Coordinates cell)
        {
            Console.WriteLine($"<- {playerType.ToString().ToLower()} move {CellsConverter.NumberToMixed(cell)}");
        }
        public void PlaceWall(Player player, Coordinates wall)
        {
            Console.WriteLine($"<- {player.Type.ToString().ToLower()} move {WallsConverter.NumberToMixed(wall)}");
        }
        
        public void EndGame(PlayerType winner)
        {
            Console.WriteLine($"<- {winner.ToString().ToLower()} won");
        }
    }
}
