using System;
using System.Collections.Generic;
using Quoridor.Model.Common;
using Random = Quoridor.Model.Common.Random;

namespace Quoridor.Model.PlayerLogic
{
    public class SmartBot : Bot
    {
        public SmartBot(ModelCommunication model, PlayerColor playerColor, PlayerType playerType, Coordinates startPosition, int startAmountOfWalls, int victoryRow) : base(model, playerColor, playerType, startPosition, startAmountOfWalls, victoryRow)
        {
        }
        
        public override void SetPossibleMoves(Coordinates[] cells, Coordinates[] jumps, Coordinates[] walls)
        {
            CalculateMove(cells, jumps, walls, out MoveType moveType, out Coordinates coordinates);
            MakeMove(moveType, coordinates);
        }

        private void CalculateMove(IList<Coordinates> cells, IEnumerable<Coordinates> jumps, IList<Coordinates> walls,
            out MoveType moveType, out Coordinates coordinates)
        {
            moveType = MoveType.MoveToCell;
            coordinates = cells[new Random().Next(cells.Count)];
            
            List<Coordinates> way = _model.FieldPathFinder.FindShortestPathToRow(Position, VictoryRow, jumps, _model.PlayerController.CurrentPlayerOpponentPosition);
            if (way == null)
            {
                return;
            }
                
            coordinates = way[1];

            moveType = (Position - coordinates).VectorLength() switch
            {
                1 => MoveType.MoveToCell,
                > 1 => MoveType.JumpToCell,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
