using System.Collections.Generic;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;

namespace Quoridor.Model
{
    public class PossibleMoves
    {
        private readonly ModelCommunication _model;
        
        private Coordinates _currentTurnPlayerCoordinates;
        private List<Coordinates> _availableWalls;

        public PossibleMoves(ModelCommunication model)
        {
            _model = model;

            _availableWalls = _model.WallsManager.WallsThatCanBePlaced;
            _model.WallsManager.WallPlaced += RecalculateAvailableWalls;
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
            return _availableWalls.ToArray();
        }

        private void RecalculateAvailableWalls()
        {
            List<Coordinates> uncheckedWalls = new List<Coordinates>(_model.WallsManager.WallsThatCanBePlaced);
            _availableWalls = new List<Coordinates>();
            
            foreach (Coordinates wall in uncheckedWalls)
            {
                _model.WallsManager.PathfindingPlaceWall(wall);
                if (IsAbilityToWin(_model.PlayersMoves.FirstPlayer) && IsAbilityToWin(_model.PlayersMoves.SecondPlayer))
                {
                    _availableWalls.Add(wall);
                }
                _model.WallsManager.PathfindingDestroyWall(wall);
            }
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
