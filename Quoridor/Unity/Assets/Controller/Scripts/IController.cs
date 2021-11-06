using UnityEngine;

namespace Quoridor.Controller
{
    public interface IController
    {
        void Restart();
        void Quit();
        
        void MoveToCell(Vector2Int cellCoordinates);
        void PlaceWall(Vector2Int wallCoordinates);
    }
}
