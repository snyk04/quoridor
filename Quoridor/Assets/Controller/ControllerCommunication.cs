using Quoridor.Model;
using Quoridor.View;
using UnityEngine;

namespace Quoridor.Controller
{
    public class ControllerCommunication : MonoBehaviour, IController
    {
        [SerializeField] private ViewCommunication _viewCommunication;
        
        [SerializeField] private GameModeController _gameModeController;
        
        private IView _view;
        // TODO : no need, because there is not gonna be another model
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
            _model.MoveCurrentPlayerToCell(new CellCoordinates(cellCoordinates.x, cellCoordinates.y));
        }
    }
}
