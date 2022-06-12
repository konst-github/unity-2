using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Piano
{
    public class NoteTextView : MonoBehaviour
    {
        // TODO: 
        // Try this approach: 
        // https://gamedev.stackexchange.com/questions/167014/how-can-i-use-the-colours-of-a-swatch-in-a-script
        
        [SerializeField] private Color colorOn;
        [SerializeField] private Color colorOff;

        [SerializeField] private TextMeshProUGUI textMeshPro;

        [SerializeField] private Image image;
        
        public void ShowNoteName(string note)
        {
            SetTextAndColor(note, colorOn);
        }

        public void HideNoteName()
        {
            SetTextAndColor(string.Empty, colorOff);
        }

        private void SetTextAndColor(string text, Color color)
        {
            textMeshPro.SetText(string.IsNullOrEmpty(text) ? string.Empty : text);
            image.color = color;
        }
    }
}