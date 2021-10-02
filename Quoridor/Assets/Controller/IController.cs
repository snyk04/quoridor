using UnityEngine;

namespace Quoridor.Controller
{
    public interface IController
    {
        void StartGame();
        void ChooseCell(Vector2Int cellCoordinates);
    }
}
