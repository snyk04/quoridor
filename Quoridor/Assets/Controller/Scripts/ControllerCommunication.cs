using Quoridor.Model;
using Quoridor.Model.Cells;
using Quoridor.View;
using UnityEngine;

namespace Quoridor.Controller
{
    public sealed class ControllerCommunication : MonoBehaviour, IController
    {
        [SerializeField] private ViewCommunication _viewCommunication;
        [SerializeField] private GameModeController _gameModeController;
        
        private IView _view;
        private IModel _model;

        private void Awake()
        {
            _view = _viewCommunication;
            _model = new ModelCommunication(_view);
        }

        public void StartNewGame()
        {
            _model.StartNewGame(_gameModeController.GameMode);
        }
        public void Quit()
        {
            Application.Quit();
        }
        
        public void ChooseCell(Vector2Int cellCoordinates)
        {
            CellCoordinates convertedCoordinates = ConvertCoordinates(cellCoordinates);
            _model.MoveCurrentPlayerToCell(convertedCoordinates);
        }
        public void TryToPlaceWall(Vector2Int wallCoordinates)
        {
            CellCoordinates convertedCoordinates = ConvertCoordinates(wallCoordinates);
            _model.TryToPlaceWall(convertedCoordinates);
        }
        
        private CellCoordinates ConvertCoordinates(Vector2Int coordinates)
        {
            return new CellCoordinates(coordinates.x, coordinates.y);
        }
    }
}
