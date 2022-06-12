using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Piano
{
    public class PianoKey : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Color colorMouseOver;
        [SerializeField] private Color colorMouseDown;
        [SerializeField] private Color colorNormal;
        
        [SerializeField] private Image buttonImage;

        private IPiano piano;

        private bool isMouseOver = false;
        private bool isMouseDown = false;

        public void InjectPiano(IPiano piano)
        {
            this.piano = piano;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isMouseDown = true;
            buttonImage.color = colorMouseDown;
            if(piano != null) piano.OnKeyClick(tag);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isMouseDown = false;
            buttonImage.color = isMouseOver ? colorMouseOver : colorNormal;
            if(piano != null) piano.OnKeyRelease();
        }

        // There is a bug , probably in Unity , when we click down and up on a button - 
        // it doesn't register enter and exit of a pointer later on.
        // So handling enter/exit in code
        public void OnPointerEnter(PointerEventData eventData)
        {
            isMouseOver = true;
            buttonImage.color = isMouseDown ? colorMouseDown : colorMouseOver;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isMouseOver = false;
            buttonImage.color = isMouseDown ? colorMouseDown : colorNormal;
        }
    }
}