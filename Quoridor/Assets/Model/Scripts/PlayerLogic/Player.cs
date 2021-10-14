using System;
using Quoridor.Model.Common;
using UnityEngine;

namespace Quoridor.Model.PlayerLogic
{
    public class Player
    {
        private readonly ModelCommunication _model;
        
        public PlayerType Type { get; }
        
        public int AmountOfWalls { get; private set; }
        public Coordinates Position { get; private set; }

        public event Action MovePerformed;
        
        public Player(ModelCommunication model, PlayerType type, Coordinates startPosition, int startAmountOfWalls)
        {
            _model = model;
            Type = type;
            AmountOfWalls = startAmountOfWalls;
            MoveTo(startPosition);
        }

        public virtual void SetAvailableMoves(Coordinates[] cells, Coordinates[] walls)
        {
            _model.ShowAvailableMoves(cells);
            if (AmountOfWalls >= 1)
            {
                Debug.Log(AmountOfWalls);
                _model.ShowAvailableWalls(walls);
            }
        }
        public void MakeMove(MoveType moveType, Coordinates coordinates)
        {
            switch (moveType)
            {
                case MoveType.Move:
                    MoveTo(coordinates);
                    break;
                case MoveType.PlaceWall:
                    PlaceWall(coordinates);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(moveType), moveType, null);
            }

            OnMovePerformed();
        }

        private void MoveTo(Coordinates coordinates)
        {
            Position = coordinates;
            _model.MovePlayer(Type, coordinates);
        }
        private void PlaceWall(Coordinates coordinates)
        {
            AmountOfWalls -= 1;
            _model.PlaceWall(this, coordinates);
        }

        protected void OnMovePerformed()
        {
            MovePerformed?.Invoke();
        }
    }
}
