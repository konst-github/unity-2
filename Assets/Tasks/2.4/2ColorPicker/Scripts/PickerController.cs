using UnityEngine;
using UnityEngine.UI;

namespace ColorPicker
{
    public class PickerController : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Transform gridContent;
        [SerializeField] private Image colorImage;
        [SerializeField] private SliderComponent slider1;
        [SerializeField] private SliderComponent slider2;
        [SerializeField] private SliderComponent slider3;
        [SerializeField] private Toggle toggleRGB;
        [SerializeField] private Toggle toggleHSV;
        [SerializeField] private CodeInputField codeInputField;
        [SerializeField] private Image cellPrefab;

        private ColorModel colorModel = ColorModel.RGB;
        private Color currentColor = Color.white;

        private void Start()
        {
            toggleRGB.onValueChanged.AddListener((isSelected) => { UpdateColorModel(ColorModel.RGB, isSelected); });
            toggleHSV.onValueChanged.AddListener((isSelected) => { UpdateColorModel(ColorModel.HSV, isSelected); });

            slider1.Listener = (value) => { UpdateColorFromComponents(slider1.Value, slider2.Value, slider3.Value); };
            slider2.Listener = (value) => { UpdateColorFromComponents(slider1.Value, slider2.Value, slider3.Value); };
            slider3.Listener = (value) => { UpdateColorFromComponents(slider1.Value, slider2.Value, slider3.Value); };

            codeInputField.Listener = (text) =>
            {
                ColorUtility.TryParseHtmlString(text, out Color color);
                if (color != null)
                {
                    currentColor = color;
                    colorImage.color = currentColor;
                    UpdateSliders();
                }
            }; ;
        }

        public void AddCellWithCurrentColor()
        {
            // https://www.studica.com/blog/unity-ui-tutorial-scroll-grid
            Image newCell = Instantiate(cellPrefab, gridContent);
            newCell.color = currentColor;
            
            // Force scroll to the bottom
            Canvas.ForceUpdateCanvases();
            scrollRect.normalizedPosition = Vector2.zero;
            Canvas.ForceUpdateCanvases();
        }

        private void UpdateColorFromComponents(float component1, float component2, float component3)
        {
            currentColor = colorModel == ColorModel.RGB ? new Color(component1, component2, component3) : Color.HSVToRGB(component1, component2, component3);
            colorImage.color = currentColor;
            codeInputField.Color = currentColor;
        }

        public void UpdateColorModel(ColorModel newColorModel, bool isSelected)
        {
            if(isSelected)
            {
                colorModel = newColorModel;
                UpdateSliders();
            }
        }

        private void UpdateSliders()
        {
            float value1 = currentColor.r;
            float value2 = currentColor.g;
            float value3 = currentColor.b;

            if (colorModel == ColorModel.HSV)
            {
                Color.RGBToHSV(currentColor, out value1, out value2, out value3);
            }

            slider1.UpdateState(value1, colorModel);
            slider2.UpdateState(value2, colorModel);
            slider3.UpdateState(value3, colorModel);
        }
    }
}