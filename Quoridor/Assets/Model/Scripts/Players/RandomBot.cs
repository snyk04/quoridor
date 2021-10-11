using System.Collections.Generic;
using Quoridor.Model.Cells;
using UnityEngine;

namespace Quoridor.Model.Players
{
    public class RandomBot : Bot
    {
        public RandomBot(int startAmountOfWalls, Coordinates startPosition) : base(startAmountOfWalls, startPosition)
        {
            
        }
        
        public override MoveType MakeMove(List<Coordinates> availableMoves, List<Coordinates> wallsThatCanBePlaced)
        {
            if (Random.value > 0.5f && AmountOfWalls >= 1)
            {
                WallToPlace = wallsThatCanBePlaced[Random.Range(0, wallsThatCanBePlaced.Count)];
                return MoveType.PlaceWall;
            }

            CellToMove = availableMoves[Random.Range(0, availableMoves.Count)];
            return MoveType.MoveToCell;
        }
    }
}
