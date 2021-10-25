using Quoridor.Model.Common;

namespace Quoridor.Controller
{
    public interface IController
    {
        void Restart();
        void Quit();
        
        void MoveToCell(Coordinates cellCoordinates);
        void PlaceWall(Coordinates wallCoordinates);
    }
}
