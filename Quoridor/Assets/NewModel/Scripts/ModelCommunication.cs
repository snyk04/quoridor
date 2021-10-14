using System;
using System.Collections.Generic;
using Quoridor.NewModel.Common;
using Quoridor.NewModel.PlayerLogic;
using Quoridor.View;

namespace Quoridor.NewModel
{
    public class ModelCommunication : IModel
    {
        private readonly IView _view;
        
        public GameCycle GameCycle { get; }
        public PlayersMoves PlayersMoves { get; }
        
        public CellField CellField { get; }
        public WallField WallField { get; }
        
        public ModelCommunication(IView view)
        {
            _view = view;
            
            GameCycle = new GameCycle();
            PlayersMoves = new PlayersMoves(this);
            CellField = new CellField();
            WallField = new WallField();
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

        internal void MovePlayer(PlayerType playerType, Coordinates coordinates)
        {
            _view.MovePlayerToCell(playerType, coordinates);
        }
        internal void PlaceWall(Player player, Coordinates coordinates)
        {
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
