using UnityEngine;

namespace Quoridor.Controller
{
    public interface IController
    {
        void StartNewGame();
        void Quit();
        
        void ChooseCell(Vector2Int cellCoordinates);
    }
}
