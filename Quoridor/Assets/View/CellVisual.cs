using UnityEngine;
using UnityEngine.UI;

namespace Quoridor.View
{
    [RequireComponent(typeof(Button))]
    public class CellVisual : MonoBehaviour
    {
        #region Components

        private Button _button;

        #endregion

        #region MonoBehaviour methods

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        #endregion
        
        #region Methods

        public void Highlight()
        {
            _button.interactable = true;
        }
        public void UnHighlight()
        {
            _button.interactable = false;
        }

        #endregion
    }
}
