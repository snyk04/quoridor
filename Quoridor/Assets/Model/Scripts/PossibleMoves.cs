using System.Collections.Generic;
using Quoridor.Model.Cells;

namespace Quoridor.Model
{
    public class PossibleMoves
    {
        private readonly ModelCommunication _model;

        private Coordinates _currentTurnPlayerCoordinates;
        
        public PossibleMoves(ModelCommunication model)
        {
            _model = model;

            _model.PlayersController.OnPlayerChange += SetCurrentTurnPlayerCoordinates;
            _model.PlayersController.OnPlayerChange += ShowPossibleMoves;
        }

        public void SetCurrentTurnPlayerCoordinates(Coordinates cellCoordinates)
        {
            _currentTurnPlayerCoordinates = cellCoordinates;
        }
        
        private void TryToAddCellToPossibleMoves(Coordinates cell, List<Coordinates> possibleMoves)
        {
            if (!_model.CellsManager.CellIsReal(cell))
            {
                return;
            }

            if (_model.CellsManager.WallIsBetweenCells(_currentTurnPlayerCoordinates, cell))
            {
                return;
            }

            if (_model.CellsManager.CellIsBusy(cell))
            {
                if (!_currentTurnPlayerCoordinates.Equals(cell))
                {
                    possibleMoves.AddRange(GetPossibleMovesFromCell(cell));
                }
            }
            else
            {
                possibleMoves.Add(cell);
            }
        }
        public List<Coordinates> GetPossibleMovesFromCell(Coordinates cellCoordinates)
        {
            var uncheckedCells = new List<Coordinates>()
            {
                new Coordinates(cellCoordinates.row + 1, cellCoordinates.column),
                new Coordinates(cellCoordinates.row - 1, cellCoordinates.column),
                new Coordinates(cellCoordinates.row, cellCoordinates.column + 1),
                new Coordinates(cellCoordinates.row, cellCoordinates.column - 1)
            };
            
            var possibleMoves = new List<Coordinates>();
            foreach (Coordinates uncheckedCell in uncheckedCells)
            {
                TryToAddCellToPossibleMoves(uncheckedCell, possibleMoves);
            }

            return possibleMoves;
        }
        private void ShowPossibleMoves(Coordinates cellCoordinates)
        {
            IEnumerable<Coordinates> availableMoves = GetPossibleMovesFromCell(cellCoordinates);
            _model.HighlightCells(availableMoves);
        }
    }
}
