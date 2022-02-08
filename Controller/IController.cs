using Quoridor.Model.Common;

namespace Quoridor.Controller
{
    public interface IController
    {
        Coordinates[] AvailableCells { set; }
        Coordinates[] AvailableWalls { set; }
        Coordinates[] AvailableJumps { set; }
        
        void StartGame();
        void StopGame();
    }
}
