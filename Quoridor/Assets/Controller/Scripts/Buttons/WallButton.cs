using UnityEngine;
using UnityEngine.UI;

namespace Quoridor.Controller.Buttons
{
    // TODO : make abstract class ControllerButton.cs or smth.
    [RequireComponent(typeof(Button))]
    public class WallButton : MonoBehaviour
    {
        // TODO : maybe controller communication saves buttons, not vice versa?
        [Header("References")]
        [SerializeField] private ControllerCommunication _controller;
        
        [Header("Settings")]
        [SerializeField] public Vector2Int _wallCoordinates;

        public void NotifyController()
        {
            _controller.TryToPlaceWall(_wallCoordinates);
        }
    }
}
