using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

namespace AllMenusUI
{
    public class PauseMenuScript : MonoBehaviour
    {
		public AudioSource myAudioMusic;
        // Use this for initialization
        void Start()
        {

        }

		void OnEnable()
		{
			myAudioMusic.volume = GameManager.inst.EffectsVol;
		}

		public void UpdateSoundSettings()
		{
			myAudioMusic.volume = GameManager.inst.EffectsVol;
		}

        public void OnMainMenuClick()
		{
			myAudioMusic.Play();
			//SceneLoaderScript.inst.LoadScene(0);
			Invoke("MainMenu", 0.2f);
		}

		public void OnSettingsClick()
		{
			myAudioMusic.Play();
			AllMenusScript.inst.OpenCloseSettings(true);
		}

		public void OnProceedClick()
		{
			myAudioMusic.Play();
			//gameObject.SetActive(false);
			Invoke("ProceedFunc", 0.2f);
		}

		void MainMenu()
		{
			SceneLoaderScript.inst.LoadScene(0);
		}
		void ProceedFunc()
		{
			gameObject.SetActive(false);
		}
    }
}