using System.Threading;
using Quoridor.NewModel.Common;

namespace Quoridor.NewModel.PlayerLogic
{
    public sealed class RandomBot : Bot
    {
        public RandomBot(ModelCommunication model, PlayerType playerType, Coordinates startPosition, int startAmountOfWalls)
            : base(model, playerType, startPosition, startAmountOfWalls)
        {
        }

        public override void SetAvailableMoves(Coordinates[] cells, Coordinates[] walls)
        {
            MoveType moveType;
            Coordinates coordinates;
            
            Random random = new Random();
            if (AmountOfWalls >= 1 && random.Value >= 0.5)
            {
                moveType = MoveType.PlaceWall;
                coordinates = walls[random.Next(walls.Length)];
            }
            else
            {
                moveType = MoveType.Move;
                coordinates = cells[random.Next(cells.Length)];
            }
            
            MakeMove(moveType, coordinates);
        }
    }
}
