using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Quoridor.View
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(EventTrigger))]
    public class WallVisual : MonoBehaviour
    {
        private EventTrigger _eventTrigger;
        private Image _image;

        private void Awake()
        {
            _eventTrigger = GetComponent<EventTrigger>();
            _image = GetComponent<Image>();
        }

        private void ChangeAlpha(float alpha)
        {
            if (alpha < 0 || alpha > 1)
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);
            }
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
    }
}
