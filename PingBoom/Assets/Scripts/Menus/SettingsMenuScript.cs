using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SystemObjects;

namespace AllMenusUI
{
    public class SettingsMenuScript : MonoBehaviour
    {
		public Sprite[] chkBoxSprites = new Sprite[2];
		public Image chkBoxImage;

		[SerializeField]
		Slider musicVolSlider;
		[SerializeField]
		Slider effectsVolSlider;
		
		bool saveProgress;
		float musicVol;
		float effectsVol;
        // Use this for initialization
        void Awake()
        {
			saveProgress = false;
			musicVolSlider.value = 1.0f;
			effectsVolSlider.value = 1.0f;
			musicVol = 1.0f;
			effectsVol = 1.0f;
			musicVolSlider.onValueChanged.AddListener(AlterSoundVol);
			effectsVolSlider.onValueChanged.AddListener(AlterEffectsVol);
        }

		public void OnEnable()
		{
			musicVolSlider.maxValue = 1.0f;
			effectsVolSlider.maxValue = 1.0f;

			// Take initial data from SystemManager
			// .......
			musicVol = GameManager.inst.MusicVol;
			effectsVol = GameManager.inst.EffectsVol;
			saveProgress = GameManager.inst.SaveScoreProgress;

			musicVolSlider.value = musicVol;
			effectsVolSlider.value = effectsVol;
			if (saveProgress)
			{
				chkBoxImage.sprite = chkBoxSprites[1];
			}
			else
			{
				chkBoxImage.sprite = chkBoxSprites[0];
			}
		}

        public void OnExitClick()
		{
			gameObject.SetActive(false);
		}

		void AlterSoundVol(float value)
		{
			musicVol = value;
			// Send data to GameManager
			//.....
			GameManager.inst.MusicVol = musicVol;
		}

		void AlterEffectsVol(float value)
		{
			effectsVol = value;
			// Send data to GameManager
			//.....
			GameManager.inst.EffectsVol = effectsVol;
		}

		public void OnChkBoxClick()
		{
			saveProgress = !saveProgress;

			if (saveProgress)
			{
				chkBoxImage.sprite = chkBoxSprites[1];
			}
			else
			{
				chkBoxImage.sprite = chkBoxSprites[0];
			}

			// Send data to SystemManager
			//.....
			GameManager.inst.SaveScoreProgress = saveProgress;
		}
    }
}