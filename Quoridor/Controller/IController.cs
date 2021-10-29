using Quoridor.Model.Common;

namespace Quoridor.Controller
{
    public interface IController
    {
        Coordinates[] AvailableMoves { set; }
        Coordinates[] AvailableWalls { set; }
        
        void StartGame();
    }
}
