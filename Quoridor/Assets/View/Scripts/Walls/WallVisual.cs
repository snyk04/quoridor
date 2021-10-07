using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Quoridor.View
{
    [RequireComponent(typeof(Button))]
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

        public void Place()
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1);
            _eventTrigger.enabled = false;
        }
        public void Highlight()
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.75f);
        }
        public void Unhighlight()
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0f);
        }
    }
}
