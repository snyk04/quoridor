using System.Collections.Generic;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;
using UnityEngine;

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
            _currentTurnPlayerCoordinates = playerCoordinates;
            return GetAvailableCellsFromCell(playerCoordinates);
        }
        public Coordinates[] GetAvailableCellsFromCell(Coordinates cellCoordinates)
        {
            Coordinates[] uncheckedCells =
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

            if (_model.CellsManager.WallIsBetweenCells(moveFrom, moveTo))
            {
                return;
            }
            
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
            List<Coordinates> uncheckedWalls = new List<Coordinates>(_model.WallsManager.WallsThatCanBePlaced);
            var checkedWalls = new List<Coordinates>();
            
            foreach (Coordinates wall in uncheckedWalls)
            {
                Debug.Log(wall.ToString());
                _model.WallsManager.PathfindingPlaceWall(wall);
                if (IsAbilityToWin(_model.PlayersMoves.FirstPlayer) && IsAbilityToWin(_model.PlayersMoves.SecondPlayer))
                {
                    checkedWalls.Add(wall);
                }
                _model.WallsManager.PathfindingDestroyWall(wall);
            }
            
            return checkedWalls.ToArray();
        }
        
        public bool IsAbilityToWin(Player player)
        {
            List<Coordinates> visitedCells = new List<Coordinates>();
            _currentTurnPlayerCoordinates = player.Position;

            return Func(player.Position, player.VictoryRow, visitedCells);
        }
        private bool Func(Coordinates cell, int rowToWin, ICollection<Coordinates> visitedCells)
        {
            visitedCells.Add(cell);
            var result = false;
            foreach (Coordinates cellToCheck in GetAvailableCellsFromCell(cell))
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
        }
    }
}
