using System;
using System.Collections.Generic;
using System.Diagnostics;
using Quoridor.Model.Cells;
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

        private void CalculateMove(IList<Coordinates> cells, IEnumerable<Coordinates> jumps, IReadOnlyList<Coordinates> walls,
            out MoveType moveType, out Coordinates coordinates)
        {
            Coordinates enemyPosition = _model.PlayerController.CurrentPlayerOpponentPosition;
            int enemyVictoryRow = _model.PlayerController.CurrentPlayerOpponentVictoryRow;

            List<Coordinates> playerShortestWay =
                _model.FieldPathFinder.FindShortestPathToRow(Position, VictoryRow, jumps, enemyPosition);
            List<Coordinates> enemyShortestWay =
                _model.FieldPathFinder.FindShortestPathToRow(enemyPosition, enemyVictoryRow, _model.PossibleMoves.AvailableJumps(enemyPosition), Position);

            if (playerShortestWay.Count + 3 > enemyShortestWay.Count && AmountOfWalls >= 1 && enemyShortestWay.Count <= 7)
            {
                moveType = MoveType.PlaceWall;
                coordinates = walls[new Random().Next(walls.Count)];

                for (int i = 0; i < enemyShortestWay.Count; i++)
                {
                    foreach (Coordinates wall in walls)
                    {
                        foreach (CellPair cellPair in _model.WallsManager[wall].BlockedCellPairs)
                        {
                            if (cellPair.Contains(enemyShortestWay[i]) && cellPair.Contains(enemyShortestWay[i + 1]))
                            {
                                coordinates = wall;
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                List<Coordinates> way = _model.FieldPathFinder.FindShortestPathToRow(Position, VictoryRow, jumps, enemyPosition);
                if (way == null)
                {
                    throw new Exception();
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
}
