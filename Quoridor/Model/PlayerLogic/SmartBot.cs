using System.Collections.Generic;
using Quoridor.Model.Cells;
using Quoridor.Model.Common;

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

        private void CalculateMove(IList<Coordinates> cells, IList<Coordinates> jumps, IList<Coordinates> walls,
            out MoveType moveType, out Coordinates coordinates)
        {
            moveType = MoveType.MoveToCell;
            coordinates = cells[new Random().Next(cells.Count)];
            
            for (int i = 0; i < CellsManager.AmountOfColumns; i++)
            {
                List<Coordinates> way = _model.FieldPathFinder.FindPath(Position, new Coordinates(VictoryRow, i));
                if (way == null)
                {
                    continue;
                }
                
                moveType = MoveType.MoveToCell;
                coordinates = way[1];
                return;
            }
        }
    }
}
