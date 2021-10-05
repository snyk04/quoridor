using System.Collections.Generic;
using UnityEngine;

namespace Quoridor.Model.Players
{
    public class RandomBot : BaseBot
    {
        public override MoveType MakeMove(List<CellCoordinates> availableMoves)
        {
            if (Random.value <= 0.5f)
            {
                CellToMove = availableMoves[Random.Range(0, availableMoves.Count)];
                return MoveType.MoveToCell;
            }
            else
            {
                // if (walls >= 1)
                // AmountOfWalls -= 1;
                Debug.Log("Bot placed a wall!");
                return MoveType.PlaceWall;
            }
        }

        public RandomBot(CellCoordinates startPosition) : base(startPosition)
        {
            
        }
    }
}
