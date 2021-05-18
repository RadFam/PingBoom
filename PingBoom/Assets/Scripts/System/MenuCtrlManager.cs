using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SystemObjects
{
    public class MenuCtrlManager : MonoBehaviour
    {

        // Use this for initialization
        public AudioSource myAudioMusic;
		public AudioSource myAudioEffect;
		public AudioClip clickEffect;
        void Start()
        {
            myAudioEffect.clip = clickEffect;
            UpdateSoundSettings();
        }

        public void UpdateSoundSettings()
		{
			myAudioMusic.volume = GameManager.inst.MusicVol;
			myAudioEffect.volume = GameManager.inst.EffectsVol;
		}

		public void ButtonClickPlay()
		{
            myAudioEffect.Play();
		}

        public void StopAllPlay()
        {
            myAudioMusic.Stop();
            myAudioEffect.Stop();
        }
    }
}