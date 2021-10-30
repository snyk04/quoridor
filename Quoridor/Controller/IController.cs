using Quoridor.Model.Common;

namespace Quoridor.Controller
{
    public interface IController
    {
        Coordinates[] AvailableCells { set; }
        Coordinates[] AvailableWalls { set; }
        
        void StartGame();
        void StopGame();
    }
}
