using UnityEngine;

namespace Quoridor.Controller.Buttons
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public abstract class Button : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] protected ControllerCommunication _controller;
        
        [Header("Settings")]
        [SerializeField] protected Vector2Int _coordinates;

        public abstract void NotifyController();
    }
}
