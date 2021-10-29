using System;
using System.Collections.Generic;
using System.Linq;
using Quoridor.Controller;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;

namespace Quoridor.View
{
    public class ViewCommunication : IView
    {
        private readonly IController _controller;

        public ViewCommunication(IController controller)
        {
            _controller = controller;
        }
        
        public void ShowAvailableMoves(IEnumerable<Coordinates> cells)
        {
            _controller.AvailableMoves = cells.ToArray();
        }
        public void ShowAvailableWalls(IEnumerable<Coordinates> walls)
        {
            _controller.AvailableWalls = walls.ToArray();
        }

        public void MovePlayerToCell(PlayerType playerType, Coordinates cell)
        {
            Console.WriteLine($"<- {playerType.ToString().ToLower()} move {CellsConverter.NumberToMixed(cell)}");
        }
        public void PlaceWall(Player player, Coordinates wall)
        {
            Console.WriteLine($"<- {player.Type.ToString().ToLower()} placed wall {WallsConverter.NumberToMixed(wall)}");
        }
        
        public void EndGame(PlayerType winner)
        {
            Console.WriteLine($"<- {winner.ToString().ToLower()} won");
        }
    }
}
