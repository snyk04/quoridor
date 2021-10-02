using System;
using System.Collections.Generic;
using Quoridor.View;

namespace Quoridor.Model
{
    public class ModelCommunication : IModel
    {
        #region Properties

        private const int AmountOfRows = 9;
        private const int AmountOfColumns = 9;

        private readonly CellCoordinates _whiteStartCoordinates = new CellCoordinates(8, 4);
        private readonly CellCoordinates _blackStartCoordinates = new CellCoordinates(0, 4);

        private readonly IView _view;

        private PawnType _currentTurnPawnType;
        private readonly Pawn _whitePawn;
        private readonly Pawn _blackPawn;

        private readonly Cell[,] _cells;

        #endregion

        public ModelCommunication(IView view)
        {
            _view = view;

            _cells = new Cell[AmountOfRows, AmountOfColumns];
            for (int i = 0; i < AmountOfRows; i++)
            {
                for (int j = 0; j < AmountOfColumns; j++)
                {
                    _cells[i, j] = new Cell();
                }
            }

            _whitePawn = new Pawn(_whiteStartCoordinates);
            _blackPawn = new Pawn(_blackStartCoordinates);
        }

        #region Methods

        public void StartGame()
        {
            MovePawnToCell(PawnType.Black, _blackStartCoordinates);
            MovePawnToCell(PawnType.White, _whiteStartCoordinates);
            
            _currentTurnPawnType = PawnType.White;
            ShowAvailableCellsForCurrentPawn();
        }
        
        public void ShowAvailableCellsForCurrentPawn()
        {
            Pawn currentTurnPawn = GetPawnByPawnType(_currentTurnPawnType);
            CellCoordinates currentCellPosition = currentTurnPawn.CurrentCellCoordinates;

            var cellCoordinatesArray = new List<CellCoordinates>();

            var lowerCell = new CellCoordinates(currentCellPosition.row + 1, currentCellPosition.column);
            if (CheckIfCellRealAndFree(lowerCell))
            {
                cellCoordinatesArray.Add(lowerCell);
            }
            
            var upperCell = new CellCoordinates(currentCellPosition.row - 1, currentCellPosition.column);
            if (CheckIfCellRealAndFree(upperCell))
            {
                cellCoordinatesArray.Add(upperCell);
            }

            var righterCell = new CellCoordinates(currentCellPosition.row, currentCellPosition.column + 1);
            if (CheckIfCellRealAndFree(righterCell))
            {
                cellCoordinatesArray.Add(righterCell);
            }

            var lefterCell = new CellCoordinates(currentCellPosition.row, currentCellPosition.column - 1);
            if (CheckIfCellRealAndFree(lefterCell))
            {
                cellCoordinatesArray.Add(lefterCell);
            }

            _view.HighlightCells(cellCoordinatesArray);
        }
        public void MovePawnToCell(PawnType pawnType, CellCoordinates cellCoordinates)
        {
            Pawn pawn = GetPawnByPawnType(pawnType);
            CellCoordinates oldCellCoordinates = pawn.CurrentCellCoordinates;
            
            Cell oldCell = _cells[oldCellCoordinates.row, oldCellCoordinates.column];
            Cell newCell = _cells[cellCoordinates.row, cellCoordinates.column];
            
            oldCell.MakeFree();
            newCell.MakeBusy();
            
            pawn.MoveToCell(cellCoordinates);
            _view.MovePawnToCell(pawnType, cellCoordinates);
        }

        public void MoveCurrentPawnToCell(CellCoordinates cellCoordinates)
        {
            MovePawnToCell(_currentTurnPawnType, cellCoordinates);
            ChangeCurrentTurnPawn();
            ShowAvailableCellsForCurrentPawn();
        }
       
        private void ChangeCurrentTurnPawn()
        {
            _currentTurnPawnType = _currentTurnPawnType switch
            {
                PawnType.White => PawnType.Black,
                PawnType.Black => PawnType.White,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        private Pawn GetPawnByPawnType(PawnType pawnType)
        {
            return pawnType switch
            {
                PawnType.White => _whitePawn,
                PawnType.Black => _blackPawn,
                _ => throw new ArgumentOutOfRangeException(nameof(pawnType), pawnType, null)
            };
        }
        private bool CheckIfCellRealAndFree(CellCoordinates cellCoordinates)
        {
            bool condition1 = cellCoordinates.row < AmountOfRows;
            bool condition2 = cellCoordinates.row >= 0;
            bool condition3 = cellCoordinates.column < AmountOfColumns;
            bool condition4 = cellCoordinates.column >= 0;

            bool isReal = condition1 & condition2 & condition3 & condition4;

            if (!isReal)
            {
                return false;
            }

            bool isFree = !_cells[cellCoordinates.row, cellCoordinates.column].IsBusy;
            return isFree;
        }

        #endregion
    }
}
