using System;
using Quoridor.Model.Common;

namespace Quoridor.Model.PlayerLogic
{
    public class Player
    {
        private readonly ModelCommunication _model;
        
        public PlayerType Type { get; }
        public int VictoryRow { get; }
        
        public int AmountOfWalls { get; private set; }
        public Coordinates Position { get; private set; }

        public event Action MovePerformed;
        
        public Player(ModelCommunication model, PlayerType type, Coordinates startPosition, int startAmountOfWalls, int victoryRow)
        {
            _model = model;
            
            Type = type;
            VictoryRow = victoryRow;
            
            AmountOfWalls = startAmountOfWalls;
            MoveTo(startPosition);
        }

        public virtual void SetAvailableMoves(Coordinates[] cells, Coordinates[] walls)
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
                // TODO
                case MoveType.Move:
                    _model.MovePlayer(this, coordinates);
                    break;
                case MoveType.PlaceWall:
                    _model.PlaceWall(this, coordinates);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(moveType), moveType, null);
            }

            OnMovePerformed();
        }

        public void MoveTo(Coordinates coordinates)
        {
            Position = coordinates;
        }
        public void PlaceWall()
        {
            AmountOfWalls -= 1;
        }

        protected void OnMovePerformed()
        {
            MovePerformed?.Invoke();
        }
    }
}
