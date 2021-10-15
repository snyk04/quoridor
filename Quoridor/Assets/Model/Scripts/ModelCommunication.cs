﻿using System;
using System.Collections.Generic;
using Model;
using Quoridor.Model.Cells;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;
using Quoridor.Model.Walls;
using Quoridor.View;

namespace Quoridor.Model
{
    public class ModelCommunication : IModel
    {
        private readonly IView _view;
        
        public GameCycle GameCycle { get; }
        public PlayersMoves PlayersMoves { get; }
        
        public CellsManager CellsManager { get; }
        public WallsManager WallsManager { get; }
        
        public PlayerMover PlayerMover { get; }
        public WallPlacer WallPlacer { get; }
        
        public PossibleMoves PossibleMoves { get; }

        public ModelCommunication(IView view)
        {
            _view = view;
            
            GameCycle = new GameCycle(this);
            PlayersMoves = new PlayersMoves(this);

            CellsManager = new CellsManager(this);
            WallsManager = new WallsManager(this);

            PlayerMover = new PlayerMover(this);
            WallPlacer = new WallPlacer(this);
            
            PossibleMoves = new PossibleMoves(this);
        }

        public void StartNewGame(GameMode gameMode)
        {
            GameCycle.StartNewGame(gameMode);
        }
        public void StopGame(GameStopType gameStopType)
        {
            PlayerType winner = gameStopType switch
            {
                GameStopType.Surrender => PlayersMoves.CurrentPlayerOpponentType,
                GameStopType.Victory => PlayersMoves.CurrentPlayerType,
                _ => throw new ArgumentOutOfRangeException(nameof(gameStopType), gameStopType, null)
            };
            
            _view.EndGame(winner);
        }

        public void MoveCurrentPlayerToCell(Coordinates cell)
        {
            PlayersMoves.MoveCurrentPlayerToCell(cell);
        }
        public void PlaceCurrentPlayerWall(Coordinates wall)
        {
            PlayersMoves.PlaceCurrentPlayerWall(wall);
        }

        internal void MovePlayer(Player player, Coordinates coordinates)
        {
            PlayerMover.Move(player, coordinates);
            _view.MovePlayerToCell(player.Type, coordinates);
        }
        internal void PlaceWall(Player player, Coordinates coordinates)
        {
            WallsManager.PlaceWall(player, coordinates);
            _view.PlaceWall(player, coordinates);
        }

        internal void ShowAvailableMoves(IEnumerable<Coordinates> cells)
        {
            _view.ShowAvailableMoves(cells);
        }
        internal void ShowAvailableWalls(IEnumerable<Coordinates> walls)
        {
            _view.ShowAvailableWalls(walls);
        }
    }
}
