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
            ShowAvailableMovesForCurrentPawn();
        }

        private void TryToAddMoveToArray(CellCoordinates cell, List<CellCoordinates> availableMoves)
        {
            if (!CheckIfCellIsReal(cell))
            {
                return;
            }
            
            if (CheckIfCellIsBusy(cell))
            {
                CellCoordinates currentPawnCoordinates = GetPawnByPawnType(_currentTurnPawnType).CurrentCellCoordinates;
                if (!currentPawnCoordinates.Equals(cell))
                {
                    availableMoves.AddRange(GetAvailableMovesFromCell(cell));
                }
            }
            else
            {
                availableMoves.Add(cell);
            }
        }
        private IEnumerable<CellCoordinates> GetAvailableMovesFromCell(CellCoordinates cellCoordinates)
        {
            var cellCoordinatesArray = new List<CellCoordinates>();

            var lowerCell = new CellCoordinates(cellCoordinates.row + 1, cellCoordinates.column);
            var upperCell = new CellCoordinates(cellCoordinates.row - 1, cellCoordinates.column);
            var righterCell = new CellCoordinates(cellCoordinates.row, cellCoordinates.column + 1);
            var lefterCell = new CellCoordinates(cellCoordinates.row, cellCoordinates.column - 1);
            
            TryToAddMoveToArray(lowerCell, cellCoordinatesArray);
            TryToAddMoveToArray(upperCell, cellCoordinatesArray);
            TryToAddMoveToArray(righterCell, cellCoordinatesArray);
            TryToAddMoveToArray(lefterCell, cellCoordinatesArray);
            
            return cellCoordinatesArray;
        }
        private void ShowAvailableMovesForCurrentPawn()
        {
            Pawn currentTurnPawn = GetPawnByPawnType(_currentTurnPawnType);
            CellCoordinates currentCellPosition = currentTurnPawn.CurrentCellCoordinates;

            IEnumerable<CellCoordinates> availableMoves = GetAvailableMovesFromCell(currentCellPosition);
            _view.HighlightCells(availableMoves);
        }
        
        private void MovePawnToCell(PawnType pawnType, CellCoordinates cellCoordinates)
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

            if (CheckIfCurrentPawnWon())
            {
                _view.UnhighlightAllCells();
                _view.ShowVictory(_currentTurnPawnType);
            }
            else
            {
                ChangeCurrentTurnPawn();
                ShowAvailableMovesForCurrentPawn();
            }
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

        private bool CheckIfCurrentPawnWon()
        {
            // TODO : hardcode?
            
            int currentPawnRow = GetPawnByPawnType(_currentTurnPawnType).CurrentCellCoordinates.row;
            switch (_currentTurnPawnType)
            {
                case PawnType.White:
                    if (currentPawnRow == _blackStartCoordinates.row)
                    {
                        return true;
                    }
                    break;
                case PawnType.Black:
                    if (currentPawnRow == _whiteStartCoordinates.row)
                    {
                        return true;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }

        private bool CheckIfCellIsReal(CellCoordinates cellCoordinates)
        {
            bool condition1 = cellCoordinates.row < AmountOfRows;
            bool condition2 = cellCoordinates.row >= 0;
            bool condition3 = cellCoordinates.column < AmountOfColumns;
            bool condition4 = cellCoordinates.column >= 0;

            return condition1 & condition2 & condition3 & condition4;
        }
        private bool CheckIfCellIsBusy(CellCoordinates cellCoordinates)
        {
            return _cells[cellCoordinates.row, cellCoordinates.column].IsBusy;
        }

        #endregion
    }
}
