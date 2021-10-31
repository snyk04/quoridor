using System.Collections.Generic;
using Quoridor.Model.Common;

namespace Quoridor.Model.PlayerLogic
{
    public sealed class RandomBot : Bot
    {
        public RandomBot(ModelCommunication model, PlayerColor playerColor, PlayerType playerType, Coordinates startPosition, int startAmountOfWalls, int victoryRow)
            : base(model, playerColor, playerType, startPosition, startAmountOfWalls, victoryRow)
        {
        }

        public override void SetPossibleMoves(Coordinates[] cells, Coordinates[] jumps, Coordinates[] walls)
        {
            CalculateMove(cells, jumps, walls, out MoveType moveType, out Coordinates coordinates);
            MakeMove(moveType, coordinates);
        }

        private void CalculateMove(IList<Coordinates> cells, IList<Coordinates> jumps, IList<Coordinates> walls,
            out MoveType moveType, out Coordinates coordinates)
        {
            Random random = new Random();
            if (AmountOfWalls >= 1 && random.Value >= 0.5)
            {
                moveType = MoveType.PlaceWall;
                coordinates = walls[random.Next(walls.Count)];
                return;
            }

            // TODO : not sure if random.Value is different with every time
            if (jumps.Count > 0 && random.Value <= 0.5)
            {
                moveType = MoveType.JumpToCell;
                coordinates = jumps[random.Next(jumps.Count)];
                return;
            }
            
            moveType = MoveType.MoveToCell; 
            coordinates = cells[random.Next(cells.Count)];
        }
    }
}
