using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Quoridor.View
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(EventTrigger))]
    [RequireComponent(typeof(Image))]
    public class WallVisual : MonoBehaviour
    {
        private Button _button;
        private EventTrigger _eventTrigger;
        private Image _image;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _eventTrigger = GetComponent<EventTrigger>();
            _image = GetComponent<Image>();
        }

        private void ChangeAlpha(float alpha)
        {
            if (alpha < 0 || alpha > 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);
        }
        
        public void HandlePlace()
        {
            ChangeAlpha(1);
            _eventTrigger.enabled = false;
        }
        public void Highlight()
        {
            ChangeAlpha(0.75f);
        }
        public void Unhighlight()
        {
            ChangeAlpha(0);
        }

        public void Disable()
        {
            _eventTrigger.enabled = false;
            _button.interactable = false;
        }
    }
}
