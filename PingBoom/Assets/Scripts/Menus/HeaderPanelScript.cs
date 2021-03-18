using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AllMenusUI
{
    public class HeaderPanelScript : MonoBehaviour
    {

        [SerializeField]
		Text lastShoots;
		[SerializeField]
		Text currentScore;

		[SerializeField]
		Text changeShoots;

		int shootsVol;
		int scoreVol;
		
		// Use this for initialization
        void Start()
        {

        }

        public void OnGearClick()
		{
			AllMenusScript.inst.OpenClosePause(true);
		}

		public void SetInitScore(int shoots, int score=0)
		{
			lastShoots.text = shoots.ToString();
			currentScore.text = score.ToString();
		}

		public void SetNewShoots(int oldShoots, int newShoots)
		{
			int val = newShoots - oldShoots;
			lastShoots.text = newShoots.ToString();
			
			// Effect on
			changeShoots.gameObject.SetActive(true);
			//changeShoots.rectTransform
			changeShoots.text = val.ToString();
			changeShoots.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			StartCoroutine(ChangeShootsCoroutine());
		}

		public void SetNewScore(int oldScore, int newScore)
		{
			// Effect on
		}

		IEnumerator ChangeShootsCoroutine()
		{
			yield return null;
		}
    }
}