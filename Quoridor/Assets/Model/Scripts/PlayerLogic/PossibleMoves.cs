using System.Collections.Generic;
using Quoridor.Model.Common;

namespace Quoridor.Model.PlayerLogic
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
        private Coordinates[] GetAvailableCellsFromCell(Coordinates cell)
        {
            Coordinates[] uncheckedCells =
            {
                new Coordinates(cell.row + 1, cell.column),
                new Coordinates(cell.row - 1, cell.column),
                new Coordinates(cell.row, cell.column + 1),
                new Coordinates(cell.row, cell.column - 1)
            };
            
            var availableCells = new List<Coordinates>();
            foreach (Coordinates uncheckedCell in uncheckedCells)
            {
                TryToAddCell(cell, uncheckedCell, availableCells);
            }

            return availableCells.ToArray();
        }
        private void TryToAddCell(Coordinates moveFrom, Coordinates moveTo, List<Coordinates> availableCells)
        {
            if (!_model.CellsManager.CellIsReal(moveTo) || _model.CellsManager.WallIsBetweenCells(moveFrom, moveTo))
            {
                return;
            }

            if (_model.CellsManager.CellIsBusy(moveTo))
            {
                if (!_currentTurnPlayerCoordinates.Equals(moveTo))
                {
                    availableCells.AddRange(GetAvailableCellsFromCell(moveTo));
                }
            }
            else
            {
                availableCells.Add(moveTo);
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
                if (PlayerCanWin(_model.PlayersMoves.FirstPlayer) && PlayerCanWin(_model.PlayersMoves.SecondPlayer))
                {
                    _availableWalls.Add(wall);
                }

                _model.WallsManager.PathfindingDestroyWall(wall);
            }
        }
        
        private bool PlayerCanWin(Player player)
        {
            List<Coordinates> visitedCells = new List<Coordinates>();
            _currentTurnPlayerCoordinates = player.Position;

            return TryToFindWay(player.Position, player.VictoryRow, visitedCells);
        }
        private bool TryToFindWay(Coordinates cell, int victoryRow, ICollection<Coordinates> visitedCells)
        {
            visitedCells.Add(cell);
            foreach (Coordinates cellToCheck in GetAvailableCellsFromCell(cell))
            {
                if (visitedCells.Contains(cellToCheck))
                {
                    continue;
                }
                if (cellToCheck.row == victoryRow || TryToFindWay(cellToCheck, victoryRow, visitedCells))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
