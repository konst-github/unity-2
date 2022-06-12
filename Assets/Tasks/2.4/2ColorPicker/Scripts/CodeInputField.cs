using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ColorPicker
{
    [RequireComponent(typeof(TMP_InputField))]
    public class CodeInputField : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;

        private const byte ColorStringLengthMax = 7; // including # symbol
        private const string sharpCharacter= "#";

        public UnityAction<string> Listener
        {
            set { inputField.onEndEdit.AddListener(value); }
        }

        public Color Color
        {
            set { inputField.text = string.Format("{0}{1}", sharpCharacter, ColorUtility.ToHtmlStringRGB(value)); }
        }

        private void Awake()
        {
            inputField.inputValidator = (InputValidator)ScriptableObject.CreateInstance(typeof(InputValidator));
        }

        internal class InputValidator : TMP_InputValidator
        {
            private const char nullTerminator = '\0';

            private List<char> allowedChars;

            internal InputValidator() : base()
            {
                allowedChars = new List<char>
                    { 
                        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                        'a', 'b', 'c', 'd', 'e', 'f',
                        'A', 'B', 'C', 'D', 'E', 'F'
                    };
            }

            override public char Validate(ref string text, ref int position, char character)
            {
                // Debug.Log($"VALIDATE: {text} LEN: {text.Length} : CHAR: {ch} : POS: {pos}");
                if (!text.StartsWith(sharpCharacter))
                {
                    text = string.Format("{0}{1}", sharpCharacter, text);
                    position += 1;
                }

                if (text.Length < ColorStringLengthMax && allowedChars.Contains(character))
                {
#if UNITY_EDITOR
                    text = text.Insert(position, character.ToString()).ToUpper();
                    position += 1;
#endif
                    return character;
                }

                return nullTerminator;
            }
        }
    }
}