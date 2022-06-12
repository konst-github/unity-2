using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ColorPicker
{
    public class SliderComponent : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Image knob;
        [SerializeField] private TextMeshProUGUI textView;

        [SerializeField] private string titleRGB;
        [SerializeField] private string titleHSV;
        [SerializeField] private Color colorRGB;
        [SerializeField] private Color colorHSV;

        public float Value => slider.value;

        public UnityAction<float> Listener
        {
            set { slider.onValueChanged.AddListener(value); }
        }

        private ColorModel ColorModel
        {
            set
            {
                switch(value)
                {
                    case ColorModel.RGB:
                        textView.SetText(titleRGB);
                        textView.color = colorRGB;
                        knob.color = colorRGB;
                        break;
                    case ColorModel.HSV:
                        textView.SetText(titleHSV);
                        textView.color = colorHSV;
                        knob.color = colorHSV;
                        break;
                }
            }
        }

        public void UpdateState(float value, ColorModel colorModel)
        {
            slider.SetValueWithoutNotify(value);
            ColorModel = colorModel;
        }
    }
}