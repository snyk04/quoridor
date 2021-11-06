using System;
using System.Collections.Generic;
using System.Linq;
using Quoridor.Controller;
using Quoridor.IO;
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
            _controller.AvailableCells = cells.ToArray();
        }
        public void ShowAvailableJumps(IEnumerable<Coordinates> jumps)
        {
            _controller.AvailableJumps = jumps.ToArray();
        }
        public void ShowAvailableWalls(IEnumerable<Coordinates> walls)
        {
            _controller.AvailableWalls = walls.ToArray();
        }
        
        public void MovePlayerToCell(Player player, Coordinates cell)
        {
            if (player.Type != PlayerType.SmartBot)
            {
                return;
            }
            
            Console.WriteLine($"move {CellsConverter.NumberToMixed(cell)}");
        }
        public void JumpPlayerToCell(Player player, Coordinates cell)
        {
            if (player.Type is not (PlayerType.SmartBot or PlayerType.RandomBot))
            {
                return;
            }
            
            Console.WriteLine($"jump {CellsConverter.NumberToMixed(cell)}");
        }
        public void PlaceWall(Player player, Coordinates wall)
        {
            if (player.Type is not (PlayerType.SmartBot or PlayerType.RandomBot))
            {
                return;
            }
            
            Console.WriteLine($"wall {WallsConverter.NumberToMixed(wall)}");
        }
        
        public void EndGame(PlayerColor winner)
        {

        }
    }
}
