using System.Collections.Generic;
using UnityEngine;

namespace Piano
{
    public class PianoController : MonoBehaviour, IPiano
    {
        [SerializeField] private NoteTextView noteTextView;

        [SerializeField]
        private AudioPlayer audioPlayer;

        [SerializeField]
        private List<PianoKey> pianoKeys = new List<PianoKey>();

        private const string suffix1 = "1";
        private const string suffix2 = "2";

        private void Awake()
        {
            KeysData.FillKeys();
        }

        private void Start()
        {
            foreach (PianoKey pianoKey in pianoKeys)
            {
                pianoKey.InjectPiano(this);
            }
        }

        public void OnKeyClick(string tag)
        {
            float pitch = KeysData.PitchForTag(tag);
            audioPlayer.PlaySoundWithPitch(pitch);

            string note = tag.Replace(suffix1, string.Empty).Replace(suffix2, string.Empty);
            noteTextView.ShowNoteName(note);
        }

        public void OnKeyRelease()
        {
            audioPlayer.StopSound();
            noteTextView.HideNoteName();
        }
    }
}