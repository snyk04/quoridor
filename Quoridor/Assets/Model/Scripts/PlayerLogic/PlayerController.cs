﻿using System;
using Quoridor.Model.Common;

namespace Quoridor.Model.PlayerLogic
{
    public sealed class PlayerController
    {
        private const int DefaultAmountOfWalls = 10;

        private readonly Coordinates _firstPlayerStartPosition = new Coordinates(8, 4);
        private readonly Coordinates _secondPlayerStartPosition = new Coordinates(0, 4);
        
        private readonly ModelCommunication _model;

        public Player FirstPlayer { get; private set; }
        public Player SecondPlayer { get; private set; }

        public bool PlayersHaveWalls => FirstPlayer.AmountOfWalls > 0 || SecondPlayer.AmountOfWalls > 0;

        private Player _currentPlayer;

        private PlayerType CurrentPlayerType => _currentPlayer.Type;
        private PlayerType CurrentPlayerOpponentType
        {
            get
            {
                return CurrentPlayerType switch
                {
                    PlayerType.First => PlayerType.Second,
                    PlayerType.Second => PlayerType.First,
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

        public PlayerType GetWinner(GameStopType gameStopType)
        {
            return gameStopType switch
            {
                GameStopType.Surrender => CurrentPlayerOpponentType,
                GameStopType.Victory => CurrentPlayerType,
                _ => throw new ArgumentOutOfRangeException(nameof(gameStopType), gameStopType, null)
            };
        }

        private void ChangeCurrentPlayer()
        {
            _currentPlayer = CurrentPlayerType switch
            {
                PlayerType.First => SecondPlayer,
                PlayerType.Second => FirstPlayer,
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
        private void InitializePlayers(GameMode gameMode)
        {
            FirstPlayer = gameMode switch
            {
                GameMode.PlayerVsPlayer => new Player(_model, PlayerType.First, _firstPlayerStartPosition, DefaultAmountOfWalls, _secondPlayerStartPosition.row),
                GameMode.PlayerVsComputer => new Player(_model, PlayerType.First, _firstPlayerStartPosition, DefaultAmountOfWalls, _secondPlayerStartPosition.row),
                GameMode.ComputerVsComputer => new RandomBot(_model, PlayerType.First, _firstPlayerStartPosition, DefaultAmountOfWalls, _secondPlayerStartPosition.row),
                _ => throw new ArgumentOutOfRangeException(nameof(gameMode), gameMode, null)
            };
            SecondPlayer = gameMode switch
            {
                GameMode.PlayerVsPlayer => new Player(_model, PlayerType.Second, _secondPlayerStartPosition, DefaultAmountOfWalls, _firstPlayerStartPosition.row),
                GameMode.PlayerVsComputer => new RandomBot(_model, PlayerType.Second, _secondPlayerStartPosition, DefaultAmountOfWalls, _firstPlayerStartPosition.row),
                GameMode.ComputerVsComputer => new RandomBot(_model, PlayerType.Second, _secondPlayerStartPosition, DefaultAmountOfWalls, _firstPlayerStartPosition.row),
                _ => throw new ArgumentOutOfRangeException(nameof(gameMode), gameMode, null)
            };
        }
        
        private void HandleMoveEndEvents()
        {
            FirstPlayer.MovePerformed += ChangeCurrentPlayer;
            FirstPlayer.MovePerformed += SetCurrentPlayerAvailableMoves;
            SecondPlayer.MovePerformed += ChangeCurrentPlayer;
            SecondPlayer.MovePerformed += SetCurrentPlayerAvailableMoves;
        }
        private void NullifyMoveEndEvents()
        {
            FirstPlayer.MovePerformed -= ChangeCurrentPlayer;
            FirstPlayer.MovePerformed -= SetCurrentPlayerAvailableMoves;
            SecondPlayer.MovePerformed -= ChangeCurrentPlayer;
            SecondPlayer.MovePerformed -= SetCurrentPlayerAvailableMoves;
        }

        private void StartGame(GameMode gameMode)
        {
            InitializePlayers(gameMode);
            HandleMoveEndEvents();
            
            _currentPlayer = FirstPlayer;
            SetCurrentPlayerAvailableMoves();
        }
        private void StopGame()
        {
            NullifyMoveEndEvents();
        }
    }
}
