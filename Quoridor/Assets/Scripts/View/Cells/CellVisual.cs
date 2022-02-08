using UnityEngine;
using UnityEngine.UI;

namespace Quoridor.View.Cells
{
    [RequireComponent(typeof(Button))]
    public sealed class CellVisual : MonoBehaviour
    {
        private Button _button;

        public Vector3 Position => transform.position;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void ChangeInteractivity(bool isInteractable)
        {
            _button.interactable = isInteractable;
        }
        
        public void Highlight()
        {
            ChangeInteractivity(true);
        }
        public void Unhighlight()
        {
            ChangeInteractivity(false);
        }
    }
}
