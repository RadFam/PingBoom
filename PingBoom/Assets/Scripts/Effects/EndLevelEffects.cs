using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SystemObjects;

namespace Effects
{
    public class EndLevelEffects : MonoBehaviour
    {

        // Use this for initialization
		[SerializeField]
		Text winText;

		[SerializeField]
		Text failText;

		[SerializeField]
		ParticleSystem winStarFall;
		LevelManager levelManager;

		public void PlayWinFinal(LevelManager LM)
		{
			levelManager = LM;
			Invoke("StepNewLevel", 4.0f);
			winText.gameObject.SetActive(true);
			winStarFall.gameObject.SetActive(true);
			winStarFall.Play();
		}
        
		public void PlayFailFinal(LevelManager LM)
		{
			levelManager = LM;
			Invoke("StepNewLevel", 4.0f);
			failText.gameObject.SetActive(true);
		}

		public void CloseAllEffects()
		{
			winText.gameObject.SetActive(false);
			failText.gameObject.SetActive(false);
			winStarFall.Stop();
			winStarFall.gameObject.SetActive(false);
		}

		void StepNewLevel()
		{
			levelManager.StepNewLevel();
		}
    }
}