using Quoridor.NewModel;
using Quoridor.NewModel.Common;
using Quoridor.View;
using UnityEngine;
using UnityEngine.SceneManagement;
using View.Scripts.UserInterface;

namespace Quoridor.Controller
{
    public sealed class ControllerCommunication : MonoBehaviour, IController
    {
        private const int MainMenuSceneId = 0;
        private const int MainSceneId = 1;
        
        [SerializeField] private ViewCommunication _viewCommunication;
        [SerializeField] private GameModeController _gameModeController;
        
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
            SceneManager.LoadScene(MainMenuSceneId);
        }
        
        public void ChooseCell(Vector2Int cellCoordinates)
        {
            Coordinates convertedCoordinates = ConvertCoordinates(cellCoordinates);
            _model.MoveCurrentPlayerToCell(convertedCoordinates);
        }
        public void TryToPlaceWall(Vector2Int wallCoordinates)
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
