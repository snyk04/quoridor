using System;
using Quoridor.Model;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;
using Quoridor.View;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Quoridor.Controller
{
    public sealed class ControllerCommunication : MonoBehaviour, IController
    {
        private const int MainMenuSceneId = 0;
        private const int MainSceneId = 1;
        
        [SerializeField] private ViewCommunication _viewCommunication;
        
        private IView _view;
        private IModel _model;

        private void Awake()
        {
            _view = _viewCommunication;
            _model = new ModelCommunication(_view);
        }
        private void Start()
        {
            GameMode gameMode = GameModeTransmitter.GameMode;
            (PlayerType whitePlayerType, PlayerType blackPlayerType) = gameMode switch
            {
                GameMode.PlayerVsPlayer => (PlayerType.Player1, PlayerType.Player2),
                GameMode.PlayerVsComputer => (PlayerType.Player1, PlayerType.SmartBot),
                GameMode.ComputerVsComputer => (PlayerType.SmartBot, PlayerType.SmartBot),
                _ => throw new ArgumentOutOfRangeException()
            };

            _model.StartNewGame(whitePlayerType, blackPlayerType);
        }

        public void Restart()
        {
            SceneManager.LoadScene(MainSceneId);
        }
        public void Quit()
        {
            GC.Collect();
            SceneManager.LoadScene(MainMenuSceneId);
        }

        public void Surrender()
        {
            _model.StopGame(GameStopType.Surrender);
        }
        
        public void MoveToCell(Vector2Int cellCoordinates)
        {
            Coordinates convertedCoordinates = ConvertCoordinates(cellCoordinates);
            _model.MoveCurrentPlayerToCell(convertedCoordinates);
        }
        public void PlaceWall(Vector2Int wallCoordinates)
        {
            Coordinates convertedCoordinates = ConvertCoordinates(wallCoordinates);
            _model.PlaceCurrentPlayerWall(convertedCoordinates);
        }
        
        private Coordinates ConvertCoordinates(Vector2Int coordinates)
        {
            return new Coordinates(coordinates.x, coordinates.y);
        }
    }
}
