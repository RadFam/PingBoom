using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SystemObjects;
using AllMenusUI;

namespace Effects
{
    public class EndLevelEffects : MonoBehaviour
    {

        // Use this for initialization
		[SerializeField]
		Camera mCam;
		[SerializeField]
		Text winText;

		[SerializeField]
		Text failText;

		[SerializeField]
		ParticleSystem winStarFall;
		[SerializeField]

		LevelManager levelManager;
		[SerializeField]
		GameObject endLevelInfo;
		[SerializeField]
		EndLevelPreInfo endLevelPreInfo;

		public void PlayWinFinal(LevelManager LM)
		{
			levelManager = LM;
			Invoke("StepNewLevelOne", 4.0f);
			winText.gameObject.SetActive(true);
			winStarFall.gameObject.SetActive(true);
			if (mCam != null)
			{
				Vector3 p = mCam.ViewportToWorldPoint(new Vector3(0.5f, 1.0f, mCam.nearClipPlane));
				winStarFall.transform.position = new Vector3(p.x, p.y, 0);
			}
			winStarFall.Play();
		}
        
		public void PlayFailFinal(LevelManager LM)
		{
			levelManager = LM;
			Invoke("StepNewLevelOne", 4.0f);
			failText.gameObject.SetActive(true);
		}

		public void CloseAllEffects()
		{
			winText.gameObject.SetActive(false);
			failText.gameObject.SetActive(false);
			winStarFall.Stop();
			winStarFall.gameObject.SetActive(false);
			endLevelInfo.SetActive(false);
		}

		public void StepNewLevelOne()
		{
			endLevelPreInfo.gameObject.SetActive(true);
			endLevelPreInfo.SetResults();
		}
		public void StepNewLevelTwo()
		{
			if (levelManager.IsVictory)
			{
				endLevelInfo.SetActive(true);
			}
		}
    }
}