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

        private void CalculateMove(IList<Coordinates> cells, IEnumerable<Coordinates> jumps, IReadOnlyList<Coordinates> walls,
            out MoveType moveType, out Coordinates coordinates)
        {
            Coordinates enemyPosition = _model.PlayerController.CurrentPlayerOpponentPosition;
            int enemyVictoryRow = _model.PlayerController.CurrentPlayerOpponentVictoryRow;

            List<Coordinates> playerShortestWay =
                _model.FieldPathFinder.FindShortestPathToRow(Position, VictoryRow, jumps, enemyPosition);
            List<Coordinates> enemyShortestWay =
                _model.FieldPathFinder.FindShortestPathToRow(enemyPosition, enemyVictoryRow, _model.PossibleMoves.AvailableJumps(enemyPosition), Position);

            // Console.WriteLine(playerShortestWay.Count + " " + enemyShortestWay.Count);
            if (playerShortestWay.Count > enemyShortestWay.Count && AmountOfWalls >= 1)
            {
                int maxDelta = enemyShortestWay.Count;
                coordinates = walls[new Random().Next(walls.Count)];
                foreach (Coordinates wall in walls)
                {
                    _model.WallsManager.PlaceTemporaryWall(wall);
                    
                    enemyShortestWay =
                        _model.FieldPathFinder.FindShortestPathToRow(enemyPosition, enemyVictoryRow, new List<Coordinates>(), Position);

                    int delta = enemyShortestWay.Count;
                    if (delta > maxDelta)
                    {
                        // Console.WriteLine(delta);
                        maxDelta = delta;
                        coordinates = wall;
                    }
                    
                    _model.WallsManager.DestroyTemporaryWall(wall);
                }
                
                moveType = MoveType.PlaceWall;
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
