using UnityEngine;
using UnityEngine.UI;

namespace Quoridor.Controller
{
    [RequireComponent(typeof(Button))]
    public class CellButton : MonoBehaviour
    {
        [SerializeField] private ControllerCommunication _controller;
        [SerializeField] private Vector2Int _cellCoordinates;

        public void NotifyController()
        {
            _controller.ChooseCell(_cellCoordinates);
        }
    }
}
