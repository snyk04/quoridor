using System.Collections.Generic;

namespace Quoridor.Model.New
{
    public class PossibleMoves
    {
        private readonly NewModel _model;

        private CellCoordinates _currentTurnPlayerCoordinates;
        
        public PossibleMoves(NewModel model)
        {
            _model = model;

            _model.PlayersController.OnPlayerChange += SetCurrentTurnPlayerCoordinates;
            _model.PlayersController.OnPlayerChange += ShowPossibleMoves;
        }

        public void SetCurrentTurnPlayerCoordinates(CellCoordinates cellCoordinates)
        {
            _currentTurnPlayerCoordinates = cellCoordinates;
        }
        
        private void TryToAddCellToPossibleMoves(CellCoordinates cell, List<CellCoordinates> possibleMoves)
        {
            if (!_model.CellsManager.CheckIfCellIsReal(cell))
            {
                return;
            }

            if (_model.CellsManager.CheckIfCellIsBusy(cell))
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
        public List<CellCoordinates> GetPossibleMovesFromCell(CellCoordinates cellCoordinates)
        {
            var uncheckedCells = new List<CellCoordinates>()
            {
                new CellCoordinates(cellCoordinates.row + 1, cellCoordinates.column),
                new CellCoordinates(cellCoordinates.row - 1, cellCoordinates.column),
                new CellCoordinates(cellCoordinates.row, cellCoordinates.column + 1),
                new CellCoordinates(cellCoordinates.row, cellCoordinates.column - 1)
            };
            
            var possibleMoves = new List<CellCoordinates>();
            foreach (CellCoordinates uncheckedCell in uncheckedCells)
            {
                TryToAddCellToPossibleMoves(uncheckedCell, possibleMoves);
            }

            return possibleMoves;
        }
        private void ShowPossibleMoves(CellCoordinates cellCoordinates)
        {
            IEnumerable<CellCoordinates> availableMoves = GetPossibleMovesFromCell(cellCoordinates);
            _model.HighlightAvailableCells(availableMoves);
        }
    }
}
