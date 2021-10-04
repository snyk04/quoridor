using Quoridor.Model;
using Quoridor.View;
using UnityEngine;

namespace Quoridor.Controller
{
    public class ControllerCommunication : MonoBehaviour, IController
    {
        [SerializeField] private ViewCommunication _viewCommunication;
        
        private IView _view;
        private IModel _model;

        private void Awake()
        {
            _view = _viewCommunication;
            _model = new ModelCommunication(_view);
        }

        public void StartNewGame()
        {
            _model.StartGame();
        }
        public void Quit()
        {
            Application.Quit();
        }
        
        public void ChooseCell(Vector2Int cellCoordinates)
        {
            _model.MoveCurrentPawnToCell(new CellCoordinates(cellCoordinates.x, cellCoordinates.y));
        }
    }
}
