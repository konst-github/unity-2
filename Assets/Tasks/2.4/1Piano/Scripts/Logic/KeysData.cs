using System.Collections.Generic;

namespace Piano 
{
    public class KeysData
    {
        private static Dictionary<string, float> dictKeyPitch;

        public static float PitchForTag(string tag)
        {
            // Just for not to bother with exception handling
            if (string.IsNullOrEmpty(tag) || !dictKeyPitch.ContainsKey(tag))
            {
                return 0f;
            }

            return dictKeyPitch[tag];
        }

        public static void FillKeys()
        {
            dictKeyPitch = new Dictionary<string, float>
            {
                { "C1", 1.0f },
                { "C#", 1.06f },
                { "D", 1.12f },
                { "D#", 1.19f },
                { "E", 1.26f },
                { "F", 1.34f },
                { "F#", 1.42f },
                { "G", 1.5f },
                { "G#", 1.59f },
                { "A", 1.69f },
                { "A#", 1.79f },
                { "B", 1.9f },
                { "C2", 2.0f },
            };
        }
    }
}