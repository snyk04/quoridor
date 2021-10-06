using System.Collections.Generic;
using UnityEngine;

namespace Quoridor.Model.Players
{
    public class RandomBot : Bot
    {
        public override MoveType MakeMove(List<CellCoordinates> availableMoves)
        {
            if (Random.value > 10) // AmountOfWalls >= 1
            {
                Debug.Log("Bot placed a wall!");
                return MoveType.PlaceWall;
            }

            CellToMove = availableMoves[Random.Range(0, availableMoves.Count)];
            return MoveType.MoveToCell;
        }

        public RandomBot(CellCoordinates startPosition) : base(startPosition)
        {
            
        }
    }
}
