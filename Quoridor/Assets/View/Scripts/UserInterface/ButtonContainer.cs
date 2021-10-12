using UnityEngine;
using UnityEngine.UI;

namespace Quoridor.View.UserInterface
{
    public class ButtonContainer : MonoBehaviour
    {
        [SerializeField] private Button[] _buttons;

        private void Start()
        {
            SelectFirstButton();
        }

        public void Enable()
        {
            SetButtonsActive(true);
            
            SelectFirstButton();
        }
        public void Disable()
        {
            SetButtonsActive(false);
        }

        private void SelectFirstButton()
        {
            _buttons[0]?.Select();
        }
        private void SetButtonsActive(bool isActive)
        {
            foreach (Button button in _buttons)
            {
                button.gameObject.SetActive(isActive);
            }
        }
    }
}
