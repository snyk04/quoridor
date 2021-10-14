using System.Collections.Generic;
using Quoridor.Model.Common;

namespace Quoridor.Model
{
    public class PossibleMoves
    {
        private readonly ModelCommunication _model;
        
        private Coordinates _currentTurnPlayerCoordinates;

        public PossibleMoves(ModelCommunication model)
        {
            _model = model;
        }

        public Coordinates[] AvailableCells(Coordinates playerCoordinates)
        {
            // TODO
            _currentTurnPlayerCoordinates = playerCoordinates;
            return GetAvailableCellsFromCell(playerCoordinates);
        }
        public Coordinates[] GetAvailableCellsFromCell(Coordinates cellCoordinates)
        {
            var uncheckedCells = new Coordinates[]
            {
                new Coordinates(cellCoordinates.row + 1, cellCoordinates.column),
                new Coordinates(cellCoordinates.row - 1, cellCoordinates.column),
                new Coordinates(cellCoordinates.row, cellCoordinates.column + 1),
                new Coordinates(cellCoordinates.row, cellCoordinates.column - 1)
            };
            
            var possibleMoves = new List<Coordinates>();
            foreach (Coordinates uncheckedCell in uncheckedCells)
            {
                TryToAddCellToAvailableCells(cellCoordinates, uncheckedCell, possibleMoves);
            }

            return possibleMoves.ToArray();
        }
        private void TryToAddCellToAvailableCells(Coordinates moveFrom, Coordinates moveTo, List<Coordinates> possibleMoves)
        {
            if (!_model.CellsManager.CellIsReal(moveTo))
            {
                return;
            }

            // if (_model.CellsManager.WallIsBetweenCells(moveFrom, moveTo))
            // {
            //     return;
            // }
            
            if (_model.CellsManager.CellIsBusy(moveTo))
            {
                if (!_currentTurnPlayerCoordinates.Equals(moveTo))
                {
                    possibleMoves.AddRange(GetAvailableCellsFromCell(moveTo));
                }
            }
            else
            {
                possibleMoves.Add(moveTo);
            }
        }
        
        
        public Coordinates[] AvailableWalls()
        {
            // TODO
            Coordinates[] array = new Coordinates[] {new Coordinates(0, 1)};

            return array;
        }
    }
}
