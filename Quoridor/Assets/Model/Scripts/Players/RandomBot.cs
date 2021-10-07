using System.Collections.Generic;
using Quoridor.Model.Cells;
using UnityEngine;

namespace Quoridor.Model.Players
{
    public class RandomBot : Bot
    {
        public override MoveType MakeMove(List<CellCoordinates> availableMoves, List<CellCoordinates> wallsThatCanBePlaced)
        {
            if (Random.value > 0.5f & AmountOfWalls >= 1)
            {
                WallToPlace = wallsThatCanBePlaced[Random.Range(0, wallsThatCanBePlaced.Count)];
                return MoveType.PlaceWall;
            }

            CellToMove = availableMoves[Random.Range(0, availableMoves.Count)];
            return MoveType.MoveToCell;
        }

        public RandomBot(int startAmountOfWalls, CellCoordinates startPosition) : base(startAmountOfWalls, startPosition)
        {
            
        }
    }
}
