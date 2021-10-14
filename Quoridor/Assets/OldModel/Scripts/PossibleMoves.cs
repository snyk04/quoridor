using System;
using System.Collections.Generic;
using Quoridor.OldModel.Cells;
using UnityEngine;

namespace Quoridor.OldModel
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
        private List<Coordinates> TestGetPossibleMovesFromCell(Coordinates cellCoordinates)
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
                GeneralMethod(cellCoordinates, uncheckedCell, possibleMoves);
            }

            return possibleMoves;
        }

        private void GeneralMethod(Coordinates moveFrom, Coordinates moveTo, List<Coordinates> possibleMoves)
        {
            if (!_model.CellsManager.CellIsReal(moveTo))
            {
                return;
            }

            if (_model.CellsManager.WallIsBetweenCells(moveFrom, moveTo))
            {
                return;
            }
            
            if (_model.CellsManager.CellIsBusy(moveTo))
            {
                if (!_currentTurnPlayerCoordinates.Equals(moveTo))
                {
                    possibleMoves.AddRange(GetPossibleMovesFromCell(moveTo));
                }
            }
            else
            {
                possibleMoves.Add(moveTo);
            }
        }
        private void TryToAddCellToPossibleMoves(Coordinates cell, List<Coordinates> possibleMoves)
        {
            GeneralMethod(_currentTurnPlayerCoordinates, cell, possibleMoves);
        }
        private void ShowPossibleMoves(Coordinates cellCoordinates)
        {
            IEnumerable<Coordinates> availableMoves = GetPossibleMovesFromCell(cellCoordinates);
            _model.HighlightCells(availableMoves);
        }

        public bool IsAbilityToWin(Coordinates cell, int rowToWin)
        {
            List<Coordinates> visitedCells = new List<Coordinates>();

            return Func(cell, rowToWin, visitedCells);
        }

        private bool Func(Coordinates cell, int rowToWin, ICollection<Coordinates> visitedCells)
        {
            visitedCells.Add(cell);
            var result = false;
            foreach (Coordinates cellToCheck in TestGetPossibleMovesFromCell(cell))
            {
                if (visitedCells.Contains(cellToCheck))
                {
                    continue;
                }
                if (cellToCheck.row == rowToWin)
                {
                    return true;
                }
                    
                visitedCells.Add(cellToCheck);
                result |= Func(cellToCheck, rowToWin, visitedCells);
            }

            return result;
            // throw new Exception();
        }
    }
}
