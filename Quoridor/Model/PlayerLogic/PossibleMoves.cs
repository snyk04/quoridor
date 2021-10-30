using System.Collections.Generic;
using Quoridor.Model.Common;

namespace Quoridor.Model.PlayerLogic
{
    public sealed class PossibleMoves
    {
        private readonly ModelCommunication _model;
        
        private List<Coordinates> _availableWalls;
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
            if (!IsWayBetweenCells(moveFrom, moveTo))
            {
                return;
            }

            if (_model.CellsManager.CellIsBusy(moveTo))
            {
                if (_currentTurnPlayerCoordinates.Equals(moveTo))
                {
                    return;
                }
                
                FindWayToJumpOver(moveFrom, moveTo, availableCells);
                return;
            }
         
            availableCells.Add(moveTo);
        }
        private bool IsWayBetweenCells(Coordinates moveFrom, Coordinates moveTo)
        {
            return _model.CellsManager.CellIsReal(moveFrom)
                   && _model.CellsManager.CellIsReal(moveTo) 
                   && !_model.CellsManager.WallIsBetweenCells(moveFrom, moveTo);
        }
        private void FindWayToJumpOver(Coordinates moveFrom, Coordinates moveTo, List<Coordinates> availableCells)
        {
            Coordinates jumpDirection = moveTo - moveFrom;
            Coordinates cellBehindEnemy = moveTo + jumpDirection;
                    
            if (IsWayBetweenCells(moveTo, cellBehindEnemy))
            {
                availableCells.Add(cellBehindEnemy);
                return;
            }
            
            availableCells.AddRange(GetAvailableCellsFromCell(moveTo));
        }

        public Coordinates[] AvailableWalls()
        {
            if (_model.PlayerController.PlayersHaveWalls)
            {
                RecalculateAvailableWalls();
            }

            return _availableWalls.ToArray();
        }
        private void RecalculateAvailableWalls()
        {
            List<Coordinates> uncheckedWalls = new List<Coordinates>(_model.WallsManager.AvailableWalls);
            _availableWalls = new List<Coordinates>();
            
            foreach (Coordinates wall in uncheckedWalls)
            {
                _model.WallsManager.PlaceTemporaryWall(wall);

                if (PlayerCanWin(_model.PlayerController.WhitePlayer)
                    && PlayerCanWin(_model.PlayerController.BlackPlayer))
                {
                    _availableWalls.Add(wall);
                }

                _model.WallsManager.DestroyTemporaryWall(wall);
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
