using System;
using Quoridor.Model;
using Quoridor.Model.Common;
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
            _model.StartNewGame(GameModeTransmitter.GameMode);
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
