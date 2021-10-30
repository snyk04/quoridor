using System;
using System.Diagnostics;
using Quoridor.Model.Common;

namespace Quoridor.Model.PlayerLogic
{
    public sealed class PlayerController
    {
        private const int DefaultAmountOfWalls = 10;

        private readonly Coordinates _whitePlayerStartPosition = new Coordinates(8, 4);
        private readonly Coordinates _blackPlayerStartPosition = new Coordinates(0, 4);
        
        private readonly ModelCommunication _model;

        public Player WhitePlayer { get; private set; }
        public Player BlackPlayer { get; private set; }

        public bool PlayersHaveWalls => WhitePlayer.AmountOfWalls > 0 || BlackPlayer.AmountOfWalls > 0;

        private Player _currentPlayer;

        private PlayerColor CurrentPlayerColor => _currentPlayer.Color;
        private PlayerColor CurrentPlayerOpponentColor
        {
            get
            {
                return CurrentPlayerColor switch
                {
                    PlayerColor.White => PlayerColor.Black,
                    PlayerColor.Black => PlayerColor.White,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        public PlayerController(ModelCommunication model)
        {
            _model = model;
            
            _model.GameCycle.GameStarted += StartGame;
            _model.GameCycle.GameStopped += StopGame;
        }

        public void MoveCurrentPlayerToCell(Coordinates cell)
        {
            _currentPlayer.MakeMove(MoveType.MoveToCell, cell);
        }
        public void PlaceCurrentPlayerWall(Coordinates wall)
        {
            _currentPlayer.MakeMove(MoveType.PlaceWall, wall);
        }

        public PlayerColor GetWinner(GameStopType gameStopType)
        {
            return gameStopType switch
            {
                GameStopType.Surrender => CurrentPlayerOpponentColor,
                GameStopType.Victory => CurrentPlayerColor,
                _ => throw new ArgumentOutOfRangeException(nameof(gameStopType), gameStopType, null)
            };
        }

        private void ChangeCurrentPlayer()
        {
            _currentPlayer = CurrentPlayerColor switch
            {
                PlayerColor.White => BlackPlayer,
                PlayerColor.Black => WhitePlayer,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        private void SetCurrentPlayerAvailableMoves()
        {
            Coordinates[] cells = _model.PossibleMoves.AvailableCells(_currentPlayer.Position);
            Coordinates[] walls = _model.PossibleMoves.AvailableWalls();
            _currentPlayer.SetPossibleMoves(cells, walls);
        }

        // TODO : maybe refactor?
        private void InitializePlayers(PlayerType whitePlayer, PlayerType blackPlayer)
        {
            WhitePlayer = CreatePlayer(whitePlayer, PlayerColor.White, _whitePlayerStartPosition, _blackPlayerStartPosition.row);
            BlackPlayer = CreatePlayer(blackPlayer, PlayerColor.Black, _blackPlayerStartPosition, _whitePlayerStartPosition.row);
        }
        
        private void HandleMoveEndEvents()
        {
            WhitePlayer.MovePerformed += ChangeCurrentPlayer;
            WhitePlayer.MovePerformed += SetCurrentPlayerAvailableMoves;
            BlackPlayer.MovePerformed += ChangeCurrentPlayer;
            BlackPlayer.MovePerformed += SetCurrentPlayerAvailableMoves;
        }
        private void NullifyMoveEndEvents()
        {
            WhitePlayer.MovePerformed -= ChangeCurrentPlayer;
            WhitePlayer.MovePerformed -= SetCurrentPlayerAvailableMoves;
            BlackPlayer.MovePerformed -= ChangeCurrentPlayer;
            BlackPlayer.MovePerformed -= SetCurrentPlayerAvailableMoves;
        }

        private void StartGame(PlayerType whitePlayer, PlayerType blackPlayer)
        {
            InitializePlayers(whitePlayer, blackPlayer);
            HandleMoveEndEvents();
            
            _currentPlayer = WhitePlayer;
            SetCurrentPlayerAvailableMoves();
        }
        private void StopGame()
        {
            NullifyMoveEndEvents();
        }
        
        private Player CreatePlayer(PlayerType playerType, PlayerColor playerColor, Coordinates startPosition, int victoryRow)
        {
            return playerType switch
            {
                PlayerType.Player1 => new Player(_model, playerColor, startPosition, DefaultAmountOfWalls, victoryRow),
                PlayerType.Player2 => new Player(_model, playerColor, startPosition, DefaultAmountOfWalls, victoryRow),
                PlayerType.RandomBot => new RandomBot(_model, playerColor, startPosition, DefaultAmountOfWalls, victoryRow),
                _ => throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null)
            };
        }
    }
}
