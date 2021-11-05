using System.Collections.Generic;
using Quoridor.Model.Cells;
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
        
        public Coordinates[] AvailableMoves(Coordinates playerCoordinates)
        {
            _currentTurnPlayerCoordinates = playerCoordinates;
            return GetAvailableMovesFromCell(playerCoordinates);
        }
        private Coordinates[] GetAvailableMovesFromCell(Coordinates cell)
        {
            Coordinates[] uncheckedCells =
            {
                new(cell.Row + 1, cell.Column),
                new(cell.Row - 1, cell.Column),
                new(cell.Row, cell.Column + 1),
                new(cell.Row, cell.Column - 1)
            };
            
            var availableCells = new List<Coordinates>();
            foreach (Coordinates uncheckedCell in uncheckedCells)
            {
                TryToAddMove(cell, uncheckedCell, availableCells);
            }

            return availableCells.ToArray();
        }
        private void TryToAddMove(Coordinates moveFrom, Coordinates moveTo, ICollection<Coordinates> availableCells)
        {
            if (!IsWayBetweenCells(moveFrom, moveTo))
            {
                return;
            }

            if (_model.CellsManager.CellIsBusy(moveTo))
            {
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

        public Coordinates[] AvailableJumps(Coordinates playerCoordinates)
        {
            _currentTurnPlayerCoordinates = playerCoordinates;
            return GetAvailableJumpsFromCell(playerCoordinates);
        }
        private Coordinates[] GetAvailableJumpsFromCell(Coordinates cell)
        {
            Coordinates[] uncheckedCells =
            {
                new(cell.Row + 1, cell.Column),
                new(cell.Row - 1, cell.Column),
                new(cell.Row, cell.Column + 1),
                new(cell.Row, cell.Column - 1)
            };
            
            var availableJumps = new List<Coordinates>();
            foreach (Coordinates uncheckedCell in uncheckedCells)
            {
                TryToAddJump(cell, uncheckedCell, availableJumps);
            }

            return availableJumps.ToArray();
        }
        private void TryToAddJump(Coordinates moveFrom, Coordinates moveTo, List<Coordinates> availableCells)
        {
            if (!IsWayBetweenCells(moveFrom, moveTo))
            {
                return;
            }

            if (!_model.CellsManager.CellIsBusy(moveTo))
            {
                return;
            }
            
            if (_currentTurnPlayerCoordinates.Equals(moveTo))
            {
                return;
            }
                
            FindWayToJumpOver(moveFrom, moveTo, availableCells);
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
            
            availableCells.AddRange(GetAvailableMovesFromCell(moveTo));
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

                if (PlayerCanWin(_model.PlayerController.WhitePlayer, _model.PlayerController.BlackPlayer)
                    && PlayerCanWin(_model.PlayerController.BlackPlayer, _model.PlayerController.WhitePlayer))
                {
                    _availableWalls.Add(wall);
                }

                _model.WallsManager.DestroyTemporaryWall(wall);
            }
        }

        private bool PlayerCanWin(Player player, Player opponent)
        {
            for (int i = 0; i < CellsManager.AmountOfColumns; i++)
            {
                if (_model.FieldPathFinder.FindShortestPathToRow(player.Position, player.VictoryRow, AvailableJumps(player.Position), opponent.Position) != null)
                {
                    return true;
                }
            }

            return false;
        }
        
        // private bool PlayerCanWin(Player player)
        // {
        //     List<Coordinates> visitedCells = new List<Coordinates>();
        //     _currentTurnPlayerCoordinates = player.Position;
        //
        //     return TryToFindWay(player.Position, player.VictoryRow, visitedCells);
        // }
        // private bool TryToFindWay(Coordinates cell, int victoryRow, ICollection<Coordinates> visitedCells)
        // {
        //     visitedCells.Add(cell);
        //     foreach (Coordinates cellToCheck in GetAvailableMovesFromCell(cell))
        //     {
        //         if (visitedCells.Contains(cellToCheck))
        //         {
        //             continue;
        //         }
        //         if (cellToCheck.Row == victoryRow || TryToFindWay(cellToCheck, victoryRow, visitedCells))
        //         {
        //             return true;
        //         }
        //     }
        //
        //     return false;
        // }
    }
}
