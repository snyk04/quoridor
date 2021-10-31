using System;
using Quoridor.Model.Common;

namespace Quoridor.Model.PlayerLogic
{
    public class Player
    {
        private readonly ModelCommunication _model;
        
        public PlayerColor Color { get; }
        public PlayerType Type { get; }
        
        public int VictoryRow { get; }
        
        public int AmountOfWalls { get; private set; }
        public Coordinates Position { get; private set; }

        public event Action MovePerformed;
        
        public Player(ModelCommunication model, PlayerColor color, PlayerType type, Coordinates startPosition, int startAmountOfWalls, int victoryRow)
        {
            _model = model;
            
            Color = color;
            Type = type;
            
            VictoryRow = victoryRow;
            
            AmountOfWalls = startAmountOfWalls;
            MoveTo(startPosition);
        }

        public virtual void SetPossibleMoves(Coordinates[] cells, Coordinates[] walls)
        {
            _model.ShowAvailableMoves(cells);
            if (AmountOfWalls >= 1)
            {
                _model.ShowAvailableWalls(walls);
            }
        }
        public void MakeMove(MoveType moveType, Coordinates coordinates)
        {
            switch (moveType)
            {
                case MoveType.MoveToCell:
                    _model.MovePlayer(this, coordinates);
                    break;
                case MoveType.PlaceWall:
                    _model.PlaceWall(this, coordinates);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(moveType), moveType, null);
            }

            MovePerformed?.Invoke();
        }

        public void MoveTo(Coordinates coordinates)
        {
            Position = coordinates;
        }
        public void PlaceWall()
        {
            AmountOfWalls -= 1;
        }
    }
}
