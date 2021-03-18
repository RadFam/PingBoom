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

		public void PlayWinFinal(LevelManager LM)
		{
			Invoke("LM.StepNewLevel", 4.0f);
			winText.gameObject.SetActive(true);
			winStarFall.gameObject.SetActive(true);
			winStarFall.Play();
		}
        
		public void PlayFailFinal(LevelManager LM)
		{
			Invoke("LM.StepNewLevel", 4.0f);
			failText.gameObject.SetActive(true);
		}

		public void CloseAllEffects()
		{
			winText.gameObject.SetActive(false);
			failText.gameObject.SetActive(false);
			winStarFall.Stop();
			winStarFall.gameObject.SetActive(false);
		}
    }
}