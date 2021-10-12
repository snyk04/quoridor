using UnityEngine;

namespace Quoridor.Controller
{
    public interface IController
    {
        void Restart();
        void Quit();
        
        void ChooseCell(Vector2Int cellCoordinates);
        void TryToPlaceWall(Vector2Int wallCoordinates);
    }
}
