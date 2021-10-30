using Quoridor.Model;
using Quoridor.Model.Common;
using Quoridor.View;

namespace Quoridor.Controller
{
    public class ControllerCommunication : IController
    {
        private readonly GameManager _gameManager;

        public Coordinates[] AvailableCells
        {
            set => _gameManager.AvailableCells = value;
        }
        public Coordinates[] AvailableWalls
        {
            set => _gameManager.AvailableWalls = value;
        }

        public ControllerCommunication()
        {
            IView view = new ViewCommunication(this);
            IModel model = new ModelCommunication(view);

            _gameManager = new GameManager(model);
        }
        
        public void StartGame()
        {
            _gameManager.StartGame();
        }
        public void StopGame()
        {
            _gameManager.StopGame();
        }
    }
}
