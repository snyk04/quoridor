using UnityEngine;
using UnityEngine.UI;

namespace Quoridor.View.Cells
{
    [RequireComponent(typeof(Button))]
    public class CellVisual : MonoBehaviour
    {
        private Button _button;
        
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
