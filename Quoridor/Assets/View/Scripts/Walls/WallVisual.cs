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
        
        public void HandlePlace()
        {
            Disable();
            ChangeAlpha(1);
        }
        public void HandleDestroy()
        {
            ChangeAlpha(0);
        }
        
        public void Highlight()
        {
            ChangeAlpha(0.75f);
        }
        public void Unhighlight()
        {
            ChangeAlpha(0);
        }

        public void Enable()
        {
            _eventTrigger.enabled = true;
            _button.enabled = true;
        }
        public void Disable()
        {
            _eventTrigger.enabled = false;
            _button.enabled = false;
        }
        
        private void ChangeAlpha(float alpha)
        {
            if (alpha < 0 || alpha > 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);
        }
    }
}
